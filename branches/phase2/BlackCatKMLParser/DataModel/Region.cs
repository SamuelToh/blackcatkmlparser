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
        private List<string> dataNames = new List<string>();
        private List<string> dataValues = new List<string>();
        private string regionName;
        private string regionType;
        private Style regionStyle;
        private Category regionCategory;

        //Constructor
        public Region(string regionName, List<String> coordinates, string regionType)
        {
            this.regionName = regionName;
            this.Coordinates = coordinates;
            this.regionType = regionType;
        }

        public Region(string regionName, string coordinates)
        {
            this.regionName = regionName;
            this.Coordinates.Add(coordinates);
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

        //Added 10th October
        public Category RegionCategory
        {
            get { return regionCategory; }
            set { regionCategory = value; }
        }

        public List<String> Coordinates 
        {
            get { return coordinates; }
            set { this.Coordinates = value; }
        }

        public List<String> DataNames 
        {
            get { return this.dataNames.ToList<string>(); }
            set { this.dataNames = value.ToList<string>(); }
        }

        public String GetDataValue(int index)
        {
            //previous 
            //if(index < this.Coordinates.Count)
            //return this.Coordinates[index];
            //return null;

            //Changed by sam 10 october 2009 10AM
            //we should be returning data values instead of coordinate value
            if (index < this.dataValues.Count)
                return this.dataValues[index];
            return null;
        }

        public void AddDataValue(String data)
        {
            //previous
            //this.Coordinates.Add(data);

            //Changed by Sam 10 October 2009 10AM
            //data values should be added to dataValue liste instead.
            dataValues.Add(data);
        }
    }
}
