using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class District : IDistrict
    {
        private String districtName;
        private List<String> regionNames = new List<String>();
        
        public String DistrictName
        {
            get { return districtName; }
            set { districtName = value; }
        }

        public List<String> RegionNames
        {
            get { return regionNames; }
            set { regionNames = value; }
        }

        public bool Contains(String regionName)
        {
            return regionNames.Contains(regionName);
        }
    }
}
