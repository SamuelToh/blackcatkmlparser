using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface ISocialLogic
    {
        void CalculateSeatWinners(GeoModel model, Boolean isMainDisplay);

        void CalculateSeatSafety(GeoModel model, Boolean isMainDisplay);
        
        Boolean CanMatchSociologicalData(GeoModel model);
    }
}
