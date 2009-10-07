using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class Region : IRegion
    {
        public const string POLYGON_CODE = "POLYGON";
        public const string PLINE_CODE = "PLINE";
        public const string LINE_CODE = "LINE";
        public const string POINT_CODE = "POINT";

        private List<string> coordinates = new List<string>();
        private List<string> dataValues = new List<string>();
        private string regionName;
        private string regionType;
        private Style regionStyle;

        //Constructor
        public Region(string regionName, List<String> coordinates, string regionType)
        {
            this.regionName = regionName;
            this.coordinates = coordinates;
            this.regionType = regionType;
        }

        public Region(string regionName, string coordinates)
        {
            this.regionName = regionName;
            this.coordinates.Add(coordinates);
        }

        public Region(string regionType)
        {
            this.RegionType = regionType;
        }

        public Region() 
        { 
        }

        //Properties 
        public string RegionName
        {
            get { return regionName; }
            set { regionName = value; }
        }


        public Style RegionStyle
        {
            get { return regionStyle; }
            set { regionStyle = value; }
        }

        public string RegionType
        {
            get { return regionType; }
            set { regionType = value; }
        }

        public List<String> Coordinates 
        {
            get { return coordinates; }
            set { this.coordinates = value; }
        }

        public String GetDataValue(int index)
        {
            if(index < this.coordinates.Count)
                return this.coordinates[index];
            return null;
        }

        public void AddDataValue(String data)
        {
            this.coordinates.Add(data);
        }
    }
}
