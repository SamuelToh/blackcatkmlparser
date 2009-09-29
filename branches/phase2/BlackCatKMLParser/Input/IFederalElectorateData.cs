using System;
namespace BlackCat
{
    interface IFederalElectorateData
    {
        double FirstPref_ALP { get; set; }
        double FirstPref_DEM { get; set; }
        double FirstPref_LP { get; set; }
        double FirstPref_NP { get; set; }
        string FirstPrefWinningParty { get; set; }
        int HeldSince { get; set; }
        string Name { get; set; }
        int PreviouslyHeld { get; set; }
        string TwoPartyPrefWinningParty { get; set; }
    }
}
