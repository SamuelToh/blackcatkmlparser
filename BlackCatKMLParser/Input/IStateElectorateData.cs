using System;
namespace BlackCat
{
    public interface IStateElectorateData
    {
        string Name { get; set; }
        string TwoPartyPrefWinningParty { get; set; }
    }
}
