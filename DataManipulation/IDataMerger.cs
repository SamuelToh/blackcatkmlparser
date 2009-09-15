using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace BlackCat
{
    public interface IDataMerger
    {
        // Tests whether kml model and sociological model can link by the selected columns.
        // The models are considered able to link if every value in the GeoModel column has a matching value
        // in the SocialModel column.
        //
        // Pre: kmlModel and socialTbl are not null and kmlColumnName and socColumnName are not empty strings
        // Post: Returns true if column names are matched, otherwise returns false. 
        // Return: true if kml model and sociological model can link. Otherwise, returns false.
        bool canLink(IGeoModel geoM, String geoColumnName, ISocialModel socialM, String socColumnName);


        // Links a Geographical Model with the data in a SocialModel using the columns the user has indicated should be used. 
        // pre: geoM and socialM are not null. geoColName and socialColName are not empty string.
        // post: Returns an integer denoting success(0) or failure (1).
        int linkDataModels(IGeoModel geoM, ISocialModel socialM, string geoColName, string socialColName);

    }
}
