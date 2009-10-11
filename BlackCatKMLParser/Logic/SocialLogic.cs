using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class SocialLogic : ISocialLogic
    {
        private ISocialReader sr;
        private const string marginal = "Marginal Seat";
        private const string moderately = "Moderately safe";
        private const string safe = "Safe";
        private const string vSafe = "Very Safe";
        private const string rockSolid = "Rock Solid";

        // Constructor
        public SocialLogic()
        {
            sr = new SocialReader();
        }

        // Calculate which party won the seat using data in the database, in accordance 
        // with the procedures set down in Appendices 1, 2 and 3 of the Requirements 
        // Specification.
        // If isMainDisplay is true, then the user wants their KML file to display this data as 
        // the colour coding. Otherwise they only want to see this data in the pop up box.
        // Does not return anything, just manipulates the inputted model.
        // Pre:model is not null and isMainDisplay is not null;
        // Post: Seat winner data has been added to the model according to whether 
        // isMainDisplay is true or false.
        public void CalculateSeatWinners(GeoModel model, Boolean isMainDisplay)
        {
            string winnerParty = "";
            String[] regions = model.RegionIdentifiers;

            for (int i = 0; i < regions.Length; i++)
            {
                FederalElectorateData feData = sr.GetFederalResults(regions[i]);

                if (feData.FirstPref_ALP_Percent.HasValue && feData.FirstPref_DEM_Percent.HasValue &&
                    feData.FirstPref_GRN_Percent.HasValue && feData.FirstPref_LP_Percent.HasValue &&
                    feData.FirstPref_NP_Percent.HasValue && feData.FirstPref_OTH_Percent.HasValue &&
                    feData.ALP_Votes.HasValue && feData.LP_Votes.HasValue)
                    //calculate winner party
                    winnerParty = calculateWinner(feData);

                // **store winner party in the geoModel
            }
        }

        // Calculates how safe each Federal seat is using data in the database, in accordance 
        // with the procedures set down in Appendices 1, 2 and 3 of the Requirements 
        // Specification.
        // If isMainDisplay is true, then the user wants their KML file to display this data as 
        // the colour coding. Otherwise they only want to see this data in the pop up box.
        // Does not return anything, just manipulates the inputted model.
        // Pre: Model is not null and isMainDisplay is not null.
        // Post: Seat safety data has been added to the model according to whether 
        // isMainDisplay is true or false.
        public void CalculateSeatSafety(GeoModel model, Boolean isMainDisplay)
        {
            float currMargin = 0;
            float prevWonFact = 0;
            float stateFact = 0;
            float safety = 0;
            string seatSafety = "";
            String[] regions = model.RegionIdentifiers;

            for (int i = 0; i < regions.Length; i++)
            {
                FederalElectorateData feData = sr.GetFederalResults(regions[i]);
                // check the winner party is not empty string
                if (!feData.FirstPref_SeatWinner.Equals(""))
                {
                    currMargin = getCurrMargin(feData);
                }
                // find previously won factor
                prevWonFact = getPrevWonFactor(feData);
                // find state impact factor
                stateFact = getStateImpactFact(feData);
                //calculate seat safety
                safety = (float)Math.Round(currMargin * prevWonFact * stateFact / 10000, 2);

                seatSafety = findSeatSafety(safety);
                //** store the seat safety to geoModel
            }
        }

        // Check if the regions in the GeoModel can be found in database. This prevent the 
        // user from trying to add sociological data to files that the database does not have 
        // data for.
        // Returns true if there is data in the database for this geographical data set, otherwise 
        // returns false.
        // Pre: model is not null
        // Post: True has been returned if sociological data for this data set can be found, else 
        // false has been returned.
        public Boolean CanMatchSociologicalData(GeoModel model)
        {
            Boolean hasMatched = false;
            String[] regions = model.RegionIdentifiers;
            List<String> fedElectorates = sr.GetFederalElectorateNames();

            //search the data
            for (int i = 0; i < regions.Length; i++)
            {
                foreach (String electorate in fedElectorates)
                {
                    if (regions[i].Equals(electorate))
                    {
                        hasMatched = true;
                        break;
                    }
                }
            }

            return hasMatched;
        }

        // Finds out the current margin for the specified federal electorate data
        // pre: feData is not null.
        // post: Returns current percentage of the winner party.
        private float getCurrMargin(FederalElectorateData feData)
        {
            float currMargin = 0;

            switch (feData.FirstPref_SeatWinner)
            {
                case "ALP": if (feData.FirstPref_ALP_Percent.HasValue)
                        currMargin = feData.FirstPref_ALP_Percent.Value;
                    break;

                case "LIB": if (feData.FirstPref_LP_Percent.HasValue)
                        currMargin = feData.FirstPref_LP_Percent.Value;
                    break;

                case "NPA": if (feData.FirstPref_NP_Percent.HasValue)
                        currMargin = feData.FirstPref_NP_Percent.Value;
                    break;

                case "DEM": if (feData.FirstPref_DEM_Percent.HasValue)
                        currMargin = feData.FirstPref_DEM_Percent.Value;
                    break;

                case "GRN": if (feData.FirstPref_GRN_Percent.HasValue)
                        currMargin = feData.FirstPref_GRN_Percent.Value;
                    break;

                default: if (feData.FirstPref_OTH_Percent.HasValue)
                        currMargin = feData.FirstPref_OTH_Percent.Value;
                    break;
            }

            return currMargin;
        }

        // Calculates the previous won factor for the specified federal electrate data.
        // This is used to calculate the seat safety.
        // pre: feData is not null.
        // post: Returns calculated previous won factor.
        private float getPrevWonFactor(FederalElectorateData feData)
        {
            float wonFactor = 0;
            int year = 0;
            int alpVote = 0;
            int lnpVote = 0;
            int tppMargin = 0;
            float increasedMargin = 0;

            //check whether the data does not contain null
            year = validIntData(feData.HeldSince);
            alpVote = validIntData(feData.ALP_Votes);
            lnpVote = validIntData(feData.LP_Votes);
            //hold 2 consecutive elections
            if (year <= 2001 && year >= 1998)
            {
                tppMargin = getMargin(alpVote, lnpVote);

                // increase 5%
                increasedMargin = (float)Convert.ToDecimal(tppMargin * 1.05);
                wonFactor = (float)Math.Round(100 * increasedMargin / (alpVote + lnpVote), 2);

            }
            //hold more than 4 consecutive elections
            else if (year <= 1996 && year > 0)
            {
                tppMargin = getMargin(alpVote, lnpVote);

                // increase 10%
                increasedMargin = (float)Convert.ToDecimal(tppMargin * 1.1);
                wonFactor = (float)Math.Round(100 * increasedMargin / (alpVote + lnpVote), 2);
            }
            // no changes in TPP margin
            else
            {
                tppMargin = getMargin(alpVote, lnpVote);

                wonFactor = (float)Math.Round((double)100 * tppMargin / (alpVote + lnpVote), 2);
            }

            return wonFactor;
        }

        // Calculates the state impact factor for the specified federal electorate data.
        // This is used to calculate the seat safety.
        // pre: feData is not null.
        // post: Returns the calculated state impact factor.
        private float getStateImpactFact(FederalElectorateData feData)
        {
            float stateFactor = 0;
            //retrieve the state seats where the federal electorate has matched
            List<String> stateElectorates = sr.GetStateSeats(feData.FederalElectorateName);

            int alpVote = validIntData(feData.ALP_Votes);
            int lnpVote = validIntData(feData.LP_Votes);
            float margin = getMargin(alpVote, lnpVote);

            foreach (String stateE in stateElectorates)
            {
                StateElectorateData seData = sr.GetStateResults(stateE);

                // check tpp party for the state is not empty string
                if (!seData.TPP_WinnerParty.Equals(""))
                {
                    if (feData.FirstPref_SeatWinner.Equals(seData.TPP_WinnerParty))
                    {
                        //margin increases 2%
                        margin = (float)Math.Round(margin * 1.02, 2);
                    }
                    // check similar party 
                    else if (feData.FirstPref_SeatWinner.Equals("LIB") && seData.TPP_WinnerParty.Equals("NPA"))
                    {
                        //margin increases 2%
                        margin = (float)Math.Round(margin * 1.02, 2);
                    }
                    // check similar party 
                    else if (feData.FirstPref_SeatWinner.Equals("NPA") && seData.TPP_WinnerParty.Equals("LIB"))
                    {
                        //margin increases 2%
                        margin = (float)Math.Round(margin * 1.02, 2);
                    }
                    //margin decrease 4%
                    else
                    {
                        margin = (float)Math.Round(margin * 0.96, 2);
                    }
                }
            }

            stateFactor = (float)Math.Round(100 * margin / (alpVote + lnpVote), 2);

            return stateFactor;
        }

        // Calculates the margin 
        // pre: alpVote and lpVote are not 0.
        // post: Returns the margin of TPP votes
        private int getMargin(int alpVote, int lpVote)
        {
            int margin;

            if (alpVote > lpVote)
                margin = alpVote - lpVote;
            else
                margin = lpVote - alpVote;

            return margin;
        }

        // Finds out the seat safety(Marginal seat, Moderately safe, safe, very safe or Rock solid)
        // by safety rate.
        // pre: safety is not negative value.
        // post: Returns the seat safety.
        private string findSeatSafety(float safety)
        {
            string seatSafety;

            if (safety <= 5)
            {
                seatSafety = marginal;
            }
            else if (safety > 5 && safety <= 10)
            {
                seatSafety = moderately;
            }
            else if (safety > 10 && safety <= 15)
            {
                seatSafety = safe;
            }
            else if (safety > 15 && safety <= 25)
            {
                seatSafety = vSafe;
            }
            else
            {
                seatSafety = rockSolid;
            }

            return seatSafety;
        }

        // Check whether the float value does not contain null
        private float validFloatData(float? val)
        {
            if (val.HasValue)
                return val.Value;
            else
            {
                Console.WriteLine("The data contains a null value.");
                return -1;
            }
        }

        // Check whether the integer value does not contain null
        private int validIntData(int? val)
        {
            if (val.HasValue)
                return val.Value;
            else
            {
                Console.WriteLine("The data contains a null value.");
                return -1;
            }
        }

        // Calculates the winner party by :
        //      If the party has 50% of the primary vote, this party is a winner.
        //      Otherwise, the winner will be the party with the highest two party preferred (TPP) vote.
        // pre: feData is not null.
        // post: Returns the winner party name
        private string calculateWinner(FederalElectorateData feData)
        {
            string winParty = "";

            if (feData.FirstPref_ALP_Percent > 50)
            {
                winParty = "ALP";
            }
            else if (feData.FirstPref_DEM_Percent > 50)
            {
                winParty = "DEM";
            }
            else if (feData.FirstPref_GRN_Percent > 50)
            {
                winParty = "GRN";
            }
            else if (feData.FirstPref_LP_Percent > 50)
            {
                winParty = "LIB";
            }
            else if (feData.FirstPref_NP_Percent > 50)
            {
                winParty = "NPA";
            }
            else if (feData.FirstPref_OTH_Percent > 50)
            {
                winParty = "OTH";
            }
            //winner is highest TPP votes
            else
            {
                // check the data is not null
                if (feData.ALP_Votes.HasValue && feData.LP_Votes.HasValue)
                {
                    if (feData.ALP_Votes > feData.LP_Votes)
                        winParty = "ALP";
                    else
                        winParty = "LNP";
                }
            }

            return winParty;

        }
    }
}
