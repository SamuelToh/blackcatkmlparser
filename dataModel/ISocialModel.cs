using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;

namespace BlackCat
{
    public interface ISocialModel
    {
        // pre: true
        // post: returns the sociological column names
        ArrayList getColumnNames();

        // pre: true
        // post: returns the original sociological table
        //DataTable getDataTbl();
        //We don't want the table - it's too much information outside the class

        // pre: true
        // post: returns list of first preferences party names
        //ArrayList getFirstPrefParties();
        // Is this being used? If not, remove it

        // pre: true
        // post: returns list of TPP names
        //ArrayList getTPPNames();
        // Is this being used? If not, remove it


        //This method is used to update social data table. The winner party is calculated by 
        //  . If a party in the first preferrences vote > 50.0, this party is winner.
        //  . Else highest in TPP is a winner party.
        //pre: true
        //post: social data table includes winner party in each row.
        // void calculateWinners();
        // I think we should just calculate the winners when asked (getSeatWinner) on a one-by-one basis

        // pre: true
        // post: returns a sociological table including winner parties
        //DataTable getUpdatedTable();
        //We don't want the table - it's too much information outside the class

        // pre: electorate is not empty string and also is not null
        // post: returns a winning party name of specified electorate. If specified electorate could not
        //       find, returns an empty string.
        string getSeatWinner(string electorate);

        // This method retrieves selected column data from the sociological data table
        // pre: tblSocialData is not null and selectedColName is not empty string
        // post: Returns the selected sociological column data
        ArrayList getSocioColumnData(String selectedColName);
    }
}
