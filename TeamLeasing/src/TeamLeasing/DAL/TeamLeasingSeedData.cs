using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamLeasing.Models;

namespace TeamLeasing.DAL
{
    public class TeamLeasingSeedData
    {
        private TeamLeasingContext _context;

        public TeamLeasingSeedData(TeamLeasingContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.Developers.Any())
            {


                List<Technology> technology = new List<Technology>()
                {
                    new Technology() {Name = "Java"},
                    new Technology() {Name = "C#"},
                    new Technology() {Name = "Python"},
                    new Technology() {Name = "Html/css"},
                    new Technology() {Name = "JavaScript"},
                    new Technology() {Name = "Ruby"},
                    new Technology() {Name = "SQL"},
                    new Technology() {Name = "C++"},
                };

                _context.AddRange(technology);
                _context.SaveChanges();

                List<Developer> developer = new List<Developer>()
                {
                    new Developer()
                    {
                        Name = "Jan",
                        BirthDate = DateTime.Parse("1992.11.20"),
                        City = "Kielce",
                        Email = "jan@com.pl",
                        Level = "junior",
                        Password = "jankowalski",
                        Experience = 2,
                        Province = "swietokrzyskie",
                        Surname = "Kowalski",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        Technology = technology[0]
                    },
                    new Developer()
                    {
                        Name = "Paweł",
                        BirthDate = DateTime.Parse("1972.01.20"),
                        City = "Warszawa",
                        Email = "jan@com.pl",
                        Level = "senior",
                        Password = "pawelnowak",
                        Experience = 2,
                        Province = "mazowieckie",
                        Surname = "Nowak",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        Technology =_context.Technologies.ToList()[3]
                    }
                };

                _context.Developers.AddRange(developer);
                _context.SaveChanges();
            }

        }
    }
}