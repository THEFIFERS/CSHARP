using Librarydlb.Tables;
using System.Collections.Generic;

namespace Librarydlb.Interfaces
{
    // This interface defines common operations that any database class should implement
    internal interface iBaseDBOps
    {
        // Method to insert a new record into the database
        int Insert(DataBaseTable dbTable);

        // Method to update an existing record in the database
        int Update(DataBaseTable dbTable);

        // Method to delete a record from the database
        int Delete(DataBaseTable dbTable);

        // Method to get a list of records using a custom query
        List<DataBaseTable> List(string query);

        // Method to get a single record by its ID
        DataBaseTable Get(int id);

        // Method to connect to the database
        void ConnectDB();
    }
}