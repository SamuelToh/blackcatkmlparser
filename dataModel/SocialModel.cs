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
        //private List<String> socioCols;
        
        private IResourceReader reader;

        //Constructor
        public SocialModel()
        {
            //socioCols = new List<String>();
        }


        //Overload constructor
        //Pre: colList, table, firstPrefParties and tpp are not null
        //Post: The SocialModel object is populated with the data contained in the Excel file
        public SocialModel(IResourceReader reader)
        {
            //this.reader = new ResourceReader();
            this.reader = reader;
            //this.socioCols = new List<string>();
        }

        // pre: true
        // post: returns the sociological column names
        // TODO: currently this is hard-coded to return only one name, for simplicity and because there is only one option
        public List<String> getColumnNames()
        {/*
            foreach (DataColumn col in reader.getSocialTable().Columns) 
            {
                socioCols.Add(col.ToString());
            }
         */ 
            List<String> socioCols = new List<string>(1);
            socioCols.Add(reader.getSocialTable().Columns[0].ColumnName);
            return socioCols;
        }


        // The winner party for the selected electorate is calculated by 
        //  . If a party in the first preferrences vote > 50.0, this party is winner.
        //  . Else highest in TPP is a winner party.
        // pre: electorate is not empty string and also is not null
        // post: returns a winning party name of specified electorate. If specified electorate could not
        //       find, returns an empty string.
        public string getSeatWinner(string electorate)
        {
            string winnerParty ="";
            String percent;
            double dbPercent;
            int percentIndex;

            DataTable table= reader.getSocialTable();
            //DataRow[] currentRows = table.Select(null, null, DataViewRowState.CurrentRows);
            DataRowCollection currentRows = table.Rows;
            DataColumnCollection columnCollection = table.Columns;

            foreach (DataRow row in currentRows)
            {
                // check matching electorate
                if (row["Division"].Equals(electorate))
                {
                    foreach (DataColumn column in columnCollection)
                    {
                        if (column.ColumnName.Contains("First"))
                        {
                            percent = row[column].ToString();
                            dbPercent = Convert.ToDouble(percent);
                            //find out any votes are >50 in the first preferences
                            if (dbPercent > 50)
                            {
                                percentIndex = column.ColumnName.IndexOf('%');
                                winnerParty = column.ColumnName.Remove(percentIndex).ToString();
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
                                winnerParty = column.ColumnName.Remove(percentIndex).ToString();
                                break;
                            }
                        }
                    }
                }

            }

            return winnerParty;
        }

        // This method retrieves selected column data from the sociological data table
        // pre: tblSocialData is not null and selectedColName is not empty string
        // post: Returns the selected sociological column data
        public List<String> getSocioColumnData(String selectedColName)
        {
            List<String> socDataList = new List<String>();

            DataRow[] currentRows = reader.getSocialTable().Select(
                             null, null, DataViewRowState.CurrentRows);

            foreach (DataRow row in currentRows)
            {
                socDataList.Add(row[selectedColName].ToString());
            }
            return socDataList;
         
        }
    }
}
