using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;

namespace BlackCat
{
    public class SocialModel : ISocialModel
    {

        //Contains a list of column names 
        private ArrayList socioCols;

        //origical data table
        private DataTable tblSocialData;
        // data table including winner party
        private DataTable updatedTbl;

        //stores party names
        private ArrayList firstPrefParties;
        private ArrayList tppNames;

        //Constructor
        public SocialModel()
        {
            socioCols = new ArrayList();
            tblSocialData = new DataTable();
            updatedTbl = new DataTable();
            firstPrefParties = new ArrayList();
            tppNames = new ArrayList();
        }


        //Overload constructor
        //Pre: colList, table, firstPrefParties and tpp are not null
        //Post: The SocialModel object is populated with the data contained in the Excel file
        public SocialModel(ArrayList colList, DataTable table, ArrayList firstPrefParties, ArrayList tpp)
        {
            this.socioCols = new ArrayList(colList);
            this.tblSocialData = table.Copy();
            this.updatedTbl = table.Copy();
            this.firstPrefParties = new ArrayList(firstPrefParties);
            this.tppNames = new ArrayList(tpp);
        }

        // pre: true
        // post: returns the sociological column names
        public ArrayList getColumnNames()
        {
            return socioCols;
        }

        // pre: true
        // post: returns the original sociological table
        public DataTable getDataTbl()
        {
            return tblSocialData;
        }

        // pre: true
        // post: returns list of first preferences party names
        public ArrayList getFirstPrefParties()
        {
            return this.firstPrefParties;
        }

        // pre: true
        // post: returns list of TPP names
        public ArrayList getTPPNames()
        {
            return tppNames;
        }

        //This method is used to update social data table. The winner party is calculated by 
        //  . If a party in the first preferrences vote > 50.0, this party is winner.
        //  . Else highest in TPP is a winner party.
        //pre: true
        //post: social data table includes winner party in each row.
        public void calculateWinners()
        {
            // stores winning party name
            String winPartyName;

            String percent;
            double dbPercent;
            int percentIndex;

            //type for the DataColumns
            Type typString = typeof(String);

            // create a column and add column data
            DataColumn col = new DataColumn("winnerParty", typString);
            updatedTbl.Columns.Add(col);

            DataRow[] currentRows = updatedTbl.Select(
                             null, null, DataViewRowState.CurrentRows);

            foreach (DataRow row in currentRows)
            {
                foreach (DataColumn column in updatedTbl.Columns)
                {
                    if (column.ColumnName.Contains("First"))
                    {
                        percent = row[column].ToString();
                        dbPercent = Convert.ToDouble(percent);
                        //find out any votes are >50 in the first preferences
                        if (dbPercent > 50)
                        {
                            percentIndex = column.ColumnName.IndexOf('%');
                            winPartyName = column.ColumnName.Remove(percentIndex);
                            //add party name in the winningParty column
                            row["winnerParty"] = winPartyName;
                            break;
                        }
                    }
                    else if (column.ColumnName.Contains("TPP"))
                    {
                        percent = row[column].ToString();
                        dbPercent = Convert.ToDouble(percent);
                        //find out which votes are >50 in the two party preferred votes
                        if (dbPercent > 50)
                        {
                            percentIndex = column.ColumnName.IndexOf('%');
                            winPartyName = column.ColumnName.Remove(percentIndex);
                            //add party name in the winningParty column
                            row["winnerParty"] = winPartyName;
                            break;
                        }
                    }
                }

            }
        }

        // pre: true
        // post: returns a sociological table including winner parties
        public DataTable getUpdatedTable()
        {
            return updatedTbl;
        }

        // pre: electorate is not empty string and also is not null
        // post: returns a winning party name of specified electorate. If specified electorate could not
        //       find, returns an empty string.
        public string getSeatWinner(string electorate)
        {
            string winnerParty = "";

            DataRow[] currentRows = updatedTbl.Select(
                             null, null, DataViewRowState.CurrentRows);

            foreach (DataRow row in currentRows)
            {
                // check matching electorate
                if (row["Division"].Equals(electorate))
                {
                    winnerParty = row["winnerParty"].ToString();
                }
            }

            return winnerParty;
        }

        // This method retrieves selected column data from the sociological data table
        // pre: tblSocialData is not null and selectedColName is not empty string
        // post: Returns the selected sociological column data
        public ArrayList getSocioColumnData(String selectedColName)
        {
            ArrayList socDataList = new ArrayList();

            DataRow[] currentRows = tblSocialData.Select(
                             null, null, DataViewRowState.CurrentRows);
            foreach (DataRow row in currentRows)
            {
                socDataList.Add(row[selectedColName]);

            }
            return socDataList;
        }
    }
}
