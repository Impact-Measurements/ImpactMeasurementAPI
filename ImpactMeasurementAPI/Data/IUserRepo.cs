using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Data
{
    public interface IUserRepo
    {
        bool SaveChanges();
        
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);

        IEnumerable<User> GetAllUsers();
    }
}