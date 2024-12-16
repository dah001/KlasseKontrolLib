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
        /// <summary>
        /// Tester StartSession-metoden for at sikre, at en session bliver aktiveret, når den ikke allerede er aktiv.
        /// </summary>
        [TestMethod()]
        public void StartSession_ShouldActivateSession_WhenNotActive()
        {
            // Arrange: Initialiser et Classroom-objekt med en inaktiv session
            var classroom = new Classroom
            {
                ClassID = 101,
                TeacherName = "Mrs. Smith",
                StudentCount = 25,
                SessionActive = false
            };

            // Act: Aktiver sessionen
            classroom.StartSession();

            // Assert: Bekræft, at sessionen nu er aktiv
            Assert.IsTrue(classroom.SessionActive, "SessionActive should be true after starting the session.");
        }

        /// <summary>
        /// Tester StopSession-metoden for at sikre, at en session bliver deaktiveret, når den er aktiv.
        /// </summary>
        [TestMethod()]
        public void StopSession_ShouldDeactivateSession_WhenActive()
        {
            // Arrange: Initialiser et Classroom-objekt og aktiver sessionen
            var classroom = new Classroom
            {
                ClassID = 101,
                TeacherName = "Mrs. Smith",
                StudentCount = 25
            };

            classroom.StartSession(); // Sørg for, at sessionen er aktiv

            // Act: Deaktiver sessionen
            classroom.StopSession();

            // Assert: Bekræft, at sessionen nu er inaktiv
            Assert.IsFalse(classroom.SessionActive, "SessionActive should be false after stopping the session.");
        }
    }
}