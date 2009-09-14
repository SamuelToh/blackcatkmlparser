using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace BlackCat
{
    interface ISocialModel
    {
        // pre: true
        // post: returns the sociological column names
        List<String> getColumnNames();


        //This method is used to update social data table. The winner party is calculated by 
        //  . If a party in the first preferrences vote > 50.0, this party is winner.
        //  . Else highest in TPP is a winner party.
        //pre: true
        //post: social data table includes winner party in each row.
        //void calculateWinners();

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
