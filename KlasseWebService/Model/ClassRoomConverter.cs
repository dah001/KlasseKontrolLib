using KlasseLib;

namespace KlasseWebService.Model
{
    public static class ClassRoomDTOConverter
    {
        // Converts a ClassRoomDTO to a Classroom entity
        public static Classroom DTO2Classroom(ClassRoomDTO dto)
        {
            return new Classroom(
                dto.ClassID, 
                dto.TeacherName, 
                dto.StudentCount, 
                dto.SessionActive
            );
        }

        // Converts a Classroom entity to a ClassRoomDTO
        public static ClassRoomDTO Classroom2DTO(Classroom classroom)
        {
            return new ClassRoomDTO(
                classroom.ClassID, 
                classroom.TeacherName, 
                classroom.StudentCount, 
                classroom.SessionActive
            );
        }
    }
    
    //a
}