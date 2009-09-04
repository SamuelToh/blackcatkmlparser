using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface IDataManipulator
    {
        //For each seat in the election data, calculates which party won the election and records this directly into the socData(a check will be marked on the
        //Pre: socData is not null
        //Post: socData is updated to contain the winning part for each seat.
        void CalculateWinners(SocialModel socData);
    }
}
