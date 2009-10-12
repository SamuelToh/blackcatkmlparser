using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace BlackCat
{
    public class SocialLogic : ISocialLogic
    {
        ILog log = LogManager.GetLogger(typeof(SocialLogic));

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
            Region[] regions = model.Regions;
            Style colStyle;

            for (int i = 0; i < regions.Length; i++)
            {
                FederalElectorateData feData = sr.GetFederalResults(regions[i].RegionName);

                if (feData.FirstPref_ALP_Percent.HasValue && feData.FirstPref_DEM_Percent.HasValue &&
                    feData.FirstPref_GRN_Percent.HasValue && feData.FirstPref_LP_Percent.HasValue &&
                    feData.FirstPref_NP_Percent.HasValue && feData.FirstPref_OTH_Percent.HasValue &&
                    feData.ALP_Votes.HasValue && feData.LP_Votes.HasValue)
                {
                    //calculate winner party
                    winnerParty = calculateWinner(feData);
                }
                else
                {
                    log.Debug("Required value is null");
                }
                log.Debug("Adding \"seat winner\" data value for " + regions[i].RegionName + " = " + winnerParty);
                //regions[i].DataNames.Add("Seat winner");
                // store winner party in the geoModel
                //regions[i].AddDataValue(winnerParty);
                log.Debug(regions[i].GetDataValue(regions[i].DataNames.Count - 1));

                //set the region style if the user wants to display colours 
                if (isMainDisplay)
                {
                    colStyle = winnerPartyStyle(winnerParty);
                    model.SetRegionStyle(regions[i].RegionName, colStyle);
                }
                else
                {
                    //Added by sam
                    model.SetRegionSecondaryData(true, regions[i].RegionName, winnerParty);
                }
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
            int prevWonFact = 0;
            float stateFact = 0;
            float safety = 0;
            string seatSafety = "";
            Region[] regions = model.Regions;
            Style colStyle;

            for (int i = 0; i < regions.Length; i++)
            {
                FederalElectorateData feData = sr.GetFederalResults(regions[i].RegionName);
                // check the winner party is not empty string
                if (!feData.FirstPref_SeatWinner.Equals(""))
                {
                    currMargin = (float)Math.Round((double)100 * Math.Abs(validIntData(feData.ALP_Votes) - validIntData(feData.LP_Votes)) /
                        (validIntData(feData.ALP_Votes) + validIntData(feData.LP_Votes)), 2);
                }
                // find previously won factor
                prevWonFact = getPrevWonFactor(feData);
                // find state impact factor
                stateFact = getStateImpactFact(feData);
                //calculate seat safety
                safety = currMargin + prevWonFact + stateFact;

                seatSafety = findSeatSafety(safety);
                //store the seat safety to geoModel
                //regions[i].DataNames.Add("Seat safety");
                //regions[i].AddDataValue(seatSafety);

                //set the region style if the user wants to display colours 
                if (isMainDisplay)
                {
                    colStyle = seatSafetyStyle(seatSafety);
                    model.SetRegionStyle(regions[i].RegionName, colStyle);
                }
                else
                {
                    //added by sam to fix the bug where program doesnt stores the secondary data
                    model.SetRegionSecondaryData(false, regions[i].RegionName, seatSafety);
                }
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
            String[] regions = model.RegionIdentifiers;
            List<String> fedElectorates = sr.GetFederalElectorateNames();

            //Minimize all string to lower case
            for (int i = 0; i < fedElectorates.Count; i++)
                fedElectorates[i] = fedElectorates[i].ToLower();

            //search the data 
            foreach (String r in regions)
            {
                //this shouldnt be case sensitive
                if (!fedElectorates.Contains(r.ToLower()))
                    return false;

            }
            return true;
        }

        private Style winnerPartyStyle(string winParty)
        {
            Style style;
 
            if (winParty.Equals("ALP"))
            {
                //colour is a hex notation(ABGR)
                style = new Style("7d0000ff", "Red");
            }
            else if (winParty.Equals("LIB") || winParty.Equals("LNP") || winParty.Equals("NPA"))
            {
                style = new Style("7dff0000", "Blue");
            }
            else if (winParty.Equals("GRN"))
            {
                style = new Style("7d00ff00", "Green");
            }
            else
            {
                style = new Style("7d00ffff", "Yellow");
            }

            return style;
        }

        private Style seatSafetyStyle(string seatSafety)
        {
            Style safetyStyle;

            if (seatSafety.Equals(marginal))
            {
                safetyStyle = new Style("A0E3BEE2", "Very Light Purple");
            }
            else if (seatSafety.Equals(moderately))
            {
                safetyStyle = new Style("A0BC588A", "Light Purple");
            }
            else if (seatSafety.Equals(safe))
            {
                safetyStyle = new Style("A0BD0080", "Purple");
            }
            else if (seatSafety.Equals(vSafe))
            {
                safetyStyle = new Style("A05B084A", "Dark Purple");
            }
            else
            {
                safetyStyle = new Style("A01E0A1A", "Very Dark Purple");
            }

            return safetyStyle;
        }

        // Calculates the previous won factor for the specified federal electrate data.
        // This is used to calculate the seat safety.
        // pre: feData is not null.
        // post: Returns calculated previous won factor.
        private int getPrevWonFactor(FederalElectorateData feData)
        {
            int wonFactor = 0;
            int year = 0;

            //check whether the data does not contain null
            year = validIntData(feData.HeldSince);
            //hold 2 consecutive elections
            if (year <= 2001 && year >= 1998)
            {
                //tppMargin = getMargin(alpVote, lnpVote);

                // increase 5%
                //increasedMargin = (float)Convert.ToDecimal(tppMargin * 1.05);
                //wonFactor = (float)Math.Round(100 * increasedMargin / (alpVote + lnpVote), 2);
                wonFactor = 5;
            }
            //hold more than 4 consecutive elections
            else if (year <= 1996 && year > 0)
            {
                //tppMargin = getMargin(alpVote, lnpVote);

                // increase 10%
                //increasedMargin = (float)Convert.ToDecimal(tppMargin * 1.1);
                //wonFactor = (float)Math.Round(100 * increasedMargin / (alpVote + lnpVote), 2);
                wonFactor = 10;
            }
            // no changes in TPP margin
            //else
            //{
            //    wonFactor = 0;
            //}

            return wonFactor;
        }

        // Calculates the state impact factor for the specified federal electorate data.
        // This is used to calculate the seat safety.
        // pre: feData is not null.
        // post: Returns the calculated state impact factor.
        private float getStateImpactFact(FederalElectorateData feData)
        {
            int stateFactorSum = 0;
            int countStateSeats = 0;
            float aveStateFactor = 0;
            //retrieve the state seats where the federal electorate has matched
            List<String> stateElectorates = sr.GetStateSeats(feData.FederalElectorateName);


            foreach (String stateE in stateElectorates)
            {
                StateElectorateData seData = sr.GetStateResults(stateE);

                // check tpp party for the state is not empty string
                if (seData.TPP_WinnerParty != null && !seData.TPP_WinnerParty.Equals(""))
                {
                    //same party or similar party
                    if (feData.FirstPref_SeatWinner.Equals(seData.TPP_WinnerParty) ||
                        feData.FirstPref_SeatWinner.Equals("LIB") && seData.TPP_WinnerParty.Equals("NPA") ||
                        feData.FirstPref_SeatWinner.Equals("NPA") && seData.TPP_WinnerParty.Equals("LIB"))
                    {
                        stateFactorSum += 2;
                        countStateSeats++;
                    }
                    //opposite party. margin decrease 4%
                    else
                    {
                        stateFactorSum -= 4;
                        countStateSeats++;
                    }
                }
            }

            aveStateFactor = stateFactorSum / countStateSeats;

            return aveStateFactor;
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

        //Added for testing purposes
        public ISocialReader Reader
        {
            get { return this.sr; }
            set { this.sr = value; }
        }
    }
}
