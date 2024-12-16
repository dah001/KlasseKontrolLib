using System.Collections.Generic;
using System.Threading.Tasks;

namespace KlasseLib.KlasseKontrolServices
{
    public interface ISensorDB
    {
        
        Task<List<Sensor>> GetAllSensorsAsync();
    }
}