using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface ISocialLogic
    {
        // Calculates how safe each Federal seat is using data in the database, in accordance 
        // with the procedures set down in Appendices 1, 2 and 3 of the Requirements 
        // Specification.
        // If isMainDisplay is true, then the user wants their KML file to display this data as 
        // the colour coding. Otherwise they only want to see this data in the pop up box.
        // Does not return anything, just manipulates the inputted model.
        // Pre: Model is not null and isMainDisplay is not null.
        // Post: Seat safety data has been added to the model according to whether 
        // isMainDisplay is true or false.

        void CalculateSeatSafety(GeoModel model, Boolean isMainDisplay);

        // Calculate which party won the seat using data in the database, in accordance 
        // with the procedures set down in Appendices 1, 2 and 3 of the Requirements 
        // Specification.
        // If isMainDisplay is true, then the user wants their KML file to display this data as // the colour coding. Otherwise they only want to see this data in the pop up box.
        // Does not return anything, just manipulates the inputted model.
        // Pre:model is not null and isMainDisplay is not null;
        // Post: Seat winner data has been added to the model according to whether 
        // isMainDisplay is true or false.

        void CalculateSeatWinners(GeoModel model, Boolean isMainDisplay);

        // Check if the regions in the GeoModel can be found in database. This prevent the 
        // user from trying to add sociological data to files that the database does not have 
        // data for.
        // Returns true if there is data in the database for this geographical data set, otherwise // returns false.
        // Pre: model is not null
        // Post: True has been returned if sociological data for this data set can be found, else // false has been returned.

        Boolean CanMatchSociologicalData(GeoModel model);    

    }
}
