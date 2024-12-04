namespace KlasseWebService.Model
{
    public record ClassRoomDTO(
        int ClassID,
        string TeacherName,
        int StudentCount,
        bool SessionActive
    );
}