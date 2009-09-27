using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class Region
    {
        public const string POLYGON_CODE = "POLYGON";
        public const string PLINE_CODE = "PLINE";
        public const string LINE_CODE = "LINE";
        public const string POINT_CODE = "POINT";

        public List<string> coordinates
                                = new List<string>();
        private string
                        m_regName,
                        m_regType;

        private Style m_regStyle;

        //Constructor
        public Region(string regionName,
                string coordinates,
                string regType)
        {
            this.regionName = regionName;
            this.coordinates.Add(coordinates);
            this.regionType = regionType;
        }
        public Region(string regionName,
                string coordinates)
        {
            this.regionName = regionName;
            this.coordinates.Add(coordinates);
        }

        public Region(string regionType)
        {
            this.regionType = regionType;
        }

        public Region() { }

        //Properties 
        public string regionName
        {
            get { return m_regName; }
            set { m_regName = value; }
        }


        public Style regionStyle
        {
            get { return m_regStyle; }
            set { m_regStyle = value; }
        }

        public string regionType
        {
            get { return m_regType; }
            set { m_regType = value; }
        }     

    }
}
