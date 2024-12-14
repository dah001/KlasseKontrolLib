using System.Collections.Generic;

namespace KlasseLib.Services
{
    public interface IClassRoom
    {
        // Starter en session for et klasseværelse
        void StartSession(int classID, string teacherName, int studentCount);

        // Stopper en session for et klasseværelse
        void StopSession(int classID);

        // Henter alle klasseværelser
        List<Classroom> GetAll();

        // Sletter et klasseværelse
        void Delete(int classID);
        
        // Henter et klasseværelse via ID
        Classroom GetById(int id);
        
        // Opdaterer et klasseværelse
        void Update(int id, Classroom classroom);
        
        // Tilføjer et nyt klasseværelse
        Classroom Add(Classroom classroom);
    }
}