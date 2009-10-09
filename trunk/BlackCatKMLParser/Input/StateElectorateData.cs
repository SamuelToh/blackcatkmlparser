using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class StateElectorateData : IStateElectorateData 
    {
        private String stateElectorateName;
        private String tPP_Winner_Party;


        //Default constructor.

        public StateElectorateData()
        {
            stateElectorateName = null;
            tPP_Winner_Party = null;
        }

        // Property to get and set the value of the stateElectorateName attribute
        // Pre: True for getting and setting.
        // Post: The value of stateElectorateName has been returned for getting or the value of 
        // stateElectorateName has been set to the input value for setting.

        public String StateElectorateName
        {
            get
            {
                return stateElectorateName;
            }
            set
            {
                stateElectorateName = value;
            }
        }

        // Property to get and set the value of the tPP_WinnerParty attribute
        // Pre: True for getting and setting.
        // Post: The value of tPP_WinnerParty has been returned for getting or the value of 
        // tPP_WinnerParty has been set to the input value for setting.

        public String TPP_WinnerParty
        {
            get
            {
                return tPP_Winner_Party;
            }
            set
            {
                tPP_Winner_Party = value;
            }
        }

        // Resets all fields in the StateElectorateData object to null values.
        // No return value.
        // Pre: True.
        // Post: All attributes of the StateElectorateData object have been returned to null values.

        public void ClearAll()
        {
            stateElectorateName = null;
            tPP_Winner_Party = null;
        }

    }
}
