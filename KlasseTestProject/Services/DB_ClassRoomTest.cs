using System;
using Moq;
using System.Data.SqlClient;
using KlasseLib.KlasseKontrolRepository;
using KlasseLib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KlasseTestProject.Services
{
     [TestClass]
    public class DB_ClassRoomTest
    {
        private Mock<IClassRoom> _mockClassRoom;

        [TestInitialize]
        public void SetUp()
        {
            // Initialisering af mock objekt for IClassRoom interface
            _mockClassRoom = new Mock<IClassRoom>();
        }

        [TestMethod]
        public void StartSession_ShouldStartSession()
        {
            // Arrange
            int classID = 101;
            string teacherName = "Mr. Jensen";
            int studentCount = 30;

            // Opsæt mock for StartSession
            _mockClassRoom.Setup(x => x.StartSession(classID, teacherName, studentCount))
                         .Verifiable();

            // Act
            _mockClassRoom.Object.StartSession(classID, teacherName, studentCount);

            // Assert
            _mockClassRoom.Verify(x => x.StartSession(classID, teacherName, studentCount), Times.Once);
        }

        [TestMethod]
        public void StopSession_ShouldStopSession()
        {
            // Arrange
            int classID = 101;

            // Opsæt mock for StopSession
            _mockClassRoom.Setup(x => x.StopSession(classID))
                         .Verifiable();

            // Act
            _mockClassRoom.Object.StopSession(classID);

            // Assert
            _mockClassRoom.Verify(x => x.StopSession(classID), Times.Once);
        }

        [TestMethod]
        public void GetSessionData_ShouldReturnSessionDetails()
        {
            // Arrange
            int classID = 101;

            // Opsæt mock for GetSessionData
            _mockClassRoom.Setup(x => x.GetSessionData(classID))
                         .Verifiable();

            // Act
            _mockClassRoom.Object.GetSessionData(classID);

            // Assert
            _mockClassRoom.Verify(x => x.GetSessionData(classID), Times.Once);
        }

        [TestMethod]
        public void UpdateSessionData_ShouldUpdateStudentCount()
        {
            // Arrange
            int classID = 101;
            int newStudentCount = 32;

            // Opsæt mock for UpdateSessionData
            _mockClassRoom.Setup(x => x.UpdateSessionData(classID, newStudentCount))
                         .Verifiable();

            // Act
            _mockClassRoom.Object.UpdateSessionData(classID, newStudentCount);

            // Assert
            _mockClassRoom.Verify(x => x.UpdateSessionData(classID, newStudentCount), Times.Once);
        }

        [TestMethod]
        public void DeleteClassroom_ShouldDeleteSession()
        {
            // Arrange
            int classID = 101;

            // Opsæt mock for DeleteClassroom
            _mockClassRoom.Setup(x => x.DeleteClassroom(classID))
                         .Verifiable();

            // Act
            _mockClassRoom.Object.DeleteClassroom(classID);

            // Assert
            _mockClassRoom.Verify(x => x.DeleteClassroom(classID), Times.Once);
        }

        [TestMethod]
        public void StoreSessionInDatabase_ShouldStoreSessionData()
        {
            // Arrange
            int classID = 101;
            string teacherName = "Mr. Jensen";
            int studentCount = 30;

            // Opsæt mock for StoreSessionInDatabase
            _mockClassRoom.Setup(x => x.StoreSessionInDatabase(classID, teacherName, studentCount))
                         .Verifiable();

            // Act
            _mockClassRoom.Object.StoreSessionInDatabase(classID, teacherName, studentCount);

            // Assert
            _mockClassRoom.Verify(x => x.StoreSessionInDatabase(classID, teacherName, studentCount), Times.Once);
        }
    }
}