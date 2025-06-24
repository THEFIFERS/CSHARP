using Librarydlb.Tables;
using Librarydlb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using MySqlDBOperations;
namespace Librarydlb.DBOperations
{
    // This class handles all DB operations for publisher table
    internal class PublisherDataBaseOps : iBaseDBOps
    {
        // SQL helper to connect and run DB commands
        MySqlDBOperations.Helper _SQLDBOps;

        // Method to connect to database
        public void ConnectDB()
        {
            _SQLDBOps = new MySqlDBOperations.Helper("Server=localhost;Database=library;UserID=yash;Password=12345");
        }

        // Delete a publisher using its ID
        public int Delete(DataBaseTable Publisher)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string delQuery = @"DELETE FROM `library`.`publisher` WHERE Publisher_Id={0};";

            result = _SQLDBOps.ExecuteCRUDCommand(string.Format(delQuery, ((Publishers)Publisher).Publisher_Id));
            return result;
        }

        // Insert a new publisher into DB
        public int Insert(DataBaseTable Publisher)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;

            string insQuery = $@"INSERT INTO `library`.`publisher`
                                (`Publisher_Id`, `Name`, `Status`)
                                VALUES
                                ({((Publishers)Publisher).Publisher_Id},
                                '{((Publishers)Publisher).Publisher_Name}',
                                {((Publishers)Publisher).Status});";

            result = _SQLDBOps.ExecuteCRUDCommand(insQuery);
            return result;
        }

        // Update publisher information using its ID
        public int Update(DataBaseTable Publisher)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;
            string updQuery = $@"UPDATE `library`.`publisher`
                                SET
                                `Publisher_Id` = {((Publishers)Publisher).Publisher_Id},
                                `Name` = '{((Publishers)Publisher).Publisher_Name}',
                                `Status` = {((Publishers)Publisher).Status}
                                WHERE Publisher_Id = {((Publishers)Publisher).Publisher_Id};";

            result = _SQLDBOps.ExecuteCRUDCommand(updQuery);
            return result;
        }

        // Get one publisher by its ID
        public DataBaseTable Get(int Id)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            Publishers oPublisher = new Publishers();

            string sQuery = @"SELECT `Publisher_Id`, `Name`, `Status` 
                              FROM `library`.`publisher` 
                              WHERE Publisher_Id = {0};";

            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(string.Format(sQuery, Id));

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                oPublisher.Publisher_Id = (int)dtResult.Rows[0]["Publisher_Id"];
                oPublisher.Publisher_Name = Convert.ToString(dtResult.Rows[0]["Name"]);
                oPublisher.Status = (int)dtResult.Rows[0]["Status"];
            }

            return oPublisher;
        }

        // Get a list of publishers from a custom query
        public List<DataBaseTable> List(string query)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            List<DataBaseTable> lstPublisher = new List<DataBaseTable>();
            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(query);

            if (dtResult != null && dtResult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    Publishers oPublisher = new Publishers();
                    oPublisher.Publisher_Id = (int)dr["Publisher_Id"];
                    oPublisher.Publisher_Name = Convert.ToString(dr["Name"]);
                    oPublisher.Status = (int)dr["Status"];

                    lstPublisher.Add(oPublisher);
                }
            }

            return lstPublisher;
        }
    }
}