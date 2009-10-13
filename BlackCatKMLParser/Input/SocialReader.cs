using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using log4net;

namespace BlackCat
{
    public class SocialReader : ISocialReader
    {
        private ILog log = LogManager.GetLogger(typeof(SocialReader));
        private OleDbConnection con;        

        //constructor
        public SocialReader()
        {
            String dataFilePath = System.IO.Directory.GetCurrentDirectory() + "\\BlackcatKMLParser.accdb";
            log.Debug("Database file path - " + dataFilePath);
            String connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                "data source= " + dataFilePath + ";";
            
            con = new OleDbConnection(connectionString);
        }

        // Gets a list of strings representing the names of all the federal seats in the database.
        // Returns the list of strings containing the names of all the federal seats in the 
        // database.
        // Pre: True
        // Post: The list of federal seats contained within the database has been returned.
        public List<String> GetFederalElectorateNames()
        {
            string selectElectNm = "SELECT FederalElectorateName FROM FederalResults2004_National ORDER BY FederalElectorateName";
            List<String> electNames = new List<String>();

            try
            {
                con.Open();
                OleDbCommand cmdElectNames = new OleDbCommand(selectElectNm, con);
                // execute query
                OleDbDataReader reader = cmdElectNames.ExecuteReader();

                while (reader.Read())
                {
                    electNames.Add(reader[0].ToString());
                }
            }
            catch (OleDbException oldEx)
            {
                log.Error(oldEx.ToString());
            }
            /*catch (Exception ex)
            {
                log.Error(ex.ToString());
            }*/
            finally
            {
                con.Close();
            }

            return electNames;

        }

        // Gets a list of string containing the names of all the state seats partially or wholly 
        // contained within the borders of federalSeat.
        // Returns a string array containing the names of every seat that is partially or wholly 
        // within the borders of federalSeat.
        // Pre:federalSeat is not null
        // Post: The list of state seat names that are partially or wholly contained within the 
        // borders of federalSeat have been returned.
        public List<String> GetStateSeats(String federalSeat)
        {
            string selectStateSeats = "SELECT StateElectorateName FROM ElectorateMapping WHERE FederalElectorateName='" + federalSeat + "'";
            List<String> stateSeats = new List<String>();

            try
            {
                con.Open();
                OleDbCommand cmdStateSeats = new OleDbCommand(selectStateSeats, con);
                // execute query
                OleDbDataReader reader = cmdStateSeats.ExecuteReader();

                while (reader.Read())
                {
                    stateSeats.Add(reader[0].ToString());
                }
            }
            catch (OleDbException oldEx)
            {
                log.Error(oldEx.ToString());
            }
            /*catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }*/
            finally
            {
                con.Close();
            }

            return stateSeats;
        }

        // Gets a StateElectorateData object containing state data for stateSeat
        // Returns a StateElectorateData object containing the state data required in a seat 
        // safety calculation.
        // Pre: stateSeat is not null
        // Post: The StateElectorateData object has been populated with the data for stateSeat 
        // and returned.
        public StateElectorateData GetStateResults(string stateSeat)
        {
            string selectStateData = "SELECT TPP_WinnerParty FROM StateResults2006_QLD WHERE StateElectorateName ='" + stateSeat + "'";
            StateElectorateData stateData = new StateElectorateData();

            try
            {
                con.Open();
                OleDbCommand cmdStateData = new OleDbCommand(selectStateData, con);
                // execute query
                OleDbDataReader reader = cmdStateData.ExecuteReader();

                while (reader.Read())
                {
                    stateData.StateElectorateName = stateSeat;
                    stateData.TPP_WinnerParty = reader[0].ToString();
                }
            }
            catch (OleDbException oldEx)
            {
                log.Error(oldEx.ToString());
            }
            /*catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }*/
            finally
            {
                con.Close();
            }

            return stateData;
        }

        // Gets a FederalElectorateData object containing federal data for federalSeat
        // Returns a FederalElectorateData object containing the federal data required to 
        // calculate the winning party and seat safety for federalSeat.
        // Pre: federalSeat is not null
        // Post: The FederalElectorateData object has been populated with the data for 
        // federalSeat and returned.
        public FederalElectorateData GetFederalResults(string federalSeat)
        {
            string selectFederalData = "SELECT ALP_Votes, FirstPref_ALP_Percent, FirstPref_LP_Percent," +
                                       " FirstPref_NP_Percent, FirstPref_DEM_Percent, FirstPref_GRN_Percent," +
                                       " FirstPref_SeatWinner, LNP_Votes, HeldSince FROM FederalResults2004_National" +
                                       " WHERE FederalElectorateName='" + federalSeat + "'";
            FederalElectorateData federalData = new FederalElectorateData();

            try
            {
                con.Open();
                OleDbCommand cmdFederalData = new OleDbCommand(selectFederalData, con);
                // execute query
                OleDbDataReader reader = cmdFederalData.ExecuteReader();
                while (reader.Read())
                {
                    federalData.FederalElectorateName = federalSeat;
                    federalData.ALP_Votes = validIntData(reader, 0);
                    federalData.FirstPref_ALP_Percent = validFloatData(reader, 1);
                    federalData.FirstPref_LP_Percent = validFloatData(reader, 2);
                    federalData.FirstPref_NP_Percent = validFloatData(reader, 3);
                    federalData.FirstPref_DEM_Percent = validFloatData(reader, 4);
                    federalData.FirstPref_GRN_Percent = validFloatData(reader, 5);
                    federalData.FirstPref_SeatWinner = reader["FirstPref_SeatWinner"].ToString();
                    federalData.LP_Votes = validIntData(reader, 7);
                    federalData.HeldSince = heldSince(reader);
                }
            }
            catch (OleDbException oldEx)
            {
                log.Error(oldEx.ToString());
            }
            /*catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }*/
            finally
            {
                con.Close();
            }

            return federalData;
        }

        public List<IDistrict> GetFederalElectorateDistricts()
        {
            string selectDistricts = "SELECT StateElectorateName, DistrictName " +
                                        "FROM Districts";
            List<IDistrict> districts = new List<IDistrict>();
            try
            {
                //Execute query
                con.Open();
                OleDbCommand cmdDistricts = new OleDbCommand(selectDistricts, con);
                OleDbDataReader reader = cmdDistricts.ExecuteReader();
                //Process results
                while (reader.Read())
                {
                    AddDistrict(districts, 
                        reader["StateElectorateName"].ToString(), 
                        reader["DistrictName"].ToString());
                }
            }
            catch (OleDbException oldEx)
            {
                log.Error(oldEx.ToString());
            }
            /*catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }*/
            finally
            {
                con.Close();
            }
            return districts;
        }

        private void AddDistrict(List<IDistrict> districts, String electorateName, String districtName)
        {
            log.Debug("Adding district - " + electorateName + " (" + districtName + ")");
            IDistrict storedDistrict = GetDistrict(districts, districtName);
            if (storedDistrict != null)
                storedDistrict.RegionNames.Add(electorateName);
            else
            {
                IDistrict newDistrict = new District();
                newDistrict.DistrictName = districtName;
                newDistrict.RegionNames.Add(electorateName);
                districts.Add(newDistrict);
            }
        }

        private IDistrict GetDistrict(List<IDistrict> districts, String districtName)
        {
            foreach (IDistrict d in districts)
            {
                if (d.DistrictName == districtName)
                    return d;
            }
            return null;
        }

        // Validation method for checking the integer data does not contain null value.
        // Returns an integer value if the data is not null. 
        // pre: odReader is not null and ordinal >= 0
        // post: An integer value is returned if the data is not null. otherwise returns null.
        private int? validIntData(OleDbDataReader odReader, int ordinal)
        {
            int? intValue;

            // check whether the data is not null
            if (odReader.GetValue(ordinal) != System.DBNull.Value)
                intValue = odReader.GetInt32(ordinal);
            else
                intValue = null;

            return intValue;
        }

        // Validation method for checking the small integer data does not contain null value.
        // Returns an integer value if the data is not null. 
        // pre: odReader is not null 
        // post: An integer value is returned if the data is not null. otherwise returns null.
        private int? heldSince(OleDbDataReader odReader)
        {
            int? year;

            // check whether the data is not null
            if (odReader.GetValue(8) != System.DBNull.Value)
                year = odReader.GetInt16(8);
            else
                year = null;

            return year;
        }

        // Validation method for checking the decimal data does not contain null value and converts to 
        // float value.
        // Returns a float value if the data is not null. 
        // pre: odReader is not null and ordinal >= 0
        // post: A float value is returned if the data is not null. otherwise returns null.
        private float? validFloatData(OleDbDataReader odReader, int ordinal)
        {
            float? fValue;

            // check whether the data is not null
            if (odReader.GetValue(ordinal) != System.DBNull.Value)
                fValue = (float)odReader.GetDecimal(ordinal);
            else
                fValue = null;

            return fValue;
        }
    }
}
