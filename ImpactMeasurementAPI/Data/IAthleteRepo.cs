using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Data
{
    public interface IAthleteRepo
    {
        bool SaveChanges();
        
        Athlete GetUserById(int id);
        void CreateUser(Athlete user);
        void UpdateUser(Athlete user);

        IEnumerable<Athlete> GetAllUsers();
    }
}