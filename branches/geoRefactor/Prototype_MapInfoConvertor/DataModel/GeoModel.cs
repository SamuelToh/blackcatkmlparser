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
    public partial class GeoModel : IGeoModel
    {

        ILog log = LogManager.GetLogger(typeof(GeoModel));


        private List<Style> styles = new List<Style>();
        private List<Region> regions = new List<Region>();
        private List<string> dataFields = new List<string>();

        long totalSize;
        long currRead = 1;        

        public bool BuildGeoModel(IGeoReader reader, ProgressBar progressBar)
        {
            this.regions = reader.ReadRegions(progressBar);
            //TODO: return is meaningless
            return true;
        }

        public Region[] GetRegions()
        {
            return this.regions.ToArray();
        }

        public void SetRegionStyle
               (string regionIdentifier, Style style)
        {

            foreach (Region r in regions)

                if (r.regionName == regionIdentifier)
                {
                    r.regionStyle = style;
                    break;
                }

            chkModelStyle(style);
        }

        public Style GetRegionStyle(String regionIdentifier)
        {
            foreach (Region r in regions)
                if (r.regionName == regionIdentifier)
                    return r.regionStyle;
            return null;
        }

        public Style[] GetStyles()
        {
            return this.styles.ToArray();
        }

        private void chkModelStyle(Style style)
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

        public String[] GetRegionIdentifiers()
        {
            string[] ident =
                new string[regions.Count];

            for (int i = 0; i < ident.Length; i++)
                ident[i] = regions[i].regionName;

            return ident;
        }

        //Added for testing purposes
        public String[] GetRegionCoordinates(String regionIdentifier)
        {
            for (int i = 0; i < regions.Count; i++)
                if (regions[i].regionName == regionIdentifier)
                    return regions[i].coordinates.ToArray();
            return null;
        }
        
        /*
         *Added on 14 Sep 
         */
        public List<String> DataFieldNames()
        {
            return dataFields;
        }

    }
}


