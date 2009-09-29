using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class FederalElectorateData : IFederalElectorateData
    {
        private String name;
        private double firstPref_ALP;
        private double firstPref_LP;
        private double firstPref_NP;
        private double firstPref_DEM;
        private String firstPrefWinningParty;
        private String twoPartyPrefWinningParty;
        private int heldSince;
        private int previouslyHeld;

        public double FirstPref_ALP
        {
            get { return firstPref_ALP; }
            set { firstPref_ALP = value; }
        }

        public double FirstPref_LP
        {
            get { return firstPref_LP; }
            set { firstPref_LP = value; }
        }

        public double FirstPref_NP
        {
            get { return firstPref_NP; }
            set { firstPref_NP = value; }
        }

        public double FirstPref_DEM
        {
            get { return firstPref_DEM; }
            set { firstPref_DEM = value; }
        }

        public String FirstPrefWinningParty
        {
            get { return firstPrefWinningParty; }
            set { firstPrefWinningParty = value; }
        }

        public String TwoPartyPrefWinningParty
        {
            get { return twoPartyPrefWinningParty; }
            set { twoPartyPrefWinningParty = value; }
        }

        public int HeldSince
        {
            get { return heldSince; }
            set { heldSince = value; }
        }

        public int PreviouslyHeld
        {
            get { return previouslyHeld; }
            set { previouslyHeld = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }


    }
}
