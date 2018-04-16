using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int maxCommandLength = 0;
        DataTable CommandDatabase = new DataTable();

        string SourceFile = "default.txt";
        string DbFile = EscPosParser.Properties.Settings.Default.database;
        List<byte> sourceData = new List<byte>();

        DataTable ResultDatabase = new DataTable();

        public class ResultColumns
        {
            public static int Name { get; set; } = 0;
            public static int Value { get; set; } = 1;
            public static int Type { get; set; } = 2;
            public static int Desc { get; set; } = 3;
            public static int Raw { get; set; } = 4;
        }

        public Form1()
        {
            InitializeComponent();
            textBox_code.Select(0, 0);
            defaultCSVToolStripTextBox.Text = EscPosParser.Properties.Settings.Default.database;
            ReadCsv(defaultCSVToolStripTextBox.Text);
            dataGridView_result.DataSource = ResultDatabase;
            dataGridView_commands.ReadOnly = true;
            ResultDatabase.Columns.Add("Name");
            ResultDatabase.Columns.Add("Value");
            ResultDatabase.Columns.Add("Type");
            ResultDatabase.Columns.Add("Desc");
            ResultDatabase.Columns.Add("Raw");
            ParseEscPos.Init(Accessory.ConvertHexToByteArray(textBox_code.Text), CommandDatabase);
            comboBox_printerType.SelectedIndex = 0;
            for (int i = 0; i < dataGridView_commands.Columns.Count; i++) dataGridView_commands.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < dataGridView_result.Columns.Count; i++) dataGridView_result.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public void ReadCsv(string fileName)
        {
            CommandDatabase.Clear();
            CommandDatabase.Columns.Clear();
            FileStream inputFile;
            try
            {
                inputFile = File.OpenRead(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening file:" + fileName + " : " + ex.Message);
                return;
            }

            StringBuilder inputStr = new StringBuilder();
            int c = inputFile.ReadByte();
            while (c != '\r' && c != '\n' && c != -1)
            {
                byte[] b = new byte[1];
                b[0] = (byte)c;
                inputStr.Append(Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage).GetString(b));
                c = inputFile.ReadByte();
            }

            int colNum = 0;
            if (inputStr.Length != 0) //count columns and read headers
            {
                string[] cells = inputStr.ToString().Split(EscPosParser.Properties.Settings.Default.CSVdelimiter);
                colNum = cells.Length - 1;
                for (int i = 0; i < colNum; i++)
                {
                    CommandDatabase.Columns.Add(cells[i]);
                }
                //DataRow row = CommandDatabase.NewRow();
            }

            if (CommandDatabase.Columns.Count < 6)
            {
                MessageBox.Show("Not enough columns.");
                return;
            }
            while (c != -1)
            {
                int i = 0;
                c = 0;
                inputStr.Clear();
                while (i < colNum && c != -1)
                {
                    c = inputFile.ReadByte();
                    byte[] b = new byte[1];
                    b[0] = (byte)c;
                    if (c == EscPosParser.Properties.Settings.Default.CSVdelimiter) i++;
                    if (c != -1) inputStr.Append(Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage).GetString(b));
                }
                if (inputStr.ToString().Replace(EscPosParser.Properties.Settings.Default.CSVdelimiter, ' ').Trim().TrimStart('\r').TrimStart('\n').TrimEnd('\n').TrimEnd('\r') != "")
                {
                    string[] cells = inputStr.ToString().Split(EscPosParser.Properties.Settings.Default.CSVdelimiter);

                    DataRow row = CommandDatabase.NewRow();
                    row[0] = Accessory.CheckHexString(cells[0]);
                    for (i = 1; i < cells.Length - 1; i++)
                    {
                        row[i] = cells[i].TrimStart('\r').TrimStart('\n').TrimEnd('\n').TrimEnd('\r');
                    }
                    CommandDatabase.Rows.Add(row);
                }
            }
            inputFile.Close();
            for (int i = 0; i < CommandDatabase.Rows.Count; i++)
            {
                if (CommandDatabase.Rows[i][0].ToString().Length > maxCommandLength) maxCommandLength = CommandDatabase.Rows[i][0].ToString().Length;
            }
            dataGridView_commands.DataSource = CommandDatabase;
            PopulatePrinterModels();
        }

        public void PopulatePrinterModels()
        {
            comboBox_printerType.Items.Clear();
            comboBox_printerType.Items.Add("");
            for (int i = 0; i < CommandDatabase.Rows.Count; i++)
            {
                string[] tmpstr = CommandDatabase.Rows[i][(int)ParseEscPos.CSVColumns.PrinterModel].ToString().Split(',');
                for (int i1 = 0; i1 < tmpstr.Count(); i1++) if (comboBox_printerType.FindStringExact(tmpstr[i1]) < 0) comboBox_printerType.Items.Add(tmpstr[i1].Trim());
            }
            comboBox_printerType.SelectedIndex = 0;
        }

        private void Button_find_Click(object sender, EventArgs e)
        {
            if (sender != listBox_commands) listBox_commands.Items.Clear();
            textBox_commandDesc.Clear();
            ResultDatabase.Clear();

            //check if cursor position in not last
            if (textBox_code.SelectionStart < textBox_code.Text.Length)
            {
                if (textBox_code.Text.Substring(textBox_code.SelectionStart, 1) == " ")
                {
                    textBox_code.SelectionStart++;
                }
            }
            //check if cursor position in not first
            if (textBox_code.SelectionStart != 0)
            {
                if (textBox_code.Text.Substring(textBox_code.SelectionStart - 1, 1) != " " && textBox_code.Text.Substring(textBox_code.SelectionStart, 1) != " ")
                {
                    textBox_code.SelectionStart--;
                }
            }
            label_currentPosition.Text = textBox_code.SelectionStart.ToString() + "/" + textBox_code.TextLength.ToString();
            if (ParseEscPos.FindCommand(textBox_code.SelectionStart / 3, comboBox_printerType.SelectedItem.ToString()))
            {
                //int currentCommand = 0;  //temp const to select 1st command found
                int currentCommand = 0;
                if (sender == listBox_commands) currentCommand = listBox_commands.SelectedIndex;
                ParseEscPos.FindParameter(currentCommand);
                if (sender != button_auto)  //only update interface if it's no auto-parsing mode
                {
                    dataGridView_commands.CurrentCell = dataGridView_commands.Rows[ParseEscPos.commandDbLineNum[currentCommand]].Cells[(int)ParseEscPos.CSVColumns.CommandName];
                    if (sender != listBox_commands)
                    {
                        for (int i = 0; i < ParseEscPos.commandName.Count; i++)
                        {
                            listBox_commands.Items.Add(ParseEscPos.commandName[i] + "[" + ParseEscPos.commandPrinterModel[i] + "]");
                        }
                    }
                    listBox_commands.SelectedItem = currentCommand;
                    textBox_commandDesc.Text = ParseEscPos.commandDesc[currentCommand];
                    for (int i = 0; i < ParseEscPos.paramName.Count; i++)
                    {
                        DataRow row = ResultDatabase.NewRow();
                        row[ResultColumns.Name] = ParseEscPos.paramName[i];
                        row[ResultColumns.Value] = ParseEscPos.paramValue[i];
                        row[ResultColumns.Type] = ParseEscPos.paramType[i];
                        row[ResultColumns.Raw] = Accessory.ConvertByteArrayToHex(ParseEscPos.paramRAWValue[i].ToArray());
                        //if (ParseEscPos.paramType[i].ToLower() != ParseEscPos.DataTypes.Bitfield) row[ResultColumns.Desc] = ParseEscPos.paramDesc[i];
                        row[ResultColumns.Desc] = ParseEscPos.paramDesc[i];
                        ResultDatabase.Rows.Add(row);

                        if (ParseEscPos.paramType[i].ToLower() == ParseEscPos.DataTypes.Bitfield)  //add bitfield display
                        {
                            for (int i1 = 0; i1 < 8; i1++)
                            {
                                row = ResultDatabase.NewRow();
                                //row[ResultColumns.Name] = ParseEscPos.paramName[i] + "[" + i1.ToString() + "]";
                                row[ResultColumns.Value] = ParseEscPos.bitValue[i1];
                                row[ResultColumns.Type] = ParseEscPos.bitName[i1];
                                row[ResultColumns.Desc] = ParseEscPos.bitDescription[i1];
                                ResultDatabase.Rows.Add(row);
                            }
                        }
                    }
                }
                //textBox_code.Select(textBox_code.SelectionStart, ParseEscPos.commandBlockLength * 3);
                textBox_code.Select(ParseEscPos.commandPosition[currentCommand] * 3, ParseEscPos.commandBlockLength * 3);
            }
            //look for the end of unrecognizable data (consider it text string)
            else
            {
                int i = 3;
                while (!ParseEscPos.FindCommand((textBox_code.SelectionStart + i) / 3, comboBox_printerType.SelectedItem.ToString()) && textBox_code.SelectionStart + i < textBox_code.TextLength) //looking for a non-parseable part end
                {
                    i += 3;
                }
                if (textBox_code.SelectionStart + i >= textBox_code.TextLength) i = textBox_code.TextLength - textBox_code.SelectionStart;
                textBox_code.Select(textBox_code.SelectionStart, i);
                ParseEscPos.commandName.Clear();
                if (textBox_code.SelectedText.Length > 0)
                {

                    if (sender != button_auto)
                    {
                        listBox_commands.Items.Add("\"" + (String)textBox_code.SelectedText + "\"");
                        dataGridView_commands.CurrentCell = dataGridView_commands.Rows[0].Cells[0];
                        if (Accessory.PrintableHex(textBox_code.SelectedText)) textBox_commandDesc.Text = "\"" + Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage).GetString(Accessory.ConvertHexToByteArray(textBox_code.SelectedText)) + "\"";
                    }
                }
            }
            textBox_code.ScrollToCaret();
        }

        private void Button_next_Click(object sender, EventArgs e)
        {
            textBox_code.SelectionStart = textBox_code.SelectionStart + textBox_code.SelectionLength;
            Button_find_Click(button_next, EventArgs.Empty);
        }

        private void Button_auto_Click(object sender, EventArgs e)
        {
            File.WriteAllText(SourceFile + ".escpos", "");
            File.WriteAllText(SourceFile + ".list", "");
            textBox_code.Select(0, 0);
            StringBuilder asciiString = new StringBuilder();
            int asciiPosition = -1;
            while (textBox_code.SelectionStart + textBox_code.SelectionLength < textBox_code.TextLength)
            {
                StringBuilder saveStr = new StringBuilder();
                //run "Find" button event as "Auto"
                textBox_code.SelectionStart = textBox_code.SelectionStart + textBox_code.SelectionLength;
                Button_find_Click(button_auto, EventArgs.Empty);
                if (ParseEscPos.commandName.Count > 0)  //?????????????
                {
                    ParseEscPos.FindParameter(0);  //?????????????
                    //Save ASCII string if collected till now
                    if (asciiString.Length != 0)
                    {
                        saveStr.Append("[" + asciiPosition.ToString() + "]" + "RAW data: \"" + asciiString.ToString() + "\"\r\n");  //??????????
                        if (Accessory.PrintableHex(asciiString.ToString())) saveStr.Append("ASCII string: \"" + Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage).GetString(Accessory.ConvertHexToByteArray(asciiString.ToString())) + "\"\r\n");
                        saveStr.Append("\r\n");
                        File.AppendAllText(SourceFile + ".list", asciiString.ToString() + "\r\n", Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage));
                        asciiString.Clear();
                        asciiPosition = -1;
                    }

                    //collect command into file
                    /* RAW: "12 34"
                    *  Command: "12 34" - "Description"
                    *  Parameter = "1234"[Word] - "Description"
                    *  Parameter: ...
                    */
                    saveStr.Append("[" + ParseEscPos.commandPosition[0].ToString() + "]" + "RAW data: \"" + textBox_code.SelectedText + "\"\r\n");
                    saveStr.Append("Command: \"" + ParseEscPos.commandName[0] + "\" - \"" + ParseEscPos.commandDesc[0] + "\"\r\n");
                    saveStr.Append("Printer model: \"" + ParseEscPos.commandPrinterModel[0] + "\"\r\n");
                    for (int i = 0; i < ParseEscPos.paramName.Count; i++)
                    {
                        saveStr.Append("\tParameter: \"" + ParseEscPos.paramName[i] + "\" = ");
                        saveStr.Append(ParseEscPos.paramValue[i]);
                        saveStr.Append("<" + ParseEscPos.paramType[i] + "> - \"" + ParseEscPos.paramDesc[i].TrimStart('\r').TrimStart('\n').TrimEnd('\n').TrimEnd('\r') + "\"\r\n");
                        if (ParseEscPos.paramType[i].ToLower() == ParseEscPos.DataTypes.Bitfield)
                        {
                            for (int i1 = 0; i1 < 8; i1++)
                            {
                                saveStr.Append("\t\t[" + ParseEscPos.bitName[i1] + "]\" = \"");
                                saveStr.Append(ParseEscPos.bitValue + "\" - \"");
                                saveStr.Append(ParseEscPos.bitDescription);
                                saveStr.Append("\"\r\n");
                            }
                        }
                    }
                    saveStr.Append("\r\n");
                    File.AppendAllText(SourceFile + ".list", textBox_code.SelectedText + "\r\n", Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage));
                    File.AppendAllText(SourceFile + ".escpos", saveStr.ToString(), Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage));
                }
                else  //consider this as a string and collect
                {
                    if (asciiPosition == -1) asciiPosition = textBox_code.SelectionStart;
                    asciiString.Append(textBox_code.SelectedText);
                }
            }
            if (asciiString.Length != 0)
            {
                StringBuilder saveStr = new StringBuilder();
                saveStr.Append("[" + asciiPosition.ToString() + "]" + "RAW data: \"" + asciiString.ToString() + "\"\r\n");
                if (Accessory.PrintableHex(asciiString.ToString())) saveStr.Append("ASCII string: \"" + Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage).GetString(Accessory.ConvertHexToByteArray(asciiString.ToString())) + "\"\r\n");
                saveStr.Append("\r\n");
                File.AppendAllText(SourceFile + ".list", asciiString.ToString() + "\r\n", Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage));
                File.AppendAllText(SourceFile + ".escpos", saveStr.ToString(), Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage));
                asciiString.Clear();
            }
        }

        private void ListBox_commands_SelectedIndexChanged(object sender, EventArgs e)
        {
            Button_find_Click(listBox_commands, EventArgs.Empty);
        }

        private void TextBox_code_Leave(object sender, EventArgs e)
        {
            if (textBox_code.ReadOnly == false)
            {
                textBox_code.Text = Accessory.CheckHexString(textBox_code.Text);
                ParseEscPos.Init(Accessory.ConvertHexToByteArray(textBox_code.Text), CommandDatabase);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveBinFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = SourceFile;
            saveFileDialog.Title = "Save BIN file";
            saveFileDialog.DefaultExt = "bin";
            saveFileDialog.Filter = "BIN files|*.bin|PRN files|*.prn|All files|*.*";
            saveFileDialog.ShowDialog();
        }

        private void SaveHexFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = SourceFile;
            saveFileDialog.Title = "Save HEX file";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text files|*.txt|HEX files|*.hex|All files|*.*";
            saveFileDialog.ShowDialog();
        }

        private void SaveCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = DbFile;
            saveFileDialog.Title = "Save CSV database";
            saveFileDialog.DefaultExt = "csv";
            saveFileDialog.Filter = "CSV files|*.csv|All files|*.*";
            saveFileDialog.ShowDialog();
        }

        private void SaveFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textBox_code.Text = Accessory.CheckHexString(textBox_code.Text);
            if (saveFileDialog.Title == "Save HEX file")
            {
                File.WriteAllText(saveFileDialog.FileName, textBox_code.Text, Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage));
            }
            else if (saveFileDialog.Title == "Save BIN file")
            {
                using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Append))
                {
                    stream.Write(Accessory.ConvertHexToByteArray(textBox_code.Text), 0, textBox_code.TextLength / 3);
                }
            }
            else if (saveFileDialog.Title == "Save CSV database")
            {
                int columnCount = dataGridView_commands.ColumnCount;
                string output = "";
                for (int i = 0; i < columnCount; i++)
                {
                    output += dataGridView_commands.Columns[i].Name.ToString() + ";";
                }
                output += "\r\n";
                for (int i = 0; i < dataGridView_commands.RowCount; i++)
                {
                    for (int j = 0; j < columnCount; j++)
                    {
                        output += dataGridView_commands.Rows[i].Cells[j].Value.ToString() + ";";
                    }
                    output += "\r\n";
                }
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, output, Encoding.GetEncoding(EscPosParser.Properties.Settings.Default.CodePage));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error writing to file " + saveFileDialog.FileName + ": " + ex.Message);
                }
            }

        }

        private void LoadBinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Title = "Open BIN file";
            openFileDialog.DefaultExt = "bin";
            openFileDialog.Filter = "BIN files|*.bin|PRN files|*.prn|All files|*.*";
            openFileDialog.ShowDialog();
        }

        private void LoadHexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Title = "Open HEX file";
            openFileDialog.DefaultExt = "txt";
            openFileDialog.Filter = "HEX files|*.hex|Text files|*.txt|All files|*.*";
            openFileDialog.ShowDialog();
        }

        private void LoadCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "";
            openFileDialog.Title = "Open CSV database";
            openFileDialog.DefaultExt = "csv";
            openFileDialog.Filter = "CSV files|*.csv|All files|*.*";
            openFileDialog.ShowDialog();
        }

        private void OpenFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (openFileDialog.Title == "Open BIN file")  //binary data read
            {
                SourceFile = openFileDialog.FileName;
                try
                {
                    sourceData.Clear();
                    sourceData.AddRange(File.ReadAllBytes(SourceFile));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\r\nError reading file " + SourceFile + ": " + ex.Message);
                }
                //Form1.ActiveForm.Text += " " + SourceFile;
                textBox_code.Text = Accessory.ConvertByteArrayToHex(sourceData.ToArray());
                textBox_code.Select(0, 0);
                ParseEscPos.Init(sourceData.ToArray(), CommandDatabase);
            }
            else if (openFileDialog.Title == "Open HEX file") //hex text read
            {
                SourceFile = openFileDialog.FileName;
                try
                {
                    textBox_code.Text = Accessory.CheckHexString(File.ReadAllText(SourceFile));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\r\nError reading file " + SourceFile + ": " + ex.Message);
                }
                //Form1.ActiveForm.Text += " " + SourceFile;
                sourceData.Clear();
                sourceData.AddRange(Accessory.ConvertHexToByteArray(textBox_code.Text));
                textBox_code.Select(0, 0);
                ParseEscPos.Init(Accessory.ConvertHexToByteArray(textBox_code.Text), CommandDatabase);
            }
            else if (openFileDialog.Title == "Open CSV database") //hex text read
            {
                ReadCsv(openFileDialog.FileName);
            }
        }

        private void DefaultCSVToolStripTextBox_Leave(object sender, EventArgs e)
        {
            if (defaultCSVToolStripTextBox.Text != EscPosParser.Properties.Settings.Default.database)
            {
                EscPosParser.Properties.Settings.Default.database = defaultCSVToolStripTextBox.Text;
                EscPosParser.Properties.Settings.Default.Save();
            }
        }

        private void EnableDatabaseEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableDatabaseEditToolStripMenuItem.Checked = !enableDatabaseEditToolStripMenuItem.Checked;
            dataGridView_commands.ReadOnly = !enableDatabaseEditToolStripMenuItem.Checked;
        }

        private void EnableFileEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableFileEditToolStripMenuItem.Checked = !enableFileEditToolStripMenuItem.Checked;
            textBox_code.ReadOnly = !enableFileEditToolStripMenuItem.Checked;
        }
    }
}
