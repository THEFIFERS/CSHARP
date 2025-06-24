using Librarydlb.Tables;
using Librarydlb.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySqlDBOperations;

namespace Librarydlb.DBOperations
{
    // This class handles all database operations related to books
    internal class BookDataBaseOps : iBaseDBOps
    {
        // Object to perform MySQL database operations
        MySqlDBOperations.Helper _SQLDBOps;

        // Connect to the database using a connection string
        public void ConnectDB()
        {
            _SQLDBOps = new MySqlDBOperations.Helper("Server=localhost;Database=library;UserID=yash;Password=12345");
        }

        // Delete a book from the database using Book_Id
        public int Delete(DataBaseTable Books)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB(); // Make sure DB is connected
            }

            int result = 0;
            string delQuery = @"DELETE FROM `library`.`books` WHERE Book_Id={0}";

            // Execute delete command using the Book_Id
            result = _SQLDBOps.ExecuteCRUDCommand(string.Format(delQuery, ((Books)Books).Book_Id));
            return result;
        }

        // Insert a new book record into the database
        public int Insert(DataBaseTable Books)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;

            // SQL insert query using book details
            string insQuery = $@"INSERT INTO `library`.`books`
                                (`Name`, `Genre_Id`, `Author_Id`, `Publisher_Id`, `Total_Quantity`, `Available_Quantity`)
                                VALUES
                                ('{((Books)Books).Book_name}',
                                 {((Books)Books).Genre_Id},
                                 {((Books)Books).Author_Id},
                                 {((Books)Books).Publisher_Id},
                                 {((Books)Books).Total_Quantity},
                                 {((Books)Books).Available_Quantity});";

            // Execute insert command
            result = _SQLDBOps.ExecuteCRUDCommand(insQuery);
            return result;
        }

        // Update existing book record in the database
        public int Update(DataBaseTable Books)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            int result = 0;

            // SQL update query with new book values
            string updQuery = $@"UPDATE `library`.`books`
                                SET
                                `Name` = '{((Books)Books).Book_name}',
                                `Genre_Id` = {((Books)Books).Genre_Id},
                                `Author_Id` = {((Books)Books).Author_Id},
                                `Publisher_Id` = {((Books)Books).Publisher_Id},
                                `Total_Quantity` = {((Books)Books).Total_Quantity},
                                `Available_Quantity` = {((Books)Books).Available_Quantity}
                                WHERE `Book_Id` = {((Books)Books).Book_Id};";

            // Execute update command
            result = _SQLDBOps.ExecuteCRUDCommand(updQuery);
            return result;
        }

        // Get a single book record by ID
        public DataBaseTable Get(int Id)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            Books oBook = new Books();

            // SQL select query to fetch a single book by ID
            string sQuery = @"SELECT `books`.`Book_Id`,
                                     `books`.`Name`,
                                     `books`.`Genre_Id`,
                                     `books`.`Author_Id`,
                                     `books`.`Publisher_Id`,
                                     `books`.`Total_Quantity`,
                                     `books`.`Available_Quantity`
                              FROM `library`.`books`
                              WHERE Book_Id = {0}";

            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(string.Format(sQuery, Id));

            // If result has rows, map it to the Books object
            if (dtResult != null && dtResult.Rows != null && dtResult.Rows.Count > 0)
            {
                oBook.Book_Id = (int)dtResult.Rows[0]["Book_Id"];
                oBook.Book_name = Convert.ToString(dtResult.Rows[0]["Name"]);
                oBook.Author_Id = (int)dtResult.Rows[0]["Author_Id"];
                oBook.Genre_Id = (int)(dtResult.Rows[0]["Genre_Id"]);
                oBook.Publisher_Id = (int)dtResult.Rows[0]["Publisher_Id"];
                oBook.Total_Quantity = (int)dtResult.Rows[0]["Total_Quantity"];
                oBook.Available_Quantity = (int)dtResult.Rows[0]["Available_Quantity"];
            }

            return oBook;
        }

        // Get a list of all books using a custom query
        public List<DataBaseTable> List(string query)
        {
            if (_SQLDBOps == null)
            {
                ConnectDB();
            }

            List<DataBaseTable> lstBooks = new List<DataBaseTable>();

            // Execute query to get list of books
            DataTable dtResult = _SQLDBOps.ExecuteSelectQuery(query);

            if (dtResult != null && dtResult.Rows != null && dtResult.Rows.Count > 0)
            {
                foreach (DataRow dr in dtResult.Rows)
                {
                    Books oBOOK = new Books();

                    // Map each DataRow to a Books object
                    oBOOK.Book_name = Convert.ToString(dr["Name"]);
                    oBOOK.Book_Id = (int)dr["Book_Id"];
                    oBOOK.Total_Quantity = (int)(dr["Total_Quantity"]);
                    oBOOK.Genre_Id = (int)(dr["Genre_Id"]);
                    oBOOK.Author_Id = (int)dr["Author_Id"];
                    oBOOK.Publisher_Id = (int)dr["Publisher_Id"];
                    oBOOK.Available_Quantity = (int)dr["Available_Quantity"];

                    lstBooks.Add(oBOOK);
                }
            }

            return lstBooks;
        }
    }
}