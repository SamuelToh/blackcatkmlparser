using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface ISocialReader
    {
        // Gets a list of strings representing the names of all the federal seats in the database.
        // Returns the list of strings containing the names of all the federal seats in the 
        // database.
        // Pre: True
        // Post: The list of federal seats contained within the database has been returned.

        List<String> GetFederalElectorateNames();

        // Gets a FederalElectorateData object containing federal data for federalSeat
        // Returns a FederalElectorateData object containing the federal data required to 
        // calculate the winning party and seat safety for federalSeat.
        // Pre: federalSeat is not null
        // Post: The FederalElectorateData object has been populated with the data for 
        // federalSeat and returned.

        FederalElectorateData GetFederalResults(string federalSeat);

        // Gets a StateElectorateData object containing state data for stateSeat
        // Returns a StateElectorateData object containing the state data required in a seat 
        // safety calculation.
        // Pre: stateSeat is not null
        // Post: The StateElectorateData object has been populated with the data for stateSeat // and returned.

        StateElectorateData GetStateResults(string stateSeat);

        // Gets a string array containing the names of all the state seats partially or wholly 
        // contained within the borders of federalSeat.
        // Returns a string array containing the names of every seat that is partially or wholly // within the borders of federalSeat.
        // Pre:federalSeat is not null
        // Post: The array of state seat names that are partially or wholly contained within the // borders of federalSeat have been returned.

        List<String> GetStateSeats(String federalSeat);

    }
}
