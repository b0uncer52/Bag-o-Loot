using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace BagOLoot
{
    public class Santa
    {
        private string _connectionString = $"Data Source={Environment.GetEnvironmentVariable("BAGOLOOT_DB")}";
        private SqliteConnection _connection;

        public Santa()
        {
            _connection = new SqliteConnection(_connectionString);
        }
        public int AddToyToBag(string toy, int childId)
        {
            int _lastId = 0;
            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();

                dbcmd.CommandText = $"INSERT INTO toy VALUES (null, '{childId}', '{toy}');";
                dbcmd.ExecuteNonQuery();
                
                dbcmd.CommandText = $"SELECT last_insert_rowid();";
                 using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    if (dr.Read()) {
                        _lastId = dr.GetInt32(0);
                    } else {
                        throw new Exception("Unable to insert value");
                    }
                }

                dbcmd.Dispose();
                _connection.Close();
            }
            return _lastId;
        }

        public Dictionary<int, string> GetToysInChildsBag(int childId)
        {
            Dictionary<int, string> _childToys = new Dictionary<int, string>();

            using (_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                
                dbcmd.CommandText = $"SELECT id, name FROM toy WHERE toy.childId = {childId};";
                using (SqliteDataReader dr = dbcmd.ExecuteReader()) 
                {
                    while (dr.Read()) 
                    {
                         _childToys.Add(dr.GetInt32(0), dr[1].ToString());
                    } 
                }

                dbcmd.Dispose();
                _connection.Close();
            }
            return _childToys;
        }

        public void RemoveToyFromBag(int toyId)
        {
            using (_connection)
            {
                _connection.Open ();
                SqliteCommand dbcmd = _connection.CreateCommand ();

                // Insert the new child
                dbcmd.CommandText = $"DELETE FROM toy WHERE toy.id = {toyId};";
                dbcmd.ExecuteNonQuery();

                dbcmd.Dispose();
                _connection.Close();
            }
        }
        
        public Dictionary<int, string> ChildrenToDeliver()
        {
            Dictionary<int, string> _children = new Dictionary<int, string>();
            using(_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = "SELECT c.id, c.name FROM child c, toy t WHERE t.childId = c.id AND c.delivered = 0 GROUP BY c.id;";

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

        public bool DeliverToChild(int childId)
        {
            using(_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = $"UPDATE child SET delivered = 1 WHERE child.id = {childId}";
                dbcmd.ExecuteNonQuery();
                dbcmd.Dispose();
                _connection.Close();
            }
            return true;
        }

        public List<(int, string, string)> DeliveryReport()
        {
             List<(int id, string name, string toy)> _report = new List<(int, string, string)>();

            using(_connection)
            {
                _connection.Open();
                SqliteCommand dbcmd = _connection.CreateCommand();
                dbcmd.CommandText = "SELECT c.id, c.name, t.name FROM child c LEFT JOIN toy t ON t.childId = c.id WHERE c.delivered = 1 GROUP BY t.childId;";

                using(SqliteDataReader dr = dbcmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        _report.Add((dr.GetInt32(0), dr[1].ToString(), dr[2].ToString()));
                    }
                }
                dbcmd.Dispose();
                _connection.Close();
            }
            return _report;
        }
    }
}