# EscPosParser

Project intended to ease life of printer technical support team.
It can reduce time for analyzing ESC/POS printer language log files while investigating printing problems.
Programm uses CSV file as a database of printer commands to parse logs collected from printer data line.
"Auto" function generates .escpos file with full parsing log.

Max. Source file length: ~2Gb
Max. database line number: ? Int32 ?

1. CSV content:

	1) Encoding – according to "CodePage" setting in the EscPosParser2.exe.config file (default "866")
	2) Column divider - according to "CSVdelimiter" setting in the EscPosParser2.exe.config file (default ";")

2. Command data base columns:
	1) Command (hex) – command string in HEX 
	2) Parameter Name – Parameter name (any variable). Could be used to calculate formulas in parameter value.
	3) Parameter type – parameter type. Defines size of the data and how to show/decode them.
	4) Parameter value (dec) – predefined parameter values if any. Could be a list of conditions with formulas on resulting (right) side.
	5) Description – description of command, parameter or predefined parameter
	6) Printer models – printer model. Could be a list divided by “,”.
	7) Comments – always ignored. Needed only for correct CSV handling.
	All other columns ignored.

3. Possible parameter types:
	1) Byte – Length: 1 byte; A numerical value displayed.
	2) BitField - Length: 1 byte; A list of bit values displayed.
	3) Word - Length: 2 byte [low, high]; A numerical value displayed.
	4) rWord - Length: 2 byte [high, low]; A numerical value displayed.
	5) TextString - Length: until byte shown in predifined value; String displayed with “” if printable.
	6) BinaryString - Length: until byte shown in predifined value; String length displayed.
	7) DecString - Length: until byte shown in predifined value; A numerical value displayed.
	8) HexString - Length: until byte shown in predifined value; A numerical value displayed.
	9) TextArray - Length: shown in predifined value; String displayed with “” if printable.
	10) BinaryArray - Length: shown in predifined value; Array length displayed.
	11) DecArray - Length: shown in predifined value; A numerical value displayed.
	12) HexArray - Length: shown in predifined value; A numerical value displayed.

4. Parameter conditional selection:
	Conditional operations must be preceeded by "?" sign:
		?<parameter>=<conditional value>:<parameter value>
	Conditions could be put in one line one after another or divided with line feeds (“\r\n”) for better readability. <conditional value> may contain a formula.
Example:
	?m=0:n?m=1:n?m=32:n*3
	?m=33:n*3

5. Parameter value calculation:
	Formula must be preceeded by "@" sign (only if not inside “?” conditional selection).
Example:
	@y*2*(c2-c1+1)

6. Command data base structure:
	Each command or parameter must have it’s 1st line clear until “Description”.
	Each parameter may have several predefined values to define proper description. Description for predefined value is added to description of the parameter in the program GUI.
	Each parameter must be followed by a parameter type definition (capitalization doesn't matter).
	“bitfield” parameter is followed by 8 lines describing each bit
----
See EscPosParser2.odt / esc_pos_olivetti.ods / esc_pos_customSX.ods for examples.
