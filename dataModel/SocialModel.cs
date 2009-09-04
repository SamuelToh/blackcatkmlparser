using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{

    //This class stores the [Party] and [states] information of a country
    public class SocialModel : ISocialModel
    {

        //Contains a list of party names 
        private List<String> partyNames;

        //A list of seats with their associated data. 
        private List<SeatElectionData> electionInfo;

        //The data field names for performing the matching operation. 
        private List<String> dataFields;


        private class SeatElectionData
        {
            //Constructor.
            SeatElectionData(string seatCode, List<string> Parties){}

            //The name of the seat 
            string seatCde;
            //The party who won the seat
            string winnerParty;
            //A hash table containing 

            //Properties
            //TO BE IMPLEMENTED YOURSELF

            /* SAMPLE PROPERTY METHOD
             * 
             public string myRegionName
                {
                    get { return this.regionName; }
                    set { this.regionName = value; }
                }
            */

            //Properties.
            //TO BE IMPLEMENTED YOURSELF
            //string stateCode{get; set}
            //string winner{get; set}
            
	        //Add a party and its vote count to the list of participating parties, if it is not already there.
	        //Pre: partyName is not the empty string
	        //Post: partyName has been added to the list, if it is not already there.
            public void addParticipatingParty(string partyName, double votePercent) { }
        }

        
        //Return the hash table containing the party names and vote counts.
	    //Pre: True
	    //Post: participatingParties has been returned.
        public SortedDictionary<string, string> getParticipatingParties() {
            return new SortedDictionary<string, string>();
        }

        //*NOTE: I have replaced HashTable with SortedDictionary (something similiar) because C# do not have HashTable




        //Populates the SocialModel object from a StreamReader object tied to a sociological data set.
        //Pre: reader is not null
        //Post: The SocialModel object the method is called on is populated with the data contained in the Excel file
        public void buildSocialModel(StreamReader reader) {
            //return this;
        }

        //Get the list of data field names associated with the original data file.
        //Pre: True
        //Post: The list of data field names has been returned.
        public List<String> DataFields() {
            return new List<String>();
        } //property method : Remove if needed

        //Returns a list of participating political parties
        //Pre: True
        //Post: A list of political parties has been returned
        public List<String> PartyNames() {
            return new List<string>();
        } //property method : Remove if needed

        //Returns a list of seat names
        //Pre: True
        //Post: A list of seat names has been returned
        public List<String> getSeatNames() {
            return new List<string>();
        }

        //Returns the voting details for the electorate of seatCode.
        //Pre: seatCode is not the empty string
        //Post: A list of seat names has been returned
        public List<String> getSeatElectionInfo(String seatCode) {
            return new List<string>();
        }

        //Set the name of the party that was the winner of seatCode
        //Pre: seatCode is not null and winner is not null
        //Post: The name of the party that was the winner of seatCode has been set
        public void setSeatWinner(String seatCode, String winner) { }

        //Return the name of the party that was the winner of seatCode
        //Pre: seatCode is not null
        //Post: The name of the party that was the winner of seatCode is returned
        public String getSeatWinner(string seatCode) {
            return "";
        }

    }
}
