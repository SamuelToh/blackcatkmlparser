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
            //TODO: return is meaningless
            return true;
        }

        public Region[] Regions
        {
            get {return this.regions.ToArray();}
        }

        public void SetRegionStyle
               (string regionIdentifier, Style style)
        {

            foreach (Region r in regions)

                if (r.RegionName == regionIdentifier)
                {
                    r.RegionStyle = style;
                    break;
                }

            ChkModelStyle(style);
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
            get { return this.styles.ToArray(); }
        }

        private void ChkModelStyle(Style style)
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

    }
}


