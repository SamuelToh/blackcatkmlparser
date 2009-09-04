using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{

    //This class contains all the business rules for calculating election winners.
    public class DataManipulator : IDataManipulator
    {
        //This method is used to update social data model (a check will be marked on the
        // party that wons the election for that particular region.
        //pre: socData != null
        //post: social model is updated
        public void CalculateWinners(SocialModel socData) { }

    }
}
