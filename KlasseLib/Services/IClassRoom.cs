namespace KlasseLib.Services
{
    public interface IClassRoom
    {
        List<Classroom> GetAll();
        Classroom GetById(int id);
        Classroom Add(Classroom classroom);
        void Update(int id, Classroom classroom);
        void Delete(int id);
    }
}