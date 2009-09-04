using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    //Data container of KML objec
    public class KMLDataModel : IKMLDataModel
    {
        private List<PartyStyle> styles;
        //A list to represent each region's coordinates
        private List<Region> regions;

        KMLDataModel() { } //Constructor

        private class PartyStyle
        {
            //Constructor
            PartyStyle() { }

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
               //Properties 
               //String partyColor{get; set;};
               //String partyName{get; set};
            }

            //Nested class for defining a region object. Contains its name, X and Y coordinates and designated style.
            private class Region
            {
                //Constructor
                Region() { }

                //Properties 
               // string regionName{get; set;};
               // string coordinateX{get; set;};
               // string coordinateY{get; set;};
	           // string regionStyle{get; set;};
            }


            //Populates a KMLModel from an existing KML file (through a StreamReader object) 
            //Pre : reader is not null 
            //Post : The KMLModel object has been initialised.
            public void buildKMLModel(StreamReader reader) { }


            //Overload of the above method, used when there is a GeograpModel as input instead of a StreamReader
            //Pre : GeographModel is not null 
            //Post : The KMLModel object has been initialised.
            public void buildKMLModel(GeographModel model) { }

            //Return the list of styles within the object
        	//Pre: Styles collection exists
            //Post: A list of styles is returned.
            public List<String> Styles { get; set; } 

            //Returns a string of X coordinates for the region at a particular index
            //Pre : partyIndex must be less than the collection count, but at least zero            
            //Post : A string of X coordinates (with delimiters in it if there is more than one point) is returned.
            public String getRegionCoordX(int regionIndex) {
                return "";
            }

            //Returns a string of Y coordinates for the region at a particular index
            //Pre : partyIndex must be less than the collection count, but at least zero
            //Post : A string of Y coordinates (with delimiters in it if there is more than one   point) is returned.
            public String getRegionCoordY(int regionIndex) {
                return "";
            }

            //Returns the name of the region located at regionIndex
            //Pre : partyIndex must be less than the collection count, but at least zero
            //Post : The name of the region located at regionIndex is returned
            public String getRegionName(int regionIndex) {
                return "";
            }

            //Set the style of the region located at regionIndex
            //Pre : regionIndex must be less than the collection count, but at least zero and styleName must not be the empty string
            //Post : The style of the region at regionIndex in the model has been set to styleName
            public String setRegionStyle(int regionIndex, String styleName) {
                return "";
            }

            //Returns the style of the region located at regionIndex
            //Pre : regionIndex must be less than the collection count, but at least zero
            //Post : The name of the style associated with the region located at regionIndex is returned
            public String getRegionStyle(int regionIndex) {
                return "";
            }

            //Write the KMLModel object to a stream associated with writer
            //Pre : True
            //Post : The current object has been written to the stream writer
            public void write(StreamWriter writer) { }

            //Returns the total number of regions
            //Pre : True
            //Post : The total number of regions is returned.
            public int countRegions() {
                return 0;
            }

            //Returns the list of data fields that data linking can be performed on
            //Pre: True
            //Post : The list of fields that can be used to link to sociological data has been returned.
            public List<String> getDataFieldNames() { 
                return new List<String>();
            }

    }
}