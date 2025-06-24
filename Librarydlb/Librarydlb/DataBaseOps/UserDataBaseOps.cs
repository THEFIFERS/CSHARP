using Librarydlb.Tables;
using Librarydlb.Interfaces;
using System;
using System.Collections.Generic;
using MySqlDBOperations;
using System.Data;

namespace Librarydlb.DBOperations
{
    // This class handles basic DB operations for the user table
    internal class UserDataBaseOps : iBaseDBOps
    {
        // Database helper object
        MySqlDBOperations.Helper _SQLDBOps;

        // Connect to the database
        public void ConnectDB()
        {
            _SQLDBOps = new MySqlDBOperations.Helper("Server=localhost;Database=library;UserID=yash;Password=12345");
        }

        // Delete a user by User_Id
        public int Delete(DataBaseTable User)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string delQuery = @"DELETE FROM `library`.`user`
                                WHERE User_Id = {0};";

            result = _SQLDBOps.ExecuteCRUDCommand(string.Format(delQuery, ((User)User).User_Id));
            return result;
        }

        // Insert a new user
        public int Insert(DataBaseTable User)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string insQuery = $@"INSERT INTO `library`.`user`
                                (`User_Id`, `Name`, `Email`, `Phone_No`)
                                VALUES
                                ({((User)User).User_Id},
                                '{((User)User).Name}',
                                '{((User)User).Email}',
                                {((User)User).Phone_No});";

            result = _SQLDBOps.ExecuteCRUDCommand(insQuery);
            return result;
        }

        // Update user details
        public int Update(DataBaseTable User)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string updQuery = $@"UPDATE `library`.`user`
                                SET
                                `User_Id` = {((User)User).User_Id},
                                `Name` = '{((User)User).Name}',
                                `Email` = '{((User)User).Email}',
                                `Phone_No` = {((User)User).Phone_No}
                                WHERE User_Id = {0};";

            result = _SQLDBOps.ExecuteCRUDCommand(string.Format(updQuery, ((User)User).User_Id));
            return result;
        }

        // Get one user by ID
        public DataBaseTable Get(int Id)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            User oUser = new User();
            string sQuery = @"SELECT `User_Id`, `Name`, `Email`, `Phone_No`
                              FROM `library`.`user`
                              WHERE User_Id = {0};";

            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(string.Format(sQuery, Id));

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                oUser.User_Id = (int)dtResult.Rows[0]["User_Id"];
                oUser.Name = Convert.ToString(dtResult.Rows[0]["Name"]);
                oUser.Email = Convert.ToString(dtResult.Rows[0]["Email"]);
                oUser.Phone_No = Convert.ToDouble(dtResult.Rows[0]["Phone_No"]);
            }

            return oUser;
        }

        // Get a list of users with a custom query
        public List<DataBaseTable> List(string query)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            List<DataBaseTable> lstUser = new List<DataBaseTable>();
            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(query);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    User oUser = new User();
                    oUser.User_Id = (int)dr["User_Id"];
                    oUser.Name = Convert.ToString(dr["Name"]);
                    oUser.Email = Convert.ToString(dr["Email"]);
                    oUser.Phone_No = Convert.ToDouble(dr["Phone_No"]);

                    lstUser.Add(oUser);
                }
            }

            return lstUser;
        }
    }
}