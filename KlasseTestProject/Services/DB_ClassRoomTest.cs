using Microsoft.VisualStudio.TestTools.UnitTesting;
using KlasseLib.Services;
using KlasseLib;

namespace KlasseTestProject.Services
{
    [TestClass]
    public class DB_ClassRoomTest
    {
        private ClassRoomDb _classRoomDb;

        [TestInitialize]
        public void SetUp()
        {
            // Initialize the ClassRoomDB instance before each test
            _classRoomDb = new ClassRoomDb();
        }

        [TestMethod]
        public void AddClassroom_ShouldAddNewClassroom()
        {
            var newClassroom = new Classroom
            {
                TeacherName = "Mr. Smith",
                StudentCount = 25,
                SessionActive = true
            };

            // Act: Add new classroom
            var addedClassroom = _classRoomDb.Add(newClassroom);

            // Assert: Check if classroom was added
            Assert.IsTrue(addedClassroom.ClassID > 0, "Classroom should have a valid ClassID.");
        }

        [TestMethod]
        public void UpdateClassroom_ShouldUpdateClassroomDetails()
        {
            int classID = 101;
            var updatedClassroom = new Classroom
            {
                TeacherName = "Mr. Green",
                StudentCount = 40,
                SessionActive = true
            };

            // Act: Update classroom details
            _classRoomDb.Update(classID, updatedClassroom);

            // Assert: Verify the updated classroom details
            var classroom = _classRoomDb.GetById(classID);
            Assert.AreEqual("Mr. Green", classroom.TeacherName);
            Assert.AreEqual(40, classroom.StudentCount);
        }
l
        [TestMethod]
        public void DeleteClassroom_ShouldDeleteClassroom()
        {
            int classID = 101;

            // Act: Delete the classroom
            _classRoomDb.Delete(classID);

            // Assert: Check if the classroom is deleted
            var classroom = _classRoomDb.GetById(classID);
            Assert.IsNull(classroom, "Classroom should be deleted.");
        }

        [TestMethod]
        public void GetById_ShouldReturnClassroom()
        {
            int classID = 101;

            // Act: Get the classroom by ID
            var classroom = _classRoomDb.GetById(classID);

            // Assert: Verify classroom details
            Assert.IsNotNull(classroom);
            Assert.AreEqual(classID, classroom.ClassID);
        }

        [TestMethod]
        public void StartSession_ShouldStartSession()
        {
            int classID = 101;
            string teacherName = "Mr. Jensen";
            int studentCount = 30;

            // Act: Start the session
            _classRoomDb.StartSession(classID, teacherName, studentCount);

            // Assert: Check if session is active
            var classroom = _classRoomDb.GetById(classID);
            Assert.IsTrue(classroom.SessionActive);
        }

        [TestMethod]
        public void StopSession_ShouldStopSession()
        {
            int classID = 101;

            // Act: Stop the session
            _classRoomDb.StopSession(classID);

            // Assert: Verify session is inactive
            var classroom = _classRoomDb.GetById(classID);
            Assert.IsFalse(classroom.SessionActive);
        }
    }
}
