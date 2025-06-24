using Librarydlb.Tables;
using Librarydlb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using MySqlDBOperations;

namespace Librarydlb.DBOperations
{
    // Handles all DB operations for Genre table
    internal class GenreDataBaseOps : iBaseDBOps
    {
        // Helper object to handle database tasks
        MySqlDBOperations.Helper _SQLDBOps;

        // Connects to the database
        public void ConnectDB()
        {
            _SQLDBOps = new MySqlDBOperations.Helper("Server=localhost;Database=library;UserID=yash;Password=12345");
        }

        // Deletes a genre by Genre_Id
        public int Delete(DataBaseTable Genre)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string delQuery = @"DELETE FROM `library`.`genre`
                                WHERE Genre_Id = {0};";
            result = _SQLDBOps.ExecuteCRUDCommand(string.Format(delQuery, ((Genre)Genre).Genre_Id));
            return result;
        }

        // Inserts a new genre into the genre table
        public int Insert(DataBaseTable Genre)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string insQuery = $@"INSERT INTO `library`.`genre`
                                 (`Genre_Id`, `Name`)
                                 VALUES
                                 ({((Genre)Genre).Genre_Id},
                                 '{((Genre)Genre).Genre_Name}');";

            result = _SQLDBOps.ExecuteCRUDCommand(insQuery);
            return result;
        }

        // Updates a genre by Genre_Id
        public int Update(DataBaseTable Genre)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string updQuery = $@"UPDATE `library`.`genre`
                                 SET
                                 `Genre_Id` = {((Genre)Genre).Genre_Id},
                                 `Name` = '{((Genre)Genre).Genre_Name}'
                                 WHERE Genre_Id = {((Genre)Genre).Genre_Id};";

            result = _SQLDBOps.ExecuteCRUDCommand(updQuery);
            return result;
        }

        // Gets one genre by its ID
        public DataBaseTable Get(int Id)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            Genre oGenre = new Genre();

            string sQuery = @"SELECT `genre`.`Genre_Id`,
                                     `genre`.`Name`
                              FROM `library`.`genre` 
                              WHERE Genre_Id = {0};";

            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(string.Format(sQuery, Id));

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                oGenre.Genre_Id = (int)dtResult.Rows[0]["Genre_Id"];
                oGenre.Genre_Name = Convert.ToString(dtResult.Rows[0]["Name"]);
            }

            return oGenre;
        }

        // Gets list of genres using a custom query
        public List<DataBaseTable> List(string query)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            List<DataBaseTable> lstGenre = new List<DataBaseTable>();
            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(query);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    Genre oGenre = new Genre();
                    oGenre.Genre_Id = (int)dr["Genre_Id"];
                    oGenre.Genre_Name = Convert.ToString(dr["Name"]);

                    lstGenre.Add(oGenre);
                }
            }

            return lstGenre;
        }
    }
}