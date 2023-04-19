using System;
using System.Collections.Generic;
using System.Linq;
using ImpactMeasurementAPI.Models;

namespace ImpactMeasurementAPI.Data
{
    public class AthleteRepo : IAthleteRepo
    {
        private readonly AppDbContext _context;

        public AthleteRepo(AppDbContext context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Athlete GetUserById(int id)
        {
            return _context.Athletes.FirstOrDefault(t => t.Id == id);
        }

        public void CreateUser(Athlete user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Athletes.Add(user);
        }

        public void UpdateUser(Athlete user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Athlete> GetAllUsers()
        {
            return _context.Athletes.ToList();
        }
    }
}