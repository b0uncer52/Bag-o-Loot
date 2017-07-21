using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class ChildRegistry
    {
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;

        public ChildRegistry()
        {
            _connection = new SqliteConnection(_connectionString);
        }

        public bool AddChild (string child) 
        {
            int _lastId = 0; // Will store the id of the last inserted record
            
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Insert the new child
                dbcmd.CommandText = $"INSERT INTO child VALUES (null, '{child}', 0);";
                dbcmd.ExecuteNonQuery();

                // Get the id of the new row
                dbcmd.CommandText = $"SELECT last_insert_rowid();";
                using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    if (dr.Read()) {
                        _lastId = dr.GetInt32(0);
                    } else {
                        throw new Exception("Unable to insert value");
                    }
                }

                // clean up
                dbcmd.Dispose();
                _connection.Close();
            }
            return true;
        }

        public Dictionary<int, string> GetChildren ()
        {
            Dictionary<int, string> _children = new Dictionary<int, string>(){};

            using(_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = "SELECT id, name FROM child;";

                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        _children.Add(dr.GetInt32(0), dr[1].ToString());
                    }
                }
                dbcmd.Dispose();
                _connection.Close();
            }
            return _children;
        }
    }
}