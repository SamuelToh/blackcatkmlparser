using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface IDataMerger
    {
        //Constructor
        //DataMerger DataMerger();

        //Tests if kmlModel and socModel can be linked through kmlColumnName and socColumnName. Returns a boolean.
        //Pre: kmlModel and socModel are not null and kmlColumnName and socColumnName are not empty strings
        //Post: True. 
        bool canLink( KMLDataModel kmlModel, String kmlColumnName, SocialModel socModel, String socModelName);
        //Links a KMLModel with the data in a SocialModel using the columns the user has indicated should be used. Extends the inputted KMLModel to contain the desired sociological data. Returns an integer denoting success(0) or failure (1).
        int linkDataModels(KMLDataModel kmlModel, String kmlColumnName, SocialModel socModel, String socModelName);
    }
}
