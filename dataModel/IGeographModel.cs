using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BlackCat
{
    public interface IGeographModel{
    
        //Populates the geographical data model from the reader objects for both the .mid and .mif files and returns it.
        //Pre: midReader and mifReader both must not be null
        //Post: The GeographModel the method is called on is populated with the data contained in the MapInfo files.
        void buildGeographModel(StreamReader midReader, StreamReader mifReader);




















        //Adds a region to the list.
        //Pre : Collection exists, regName is not null, regCoord is not null and colorCode is not null
        //Post : A region has been added to the collection
        //void addGeographicData(string regName, string regCoord, string colorCode);

        //Count the number of regions in the collection
        //Pre: Collection exists
        //Post: An integer representing the number of regions in the collection is returned.
        //int countGeographicRegions();

        //Return a string of X and Y coordinates for the object at the given index
        //Pre: Index must be less than the collection count, but at least zero.
        //Post: A string of X and Y coordinates is returned (partitioned with a delimiter of | )
        //String getGeographicCoord(int geoIndex);

        //Returns the GeographicRegion located at the given index – can be used to iterate
        //Pre: Index must be less than the collection count, but at least zero.
        //Post: The GeographicRegion at geoIndex is returned.
        //GeographicRegion getGeograpicRegion(int geoIndex);

        //*NOTE: Geographic Region () needs to be changed, because you can never return a private class object.
        // Maybe Get Region.RegionName? or RegionCode ?

    }
}
