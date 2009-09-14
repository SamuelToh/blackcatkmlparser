using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace BlackCat
{
    public class SocialModel : ISocialModel
    {

        //Contains a list of column names 
        //private ArrayList socioCols;
        private List<String> socioCols;
        
        private ResourceReader reader;

        //Constructor
        public SocialModel()
        {
            socioCols = new List<String>();
        }


        //Overload constructor
        //Pre: colList, table, firstPrefParties and tpp are not null
        //Post: The SocialModel object is populated with the data contained in the Excel file
        public SocialModel(ResourceReader reader)
        {
            this.reader = new ResourceReader();
            this.reader = reader;
            this.socioCols = new List<string>();
        }

        // pre: true
        // post: returns the sociological column names
        public List<String> getColumnNames()
        {
            foreach (DataColumn col in reader.getSocialTable().Columns) 
            {
                socioCols.Add(col.ToString());
            }
            return socioCols;
        }

        //This method is used to update social data table. The winner party is calculated by 
        //  . If a party in the first preferrences vote > 50.0, this party is winner.
        //  . Else highest in TPP is a winner party.
        //pre: true
        //post: social data table includes winner party in each row.
        private void calculateWinners()
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
            
            reader.getSocialTable().Columns.Add(col);

            DataRow[] currentRows = reader.getSocialTable().Select(
                             null, null, DataViewRowState.CurrentRows);

            foreach (DataRow row in currentRows)
            {
                foreach (DataColumn column in reader.getSocialTable().Columns)
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

        // pre: electorate is not empty string and also is not null
        // post: returns a winning party name of specified electorate. If specified electorate could not
        //       find, returns an empty string.
        public string getSeatWinner(string electorate)
        {
            string winnerParty ="";

            DataRow[] currentRows = reader.getSocialTable().Select(
                             null, null, DataViewRowState.CurrentRows);

            calculateWinners();

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

            DataRow[] currentRows = reader.getSocialTable().Select(
                             null, null, DataViewRowState.CurrentRows);

            foreach (DataRow row in currentRows)
            {
                socDataList.Add(row[selectedColName]);
            }
            return socDataList;
        }
    }
}
