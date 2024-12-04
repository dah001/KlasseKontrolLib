using System.Data.SqlClient;
using KlasseLib.Services;

namespace KlasseLib.KlasseKontrolRepository;

public class DB_ClassRoom: IClassRoom
{
    private const string ConnectionString = "Data Source=mssql17.unoeuro.com;Initial Catalog=kunforhustlers_dk_db_test;User ID=kunforhustlers_dk;Password=RmcAfptngeBaxkw6zr5E;";

        public DB_ClassRoom() {}

        // CREATE: Create a new session in the database
        public void CreateClassroomSession(int classID, string teacherName, int studentCount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO ClassroomSessions (ClassID, TeacherName, StudentCount, SessionStart, SessionEnd) VALUES (@ClassID, @TeacherName, @StudentCount, @SessionStart, @SessionEnd)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassID", classID);
                        command.Parameters.AddWithValue("@TeacherName", teacherName);
                        command.Parameters.AddWithValue("@StudentCount", studentCount);
                        command.Parameters.AddWithValue("@SessionStart", DateTime.Now); // Current time as start
                        command.Parameters.AddWithValue("@SessionEnd", DateTime.Now.AddMinutes(5)); // End time after 5 minutes

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Session data stored in database successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to store session data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while storing session data: {ex.Message}");
            }
        }

        // Start session
        public void StartSession(int classID, string teacherName, int studentCount)
        {
            Console.WriteLine($"Session started for class {classID} with teacher {teacherName}.");
            CreateClassroomSession(classID, teacherName, studentCount);

            // Vent i 5 minutter (synkront)
            System.Threading.Thread.Sleep(TimeSpan.FromMinutes(5)); // Vent i 5 minutter (blokere tråden)

            // Stop sessionen efter 5 minutter
            StopSession(classID);

            // Start en ny session
            StartSession(classID, teacherName, studentCount); // Rekursiv kald
        }
        
        // Stop session
        public void StopSession(int classID)
        {
            Console.WriteLine($"Session stopped for class {classID}.");
            
            // Update session end time in database
            UpdateSessionEndTime(classID);
        }

        // Update session end time in the database
        private void UpdateSessionEndTime(int classID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE ClassroomSessions SET SessionEnd = @SessionEnd WHERE ClassID = @ClassID AND SessionEnd IS NULL";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassID", classID);
                        command.Parameters.AddWithValue("@SessionEnd", DateTime.Now); // Set current time as session end

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Session end time updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No active session found to update.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating session end time: {ex.Message}");
            }
        }

        // READ: Get session data from the database
        public void GetSessionData(int classID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM ClassroomSessions WHERE ClassID = @ClassID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassID", classID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"ClassID: {reader["ClassID"]}, Teacher: {reader["TeacherName"]}, StudentCount: {reader["StudentCount"]}, SessionStart: {reader["SessionStart"]}, SessionEnd: {reader["SessionEnd"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No session data found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // UPDATE: Update session data (e.g., student count)
        public void UpdateSessionData(int classID, int newStudentCount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "UPDATE ClassroomSessions SET StudentCount = @StudentCount WHERE ClassID = @ClassID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassID", classID);
                        command.Parameters.AddWithValue("@StudentCount", newStudentCount);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Session data updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No session found to update.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating session data: {ex.Message}");
            }
        }

        // DELETE: Delete a session from the database
        public void DeleteClassroom(int classID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM ClassroomSessions WHERE ClassID = @ClassID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassID", classID);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Session deleted successfully.");
                        }
                        else
                        {
                            Console.WriteLine("No session found to delete.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while deleting session: {ex.Message}");
            }
        }

        public void StoreSessionInDatabase(int classID, string teacherName, int studentCount)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Query to insert a new session into the database
                    string query = "INSERT INTO ClassroomSessions (ClassID, TeacherName, StudentCount, SessionStart, SessionEnd) " +
                                   "VALUES (@ClassID, @TeacherName, @StudentCount, @SessionStart, @SessionEnd)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adding parameters to avoid SQL injection
                        command.Parameters.AddWithValue("@ClassID", classID);
                        command.Parameters.AddWithValue("@TeacherName", teacherName);
                        command.Parameters.AddWithValue("@StudentCount", studentCount);
                        command.Parameters.AddWithValue("@SessionStart", DateTime.Now); // Current time as start
                        command.Parameters.AddWithValue("@SessionEnd", DateTime.Now.AddMinutes(5)); // Set end time after 5 minutes

                        // Execute the insert query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Session data stored in database successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to store session data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while storing session data: {ex.Message}");
            }
        }

        public List<Classroom> GetAll()
        {
            throw new NotImplementedException();
        }

        public Classroom GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Classroom Add(Classroom classroom)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Classroom classroom)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
}


    
