using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    //this class stores the [ region ] information found in both mid and mif files
    public class GeographModel : IGeographModel
    {
        //Contains the list of column names derived from the original .mid file
        private List<String> dataFieldNames;
        //The list of regions with their coordinates
        private List<GeographicRegion> electorates;

        private class GeographicRegion
        {
        
            //Name of the region
            private string regionName;
            //Contains the colour that the region will eventually be depicted with
            private string colorCode;
            //The X coordinate for the region.
             private string X;
            //The Y coordinate for the region.
            private string Y;

            GeographicRegion() { }

            //Properties
            //TO BE IMPLEMENTED YOURSELF

            /* SAMPLE PROPERTY METHOD
             * 
             public string myRegionName
                {
                    get { return this.regionName; }
                    set { this.regionName = value; }
                }
            */

            //string myRegionName{get; set};
            //string coordinateX{get; set};
            //string coordinateY{get; set};

        }

        //Populates the geographical data model from the reader objects for both the .mid and .mif files and returns it.
        //Pre: midReader and mifReader both must not be null
        //Post: The GeographModel the method is called on is populated with the data contained in the MapInfo files.
        public void buildGeographModel(StreamReader midReader, StreamReader mifReader) { }

        //Adds a region to the list.
        //Pre : Collection exists, regName is not null, regCoord is not null and colorCode is not null
        //Post : A region has been added to the collection
        public void addGeographicData(string regName, string regCoord, string colorCode) { }

        //Count the number of regions in the collection
        //Pre: Collection exists
        //Post: An integer representing the number of regions in the collection is returned.
        public int countGeographicRegions() {
            return 0;
        }

        //Return a string of X and Y coordinates for the object at the given index
        //Pre: Index must be less than the collection count, but at least zero.
        //Post: A string of X and Y coordinates is returned (partitioned with a delimiter of | )
        public String getGeographicCoord(int geoIndex) {
            return "";
        }

        //Returns the GeographicRegion located at the given index – can be used to iterate
        //Pre: Index must be less than the collection count, but at least zero.
        //Post: The GeographicRegion at geoIndex is returned.
        //public GeographicRegion getGeograpicRegion(int geoIndex) { }
    }
}
