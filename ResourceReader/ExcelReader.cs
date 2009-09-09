using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace ReadExcelData
{
    class ExcelReader : IExcelReader
    {
        private const int numFirstPref = 6;
        private ArrayList colList = new ArrayList();
        private DataTable socialTable = new DataTable("SocialTable");
        // stores first preference party names
        private ArrayList firstPrefParties = new ArrayList();
        // stores TPP
        private ArrayList tppNames = new ArrayList();

        // Reads excel file from the specified path. List of column names, table data, 
        // first preferences party names and TPP names are stored.
        // pre: path is not empty string and exists.
        // post: An excel file has read and data is stored.
        public ExcelReader(String path)
        {
            string partyName;

            string connectionString = "Provider=Microsoft.Jet.OleDb.4.0;" + 
                                      "data source=" + path + "; Extended Properties=Excel 8.0;";

            // Select using a Worksheet name
            string selectString = "SELECT * FROM [2004 Election$]";
            string partyString = "SELECT * FROM Parties";  

            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand cmdElection = new OleDbCommand(selectString, con);
            OleDbCommand cmdParty = new OleDbCommand(partyString, con);

            try
            {
                con.Open();

                OleDbDataAdapter electionAdapter = new OleDbDataAdapter();
                OleDbDataAdapter partyAdapter = new OleDbDataAdapter();

                electionAdapter.SelectCommand = cmdElection;
                partyAdapter.SelectCommand = cmdParty;

                DataSet dsElection = new DataSet();
                DataSet dsParty = new DataSet();
                // fill data in a data set
                electionAdapter.Fill(dsElection);
                partyAdapter.Fill(dsParty);


                //stores party names
                int num = 0;
                foreach (DataColumn partyCol in dsParty.Tables[0].Columns)
                {
                    if (num < numFirstPref)
                    {
                        firstPrefParties.Add(partyCol);
                        num++;
                    }
                    else
                    {
                        tppNames.Add(partyCol);
                        num++;
                    }
                }

                //retrieve column names
                foreach (DataTable table in dsElection.Tables)
                {
                    int rowCount = table.Rows.Count;
                    int columnCount = table.Columns.Count;

                    //get column names. If column name is %First preferences and %TPP, append it on a party name
                    foreach (DataColumn column in table.Columns)
                    {
                        if (!column.ColumnName.StartsWith("F"))
                        {
                            if (column.ColumnName.Contains("First"))
                            {
                                foreach (DataColumn party in firstPrefParties)
                                {
                                    partyName = string.Concat(party.ToString(), column.ColumnName.ToString());
                                    colList.Add(partyName);

                                }
                            }
                            else if (column.ColumnName.Contains("TPP"))
                            {
                                foreach (DataColumn party in tppNames)
                                {
                                    // if a number contains at the end of column name, remove it.
                                    if (party.ToString().EndsWith("1"))
                                    {
                                        int numIndex = party.ToString().IndexOf('1');
                                        partyName = string.Concat(party.ToString().Remove(numIndex), column.ColumnName.ToString());
                                        colList.Add(partyName);
                                    }
                                    else
                                    {
                                        partyName = string.Concat(party.ToString(), column.ColumnName.ToString());
                                        colList.Add(partyName);
                                    }
                                }
                            }
                            else
                                colList.Add(column.ColumnName);
                        }
                    }

                    
                    //type for the DataColumns
                    Type typString = typeof(String);
                    // create column
                    foreach (String colName in getColList())
                    {
                        DataColumn col = new DataColumn(colName, typString);
                        socialTable.Columns.Add(col);
                    }

                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow dr = socialTable.NewRow();

                        for (int column = 0; column < columnCount; column++)
                        {
                            dr[column] = table.Rows[i][column];
                        }
                        //check the data is not empty
                        if (!(dr[0].ToString().Equals("")))
                        {
                            socialTable.Rows.Add(dr);
                        }
                    }
                }


            }
            catch (OleDbException oleEx)
            {
                Console.WriteLine(oleEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        // pre: true
        // post: Returns list of column names
        public ArrayList getColList()
        {
            return colList;
        }

        // pre: true
        // post: Returns a sociological table
        public DataTable getSocialTable()
        {
            return socialTable;
        }

        // pre: true
        // post: Returns first preferences party names
        public ArrayList getFirstPrefParties()
        {
            return firstPrefParties;
        }

        // pre: true
        // post: Returns TPP party names
        public ArrayList getTPPNames()
        {
            return tppNames;
        }
    }
}
