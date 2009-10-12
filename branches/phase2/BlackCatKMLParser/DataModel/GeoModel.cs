using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;

namespace BlackCat
{
    public class GeoModel : IGeoModel
    {

        ILog log = LogManager.GetLogger(typeof(GeoModel));


        private List<Style> styles = new List<Style>();
        private List<Region> regions = new List<Region>();
        private List<string> dataFields = new List<string>();

        long totalSize;
        long currRead = 1;        


        public bool BuildGeoModel(IGeoReader reader, ProgressBar progressBar)
        {
            this.regions  = reader.ReadRegions(progressBar);
            if (regions.Count > 0)
                this.dataFields = regions[0].DataNames;
            //TODO: return is meaningless
            return true;
        }

        public Region[] Regions
        {
            get {return this.regions.ToArray();}
            //added for testing
            set {this.regions = new List<Region>(value);}
        }

        //first or secondary display also calls this
        public void SetRegionStyle
               (string regionIdentifier, Style style)
        {

            foreach (Region r in regions)

                if (r.RegionName == regionIdentifier)
                {
                    r.RegionStyle = style;
                    break;
                }

            //ChkModelStyle(style);
        }

        public void SetRegionSecondaryData
            (bool seatWinnerIsMainDisplay, string regionIdentifier, string data)
        {
            StringBuilder key =  new StringBuilder();
            //string key = "";

            if (!seatWinnerIsMainDisplay)
                key.Append("Seat safety :"); 
                
            else
                
                key.Append("Party winner : ");

            foreach (Region r in regions)
            {
                if (r.RegionName == regionIdentifier)
                {
                    r.DataNames.Add(key.ToString());
                    r.AddDataValue(data);
                    break;
                }
            }

        }

        public Style GetRegionStyle(String regionIdentifier)
        {
            foreach (Region r in regions)
                if (r.RegionName == regionIdentifier)
                    return r.RegionStyle;
            return null;
        }

        public Style[] Styles
        {
            //get { return this.styles.ToArray(); }
            get
            {
                styles = new List<Style>();
                foreach (Region r in regions)
                    ChkModelStyle(r.RegionStyle);
                return styles.ToArray();
            }
        }

        private void ChkModelStyle(Style style)
        {
            if (style != null)
            {
                if (this.styles.Count < 1)
                    styles.Add(style);
                else
                {
                    bool hasStyle = false;

                    foreach (Style s in styles)
                        if (s.StyleName == style.StyleName)
                            hasStyle = true;

                    if (!hasStyle)
                        this.styles.Add(style);
                }
            }
        }

        public String[] RegionIdentifiers
        {
            get
            {
                string[] ident = new string[regions.Count];

                for (int i = 0; i < ident.Length; i++)
                    ident[i] = regions[i].RegionName;

                return ident;
            }
        }

        //Added for testing purposes
        public String[] GetRegionCoordinates(String regionIdentifier)
        {
            for (int i = 0; i < regions.Count; i++)
                if (regions[i].RegionName == regionIdentifier)
                    return regions[i].Coordinates.ToArray();
            return null;
        }
        
        /*
         *Added on 14 Sep //do we need this?
         */
        public List<String> DataFieldNames
        {
            get { return dataFields; }
            set { dataFields = value; }
        }

/*
        private string getColorCode(string status)
        {
            string code = "#00000000";

            switch (status)
            {
                case "Marginal Seat":
                    {
                        code = "A0E3BEE2";
                        break;
                    }

                case "Moderately Safe":
                    {
                        code = "A0BC588A";
                        break;
                    }

                case "Safe":
                    {
                        code = "A0BD0080";
                        break;
                    }

                case "Very Safe":
                    {
                        code = "A05B084A";
                        break;
                    }

                case "Rock Solid":
                    {
                        code = "A01E0A1A";
                        break;
                    }

            }

            return code;
        }
*/
    }
}


