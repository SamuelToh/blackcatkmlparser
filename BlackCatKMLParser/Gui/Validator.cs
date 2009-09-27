using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Xml;
using System.Security.AccessControl;
using System.Xml.Schema;


namespace BlackCat
{

    //This class contains a list of methods to verify file/folder 
    //and native file format.
    public class Validator
    {
        const int MINIMUM_AMT_OF_DISKSPACE = 1000;

        //TODOs:

        private bool validateDelimiter(string delimiter)
        {
            string[] tempString = delimiter.Split(' ');
            if (tempString[0] == "" || tempString[0] == null)
            {
                return false;
            }
            else if (tempString[1] != "\",\"" && tempString[1] != "\"\t\"")
            {
                return false;
            }
            return true;
        }

        private bool validateCharset(string charset)
        {
            string[] tempString = charset.Split(' ');
            if (tempString[0] == "" || tempString[0] == null)
            {
                return false;
            }
            else if (tempString[0] != "Charset")
            {
                return false;
            }
            if (tempString[1] == "" || tempString[1] == null)
            {
                return false;
            }
            else if (tempString[1] != "\"WindowsLatin1\"")
            {
                return false;
            }
            return true;
        }

        private bool validateVersion(string version)
        {
            if (version == null || version == "") { return false; }
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

                case ("validateColumns"):
                    return validateColumns(validateString);

                default: return true;
            }
        }

        private bool validateColumns(string columns)
        {
            string[] tempString = columns.Split(' ');
            if (!IsNum(tempString[1])) { return false; }
            return true;
        }

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



        public bool validationFileFomart(string filePath, string fileFormat)
        {
            if (filePath == "" || filePath == null || fileFormat == "" || fileFormat == null)
            {
                return false;
            }
            FileInfo fileinfo = new FileInfo(filePath);
            if (fileinfo.Extension != fileFormat)
            {
                return false;
            }
            return true;
        }

        public bool hasSufficientDiskSpace(String drivePath)
        {
            DriveInfo drive = new DriveInfo(drivePath);
            if (drive.AvailableFreeSpace / (1024 * 1024) < MINIMUM_AMT_OF_DISKSPACE)
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
            try
            {
                DirectorySecurity dirsecurity = directory.GetAccessControl();
            }
            catch
            {
                return false;
            }
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
            FileInfo fileInfo = new FileInfo(fileURL);
            try
            {
                FileSecurity filesecurity = fileInfo.GetAccessControl();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool fileExists(String fileURL)
        {
            if (File.Exists(fileURL) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool urlLengthIsValid(String fileURL)
        {
            char[] tempString = fileURL.ToCharArray();
            int length = 0;
            foreach (char c in tempString)
            {
                length++;
            }
            if (length >= 248)
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
            try
            {
                myConn.Open();
            }
            catch { return false; }
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

        public bool validateMidFormat(String midURL)
        {
            FileInfo fileinfo = new FileInfo(midURL);
            if (fileinfo.Extension.ToString() != ".mid")
            {
                return false;
            }
            return true;
        }

        public bool validateMifFormat(String mifURL)
        {
            string validationSection = "Header";
            StreamReader fileReader = new StreamReader(mifURL);
            string fileData = "";
            //lineNum  is plus 1 after read one line. So the final lineNum is the actual line number.
            //But in the processing, it is (actual line number) -1.
            Int32 lineNum = 0;
            Int32 tempLineNum = 0;
            int columnsNum = 0; //actual columns number
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

                        if (tempString[0] == "Columns" || tempString[0] == "columns")
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
                        if (lineNum == tempLineNum + columnsNum) { validationSection = "Data"; }
                    }
                }
                else if (validationSection == "Data")
                {

                    if (fileData != "Data" && fileData != "data")
                    {
                        return false;
                    }
                    fileData = null;
                }
                if (fileData != null)
                {
                    lineNum++;
                }
            }
            fileReader.Close();
            return true;
        }

        public bool validateMapInfoFormat(String mifURL, String midURL)
        {
            string mifFileData = "";
            string midFileData = "";
            string delimiter = "";
            int columnsNum = 0;
            int midEntryNum = 0;
            StreamReader mifFileReader = new StreamReader(mifURL);
            while (mifFileData != null)
            {
                mifFileData = mifFileReader.ReadLine();
                if (mifFileData != "" && mifFileData != null)
                {
                    string[] tempString_MifFile = mifFileData.Split(' ');
                    if (tempString_MifFile.Length == 2)
                    {
                        if (tempString_MifFile[0] == "Delimiter")
                        {
                            string[] tempString_Delimiter = tempString_MifFile[1].Split('"');
                            delimiter = tempString_Delimiter[1];
                        }
                        if (tempString_MifFile[0] == "Columns" || tempString_MifFile[0] == "columns")
                        {
                            if (!IsNum(tempString_MifFile[1]))
                            {
                                return false;
                            }
                            else
                            {
                                columnsNum = int.Parse(tempString_MifFile[1]);
                            }
                        }
                    }
                }
            }
            mifFileReader.Close();
            StreamReader midFileReader = new StreamReader(midURL);
            while (midFileData != null)
            {
                midFileData = midFileReader.ReadLine();
                if (midFileData != null && midFileData != "")
                {
                    string[] tempString_MidFileData = midFileData.Split(delimiter.ToCharArray());
                    if (tempString_MidFileData.Length != columnsNum)
                    {
                        return false;
                    }
                }
                midEntryNum++;
            }
            midFileReader.Close();
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
            XmlNamespaceManager nameSpace = new XmlNamespaceManager(kmldoc.NameTable);
            nameSpace.AddNamespace("kml_xmlns", "http://earth.google.com/kml/2.0");
            XmlElement kml = kmldoc.DocumentElement;
            //read the node content (need to add namespane prefix front of node. "kml_xmlns")
            kml.SelectSingleNode("kml_xmlns:kml", nameSpace);
            XmlElement xeKML = (XmlElement)kml;
            XmlNodeList xnl = xeKML.ChildNodes;
            //check Ducument element;
            if (xeKML.Name != "kml")
            {
                return false;
            }
            if (xnl == null) { return false; }
            return true;
        }

      

    }

}//END Validation Class

