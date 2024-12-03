namespace KlasseLib.KlasseKontrolRepository;

public interface IClassRoom
{
    // Start a new session
    void StartSession(int classID, string teacherName, int studentCount);

    // Stop a session
    void StopSession(int classID);

    // Get session data from the database
    void GetSessionData(int classID);

    // Update session data (e.g., student count)
    void UpdateSessionData(int classID, int newStudentCount);

    // Delete a session from the database
    void DeleteClassroom(int classID);

    // Store a session in the database
    void StoreSessionInDatabase(int classID, string teacherName, int studentCount);
    
}