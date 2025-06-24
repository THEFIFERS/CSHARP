using Librarydlb.Tables;
using Librarydlb.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySqlDBOperations;

namespace Librarydlb.DBOperations
{
    // This class handles all DB operations for bookings
    internal class BookingDataBaseOps : iBaseDBOps
    {
        // DB helper for executing SQL queries
        MySqlDBOperations.Helper _SQLDBOps;

        // Connect to the database
        public void ConnectDB()
        {
            _SQLDBOps = new MySqlDBOperations.Helper("Server=localhost;Database=library;UserID=yash;Password=12345");
        }

        // Delete a booking using Book_Id
        public int Delete(DataBaseTable Booking)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string delQuery = @"DELETE FROM `library`.`booking`
                                WHERE Book_Id = {0};";
            result = _SQLDBOps.ExecuteCRUDCommand(string.Format(delQuery, ((Booking)Booking).Book_Id));
            return result;
        }

        // Insert a new booking
        public int Insert(DataBaseTable Booking)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string insQuery = $@"INSERT INTO `library`.`booking`
                                (`Book_Id`, `User_Id`, `Start_Date`, `End_Date`)
                                VALUES
                                ({((Booking)Booking).Book_Id},
                                 {((Booking)Booking).User_Id},
                                '{((Booking)Booking).Start_Date}',
                                '{((Booking)Booking).End_Date}');";

            result = _SQLDBOps.ExecuteCRUDCommand(insQuery);
            return result;
        }

        // Update booking info using Booking_Id
        public int Update(DataBaseTable Booking)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string updQuery = $@"UPDATE `library`.`booking`
                                SET
                                `Book_Id` = {((Booking)Booking).Book_Id},
                                `User_Id` = {((Booking)Booking).User_Id},
                                `Start_Date` = '{((Booking)Booking).Start_Date}',
                                `End_Date` = '{((Booking)Booking).End_Date}'
                                WHERE `Booking_Id` = {0};";

            result = _SQLDBOps.ExecuteCRUDCommand(string.Format(updQuery, ((Booking)Booking).Booking_Id));
            return result;
        }

        // Get one booking using Booking_Id
        public DataBaseTable Get(int Id)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            Booking oBooking = new Booking();
            string sQuery = @"SELECT `booking`.`Book_Id`,
                                     `booking`.`User_Id`,
                                     `booking`.`Start_Date`,
                                     `booking`.`End_Date`
                              FROM `library`.`booking` 
                              WHERE Booking_Id = {0}";

            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(string.Format(sQuery, Id));

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                oBooking.Book_Id = (int)dtResult.Rows[0]["Book_Id"];
                oBooking.User_Id = (int)dtResult.Rows[0]["User_Id"];
                oBooking.Start_Date = Convert.ToString(dtResult.Rows[0]["Start_Date"]);
                oBooking.End_Date = Convert.ToString(dtResult.Rows[0]["End_Date"]);
            }

            return oBooking;
        }

        // Get a list of bookings using query
        public List<DataBaseTable> List(string query)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            List<DataBaseTable> lstBooking = new List<DataBaseTable>();
            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(query);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    Booking oBOOKing = new Booking();
                    oBOOKing.Book_Id = (int)dr["Book_Id"];
                    oBOOKing.User_Id = (int)dr["User_Id"];
                    oBOOKing.Start_Date = Convert.ToString(dr["Start_Date"]);
                    oBOOKing.End_Date = Convert.ToString(dr["End_Date"]);

                    lstBooking.Add(oBOOKing);
                }
            }

            return lstBooking;
        }
    }
}