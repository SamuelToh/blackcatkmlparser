using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    public interface ISocialModel
    {


        //Populates the SocialModel object from a StreamReader object tied to a sociological data set.
        //Pre: reader is not null
        //Post: The SocialModel object the method is called on is populated with the data contained in the Excel file
        void buildSocialModel(StreamReader reader);

        //Get the list of data field names associated with the original data file.
        //Pre: True
        //Post: The list of data field names has been returned.
        List<String> DataFields(); //property method : Remove if needed

        //Returns a list of participating political parties
        //Pre: True
        //Post: A list of political parties has been returned
        List<String> PartyNames(); //property method : Remove if needed

        //Returns a list of seat names
        //Pre: True
        //Post: A list of seat names has been returned
        List<String> getSeatNames();

        //Returns the voting details for the electorate of seatCode.
        //Pre: seatCode is not the empty string
        //Post: A list of seat names has been returned
        List<String> getSeatElectionInfo(String seatCode);

        //Set the name of the party that was the winner of seatCode
        //Pre: seatCode is not null and winner is not null
        //Post: The name of the party that was the winner of seatCode has been set
        void setSeatWinner(String seatCode, String winner);

        //Return the name of the party that was the winner of seatCode
        //Pre: seatCode is not null
        //Post: The name of the party that was the winner of seatCode is returned
        String getSeatWinner(string seatCode);


    }
}
