using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCat
{
    public interface ISocialReader
    {
        String[] GetStateSeats(String federalSeat);
        
        StateElectorateData GetStateResults(string stateSeat);
        
        FederalElectorateData GetFederalResults(string federalSeat);
    }
}
