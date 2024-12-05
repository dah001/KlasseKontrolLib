using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KlasseLib.Services
{
    public class ClassRoomDb : IClassRoom
    {
        private readonly string _connectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;";

        public List<Classroom> GetAll()
        {
            var classrooms = new List<Classroom>();
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Classroom";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            classrooms.Add(new Classroom
                            {
                                ClassID = (int)reader["ClassID"],
                                TeacherName = reader["TeacherName"]?.ToString(),
                                StudentCount = (int)reader["StudentCount"],
                                SessionActive = (bool)reader["SessionActive"]
                            });
                        }
                    }
                }
            }
            return classrooms;
        }

        public Classroom GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = "SELECT * FROM Classroom WHERE ClassID = @ClassID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassID", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Classroom
                            {
                                ClassID = (int)reader["ClassID"],
                                TeacherName = reader["TeacherName"]?.ToString(),
                                StudentCount = (int)reader["StudentCount"],
                                SessionActive = (bool)reader["SessionActive"]
                            };
                        }
                    }
                }
            }
            throw new KeyNotFoundException($"Classroom with ID {id} not found.");
        }

        public Classroom Add(Classroom classroom)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    INSERT INTO Classroom (TeacherName, StudentCount, SessionActive) 
                    VALUES (@TeacherName, @StudentCount, @SessionActive);
                    SELECT SCOPE_IDENTITY();";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeacherName", classroom.TeacherName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StudentCount", classroom.StudentCount);
                    command.Parameters.AddWithValue("@SessionActive", classroom.SessionActive);

                    connection.Open();
                    classroom.ClassID = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return classroom;
        }

        public void Update(int id, Classroom classroom)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    UPDATE Classroom 
                    SET TeacherName = @TeacherName, StudentCount = @StudentCount, SessionActive = @SessionActive 
                    WHERE ClassID = @ClassID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassID", id);
                    command.Parameters.AddWithValue("@TeacherName", classroom.TeacherName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StudentCount", classroom.StudentCount);
                    command.Parameters.AddWithValue("@SessionActive", classroom.SessionActive);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new KeyNotFoundException($"Classroom with ID {id} not found.");
                    }
                }
            }
        }

        public void Delete(int classID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = "DELETE FROM Classroom WHERE ClassID = @ClassID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassID", classID);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new KeyNotFoundException($"Classroom with ID {classID} not found.");
                    }
                }
            }
        }

        public void StartSession(int classID, string teacherName, int studentCount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = @"
                    UPDATE Classroom 
                    SET TeacherName = @TeacherName, StudentCount = @StudentCount, SessionActive = @SessionActive 
                    WHERE ClassID = @ClassID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassID", classID);
                    command.Parameters.AddWithValue("@TeacherName", teacherName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StudentCount", studentCount);
                    command.Parameters.AddWithValue("@SessionActive", true);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new KeyNotFoundException($"Classroom with ID {classID} not found.");
                    }
                }
            }
        }

        public void StopSession(int classID)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                const string query = "UPDATE Classroom SET SessionActive = @SessionActive WHERE ClassID = @ClassID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClassID", classID);
                    command.Parameters.AddWithValue("@SessionActive", false);

                    connection.Open();
                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new KeyNotFoundException($"Classroom with ID {classID} not found.");
                    }
                }
            }
        }
    }
}
