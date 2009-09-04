using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace BlackCat
    {
        public interface IKMLDataModel
        {


            //Populates a KMLModel from an existing KML file (through a StreamReader object) 
            //Pre : reader is not null 
            //Post : The KMLModel object has been initialised.
            void buildKMLModel(StreamReader reader);

            //Overload of the above method, used when there is a GeograpModel as input instead of a StreamReader
            //Pre : GeographModel is not null 
            //Post : The KMLModel object has been initialised.
            void buildKMLModel(GeographModel model);


            //Returns a string of X coordinates for the region at a particular index
            //Pre : partyIndex must be less than the collection count, but at least zero 
            //Post : A string of X coordinates (with delimiters in it if there is more than one point) is returned.
            String getRegionCoordX(int regionIndex);

            //Returns a string of Y coordinates for the region at a particular index
            //Pre : partyIndex must be less than the collection count, but at least zero
            //Post : A string of Y coordinates (with delimiters in it if there is more than one   point) is returned.
            String getRegionCoordY(int regionIndex);

            //Returns the name of the region located at regionIndex
            //Pre : partyIndex must be less than the collection count, but at least zero
            //Post : The name of the region located at regionIndex is returned
            String getRegionName(int regionIndex);

            //Set the style of the region located at regionIndex
            //Pre : regionIndex must be less than the collection count, but at least zero and styleName must not be the empty string
            //Post : The style of the region at regionIndex in the model has been set to styleName
            String setRegionStyle(int regionIndex, String styleName);

            //Returns the style of the region located at regionIndex
            //Pre : regionIndex must be less than the collection count, but at least zero
            //Post : The name of the style associated with the region located at regionIndex is returned
            String getRegionStyle(int regionIndex);

            //Write the KMLModel object to a stream associated with writer
            //Pre : True
            //Post : The current object has been written to the stream writer
            void write(StreamWriter writer);

            //Returns the total number of regions
            //Pre : True
            //Post : The total number of regions is returned.
            int countRegions();

            //Returns the list of data fields that data linking can be performed on
            //Pre: True
            //Post : The list of fields that can be used to link to sociological data has been returned.
            List<String> getDataFieldNames();

            }
}

    