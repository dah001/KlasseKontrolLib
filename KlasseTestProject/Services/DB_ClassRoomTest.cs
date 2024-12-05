using Microsoft.VisualStudio.TestTools.UnitTesting;
using KlasseLib.Services;
using KlasseLib;

namespace KlasseTestProject.Services
{
    [TestClass]
    public class DB_ClassRoomTest
    {
     [TestClass]
    public class ClassRoomDbTests
    {
        private ClassRoomDb _classRoomDb;

        [TestInitialize]
        public void SetUp()
        {
            // Initialize the ClassRoomDb instance before each test
            _classRoomDb = new ClassRoomDb();
        }

        [TestMethod]
        public void AddClassroom_ShouldAddNewClassroom()
        {
            // Arrange
            var classroom = new Classroom
            {
                TeacherName = "Mr. Smith",
                StudentCount = 30,
                SessionActive = true
            };

            // Act
            var addedClassroom = _classRoomDb.Add(classroom);

            // Assert
            Assert.IsNotNull(addedClassroom.ClassID); // Ensure ClassID is set (auto-generated)
            Assert.AreEqual("Mr. Smith", addedClassroom.TeacherName);
            Assert.AreEqual(30, addedClassroom.StudentCount);
            Assert.IsTrue(addedClassroom.SessionActive);
        }

        [TestMethod]
        public void UpdateClassroom_ShouldUpdateClassroomDetails()
        {
            // Arrange: Create and add a classroom
            var classroom = new Classroom
            {
                TeacherName = "Ms. Blue",
                StudentCount = 18,
                SessionActive = false
            };

            var addedClassroom = _classRoomDb.Add(classroom);

            // Act: Update the classroom details
            addedClassroom.TeacherName = "Ms. Yellow";
            addedClassroom.StudentCount = 22;
            addedClassroom.SessionActive = true;

            _classRoomDb.Update(addedClassroom.ClassID, addedClassroom);

            // Assert: Ensure the classroom is updated
            var updatedClassroom = _classRoomDb.GetById(addedClassroom.ClassID);
            Assert.AreEqual("Ms. Yellow", updatedClassroom.TeacherName);
            Assert.AreEqual(22, updatedClassroom.StudentCount);
            Assert.IsTrue(updatedClassroom.SessionActive);
        }
        
        [TestMethod]
        public void DeleteClassroom_ShouldDeleteClassroom()
        {
            // Arrange: Create and add a classroom
            var classroom = new Classroom
            {
                TeacherName = "Mr. Jones",
                StudentCount = 20,
                SessionActive = true
            };

            // Add the classroom to the database
            var addedClassroom = _classRoomDb.Add(classroom);

            // Act: Delete the classroom using its ClassID
            _classRoomDb.Delete(addedClassroom.ClassID);

            // Assert: Ensure that the classroom is deleted by trying to fetch it
            Assert.ThrowsException<KeyNotFoundException>(() => _classRoomDb.GetById(addedClassroom.ClassID));
        }
        
        [TestMethod]
        public void GetById_ShouldReturnClassroom()
        {
            // Arrange: Create and add a classroom
            var classroom = new Classroom
            {
                TeacherName = "Mr. Black",
                StudentCount = 20,
                SessionActive = false
            };

            var addedClassroom = _classRoomDb.Add(classroom);

            // Act: Get the classroom by ID
            var fetchedClassroom = _classRoomDb.GetById(addedClassroom.ClassID);

            // Assert: Ensure the returned classroom matches the added classroom
            Assert.AreEqual(addedClassroom.ClassID, fetchedClassroom.ClassID);
            Assert.AreEqual("Mr. Black", fetchedClassroom.TeacherName);
            Assert.AreEqual(20, fetchedClassroom.StudentCount);
            Assert.IsFalse(fetchedClassroom.SessionActive);
        }

        [TestMethod]
        public void StartSession_ShouldStartSession()
        {
            // Arrange: Create and add a classroom
            var classroom = new Classroom
            {
                TeacherName = "Mr. White",
                StudentCount = 15,
                SessionActive = false
            };

            var addedClassroom = _classRoomDb.Add(classroom);

            // Act: Start the session
            _classRoomDb.StartSession(addedClassroom.ClassID, "Mr. White", 15);

            // Assert: Ensure the session is active
            var updatedClassroom = _classRoomDb.GetById(addedClassroom.ClassID);
            Assert.IsTrue(updatedClassroom.SessionActive);
            Assert.AreEqual("Mr. White", updatedClassroom.TeacherName);
            Assert.AreEqual(15, updatedClassroom.StudentCount);
        }

        [TestMethod]
        public void StopSession_ShouldStopSession()
        {
            // Arrange: Create and add a classroom with an active session
            var classroom = new Classroom
            {
                TeacherName = "Mr. Gray",
                StudentCount = 10,
                SessionActive = true
            };

            var addedClassroom = _classRoomDb.Add(classroom);

            // Act: Stop the session
            _classRoomDb.StopSession(addedClassroom.ClassID);

            // Assert: Ensure the session is stopped (inactive)
            var updatedClassroom = _classRoomDb.GetById(addedClassroom.ClassID);
            Assert.IsFalse(updatedClassroom.SessionActive);
        }

        [TestMethod]
        public void GetAll_ShouldReturnAllClassrooms()
        {
            // Act: Get all classrooms
            List<Classroom> classrooms = _classRoomDb.GetAll();

            // Assert: Check if the list of classrooms is not empty
            Assert.IsTrue(classrooms.Count > 0, "There should be at least one classroom.");
        }
    }

    }
}