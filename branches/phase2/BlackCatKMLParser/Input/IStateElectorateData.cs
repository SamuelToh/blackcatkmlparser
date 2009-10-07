using System;
namespace BlackCat
{
    public interface IStateElectorateData
    {
        // Property to get and set the value of the stateElectorateName attribute
        // The getter returns the stateElectorateName, the setter returns nothing.
        // Pre: True for getting, name is not null for setting
        // Post: The value of stateElectorateName has been returned for getting or the value of 
        // stateElectorateName has been set to the input value for setting.

        String StateElectorateName { get; set; }

        // Property to get and set the value of the tPP_WinnerParty attribute
        // The getter returns the tPP_WinnerParty, the setter returns nothing.
        // Pre: True for getting, partyName is not null for setting
        // Post: The value of tPP_WinnerParty has been returned for getting or the value of 
        // tPP_WinnerParty has been set to the input value for setting.

        String TPP_WinnerParty { get; set; }

        // Resets all fields in the StateElectorateData object to null values.
        // No return value.
        // Pre: True.
        // Post: All attributes of the StateElectorateData object have been returned to null 
        // values.

        void clearAll();

    }
}
