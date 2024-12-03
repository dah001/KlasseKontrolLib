using Microsoft.VisualStudio.TestTools.UnitTesting;
using KlasseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasseLib.Tests
{
    [TestClass()]
    public class ClassroomTests
    {
        [TestMethod()]
            public void StartSession_ShouldActivateSession_WhenNotActive()
            {
            // Arrange
                var classroom = new Classroom
                {
                    ClassID = 101,
                    TeacherName = "Mrs. Smith",
                    StudentCount = 25,
                    SessionActive = false
                };

                // Act
                classroom.StartSession();

                // Assert
                Assert.IsTrue(classroom.SessionActive, "SessionActive should be true after starting the session.");
            }

        [TestMethod()]
        public void StopSession_ShouldDeactivateSession_WhenActive()
        {
            // Arrange
            var classroom = new Classroom
            {
                ClassID = 101,
                TeacherName = "Mrs. Smith",
                StudentCount = 25
            };

            classroom.StartSession(); // Sørg for, at sessionen er aktiv

            // Act
            classroom.StopSession();

            // Assert
            Assert.IsFalse(classroom.SessionActive, "SessionActive should be false after stopping the session.");
        }
    }
}