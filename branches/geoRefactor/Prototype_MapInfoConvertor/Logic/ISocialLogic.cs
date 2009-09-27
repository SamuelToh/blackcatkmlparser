using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface ISocialLogic
    {
        void calculateSeatWinners(GeoModel model, Boolean isMainDisplay);

        void calculateSeatSafety(GeoModel model, Boolean isMainDisplay);
        
        Boolean canMatchSociologicalData(GeoModel model);
    }
}
