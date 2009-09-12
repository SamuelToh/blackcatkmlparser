using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class DataMerger //: IDataMerger
    {
        // constructor
        public DataMerger()
        {
        }

        // Tests whether kml model and sociological model can link by the selected columns.
        // Returns true if kml model and sociological model can link. Otherwise, returns false.
        // Pre: kmlModel and socialTbl are not null and kmlColumnName and socColumnName are not empty strings
        // Post: Returns true if column names are matched, otherwise returns false. 
        public bool canLink(SocialModel socialM, String socColumnName)
        {
            ArrayList socioDataList;
            ArrayList kmlDataList;
            bool matched = false;
            int i = 0;
            int j = 0;

            //gets sociological data that is selected sociological column name
            socioDataList = new ArrayList(socialM.getSocioColumnData(socColumnName));
            //gets kml data that is selected kml column name  **********
            kmlDataList = new ArrayList();

            //- for testing only
            kmlDataList.Add("Blair");
            kmlDataList.Add("Bonner");
            kmlDataList.Add("Bowman");
            kmlDataList.Add("Brisbane");
            //kmlDataList.Add("123");
            kmlDataList.Add("Capricornia");

            //-----------------------------
            int kmlDataCount = kmlDataList.Count;
            int socialDataCount = socioDataList.Count;

            // search matching column names
            while (j < socialDataCount && i < kmlDataCount)
            {
                if (kmlDataList[i].Equals(socioDataList[j]))
                {
                    // found matched data
                    matched = true;
                    i++;
                    j = 0;
                }
                else
                {
                    // has not found matched data
                    j++;
                    matched = false;
                }

            }

            return matched;
        }

        // Links a Geographical Model with the data in a SocialModel using the columns the user has indicated should be used. 
        // pre: geoM and socialM are not null. geoColName and socialColName are not empty string.
        // post: Returns an integer denoting success(0) or failure (1).
        public int linkDataModels(GeoModel geoM, SocialModel socialM, string geoColName, string socialColName)
        {
            int success = 0;
            // create a hash table that stores key:region name and value:winner party
            Hashtable hTable = new Hashtable();
            string winnerParty = "";
            Style style;

            // get all regions in the model and store them in the array
            string[] regions = geoM.getRegionIdentifiers();

            foreach (string region in regions)
            {
                winnerParty = socialM.getSeatWinner(region);
                // if there is no winner party in the specified region, link is unsuccessful
                if (winnerParty.Equals(""))
                {
                    success = 1;
                }
                // add the region name and winner party in the hash table
                hTable.Add(region, winnerParty);

                // determines the colour
                if (winnerParty.Equals("ALP"))
                {
                    //colour is a hex notation(ABGR)
                    style = new Style("ff000000ff", "Red");
                }
                else if (winnerParty.Equals("LP") || winnerParty.Equals("LNP") || winnerParty.Equals("NP"))
                {
                    style = new Style("ffff0000", "Blue");
                }
                else if (winnerParty.Equals("GRN"))
                {
                    style = new Style("ff00ff00", "Green");
                }
                else
                {
                    style = new Style("ff00ffff", "Yellow");
                }

                geoM.setRegionStyle(region, style);
            }

            return success;
        }
    

    }
}
