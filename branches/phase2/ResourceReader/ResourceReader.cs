using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace BlackCat
{
    public class ResourceReader : IResourceReader
    {
        private ILog log;

        //private const int numFirstPref = 6;
        //private ArrayList colList = new ArrayList();
        private DataTable socialTable = new DataTable("SocialTable");

        private static string DIVISION = "Division";
        private static string STATE = "State";
        private static string ALP_FIRST_PREF = "ALP%First Preferences";
        private static string LP_FIRST_PREF = "LP%First Preferences";
        private static string NP_FIRST_PREF = "NP%First Preferences";
        private static string DEM_FIRST_PREF = "DEM%First Preferences";
        private static string GRN_FIRST_PREF = "GRN%First Preferences";
        private static string OTH_FIRST_PREF = "OTH%First Preferences";
        private static string LNP_TPP = "LNP%TPP";
        private static string ALP_TPP = "ALP%TPP";

        /*public ResourceReader() 
        {
        }*/

        // Reads excel file from the specified path. List of column names, table data, 
        // first preferences party names and TPP names are stored.
        // pre: path is not empty string and exists.
        // post: An excel file has read and data is stored.
        public ResourceReader(String path, ProgressBar progress)
        { 
            log = LogManager.GetLogger(this.ToString());
            log.Debug("ResourceReader constructor");

            //Set up the DataTable columns
            socialTable.Columns.Add(DIVISION);
            socialTable.Columns.Add(STATE);
            socialTable.Columns.Add(ALP_FIRST_PREF);
            socialTable.Columns.Add(LP_FIRST_PREF);
            socialTable.Columns.Add(NP_FIRST_PREF);
            socialTable.Columns.Add(DEM_FIRST_PREF);
            socialTable.Columns.Add(GRN_FIRST_PREF);
            socialTable.Columns.Add(OTH_FIRST_PREF);
            socialTable.Columns.Add(LNP_TPP);
            socialTable.Columns.Add(ALP_TPP);

            //string partyName;

            string connectionString = "Provider=Microsoft.Jet.OleDb.4.0;" + 
                    "data source=" + path + "; Extended Properties=Excel 8.0;";

            // Select using a Worksheet name
            string selectString = "SELECT * FROM [2004 Election$]";
            //TODO: party? string partyString = "SELECT * FROM Parties";  

            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand cmdElection = new OleDbCommand(selectString, con);
            //TODO: party? OleDbCommand cmdParty = new OleDbCommand(partyString, con);
            
            try
            {
                log.Debug("Opening DB connection");
                con.Open();

                log.Debug("Creating adapter");
                OleDbDataAdapter electionAdapter = new OleDbDataAdapter();
                //TODO: party? OleDbDataAdapter partyAdapter = new OleDbDataAdapter();

                log.Debug("Setting command on adapter");
                electionAdapter.SelectCommand = cmdElection;
                //TODO: party? partyAdapter.SelectCommand = cmdParty;

                DataSet dsElection = new DataSet();
                //TODO: party? DataSet dsParty = new DataSet();
                log.Debug("fill data in the data set");
                electionAdapter.Fill(dsElection);
                //partyAdapter.Fill(dsParty);

                log.Debug("Copying rows from excel table");
                //Copy rows from excel table
                if (dsElection.Tables.Count > 0 && 
                    dsElection.Tables[0].Rows.Count > 1 &&
                    dsElection.Tables[0].Columns.Count == socialTable.Columns.Count)
                {
                    DataTable tempTable = dsElection.Tables[0];
                    log.Debug("tempTable has " + tempTable.Columns.Count + " columns");
                    log.Debug("socioTable has " + socialTable.Columns.Count + " columns");
                    //Copy rows, leaving out the first row (column names) and the last row (totals)
                    for (int rowCount = 1; rowCount < tempTable.Rows.Count -1; rowCount++)
                    {
                        DataRow row = socialTable.NewRow();
                        for (int columnCount = 0; columnCount < socialTable.Columns.Count; columnCount++)
                            row[columnCount] = tempTable.Rows[rowCount][columnCount];
                        socialTable.Rows.Add(row);
                    }
                }
                //stores party names
                //int num = 0;
                // stores first preference party names
                //ArrayList firstPrefParties = new ArrayList();
                // stores TPP
                //ArrayList tppNames = new ArrayList();

                /*TODO: party? foreach (DataColumn partyCol in dsParty.Tables[0].Columns)
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
                }*/
            
                //retrieve column names
                //log.Debug("Retrieving column names");

                /*
                foreach (DataTable table in dsElection.Tables)
                {
                    int rowCount = table.Rows.Count;
                    int columnCount = table.Columns.Count;

                    //get column names. If column name is %First preferences and %TPP, append it on a party name
                    foreach (DataColumn column in table.Columns)
                    {
                        log.Debug("Column - " + column.ColumnName);
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
                    foreach (String colName in colList)
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
                }*/


            }
            catch (OleDbException oleEx)
            {
                log.Debug("OleDbException occurred");
                log.Debug(oleEx.Message);
            }
            catch (Exception ex)
            {
                log.Debug("Exception occurred");
                log.Debug(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        // pre: true
        // post: Returns a sociological table
        public DataTable getSocialTable()
        {
            return socialTable;
        }
     }
}
