using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public class FederalElectorateData : IFederalElectorateData
    {
        private int? aLP_Votes;
        private String federalElectorateName;
        private float? firstPref_ALP_Percent;
        private float? firstPref_DEM_Percent;
        private float? firstPref_GRN_Percent;
        private float? firstPref_LP_Percent;
        private float? firstPref_NP_Percent;
        private float? firstPref_OTH_Percent;
        private String firstPref_SeatWinner;
        private int? heldSince;
        private int? lP_Votes;

        //Default constructor.

        public FederalElectorateData()
        {
            aLP_Votes = null;
            federalElectorateName = null;
            firstPref_ALP_Percent = null;
            firstPref_DEM_Percent = null;
            firstPref_GRN_Percent = null;
            firstPref_LP_Percent = null;
            firstPref_NP_Percent = null;
            firstPref_NP_Percent = null;
            firstPref_OTH_Percent = null;
            firstPref_SeatWinner = null;
            heldSince = null;
            lP_Votes = null;
        }

        // Property to get and set the value of the aLP_Votes attribute
        // Pre: True for getting and setting.
        // Post: The value of aLP_Votes has been returned for getting or the value of 
        // aLP_Votes has been set to the input value for setting.

        public int? ALP_Votes
        {
            get
            {
                return aLP_Votes;
            }
            set
            {
                aLP_Votes = value;
            }
        }

        // Property to get and set the value of the federalElectorateName attribute
        // Pre: True for getting and setting.
        // Post: The value of federalElectorateName has been returned for getting or the value of 
        // federalElectorateName has been set to the input value for setting.

        public String FederalElectorateName
        {
            get
            {
                return federalElectorateName;
            }
            set
            {
                federalElectorateName = value;
            }
        }

        // Property to get and set the value of the firstPref_ALP_Percent attribute
        // Pre: True for getting and setting.
        // Post: The value of firstPref_ALP_Percent has been returned for getting or the value of 
        // firstPref_ALP_Percent has been set to the input value for setting.

        public float? FirstPref_ALP_Percent
        {
            get
            {
                return firstPref_ALP_Percent;
            }
            set
            {
                firstPref_ALP_Percent = value;
            }
        }

        // Property to get and set the value of the firstPref_DEM_Percent attribute
        // Pre: True for getting and setting.
        // Post: The value of firstPref_DEM_Percent has been returned for getting or the value of 
        // firstPref_DEM_Percent has been set to the input value for setting.

        public float? FirstPref_DEM_Percent
        {
            get
            {
                return firstPref_DEM_Percent;
            }
            set
            {
                firstPref_DEM_Percent = value;
            }
        }

        // Property to get and set the value of the firstPref_GRN_Percent attribute
        // Pre: True for getting and setting.
        // Post: The value of firstPref_GRN_Percent has been returned for getting or the value of 
        // firstPref_GRN_Percent has been set to the input value for setting.

        public float? FirstPref_GRN_Percent
        {
            get
            {
                return firstPref_GRN_Percent;
            }
            set
            {
                firstPref_GRN_Percent = value;
            }
        }

        // Property to get and set the value of the firstPref_LP_Percent attribute
        // Pre: True for getting and setting.
        // Post: The value of firstPref_LP_Percent has been returned for getting or the value of 
        // firstPref_LP_Percent has been set to the input value for setting.

        public float? FirstPref_LP_Percent
        {
            get
            {
                return firstPref_LP_Percent;
            }
            set
            {
                firstPref_LP_Percent = value;
            }
        }

        // Property to get and set the value of the firstPref_NP_Percent attribute
        // Pre: True for getting and setting.
        // Post: The value of firstPref_NP_Percent has been returned for getting or the value of 
        // firstPref_NP_Percent has been set to the input value for setting.

        public float? FirstPref_NP_Percent
        {
            get
            {
                return firstPref_NP_Percent;
            }
            set
            {
                firstPref_NP_Percent = value;
            }
        }

        // Property to get (but not set) the value of the firstPref_OTH_Percent attribute.
        // Returns the firstPref_OTH_Percent
        // Note: This value is not in the database, it is calculated as 100 - (sum of
        // all the other percentages).
        // Pre: True 
        // Post: The value of firstPref_OTH_Percent has been returned.

        public float? FirstPref_OTH_Percent
        {
            get
            {
                // If firstPref_OTH_Percent hasn't been set yet, set it.

                if (firstPref_OTH_Percent == null)
                {
                    // Check that all other party percentages are valid, i.e. non null 
                    // and if they are, calculate a value for firstPref_OTH_Percent, otherwise leave 
                    // invalid value in place. Note, there would likely only be a problem here if this
                    // value is being calcuated before fetching data from the database or if the data
                    // in the database is incomplete.

                    if (firstPref_ALP_Percent != null && firstPref_DEM_Percent != null && firstPref_GRN_Percent != null
                        && firstPref_LP_Percent != null && firstPref_NP_Percent != null)
                    {
                        firstPref_OTH_Percent = (float)Math.Round((double)(100 - firstPref_ALP_Percent - firstPref_DEM_Percent - firstPref_GRN_Percent
                            - firstPref_LP_Percent - firstPref_NP_Percent), 2);
                    }
                }

                // Now return whatever is in firstPref_OTH_Percent.

                return firstPref_OTH_Percent;
            }
        }

        // Property to get and set the value of the firstPref_SeatWinner attribute
        // The getter returns the firstPref_SeatWinner party, the setter returns nothing.
        // Pre: True for getting and setting.
        // Post: The value of firstPref_SeatWinner has been returned for getting or the value 
        // of firstPref_SeatWinner has been set to the input value for setting.

        public String FirstPref_SeatWinner
        {
            get
            {
                return firstPref_SeatWinner;
            }
            set
            {
                firstPref_SeatWinner = value;
            }
        }

        // Property to get and set the value of the heldSince attribute
        // Pre: True for getting and setting.
        // Post: The value of heldSince has been returned for getting or the value of 
        // heldSince has been set to the input value for setting.

        public int? HeldSince
        {
            get
            {
                return heldSince;
            }
            set
            {
                heldSince = value;
            }
        }

        // Property to get and set the value of the lP_Votes attribute
        // Pre: True for getting and setting.
        // Post: The value of lP_Votes has been returned for getting or the value of 
        // lP_Votes has been set to the input value for setting.

        public int? LP_Votes
        {
            get
            {
                return lP_Votes;
            }
            set
            {
                lP_Votes = value;
            }
        }

        // Resets all fields in the FederalElectorateData object to null values.
        // No return value.
        // Pre: True.
        // Post: All attributes of the FederalElectorateData object have been returned to null values.

        public void ClearAll()
        {
            aLP_Votes = null;
            federalElectorateName = null;
            firstPref_ALP_Percent = null;
            firstPref_DEM_Percent = null;
            firstPref_GRN_Percent = null;
            firstPref_LP_Percent = null;
            firstPref_NP_Percent = null;
            firstPref_NP_Percent = null;
            firstPref_OTH_Percent = null;
            firstPref_SeatWinner = null;
            heldSince = null;
            lP_Votes = null;
        }
    }
}
