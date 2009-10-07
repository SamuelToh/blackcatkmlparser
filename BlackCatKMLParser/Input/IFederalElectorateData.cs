using System;
namespace BlackCat
{
    public interface IFederalElectorateData
    {
        // Property to get and set the value of the aLP_Votes attribute
        // The getter returns the aLP_Votes, the setter returns nothing.
        // Pre: True for getting, labor_Votes is not null for setting
        // Post: The value of aLP_Votes has been returned for getting or the value of 
        // aLP_Votes has been set to the input value for setting.

        int ALP_Votes { get; set; }

        // Property to get and set the value of the federalElectorateName attribute
        // The getter returns the federalElectorateName, the setter returns nothing.
        // Pre: True for getting, seatName is not null for setting
        // Post: The value of federalElectorateName has been returned for getting or the 
        // value of federalElectorateName has been set to the input value for setting.

        String FederalElectorateName { get; set; }

        // Property to get and set the value of the firstPref_ALP_Percent attribute
        // The getter returns the firstPref_ALP_Percent, the setter returns nothing.
        // Pre: True for getting, percent is not null for setting
        // Post: The value of firstPref_ALP_Percent has been returned for getting or the value // of firstPref_ALP_Percent has been set to the input value for setting.

        float FirstPref_ALP_Percent { get; set; }

        // Property to get and set the value of the firstPref_DEM_Percent attribute
        // The getter returns the firstPref_DEM_Percent, the setter returns nothing.
        // Pre: True for getting, percent is not null for setting
        // Post: The value of firstPref_DEM_Percent has been returned for getting or the 
        // value of firstPref_DEM_Percent has been set to the input value for setting.

        float FirstPref_DEM_Percent { get; set; }

        // Property to get and set the value of the firstPref_GRN_Percent attribute
        // The getter returns the firstPref_GRN_Percent, the setter returns nothing.
        // Pre: True for getting, percent is not null for setting
        // Post: The value of firstPref_GRN_Percent has been returned for getting or the value // of  firstPref_GRN_Percent has been set to the input value for setting.

        float FirstPref_GRN_Percent { get; set; }

        // Property to get and set the value of the firstPref_LP_Percent attribute
        // The getter returns the firstPref_LP_Percent, the setter returns nothing.
        // Pre: True for getting, percent is not null for setting
        // Post: The value of firstPref_LP_Percent has been returned for getting or the value 
        // of firstPref_LP_Percent has been set to the input value for setting.

        float FirstPref_LP_Percent { get; set; }

        // Property to get and set the value of the firstPref_NP_Percent attribute
        // The getter returns the firstPref_NP_Percent, the setter returns nothing.
        // Pre: True for getting, percent is not null for setting
        // Post: The value of firstPref_NP_Percent has been returned for getting or the value 
        // of firstPref_NP_Percent has been set to the input value for setting.

        float FirstPref_NP_Percent { get; set; }

        // Property to get (but not set) the value of the firstPref_OTH_Percent attribute.
        // Returns the firstPref_OTH_Percent
        // Note: This value is not in the database, it is calculated as 100 - (sum of
        // all the other percentages).
        // Pre: True 
        // Post: The value of firstPref_OTH_Percent has been returned.

        float FirstPref_OTH_Percent { get; }

        // Property to get and set the value of the firstPref_SeatWinner attribute
        // The getter returns the firstPref_SeatWinner party, the setter returns nothing.
        // Pre: True for getting, party is not null for setting
        // Post: The value of firstPref_SeatWinner has been returned for getting or the value 
        // of firstPref_SeatWinner has been set to the input value for setting.

        String FirstPref_SeatWinner { get; set; }

        // Property to get and set the value of the heldSince attribute
        // The getter returns the held_Since year, the setter returns nothing.
        // Pre: True for getting, year is not null for setting
        // Post: The value of heldSince has been returned for getting or the value of 
        // heldSince has been set to the input value for setting.

        int HeldSince { get; set; }

        // Property to get and set the value of the lP_Votes attribute
        // The getter returns the lP_Votes, the setter returns nothing.
        // Pre: True for getting, liberal_Votes is not null for setting
        // Post: The value of lP_Votes has been returned for getting or the value of 
        // lP_Votes has been set to the input value for setting.

        int LP_Votes { get; set; }

        // Resets all fields in the FederalElectorateData object to null values.
        // No return value.
        // Pre: True.
        // Post: All attributes of the FederalElectorateData object have been returned to null 
        // values.

        void clearAll();

    }
}
