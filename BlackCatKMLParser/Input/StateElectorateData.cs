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

        public String StateElectorateName
        {
            get { return name; }
            set { name = value; }
        }

        public String TPP_WinnerParty
        {
            get { return twoPartyPrefWinningParty; }
            set { twoPartyPrefWinningParty = value; }
        }

        public void clearAll()
        {
        }
    }
}
