using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class StateElectorateData : IStateElectorateData 
    {
        private String name;
        private String twoPartyPrefWinningParty;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String TwoPartyPrefWinningParty
        {
            get { return twoPartyPrefWinningParty; }
            set { twoPartyPrefWinningParty = value; }
        }
    }
}
