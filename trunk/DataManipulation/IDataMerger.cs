using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface IDataMerger
    {
        // Tests whether kml model and sociological model can link by the selected columns.
        // Returns true if kml model and sociological model can link. Otherwise, returns false.
        // Pre: kmlModel and socialTbl are not null and kmlColumnName and socColumnName are not empty strings
        // Post: Returns true if column names are matched, otherwise returns false. 
        bool canLink(SocialModel socialM, String socColumnName);


        //Links a KMLModel with the data in a SocialModel using the columns the user has indicated should be used. Extends the inputted KMLModel to contain the desired sociological data. Returns an integer denoting success(0) or failure (1).
        int linkDataModels(GeoModel geoM, SocialModel socialM, string geoColName, string socialColName);
    }
}
