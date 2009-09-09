using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Xml;

namespace BlackCat
{

    //This class contains a list of methods to verify file/folder 
    //and native file format.
    public class Validator : IValidator
    {
        const int MINIMUM_AMT_OF_DISKSPACE = 1000;
        //TODOs:

        public bool validationFileType(string filePath, string fileType)
        {
            //TODO: implement
            return true;
        }

        public bool validationFileFomart(string filePath, string fileFormat)
        {
            if (filePath == "" || filePath == null || fileFormat == "" || fileFormat == null) { return false; }
            string[] tempString = filePath.Split('/', '/');
            string[] fileName = tempString[tempString.Length - 1].Split('.');
            if (fileName[fileName.Length - 1] != fileFormat) { return false; }
            return true;
        }

        private bool IsDouble(string _double)
        {
            if (_double == "" || _double == null) { return false; }
            int dotNum = 0;
            int charNum = _double.Length;
            char[] charArray = new char[charNum];
            foreach (char c in _double.ToCharArray())
            {
                if (c == '.') { dotNum++; }
                for (int i = 0; i < charNum; i++)
                {
                    charArray[i] = c;
                }
            }
            if (dotNum > 1) { return false; }// check how many "." contained in string
            else
            {
                if (charArray[0] == '-')
                {
                    if (charArray[1] == '.') { return false; }
                    else
                    {
                        for (int i = 1; i < charNum; i++)
                        {
                            if (!char.IsNumber(charArray[i]) && charArray[i] != '.') { return false; }
                        }
                    }
                }
                else
                {
                    if (charArray[0] == '.') { return false; }
                    for (int i = 0; i < charNum; i++)
                    {
                        if (!char.IsNumber(charArray[i]) && charArray[i] != '.') { return false; }
                    }
                }
            }
            return true;
        }



        private bool IsName(string _name)
        {
            bool isLetter = true;
            string[] tempString = _name.Split(' ');
            if (tempString.Length > 3)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < tempString.Length; i++)
                {

                    foreach (char c in tempString[i].ToCharArray())
                    {
                        isLetter = char.IsLetter(c);
                        if (!isLetter && c != '-' && c != '\'')
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        private bool IsLetter(string _string)
        {
            bool isLetter = true;
            foreach (char c in _string.ToCharArray())
            {
                isLetter = char.IsLetter(c);
                if (!isLetter) { return false; }
            }
            return true;
        }

        private bool IsSeat(string seat)
        {
            bool isLetter = true;
            foreach (char c in seat.ToCharArray())
            {
                isLetter = char.IsLetter(c);
                if (!isLetter && c != ' ')
                {
                    return false;
                }

            }
            return true;
        }






        //check if it is a number
        private bool IsNum(string stringNum)
        {
            bool IsNumber = true;
            foreach (char c in stringNum.ToCharArray())
            {
                IsNumber = char.IsNumber(c);
                if (!IsNumber)
                {
                    return false;
                }
            }
            return true;
        }


        private bool validationPenData(string penData)
        {
            if (penData == "" || penData == null) { return false; }
            else
            {
                string[] tempString = penData.Split(' ', '(', ')', ',');
                if (tempString[1] == "" || tempString[2] == "" || tempString[3] == "")
                {
                    return false;
                }
                else
                {
                    if (!IsNum(tempString[1]) || !IsNum(tempString[2]) || !IsNum(tempString[3])) { return false; }
                    else
                    {
                        if (int.Parse(tempString[1]) > 7 || int.Parse(tempString[1]) < 1) { return false; }
                        else if (int.Parse(tempString[2]) > 118 || int.Parse(tempString[2]) < 0) { return false; }
                        else if (Int32.Parse(tempString[3]) > 16777216 || Int32.Parse(tempString[3]) < 0) { return false; }
                    }
                }
            }
            return true;
        }


        private bool validationBrushData(string brushData)
        {
            string[] tempString = brushData.Split(' ', '(', ')', ',');
            if (tempString[1] == "" || tempString[2] == "" || tempString[3] == "") { return false; }
            else
            {
                if (!IsNum(tempString[1]) || !IsNum(tempString[2]) || !IsNum(tempString[3])) { return false; }
                else
                {
                    if (int.Parse(tempString[1]) > 71 || int.Parse(tempString[1]) < 1) { return false; }
                    else if (Int32.Parse(tempString[2]) > 16777216 || Int32.Parse(tempString[2]) < 0) { return false; }
                    else if (Int32.Parse(tempString[3]) > 16777216 || Int32.Parse(tempString[3]) < 0) { return false; }
                }
            }
            return true;
        }


        private bool validationCenterData(string centerDataX, string centerDataY)
        {
            string coordinate = "(" + centerDataX + "," + centerDataY + ")";
            if (!validationCoordinate(coordinate)) { return false; }
            return true;
        }


        private bool validationRegPolygon(string polygon)
        {
            if (polygon == "" || polygon == null) { return false; }
            string[] tempString = polygon.Split(' ');
            if (tempString[4] != "Pen" && tempString[4] != "Brush" && tempString[4] != "Center") { return false; }
            else
            {
                if (tempString[4] == "Pen")
                {
                    if (tempString[5] == "" || tempString[5] == null) { return false; }
                    else if (!validationPenData(tempString[5])) { return false; }
                }
                else if (tempString[4] == "Brush")
                {
                    if (tempString[5] == "" || tempString[5] == null) { return false; }
                    else if (!validationBrushData(tempString[5])) { return false; }
                }
                else if (tempString[4] == "Center")
                {
                    if (tempString[5] == "" || tempString[5] == null || tempString[6] == "" || tempString[6] == null) { return false; }
                    else if (!validationCenterData(tempString[5], tempString[6])) { return false; }
                }
            }
            return true;
        }

        private bool validationRegCoordinate(string coordinate)
        {
            string[] tempString = coordinate.Split(' ');
            if (tempString.Length != 2) { return false; }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    foreach (char c in tempString[i].ToCharArray())
                    {
                        if (!char.IsNumber(c) && c != '-' && c != '.')
                        {
                            return false;
                        }
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (float.Parse(tempString[i]) > float.Parse("180") || float.Parse(tempString[i]) < float.Parse("-180")) { return false; }
                }
            }
            return true;
        }


        private bool validationCoordinate(string coordinate)
        {
            if (coordinate == "" || coordinate == null) { return false; }
            string[] tempCoord = coordinate.Split('(', ')');
            if (tempCoord.Length != 3) { return false; }
            else
            {
                string[] coord = tempCoord[1].Split(',');
                for (int i = 0; i < 2; i++)
                {
                    foreach (char c in coord[i].ToCharArray())
                    {
                        if (!char.IsNumber(c) && c != '-' && c != '.')
                        {
                            return false;
                        }
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (float.Parse(coord[i]) > float.Parse("180") || float.Parse(coord[i]) < float.Parse("-180")) { return false; }
                }
            }
            return true;
        }


        //check the format of each region section
        private bool validateRegionData(String mifURL, int startLineNum, int endLineNum)
        {
            Int32 totalNum = endLineNum - startLineNum + 1;
            string[] dataArray = new string[totalNum];
            Int32 tempArrNum = 0;
            Int16 regionNum = 0;
            Int32 startLine = 1; //represent the start line for each region section.
            StreamReader fileReader = new StreamReader(mifURL);
            string fileData = "";
            //Read Region section and stores these data in an array.
            while (fileData != null && totalNum != 0)
            {
                fileData = fileReader.ReadLine();
                if (startLineNum == 0)
                {//读第一行
                    dataArray[tempArrNum] = fileData;
                    totalNum--;
                    tempArrNum++;
                }
                if (startLineNum != 0)
                {
                    startLineNum--;
                }
            }
            fileReader.Close(); // end Region section data reading

            //get the number of sections included in region 
            if (dataArray[0] != "")
            {
                string[] tempSplitString = dataArray[0].Split(' ');
                if (tempSplitString[2] != "")
                {
                    if (!IsNum(tempSplitString[2]))
                    {
                        return false;
                    }
                    //get the number of sections included in region 
                    else { regionNum = Int16.Parse(tempSplitString[2]); }
                }
                else
                {
                    return false;
                }
            }
            else { return false; }
            //check the sections data of region.
            for (Int16 i = 0; i < regionNum; i++)
            {
                string[] tempElementNum = dataArray[startLine].Split(' ');
                if (tempElementNum.Length != 3) { return false; }
                else if (!IsNum(tempElementNum[2]))
                {
                    return false;
                } //here can check format and number of Region section.
                else
                {
                    Int32 elementNum = Int32.Parse(dataArray[startLine]);
                    Int32 readLine = startLine;
                    startLine = startLine + elementNum + 1;
                    for (Int32 j = readLine + 1; j < startLine; j++)
                    {
                        if (!validationRegCoordinate(dataArray[j]))
                        {
                            return false;
                        }
                    }
                }
            }
            for (Int32 j = (dataArray.Length - 3); j < dataArray.Length; j++)
            {
                if (!validationRegPolygon(dataArray[j]))
                {
                    return false;
                }
            }
            return true;
        }




        private int getRegionNum(string midURL)
        {
            int tempNum = 0;
            StreamReader fileReader = new StreamReader(midURL);
            string fileData = "";
            while (fileData != null)
            {
                fileData = fileReader.ReadLine();
                if (fileData != null)
                {
                    tempNum++;
                }
            }
            fileReader.Close();
            return tempNum;
        }

        private bool validateColumns(string columns)
        {
            string[] tempString = columns.Split(' ');
            if (!IsNum(tempString[1])) { return false; }
            return true;
        }

        private bool validateCoordsys(string coordsys)
        {
            string[] tempString = coordsys.Split(' ');
            if (tempString.Length != 7 && tempString.Length != 5 && tempString.Length != 4)
            {
                return false;
            }
            else
            {
                if (tempString[1] != "Earth") { return false; }
                else if (tempString[2] != "Projection" && tempString[2] != "Bounds") { return false; }
                else if (tempString[2] == "Projection")
                {
                    string[] tempNum = tempString[3].Split(',');
                    bool IsNumber = true;
                    foreach (char c in tempNum[0].ToCharArray())
                    {
                        IsNumber = char.IsNumber(c);
                        if (!IsNumber)
                        {
                            return false;
                        }
                    }
                    foreach (char c in tempString[4].ToCharArray())
                    {
                        IsNumber = char.IsNumber(c);
                        if (!IsNumber)
                        {
                            return false;
                        }
                    }
                }
                if (tempString[5] == "Bounds")
                {
                    string[] tempCoord = tempString[6].Split('(', ')');
                    if (tempCoord.Length != 3) { return false; }
                    else
                    {
                        string[] coord = tempCoord[1].Split(',');
                        for (int i = 0; i < 2; i++)
                        {
                            foreach (char c in coord[i].ToCharArray())
                            {
                                if (!char.IsNumber(c) && c != '-' && c != '.')
                                {
                                    return false;
                                }
                            }
                        }
                        for (int i = 0; i < 2; i++)
                        {

                            if (float.Parse(coord[i]) > float.Parse("180") || float.Parse(coord[i]) < float.Parse("-180")) { return false; }
                        }
                    }
                }
            }
            return true;
        }


        private bool validateIndex(string index)
        {
            string[] tempString = index.Split(' ');
            string[] indexNum = tempString[1].Split(',');
            int tempNum = indexNum.Length;
            bool IsNumber = true;
            for (int i = 0; i < tempNum; i++)
            {
                foreach (char c in indexNum[i].ToCharArray())
                {
                    IsNumber = char.IsNumber(c);
                    if (!IsNumber)
                    {
                        return false;
                    }
                }
            }
            return true;

        }


        private bool validateDelimiter(string delimiter)
        {
            string[] tempString = delimiter.Split(' ');
            //check DelimiterElement
            if (tempString[0] != "Delimiter")
            {
                return false;
            }
            //if Delimiter element is contains char
            if (tempString[1] != "")
            {
                int charNum = tempString[1].ToCharArray().Length;
                //check if the Delimiter character format is correct. 3 chars.
                if (charNum != 3)
                {
                    return false;
                }
                //contains 3 chars.
                else
                {
                    char[] character = new char[3];
                    int i = 0;
                    foreach (char c in tempString[1].ToCharArray())
                    {
                        character[i] = c;
                        i++;
                    }
                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 0 || j == 2)
                        {
                            if (character[j] != '"')
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            //if Delimiter element is not contains char
            else
            {
                return false;
            }
            return true;
        }


        private bool validateCharset(string charset)
        {
            string[] tempString = charset.Split(' ');
            if (tempString[0] != "Charset")
            {
                return false;
            }
            if (tempString[1] != "\"WindowsLatin1\"")
            {
                return false;
            }
            return true;
        }

        private bool validateVersion(string version)
        {
            string[] tempString = version.Split(' ');
            bool IsNumber = true;
            if (tempString[0] != "Version")
            {
                return false;
            }
            foreach (char c in tempString[1].ToCharArray())
            {
                IsNumber = char.IsNumber(c);
                if (!IsNumber)
                {
                    return false;
                }
            }
            return true;
        }



        private bool validateHeader(string validateString, string caseName)
        {
            switch (caseName)
            {
                case ("validateVersion"):
                    return validateVersion(validateString);

                case ("validateCharset"):
                    return validateCharset(validateString);

                case ("validateDelimiter"):
                    return validateDelimiter(validateString);

                case ("validateIndex"):
                    return validateIndex(validateString);

                case ("validateCoordsys"):
                    return validateCoordsys(validateString);

                case ("validateColumns"):
                    return validateColumns(validateString);

                default: return true;
            }
        }





        private bool IsHexNum(string hexNum)
        {
            foreach (char c in hexNum.ToCharArray())
            {
                if (!char.IsNumber(c) && c != 'a' && c != 'b' && c != 'c' && c != 'd' && c != 'e' && c != 'f') { return false; }
            }
            return true;
        }






        public bool hasSufficientDiskSpace(String drivePath)
        {

            DriveInfo drive = new DriveInfo(drivePath);
            if (drive.AvailableFreeSpace / (1024 * 1024) > MINIMUM_AMT_OF_DISKSPACE)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool folderIsWritable(String folderURL)
        {
            DirectoryInfo directory = new DirectoryInfo(folderURL);
            string attributes = directory.Attributes.ToString();
            string[] attributesList = attributes.Split(',', ' ');
            foreach (string attribute in attributesList)
            {
                if (attribute == "ReadOnly")
                {
                    return false;
                }
            }
            return true;
        }



        public bool folderExists(String folderURL)
        {
            if (Directory.Exists(folderURL) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public bool fileIsReadable(String fileURL)
        {
            if (File.Exists(fileURL) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public bool fileExists(String fileURL)
        {
            if (File.Exists(fileURL) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool validateQFREExcelFormat(String excelURL)
        {//Qld_FederalResults by Electorate-2004.xls
            string conn = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + excelURL + ";Extended Properties=Excel 8.0";
            OleDbConnection myConn = new OleDbConnection(conn);
            string comm = " SELECT * FROM [2004 Election$] ";
            myConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(comm, myConn);
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet, "[2004 Election$]");
            myConn.Close();
            DataTable dataTable = myDataSet.Tables[0];
            //check columns names.
            if (dataTable.Columns.Count != 10)
            {
                return false;
            }
            else
            {
                if (dataTable.Columns[2].ToString() != "% First Preferences" && dataTable.Columns[8].ToString() != "% TPP")
                {
                    return false;
                }
                int rowNum = dataTable.Rows.Count;
                for (int a = 1; a < rowNum; a++)
                {
                    //if (a == 0)
                    //{
                    //    for (int j = 0; j < 10; j++)
                    //    {
                    //       string colString = dataTable.Rows[1][j].ToString();
                    //        if (colString != "Division" && colString != "State" && colString != "ALP" && colString != "LP" && colString != "NP" && colString != "DEM" && colString != "GRN" && colString != "OTH" && colString != "LNP" && colString != "ALP")
                    //        {
                    //            //return false;
                    //        }
                    //    }
                    //}

                    for (int b = 0; b < 10; b++)
                    {
                        if (b == 0)
                        {
                            if (!IsName(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                        }
                        else if (b == 1)
                        {
                            if (!IsLetter(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                        }
                        else if (b == 2)
                        {
                            if (!IsDouble(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                            else if (double.Parse(dataTable.Rows[a][b].ToString()) < 0 || double.Parse(dataTable.Rows[a][b].ToString()) > 100.00)
                            {
                                return false;
                            }
                        }
                        else if (b == 3)
                        {
                            if (!IsDouble(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                            else if (double.Parse(dataTable.Rows[a][b].ToString()) < 0 || double.Parse(dataTable.Rows[a][b].ToString()) > 100.00)
                            {
                                return false;
                            }
                        }
                        else if (b == 4)
                        {
                            if (!IsDouble(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                            else if (double.Parse(dataTable.Rows[a][b].ToString()) < 0 || double.Parse(dataTable.Rows[a][b].ToString()) > 100.00)
                            {
                                return false;
                            }
                        }
                        else if (b == 5)
                        {
                            if (!IsDouble(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                            else if (double.Parse(dataTable.Rows[a][b].ToString()) < 0 || double.Parse(dataTable.Rows[a][b].ToString()) > 100.00)
                            {
                                return false;
                            }
                        }
                        else if (b == 6)
                        {
                            if (!IsDouble(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                            else if (double.Parse(dataTable.Rows[a][b].ToString()) < 0 || double.Parse(dataTable.Rows[a][b].ToString()) > 100.00)
                            {
                                return false;
                            }
                        }
                        else if (b == 7)
                        {
                            if (!IsDouble(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                            else if (double.Parse(dataTable.Rows[a][b].ToString()) < 0 || double.Parse(dataTable.Rows[a][b].ToString()) > 100.00)
                            {
                                return false;
                            }
                        }
                        else if (b == 8)
                        {
                            if (!IsDouble(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                            else if (double.Parse(dataTable.Rows[a][b].ToString()) < 0 || double.Parse(dataTable.Rows[a][b].ToString()) > 100.00)
                            {
                                return false;
                            }
                        }
                        else if (b == 9)
                        {
                            if (!IsDouble(dataTable.Rows[a][b].ToString()))
                            {
                                return false;
                            }
                            else if (double.Parse(dataTable.Rows[a][b].ToString()) < 0 || double.Parse(dataTable.Rows[a][b].ToString()) > 100.00)
                            {
                                return false;
                            }
                        }

                    }

                }
            }
            return true;
        }



        public bool validateFERQExcelFormat(String excelURL)
        {//Federal Election Results-Qld-2004.xls
            string conn = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + excelURL + ";Extended Properties=Excel 8.0";
            OleDbConnection myConn = new OleDbConnection(conn);
            string comm = " SELECT * FROM [2004 Election Results$] ";
            myConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(comm, myConn);
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet, "[2004 Election Results$]");
            myConn.Close();
            DataTable dataTable = myDataSet.Tables[0];
            //check columns names.
            if (dataTable.Columns.Count != 6)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    string colString = dataTable.Columns[i].ToString();
                    if (colString != "Seat" && colString != "Party" && colString != "Percentage" && colString != "MP" && colString != "Held Since" && colString != "Previously Held") { return false; }
                }
            }
            //check data rows
            int rowNum = dataTable.Rows.Count;
            for (int a = 0; a < rowNum; a++)
            {
                for (int b = 0; b < 6; b++)
                {
                    if (b == 0)
                    {
                        if (!IsSeat(dataTable.Rows[a][b].ToString()))
                        {
                            return false;
                        }
                    }
                    else if (b == 1)
                    {
                        if (!IsLetter(dataTable.Rows[a][b].ToString()))
                        {
                            return false;
                        }
                        else
                        {
                            string tempParty = dataTable.Rows[a][b].ToString();
                            if (tempParty != "LIB" && tempParty != "ALP" && tempParty != "NAT" && tempParty != "IND")
                            {
                                return false;
                            }
                        }
                    }
                    else if (b == 2)
                    {
                        if (!IsDouble(dataTable.Rows[a][b].ToString()))
                        {
                            return false;
                        }
                        else if (double.Parse(dataTable.Rows[a][b].ToString()) > 100.00 || double.Parse(dataTable.Rows[a][b].ToString()) < 0.00)
                        {
                            return false;
                        }
                    }
                    else if (b == 3)
                    {
                        if (!IsName(dataTable.Rows[a][b].ToString()))
                        {
                            return false;
                        }
                    }
                    else if (b == 4)
                    {
                        if (!IsNum(dataTable.Rows[a][b].ToString()))
                        {
                            return false;
                        }
                        else
                        {
                            if (int.Parse(dataTable.Rows[a][b].ToString()) > 2010 || int.Parse(dataTable.Rows[a][b].ToString()) < 1900)
                            {
                                return false;
                            }
                        }
                    }
                    else if (b == 5)
                    {
                        if (dataTable.Rows[a][b].ToString() != "" && dataTable.Rows[a][b].ToString() != null)
                        {
                            string[] tempSeatString = dataTable.Rows[a][b].ToString().Split(' ');
                            for (int i = 0; i < tempSeatString.Length; i++)
                            {
                                if (IsNum(tempSeatString[i]))
                                {
                                    if (int.Parse(tempSeatString[i]) > 2010 || int.Parse(tempSeatString[i]) < 1900)
                                    {
                                        return false;
                                    }
                                }
                                else if (tempSeatString[i] != "-" && tempSeatString[i] != "LIB" && tempSeatString[i] != "ALP" && tempSeatString[i] != "NAT" && tempSeatString[i] != "IND")
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }


        //only check the number of region contained in MID file, if it match to the number of
        //Region section contained in MIF file. And also check the number of the each line in MID file.
        public bool validateMidFormat(String mifURL, String midURL)
        {
            StreamReader mifFileReader = new StreamReader(mifURL);
            StreamReader midFileReader = new StreamReader(midURL);
            string mifFileData = "";
            string midFileData = "";
            Int32 lineNum = 0;
            Int16 regionNum = 0;
            Int16 midDataNum = 0;
            while (mifFileData != null)
            {
                mifFileData = mifFileReader.ReadLine();
                if (mifFileData != null)
                {
                    string[] tempString = mifFileData.Split(' ');
                    if (tempString[0] == "Region")
                    {
                        regionNum++;
                    }
                }
                lineNum++;
            }
            mifFileReader.Close();

            while (midFileData != null)
            {
                midFileData = midFileReader.ReadLine();
                if (midFileData != null)
                {
                    string[] tempString = midFileData.Split(',');
                    if (!IsNum(tempString[0]))
                    {
                        return false;
                    }
                    else if (Int16.Parse(tempString[0]) != (midDataNum + 1))
                    {
                        return false;
                    }//check the number of the each line, if it match the number of the line.
                    midDataNum++;
                }
            }
            midFileReader.Close();

            if (midDataNum != regionNum)
            {
                return false;
            }
            return true;
        }


        public bool validateMifFormat(String mifURL, String midURL)
        {
            string validationSection = "Header";
            StreamReader fileReader = new StreamReader(mifURL);
            string fileData = "";
            //lineNum  is plus 1 after read one line. So the final lineNum is the actual line number.
            //But in the processing, it is (actual line number) -1.
            Int32 lineNum = 0;
            Int32 dataSectionLineNum = 0;//actual line number
            Int32 tempLineNum = 0;
            int columnsNum = 0; //actual columns number
            int regionNum = getRegionNum(midURL);//actual Region number
            Int32[] regionArray = new int[regionNum]; //start line of each region.
            int regionArrNum = 0;
            while (fileData != null)
            {
                fileData = fileReader.ReadLine();
                if (validationSection == "Header")
                {
                    //validate version
                    if (lineNum == 0)
                    {
                        if (validateHeader(fileData, "validateVersion") == false)
                        {
                            return false;
                        }
                    }
                    //validate Charset
                    else if (lineNum == 1)
                    {
                        if (validateHeader(fileData, "validateCharset") == false)
                        {
                            return false;
                        }
                    }
                    //validate Delimiter
                    else if (lineNum == 2)
                    {
                        if (validateHeader(fileData, "validateDelimiter") == false)
                        {
                            return false;
                        }
                    }
                    else if (lineNum == 3 || lineNum == 4 || lineNum == 5 || lineNum == 6 || lineNum == 7)
                    {
                        //check index format
                        string[] tempString = fileData.Split(' ');
                        if (tempString[0] == "Index")
                        {
                            if (validateHeader(fileData, "validateIndex") == false)
                            {
                                return false;
                            }
                        }
                        if (tempString[0] == "CoordSys")
                        {
                            if (validateHeader(fileData, "validateCoordsys") == false)
                            {
                                return false;
                            }
                        }
                        if (tempString[0] == "Columns")
                        {
                            if (validateHeader(fileData, "validateColumns") == false)
                            {
                                return false;
                            }
                            else
                            {
                                columnsNum = int.Parse(tempString[1]);
                                tempLineNum = lineNum;
                            }
                        }
                    }
                    //check columns number
                    else if (columnsNum != 0 && lineNum <= tempLineNum + columnsNum)
                    {

                        string[] tempString = fileData.Split(' ');
                        if (tempString[0] != "") { return false; }
                        else
                        {
                            if (tempString.Length != 4) { return false; }
                        }
                        //end of check header section
                        if (lineNum == tempLineNum + columnsNum) { validationSection = "Data"; }
                    }
                }
                else if (validationSection == "Data")
                {
                    dataSectionLineNum++;
                    if (dataSectionLineNum == 1)
                    {
                        if (fileData != "Data") { return false; }
                    }
                    else if (dataSectionLineNum == 2)
                    {
                        if (fileData != "") { return false; }
                        validationSection = "Region";
                        dataSectionLineNum = 0;
                    }
                }
                else if (validationSection == "Region")
                {
                    if (fileData != "" && fileData != null)
                    {
                        string[] regionString = fileData.Split(' ');
                        if (regionString[0] == "Region")
                        {
                            if (!IsNum(regionString[2])) { return false; }
                            else
                            {
                                if (regionArrNum > regionNum - 1) { return false; }
                                regionArray[regionArrNum] = lineNum;
                                regionArrNum++;
                            }
                        }
                    }
                }
                if (fileData != null)
                {
                    lineNum++;
                }
            }
            fileReader.Close();
            if (regionArray.Length != regionNum) { return false; }

            //check each region data
            for (int i = 0; i < regionArray.Length; i++)
            {
                if (i == regionArray.Length - 1)
                {
                    if (!validateRegionData(mifURL, (regionArray[i]), (lineNum - 1))) { return false; }
                }
                else
                {
                    if (!validateRegionData(mifURL, (regionArray[i]), (regionArray[i + 1] - 1)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool validateKMLFormat(String kmlURL)
        {
            XmlDocument kmldoc = new XmlDocument();
            try //here is system self validates tag pairs format.
            {
                kmldoc.Load(kmlURL);//load KML file
            }
            catch
            {
                return false;
            }
            //XmlNode test=kmldoc.SelectSingleNode("Document");
            //int woria;
            //woria=test.Count;
            //Console.WriteLine(test.Name );
            //check if contain "xmlns" attribute.
            //if(){}

            XmlNamespaceManager nameSpace = new XmlNamespaceManager(kmldoc.NameTable);
            nameSpace.AddNamespace("kml_xmlns", "http://earth.google.com/kml/2.0");
            XmlElement kml = kmldoc.DocumentElement;
            //read the node content (need to add namespane prefix front of node. "kml_xmlns")
            kml.SelectSingleNode("kml_xmlns:kml", nameSpace);
            XmlElement xeKML = (XmlElement)kml;
            XmlNodeList xnl = xeKML.ChildNodes;
            //check Ducument element;
            if (xnl == null) { return false; }
            else
            {
                foreach (XmlNode xn in xnl)
                { //should contain "Document"
                    XmlElement xe = (XmlElement)xn;
                    if (xe.Name != "Document") { return false; }//check the element's name (Document)
                    else
                    {
                        XmlNodeList xnl_1 = xe.ChildNodes;
                        //check the two elements included in Document element.
                        if (xnl_1 == null) { return false; }
                        else
                        {
                            foreach (XmlNode xn1 in xnl_1) //should contain "Style","Folder"
                            {
                                XmlNodeType nodeType = xn1.NodeType;
                                if (nodeType == XmlNodeType.Element)
                                {
                                    XmlElement xe1 = (XmlElement)xn1;
                                    if (xe1.Name != "Style" && xe1.Name != "Folder") { return false; }
                                    else if (xe1.Name == "Style")
                                    {
                                        XmlNodeList xnl_2 = xe1.ChildNodes;
                                        if (xnl_2 == null) { return false; }
                                        foreach (XmlNode xn2 in xnl_2)
                                        {//should contain "IconStyle","LabelStyle","LineStyle","PolyStyle"
                                            XmlElement xe2 = (XmlElement)xn2;
                                            if (xnl_2.Count != 4) { return false; }
                                            if (xe2.Name != "IconStyle" && xe2.Name != "LabelStyle" && xe2.Name != "LineStyle" && xe2.Name != "PolyStyle") { return false; }
                                            if (xe2.Name == "IconStyle")
                                            {
                                                XmlNodeList xnl_IconStyle = xe2.ChildNodes;
                                                if (xnl_IconStyle == null) { return false; }
                                                foreach (XmlNode nodeInIconStyle in xnl_IconStyle)
                                                {//should contain "color","scale","Icon"
                                                    XmlElement _nodeInIconStyle = (XmlElement)nodeInIconStyle;
                                                    if (xnl_IconStyle.Count != 3) { return false; }
                                                    if (_nodeInIconStyle.Name != "color" && _nodeInIconStyle.Name != "scale" && _nodeInIconStyle.Name != "Icon") { return false; }
                                                    if (_nodeInIconStyle.Name == "Icon")
                                                    {
                                                        XmlNodeList xnl_Icon = _nodeInIconStyle.ChildNodes;
                                                        if (xnl_Icon == null) { return false; }
                                                        foreach (XmlNode nodeInIcon in xnl_Icon)
                                                        {//should contains "href","x","y","w","","h"
                                                            XmlElement _nodeInIcon = (XmlElement)nodeInIcon;
                                                            if (xnl_Icon.Count != 5)
                                                            {
                                                                return false;
                                                            }
                                                            if (_nodeInIcon.Name != "href" && _nodeInIcon.Name != "x" && _nodeInIcon.Name != "y" && _nodeInIcon.Name != "w" && _nodeInIcon.Name != "h") { return false; }
                                                            //check elements format included in "Icon"
                                                            if (_nodeInIcon.Name == "x") { if (!IsNum(_nodeInIcon.InnerText)) { return false; } }
                                                            else if (_nodeInIcon.Name == "y")
                                                            {
                                                                if (!IsNum(_nodeInIcon.InnerText)) { return false; }
                                                            }
                                                            else if (_nodeInIcon.Name == "w") { if (!IsNum(_nodeInIcon.InnerText)) { return false; } }
                                                            else if (_nodeInIcon.Name == "h") { if (!IsNum(_nodeInIcon.InnerText)) { return false; } }
                                                        }
                                                    }
                                                }
                                            }
                                            else if (xe2.Name == "LabelStyle")
                                            {//should contain "color","scale"
                                                XmlNodeList xnl_LabelStyle = xe2.ChildNodes;
                                                if (xnl_LabelStyle == null) { return false; }
                                                foreach (XmlNode nodeInLableStyle in xnl_LabelStyle)
                                                {
                                                    XmlElement _nodeInLableStyle = (XmlElement)nodeInLableStyle;
                                                    if (xnl_LabelStyle.Count != 2) { return false; }
                                                    if (_nodeInLableStyle.Name != "color" && _nodeInLableStyle.Name != "scale") { return false; }
                                                    if (_nodeInLableStyle.Name == "color")
                                                    {
                                                        if (!IsHexNum(_nodeInLableStyle.InnerText)) { return false; }
                                                        else if (Convert.ToInt64(_nodeInLableStyle.InnerText, 16) > Convert.ToInt64("ffffffff", 16) || Convert.ToInt64(_nodeInLableStyle.InnerText, 16) < 0) { return false; }
                                                    }
                                                    else if (_nodeInLableStyle.Name == "scale")
                                                    {
                                                        if (!IsDouble(_nodeInLableStyle.InnerText)) { return false; }
                                                        else if (double.Parse(_nodeInLableStyle.InnerText) < 0) { return false; }
                                                    }
                                                }
                                            }
                                            else if (xe2.Name == "LineStyle")
                                            {//should contain "color","width"
                                                XmlNodeList xnl_LineStyle = xe2.ChildNodes;
                                                if (xnl_LineStyle == null) { return false; }
                                                foreach (XmlNode nodeInLineStyle in xnl_LineStyle)
                                                {
                                                    XmlElement _nodeInLineStyle = (XmlElement)nodeInLineStyle;
                                                    if (xnl_LineStyle.Count != 2) { return false; }
                                                    if (_nodeInLineStyle.Name != "color" && _nodeInLineStyle.Name != "width") { return false; }
                                                    if (_nodeInLineStyle.Name == "color")
                                                    {
                                                        if (!IsHexNum(_nodeInLineStyle.InnerText)) { return false; }
                                                        else if (Convert.ToInt64(_nodeInLineStyle.InnerText, 16) > Convert.ToInt64("ffffffff", 16) || Convert.ToInt64(_nodeInLineStyle.InnerText, 16) < 0) { return false; }
                                                    }
                                                    else if (_nodeInLineStyle.Name == "width")
                                                    {
                                                        if (!IsNum(_nodeInLineStyle.InnerText)) { return false; }
                                                        else if (int.Parse(_nodeInLineStyle.InnerText) < 0) { return false; }
                                                    }
                                                }
                                            }
                                            else if (xe2.Name == "PolyStyle")
                                            {//should contain "color","colorMode"
                                                XmlNodeList xnl_PolyStyle = xe2.ChildNodes;
                                                if (xnl_PolyStyle == null) { return false; }
                                                foreach (XmlNode nodeInPolyStyle in xnl_PolyStyle)
                                                {
                                                    XmlElement _nodeInPolyStyle = (XmlElement)nodeInPolyStyle;
                                                    if (xnl_PolyStyle.Count != 2) { return false; }
                                                    if (_nodeInPolyStyle.Name != "color" && _nodeInPolyStyle.Name != "colorMode") { return false; }
                                                    if (_nodeInPolyStyle.Name == "color")
                                                    {
                                                        if (!IsHexNum(_nodeInPolyStyle.InnerText)) { return false; }
                                                        else if (Convert.ToInt64(_nodeInPolyStyle.InnerText, 16) > Convert.ToInt64("ffffffff", 16) || Convert.ToInt64(_nodeInPolyStyle.InnerText, 16) < 0) { return false; }
                                                    }
                                                    else if (_nodeInPolyStyle.Name == "colorMode")
                                                    {
                                                        if (_nodeInPolyStyle.InnerText != "normal" && _nodeInPolyStyle.InnerText != "random ") { return false; }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (xe1.Name == "Folder")//should contain "Placemark"
                                    {
                                        XmlNodeList xnl_2 = xe1.ChildNodes;
                                        if (xnl_2 == null) { return false; }
                                        int placeMarkNum = 0;
                                        foreach (XmlNode nodeInFolder in xnl_2)
                                        {
                                            int polygonNum = 0;
                                            int lineStringNum = 0;
                                            int pointNum = 0;
                                            XmlElement _nodeInFolder = (XmlElement)nodeInFolder;
                                            if (_nodeInFolder.Name == "Placemark")
                                            { //should contian "LookAt"
                                                placeMarkNum++;
                                                XmlNodeList xnl_Placemark = _nodeInFolder.ChildNodes;
                                                if (xnl_Placemark == null) { return false; }
                                                foreach (XmlNode nodeInPlacemark in xnl_Placemark)
                                                {
                                                    XmlElement _nodeInPlacemark = (XmlElement)nodeInPlacemark;
                                                    if (_nodeInPlacemark.Name == "LookAt")
                                                    {//should contain "longitude","latitude","altitude","heading","tilt","range","altitudeMode"
                                                        XmlNodeList xnl_LookAt = _nodeInPlacemark.ChildNodes;
                                                        if (xnl_LookAt == null) { return false; }
                                                        foreach (XmlNode nodeInLookAtk in xnl_LookAt)
                                                        {
                                                            XmlElement _nodeInLookAtk = (XmlElement)nodeInLookAtk;
                                                            if (_nodeInLookAtk.Name != "longitude" && _nodeInLookAtk.Name != "latitude" && _nodeInLookAtk.Name != "altitude" && _nodeInLookAtk.Name != "heading" && _nodeInLookAtk.Name != "tilt" && _nodeInLookAtk.Name != "altitudeMode" && _nodeInLookAtk.Name != "range")
                                                            {
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                    //Placemarks contain only 1 of either a Polygon, a LineString or a Point.
                                                    if (_nodeInPlacemark.Name == "Polygon") { polygonNum++; }
                                                    if (_nodeInPlacemark.Name == "LineString")
                                                    { //should contain "coordinates","altitudeMode","tessellate","extrude"
                                                        lineStringNum++;
                                                        XmlNodeList xnl_LineString = _nodeInPlacemark.ChildNodes;
                                                        if (xnl_LineString == null)
                                                        {
                                                            return false;
                                                        }
                                                        foreach (XmlNode nodeInLineString in xnl_LineString)
                                                        {
                                                            XmlElement _nodeInLineString = (XmlElement)nodeInLineString;
                                                            if (_nodeInLineString.Name != "coordinates" && _nodeInLineString.Name != "extrude" && _nodeInLineString.Name != "tessellate" && _nodeInLineString.Name != "altitudeMode")
                                                            {
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                    if (_nodeInPlacemark.Name == "Point")
                                                    { //should contain "coordinates","altitudeMode","tessellate","extrude"
                                                        pointNum++;
                                                        XmlNodeList xnl_Point = _nodeInPlacemark.ChildNodes;
                                                        if (xnl_Point == null)
                                                        {
                                                            return false;
                                                        }
                                                        foreach (XmlNode nodeInPoint in xnl_Point)
                                                        {
                                                            XmlElement _nodeInPoint = (XmlElement)nodeInPoint;
                                                            if (_nodeInPoint.Name != "coordinates" && _nodeInPoint.Name != "extrude" && _nodeInPoint.Name != "tessellate" && _nodeInPoint.Name != "altitudeMode")
                                                            {
                                                                return false;
                                                            }
                                                        }
                                                    }

                                                }
                                                if ((polygonNum + lineStringNum + pointNum) > 1)
                                                {
                                                    return false;
                                                }
                                            }
                                        }
                                        if (placeMarkNum == 0) { return false; }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
      
    }


}//END Validation Class

