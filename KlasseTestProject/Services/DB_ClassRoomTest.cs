using Microsoft.VisualStudio.TestTools.UnitTesting;
using KlasseLib.Services;
using KlasseLib;

namespace KlasseTestProject.Services
{
    /// <summary>
    /// Testklasse for ClassRoomDb service.
    /// </summary>
    [TestClass]
    public class DB_ClassRoomTest
    {
        private ClassRoomDb _classRoomDb;

        /// <summary>
        /// Initialiserer ClassRoomDb før hver test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            // Initialize the ClassRoomDB instance before each test
            _classRoomDb = new ClassRoomDb();
        }

        /// <summary>
        /// Tester om en ny klasseværelse kan tilføjes.
        /// </summary>
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

        /// <summary>
        /// Tester om klasseværelsesdetaljer kan opdateres.
        /// </summary>
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

        /// <summary>
        /// Tester om et klasseværelse kan slettes.
        /// </summary>
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

        /// <summary>
        /// Tester om et klasseværelse kan hentes ved ID.
        /// </summary>
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

        /// <summary>
        /// Tester om en session kan startes.
        /// </summary>
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

        /// <summary>
        /// Tester om en session kan stoppes.
        /// </summary>
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
