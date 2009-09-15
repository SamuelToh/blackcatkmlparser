using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using log4net;

namespace BlackCat
{
    public class DataMerger : IDataMerger
    {
        private ILog log;

        // constructor
        public DataMerger() 
        {
            log = LogManager.GetLogger(this.ToString());
        }

        // Tests whether geographical model and sociological model can link by the selected columns.
        // Returns true if kml model and sociological model can link. Otherwise, returns false.
        // Pre: kmlModel and socialTbl are not null and kmlColumnName and socColumnName are not empty strings
        // Post: Returns true if column names are matched, otherwise returns false. 
        public bool canLink(IGeoModel geoM, String geoColumnName, ISocialModel socialM, String socColumnName)
        {
            log.Debug("Start canLink");
            List<String> socioDataList = socialM.getSocioColumnData(socColumnName);
            String[] geoDataList = geoM.GetRegionIdentifiers();
            bool matched = false;
            int i = 0;
            int j = 0;
                        
            int geoDataCount = geoDataList.Length;
            int socialDataCount = socioDataList.Count;

            // search matching column names
            while (j < socialDataCount && i < geoDataCount)
            {
                if (geoDataList[i].Trim().ToLower().Equals(socioDataList[j].Trim().ToLower()))
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
        public int linkDataModels(IGeoModel geoM, string geoColName, ISocialModel socialM, string socialColName)
        {
            log.Debug("Set up for linking");
            int success = 0;
            // create a hash table that stores key:region name and value:winner party
            Hashtable hTable = new Hashtable();
            string winnerParty = "";
            Style style;

            // get all regions in the model and store them in the array
            log.Debug("Getting region ids");
            string[] regions = geoM.GetRegionIdentifiers();

            foreach (string region in regions)
            {
                log.Debug("Determining winner");
                winnerParty = socialM.getSeatWinner(region);
                // if there is no winner party in the specified region, link is unsuccessful
                if (winnerParty.Equals(""))
                {
                    log.Debug("Winner not found for " + region);
                    success = 1;
                }
                // add the region name and winner party in the hash table
                hTable.Add(region, winnerParty);

                // determines the colour
                if (winnerParty.Equals("ALP"))
                {
                    //colour is a hex notation(ABGR)
                    style = new Style("700000ff", "Red");
                }
                else if (winnerParty.Equals("LP") || winnerParty.Equals("LNP") || winnerParty.Equals("NP"))
                {
                    style = new Style("70ff0000", "Blue");
                }
                else if (winnerParty.Equals("GRN"))
                {
                    style = new Style("7000ff00", "Green");
                }
                else
                {
                    style = new Style("7000ffff", "Yellow");
                }
                log.Debug("Setting region style for " + region + " to " + style.StyleName);
                geoM.SetRegionStyle(region, style);
            }

            return success;
        }

    }
}
