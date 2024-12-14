namespace KlasseLib;

public class Classroom
{
    public int ClassID { get; set; }
    public string TeacherName { get; set; }
    public int StudentCount { get; set; }
    public bool SessionActive { get; set; }

    public Classroom(int classID, string teacherName, int studentCount, bool sessionActive)
    {
        ClassID = classID;
        TeacherName = teacherName;
        StudentCount = studentCount;
        SessionActive = sessionActive;
    }

    public Classroom()
    {

    }

    // Starter sessionen
    public void StartSession()
    {
        if (SessionActive)
        {
            Console.WriteLine("Session is already active.");
            return;
        }

        SessionActive = true;
        Console.WriteLine($"Session started for class {ClassID} with teacher {TeacherName}.");
    }

    // Stopper sessionen
    public void StopSession()
    {
        if (!SessionActive)
        {
            Console.WriteLine("Session is not active.");
            return;
        }

        SessionActive = false;
        Console.WriteLine($"Session stopped for class {ClassID}.");
    }

    public override string ToString()
    {
        return $"{{{nameof(ClassID)}={ClassID.ToString()}, {nameof(TeacherName)}={TeacherName}, {nameof(StudentCount)}={StudentCount.ToString()}, {nameof(SessionActive)}={SessionActive.ToString()}}}";
    }
}
