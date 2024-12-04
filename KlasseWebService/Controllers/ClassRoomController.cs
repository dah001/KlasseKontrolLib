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
    }
}
