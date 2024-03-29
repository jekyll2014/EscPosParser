﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using EscPosParser.Properties;

namespace EscPosParser
{
    public class ParseEscPos
    {
        //INTERFACES
        //source of the data to parce
        private static readonly List<byte> sourceData = new List<byte>(); //in Init()

        //source of the command description (DataTable)
        private static DataTable _commandDataBase = new DataTable(); //in Init()

        //INTERNAL VARIABLES
        //Command list preselected
        private static readonly Dictionary<int, string> _commandList = new Dictionary<int, string>(); //in Init()

        //RESULT VALUES

        //Printer model
        public static string printerModel = ""; //in findCommand()

        //Command text
        public static List<string> commandName = new List<string>(); //in findCommand()

        //Command desc
        public static List<string> commandDesc = new List<string>(); //in findCommand()

        //place of the command start in the text
        public static List<int> commandPosition = new List<int>(); //in findCommand()

        //string number of the command found
        public static List<int> commandDbLineNum = new List<int>(); //in findCommand()

        //height of the command
        public static List<int> commandDbHeight = new List<int>(); //in findCommand()

        //printer model for the command
        public static List<string> commandPrinterModel = new List<string>(); //in findCommand()

        //in FindParameter()

        //string number of the parameter found
        public static List<int> paramDbLineNum = new List<int>();

        //place of the parameter value start in the text
        public static List<int> paramPosition = new List<int>();

        //list of parameters names
        public static List<string> paramName = new List<string>();

        //parameter description
        public static List<string> paramDesc = new List<string>();

        //parameter type
        public static List<string> paramType = new List<string>();

        //parameter RAW value
        public static List<List<byte>> paramRAWValue = new List<List<byte>>();

        //parameter value
        public static List<string> paramValue = new List<string>();

        public static List<List<string>> bitName = new List<List<string>>();
        public static List<List<string>> bitValue = new List<List<string>>();
        public static List<List<string>> bitDescription = new List<List<string>>();

        //Length of command+parameters text
        public static int commandBlockLength;

        //If there are any errors in the parameters parsing
        public static bool errors;

        public class CSVColumns
        {
            public static int CommandName { get; set; } = 0;
            public static int ParameterName { get; set; } = 1;
            public static int ParameterType { get; set; } = 2;
            public static int ParameterValue { get; set; } = 3;
            public static int Description { get; set; } = 4;
            public static int PrinterModel { get; set; } = 5;
        }

        public class DataTypes
        {
            public static string Byte { get; set; } = "byte";
            public static string Bitfield { get; set; } = "bitfield";
            public static string Word { get; set; } = "word";
            public static string Rword { get; set; } = "rword";
            public static string Textstring { get; set; } = "textstring";
            public static string Binarystring { get; set; } = "binarystring";
            public static string Decstring { get; set; } = "decstring";
            public static string Hexstring { get; set; } = "hexstring";
            public static string Textarray { get; set; } = "textarray";
            public static string Binaryarray { get; set; } = "binaryarray";
            public static string Decarray { get; set; } = "decarray";
            public static string Hexarray { get; set; } = "hexarray";
        }

        internal static void
            Init(byte[] _data, DataTable _dataBase) //Setup source table of commands and source text field
        {
            sourceData.Clear();
            sourceData.AddRange(_data);
            _commandDataBase = _dataBase;

            ClearCommand();

            _commandList.Clear();
            var _tempCommandList = new Dictionary<int, string>();

            for (var i = 0; i < _commandDataBase.Rows.Count; i++) //collect command strings for sorting/selecting
                if (_commandDataBase.Rows[i][CSVColumns.CommandName].ToString() != "")
                    _tempCommandList.Add(i, _commandDataBase.Rows[i][CSVColumns.CommandName].ToString());
            var _sortedDict =
                _tempCommandList.OrderByDescending(j => j.Value.Length)
                    .ThenBy(j => j.Key); //sort command list: longer 1st
            for (var i = 0; i < _sortedDict.Count(); i++) //put sorted at the public list
                _commandList.Add(_sortedDict.ElementAt(i).Key, _sortedDict.ElementAt(i).Value);
        }

        public static bool FindCommand(int _pos, string _printerModel)
        {
            //reset all result values
            ClearCommand();

            //find command
            var _tempCommandList = new Dictionary<int, string>();
            for (var i = 0; i < _commandList.Count; i++)
                if (_pos + _commandList.ElementAt(i).Value.Length / 3 <= sourceData.Count
                ) //check if it doesn't go over the last symbol
                    if (Accessory.ConvertByteArrayToHex(sourceData
                            .GetRange(_pos, _commandList.ElementAt(i).Value.Length / 3).ToArray()) ==
                        _commandList.ElementAt(i).Value) //find command
                    {
                        if (_printerModel == "")
                        {
                            _tempCommandList.Add(_commandList.ElementAt(i).Key,
                                _commandList.ElementAt(i).Value); // check if printer model is correct
                        }
                        else
                        {
                            var tmpstr = _commandDataBase.Rows[_commandList.ElementAt(i).Key][CSVColumns.PrinterModel]
                                .ToString().Split(',');
                            for (var i1 = 0; i1 < tmpstr.Count(); i1++)
                                if (tmpstr[i1].Trim() == _printerModel)
                                    _tempCommandList.Add(_commandList.ElementAt(i).Key,
                                        _commandList.ElementAt(i).Value);
                        }
                    }

            //else return false;

            if (_tempCommandList.Count <= 0) return false;
            for (var i = 0; i < _tempCommandList.Count; i++)
            {
                commandName.Add(_tempCommandList.ElementAt(i).Value);
                commandDbLineNum.Add(_tempCommandList.ElementAt(i).Key);
                commandDesc.Add(_commandDataBase.Rows[commandDbLineNum[i]][CSVColumns.Description].ToString());
                commandPosition.Add(_pos);
                commandPrinterModel.Add(_commandDataBase.Rows[commandDbLineNum[i]][CSVColumns.PrinterModel].ToString());
                //check command height - how many rows are occupated
                var i1 = 0;
                while (commandDbLineNum[i] + i1 + 1 < _commandDataBase.Rows.Count &&
                       _commandDataBase.Rows[commandDbLineNum[i] + i1 + 1][CSVColumns.CommandName].ToString() ==
                       "") i1++;
                commandDbHeight.Add(i1);
            }

            return true;
        }

        public static bool FindParameter(int _commandNum)
        {
            ClearParameters();
            if (_commandNum < 0 || _commandNum >= commandName.Count) return false;
            //collect parameters
            var _stopSearch = commandDbLineNum[_commandNum] + 1;
            while (_stopSearch < _commandDataBase.Rows.Count &&
                   _commandDataBase.Rows[_stopSearch][CSVColumns.CommandName].ToString() == "") _stopSearch++;
            for (var i = commandDbLineNum[_commandNum] + 1; i < _stopSearch; i++)
                if (_commandDataBase.Rows[i][CSVColumns.ParameterName].ToString() != "")
                {
                    paramDbLineNum.Add(i);
                    paramName.Add(_commandDataBase.Rows[i][CSVColumns.ParameterName].ToString());
                    paramDesc.Add(_commandDataBase.Rows[i][CSVColumns.Description].ToString());
                    paramType.Add(_commandDataBase.Rows[i][CSVColumns.ParameterType].ToString());
                }

            errors = false; //Error in parameter found
            //process each parameter
            for (var parameter = 0; parameter < paramDbLineNum.Count; parameter++)
            {
                paramPosition.Add(commandPosition[_commandNum] + ResultLength(_commandNum));
                bitName.Add(new List<string>());
                bitValue.Add(new List<string>());
                bitDescription.Add(new List<string>());

                //collect predefined RAW values
                var predefinedParamsRaw = new List<string>();
                var j = paramDbLineNum[parameter] + 1;
                while (j < _commandDataBase.Rows.Count &&
                       _commandDataBase.Rows[j][CSVColumns.ParameterValue].ToString() != "")
                {
                    predefinedParamsRaw.Add(_commandDataBase.Rows[j][CSVColumns.ParameterValue].ToString());
                    j++;
                }

                //Calculate predefined params
                var predefinedParamsVal = new List<int>();
                foreach (var expresion in predefinedParamsRaw)
                {
                    var val = 0;
                    //select formula basing on parameter value "?k=1:n+n1 ?k-2:n*n1"
                    if (expresion.StartsWith("?"))
                    {
                        var tmpstr = expresion.Trim().Replace("\r", "").Replace("\n", "").Split('?');
                        foreach (var str in tmpstr)
                            if (str != "")
                            {
                                var equation =
                                    str.Substring(0,
                                        str.IndexOf(':')); //calculate equation if needed                            
                                for (var i2 = 0;
                                    i2 < paramName.Count - 1;
                                    i2++) //insert all parameters before current into equation
                                {
                                    equation = equation.Replace(paramName[i2], paramValue[i2]);
                                    equation = equation.Replace('=', '-');
                                }

                                if (Accessory.Evaluate(equation) == 0)
                                {
                                    var equation2 = str.Substring(str.IndexOf(':') + 1).Trim();
                                    for (var i3 = 0; i3 < paramName.Count - 1; i3++)
                                        equation2 = equation2.Replace(paramName[i3], paramValue[i3]);
                                    try
                                    {
                                        val = (int)Accessory.Evaluate(equation2);
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                    }
                    else if (expresion.StartsWith("@"))
                    {
                        var equation = expresion.Substring(1);
                        //insert all parameters before current into equation
                        for (var i2 = 0; i2 < paramName.Count - 1; i2++)
                            equation = equation.Replace(paramName[i2], paramValue[i2]);
                        val = (int)Accessory.Evaluate(equation); // = str.Substring(str.IndexOf(':') + 1);
                    }
                    else
                    {
                        if (!int.TryParse(expresion.Trim(), out val)) val = 0;
                    }

                    predefinedParamsVal.Add(val);
                }

                //get parameter from text
                var tmpStrLength = 0;
                var predefinedFound =
                    false; //Matching predefined parameter found and it's number is in "predefinedParameterMatched"
                var errMessage = "";
                byte l = 0, h = 0;
                var predefinedParameterMatched = 0;
                var _prmType = _commandDataBase.Rows[paramDbLineNum[parameter]][CSVColumns.ParameterType].ToString()
                    .ToLower();
                if (_prmType == DataTypes.Byte)
                {
                    if (paramPosition[parameter] + 1 <= sourceData.Count)
                    {
                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter], 1));
                        l = paramRAWValue[parameter].ToArray()[0];
                    }
                    else
                    {
                        errors = true;
                        errMessage = "!!!ERR: Out of data!!!";
                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter],
                            sourceData.Count - paramPosition[parameter]));
                    }

                    paramValue.Add(l.ToString());
                }
                else if (_prmType == DataTypes.Bitfield)
                {
                    if (paramPosition[parameter] + 1 <= sourceData.Count)
                    {
                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter], 1));
                        l = paramRAWValue[parameter].ToArray()[0];
                        for (byte i2 = 0; i2 < 8; i2++)
                        {
                            bitName[parameter].Add("bit" + i2);
                            bitValue[parameter].Add(Accessory.GetBit(l, i2).ToString());
                            bitDescription[parameter]
                                .Add(_commandDataBase.Rows[paramDbLineNum[parameter] + i2 + 1][CSVColumns.Description]
                                    .ToString());
                        }
                    }
                    else
                    {
                        errors = true;
                        errMessage = "!!!ERR: Out of data!!!";
                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter],
                            sourceData.Count - paramPosition[parameter]));
                    }

                    paramValue.Add(l.ToString());
                }
                else if (_prmType == DataTypes.Word)
                {
                    if (paramPosition[parameter] + 2 <= sourceData.Count)
                    {
                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter], 2));
                        l = paramRAWValue[parameter].GetRange(0, 1)[0];
                        h = paramRAWValue[parameter].GetRange(1, 1)[0];
                    }
                    else
                    {
                        errors = true;
                        errMessage = "!!!ERR: Out of data!!!";
                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter],
                            sourceData.Count - paramPosition[parameter]));
                    }

                    paramValue.Add((h * 256 + l).ToString());
                }
                else if (_prmType == DataTypes.Rword)
                {
                    if (paramPosition[parameter] + 2 <= sourceData.Count)
                    {
                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter], 2));
                        h = paramRAWValue[parameter].GetRange(0, 1)[0];
                        l = paramRAWValue[parameter].GetRange(1, 1)[0];
                    }
                    else
                    {
                        errors = true;
                        errMessage = "!!!ERR: Out of data!!!";
                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter],
                            sourceData.Count - paramPosition[parameter]));
                    }

                    paramValue.Add((h * 256 + l).ToString());
                }
                else if (_prmType == DataTypes.Textstring || _prmType == DataTypes.Decstring ||
                         _prmType == DataTypes.Hexstring || _prmType == DataTypes.Binarystring)
                {
                    if (predefinedParamsVal.Count > 0)
                    {
                        //look for the end of the string (predefined byte)
                        while (predefinedFound == false && paramPosition[parameter] + tmpStrLength <= sourceData.Count)
                        {
                            //check for each predefined value
                            for (var i1 = 0; i1 < predefinedParamsVal.Count; i1++)
                                if (paramPosition[parameter] + tmpStrLength + 1 <= sourceData.Count)
                                {
                                    if (sourceData[paramPosition[parameter] + tmpStrLength] == predefinedParamsVal[i1])
                                    {
                                        predefinedFound = true;
                                        predefinedParameterMatched = i1;
                                        paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter], tmpStrLength));
                                    }
                                }
                                else
                                {
                                    errors = true;
                                    errMessage = "!!!ERR: Out of data!!!";
                                }

                            tmpStrLength++;
                        }

                        if (tmpStrLength < 1)
                        {
                            errors = true;
                            errMessage = "!!!ERR: Out of data!!!";
                        }

                        if (errors)
                            paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter],
                                sourceData.Count - paramPosition[parameter]));
                        if (paramRAWValue[parameter].Count != 0)
                        {
                            if ((_prmType == DataTypes.Textstring || _prmType == DataTypes.Decstring ||
                                 _prmType == DataTypes.Hexstring) &&
                                Accessory.PrintableByteArray(paramRAWValue[parameter].ToArray()))
                                paramValue.Add(Encoding.GetEncoding(Settings.Default.CodePage)
                                    .GetString(paramRAWValue[parameter].ToArray()));
                            else
                                paramValue.Add("[" +
                                               Accessory.ConvertByteArrayToHex(paramRAWValue[parameter].ToArray()) +
                                               "]");
                        }
                        else
                        {
                            paramValue.Add("");
                        }
                    }
                    else
                    {
                        errors = true;
                        errMessage = "!!!ERR: There must be predefined values!!!";
                        paramRAWValue.Add(null);
                        paramValue.Add("");
                    }
                }
                else if (_prmType == DataTypes.Textarray || _prmType == DataTypes.Decarray ||
                         _prmType == DataTypes.Hexarray || _prmType == DataTypes.Binaryarray)
                {
                    if (predefinedParamsVal.Count == 1)
                    {
                        predefinedFound = true;
                        predefinedParameterMatched = 0;
                        if (paramPosition[parameter] + predefinedParamsVal[0] <= sourceData.Count)
                        {
                            paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter],
                                (byte)predefinedParamsVal[0]));
                        }
                        else
                        {
                            errors = true;
                            errMessage = "!!!ERR: Out of data bound!!!";
                            paramRAWValue.Add(sourceData.GetRange(paramPosition[parameter],
                                sourceData.Count - paramPosition[parameter]));
                        }

                        if (paramRAWValue[parameter].Count != 0)
                        {
                            if ((_prmType == DataTypes.Textarray || _prmType == DataTypes.Decarray ||
                                 _prmType == DataTypes.Hexarray) &&
                                Accessory.PrintableByteArray(paramRAWValue[parameter].ToArray()))
                                paramValue.Add(Encoding.GetEncoding(Settings.Default.CodePage)
                                    .GetString(paramRAWValue[parameter].ToArray()));
                            else
                                paramValue.Add("[" +
                                               Accessory.ConvertByteArrayToHex(paramRAWValue[parameter].ToArray()) +
                                               "]");
                        }
                        else
                        {
                            paramValue.Add("");
                        }
                    }
                    else
                    {
                        errors = true;
                        errMessage = "!!!ERR: There must be only one predefined value!!!";
                        paramRAWValue.Add(null);
                        paramValue.Add("");
                    }
                }
                else
                {
                    predefinedFound = true;
                    errors = true;
                    errMessage = "!!!ERR: Incorrect parameter type!!!";
                    paramRAWValue.Add(new List<byte>());
                    paramValue.Add("");
                }

                if (errors) paramDesc[parameter] += errMessage + "\r\n";

                //compare parameter value with predefined values to get proper description
                if (predefinedFound == false && !errors)
                {
                    for (var i1 = 0; i1 < predefinedParamsVal.Count; i1++)
                        if (paramValue[parameter] == predefinedParamsVal[i1].ToString())
                        {
                            predefinedFound = true;
                            predefinedParameterMatched = i1;
                            i1 = predefinedParamsVal.Count;
                        }

                    if (predefinedParamsVal.Count > 0 && predefinedFound == false)
                        errors = true; //if no parameters match predefined
                }

                //get description for predefined parameter
                //if description is within current command parameters group
                if (paramDbLineNum[parameter] + predefinedParameterMatched + 1 <=
                    commandDbLineNum[_commandNum] + commandDbHeight[_commandNum] && predefinedFound)
                    paramDesc[parameter] +=
                        _commandDataBase.Rows[paramDbLineNum[parameter] + predefinedParameterMatched + 1][
                            CSVColumns.Description].ToString();
            }

            ResultLength(_commandNum);
            return !errors;
        }

        internal static void ClearCommand()
        {
            commandPrinterModel.Clear();
            commandPosition.Clear();
            commandDbLineNum.Clear();
            commandDbHeight.Clear();
            commandName.Clear();
            commandDesc.Clear();

            ClearParameters();
        }

        internal static void ClearParameters()
        {
            paramDbLineNum.Clear();
            paramPosition.Clear();
            paramName.Clear();
            paramDesc.Clear();
            paramType.Clear();
            paramRAWValue.Clear();
            paramValue.Clear();
            bitName.Clear();
            bitValue.Clear();
            bitDescription.Clear();
            commandBlockLength = 0;
            errors = false;
        }

        //Calc "CommandBlockLength" - length of command text in source text field
        internal static int ResultLength(int _commandNum)
        {
            commandBlockLength = commandName[_commandNum].Length / 3;
            foreach (var p in paramRAWValue) commandBlockLength += p.Count;
            return commandBlockLength;
        }
    }
}