using KlasseLib.Services;
using Microsoft.AspNetCore.Mvc;
using KlasseWebService.Model;

namespace KlasseWebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomsController : ControllerBase
    {
        private readonly IClassRoom _classroomService;

        public ClassroomsController(IClassRoom classroomService)
        {
            _classroomService = classroomService;
        }
l
        [HttpGet]
        public IActionResult GetAllClassrooms()
        {
            var classrooms = _classroomService.GetAll();
            var classroomDtos = classrooms.Select(ClassRoomDTOConverter.Classroom2DTO).ToList();
            return Ok(classroomDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassroomById(int id)
        {
            try
            {
                var classroom = _classroomService.GetById(id);
                var classroomDto = ClassRoomDTOConverter.Classroom2DTO(classroom);
                return Ok(classroomDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Classroom with ID {id} not found.");
            }
        }

        [HttpPost]
        public IActionResult AddClassroom([FromBody] ClassRoomDTO classroomDto)
        {
            try
            {
                var classroom = ClassRoomDTOConverter.DTO2Classroom(classroomDto);
                var addedClassroom = _classroomService.Add(classroom);
                var addedClassroomDto = ClassRoomDTOConverter.Classroom2DTO(addedClassroom);
                return CreatedAtAction(nameof(GetClassroomById), new { id = addedClassroomDto.ClassID }, addedClassroomDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClassroom(int id, [FromBody] ClassRoomDTO classroomDto)
        {
            try
            {
                if (id != classroomDto.ClassID)
                {
                    return BadRequest("ID in URL and body do not match.");
                }

                var classroom = ClassRoomDTOConverter.DTO2Classroom(classroomDto);
                _classroomService.Update(id, classroom);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Classroom with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClassroom(int id)
        {
            try
            {
                _classroomService.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Classroom with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/start-session")]
        public IActionResult StartSession(int id, string teacherName, int studentCount)
        {
            if (string.IsNullOrWhiteSpace(teacherName))
            {
                return BadRequest("TeacherName cannot be empty or null.");
            }

            if (studentCount < 1)
            {
                return BadRequest("StudentCount must be greater than zero.");
            }

            try
            {
                _classroomService.StartSession(id, teacherName, studentCount);
                return Ok(new { Message = "Session started successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }



        [HttpPost("{id}/stop-session")]
        public IActionResult StopSession(int id)
        {
            try
            {
                _classroomService.StopSession(id);
                return Ok("Session stopped.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

  
}