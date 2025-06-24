using Librarydlb.Tables;
using Librarydlb.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySqlDBOperations;

namespace Librarydlb.DataBaseOps
{
    internal class AuthorDataBaseOps : iBaseDBOps
    {
        // db helper
        MySqlDBOperations.Helper objDB;

        // connect to the database
        public void ConnectDB()
        {
            objDB = new MySqlDBOperations.Helper("Server=localhost;Database=library;UserID=yash;Password=12345");
        }

        // delete an author using author id
        public int Delete(DataBaseTable obj)
        {
            if (objDB == null)
            {
                ConnectDB();
            }

            int result = 0;
            string query = @"DELETE FROM authors
                             USING authors
                             WHERE authors.Author_Id = {0}";

            result = objDB.ExecuteCRUDCommand(string.Format(query, ((Authors)obj).Author_Id));
            return result;
        }

        // add a new author
        public int Insert(DataBaseTable obj)
        {
            if (objDB == null)
            {
                ConnectDB();
            }

            int result = 0;
            string query = @"INSERT INTO authors (Author_Name, Status) VALUES ('{0}', {1})";

            result = objDB.ExecuteCRUDCommand(string.Format(query,
                ((Authors)obj).Author_Name,
                ((Authors)obj).Status));

            return result;
        }

        // update author details
        public int Update(DataBaseTable obj)
        {
            if (objDB == null)
            {
                ConnectDB();
            }

            int result = 0;
            string query = @"UPDATE authors
                             SET Name = '{1}', Status = {2}
                             WHERE Author_Id = {0};";

            result = objDB.ExecuteCRUDCommand(string.Format(query,
                ((Authors)obj).Author_Id,
                ((Authors)obj).Author_Name,
                ((Authors)obj).Status));

            return result;
        }

        // get one author's details using id
        public DataBaseTable Get(int id)
        {
            if (objDB == null)
            {
                ConnectDB();
            }

            Authors author = new Authors();
            string query = $@"SELECT Author_Id, Name, Status FROM authors WHERE Author_Id = {id};";

            DataTable dt = objDB.ExecuteSelectQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                author.Author_Id = (int)dt.Rows[0]["Author_Id"];
                author.Author_Name = Convert.ToString(dt.Rows[0]["Name"]);
                author.Status = (int)dt.Rows[0]["Status"];
            }

            return author;
        }

        // get all authors using custom query
        public List<DataBaseTable> List(string query)
        {
            if (objDB == null)
            {
                ConnectDB();
            }

            List<DataBaseTable> authorsList = new List<DataBaseTable>();
            DataTable dt = objDB.ExecuteSelectQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Authors author = new Authors();
                    author.Author_Id = (int)row["Author_Id"];
                    author.Author_Name = Convert.ToString(row["Author_Name"]);
                    author.Status = (int)row["Status"];
                    authorsList.Add(author);
                }
            }

            return authorsList;
        }
    }
}