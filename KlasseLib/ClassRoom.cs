namespace KlasseLib;

public class Classroom
{
    public string ClassID { get; set; }
    public string TeacherName { get; set; }
    public int StudentCount { get; set; }

    public Classroom(string classID, string teacherName, int studentCount)
    {
        ClassID = classID;
        TeacherName = teacherName;
        StudentCount = studentCount;
    }
}
