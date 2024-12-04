using System.Data.SqlClient;

namespace KlasseLib.KlasseKontrolRepository
{
    public interface ISensorDB
    {
        public interface ISqlDatabaseConnection
        {
            SqlCommand CreateCommand(string commandText);
            // Eventuelle andre nødvendige metoder som Open, Close, etc.
        }

        // Metode til at tilføje en sensor til databasen
        void AddSensor(Sensor sensor);

        // Metode til at hente en sensor ved ID fra databasen
        Sensor GetSensorById(int id);

        // Metode til at opdatere en sensors værdi og tidsstempel
        Sensor UpdateSensor(int id, double newValue);

        // Metode til at slette en sensor fra databasen
        Sensor DeleteSensor(int id);
    }
}

