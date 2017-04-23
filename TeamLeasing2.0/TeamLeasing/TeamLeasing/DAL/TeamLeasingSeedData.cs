using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.Models;

namespace TeamLeasing.DAL
{
    public class TeamLeasingSeedData
    {
        private UserManager<User> _manager { get; set; }
        private TeamLeasingContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TeamLeasingSeedData(TeamLeasingContext context, UserManager<User> manager, RoleManager<IdentityRole> roleManager)
        {
            _manager = manager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            // _context.Technologies.RemoveRange(_context.Technologies.ToArray());
            //_context.Developers.RemoveRange(_context.Developers.ToArray());
            //_context.Jobs.RemoveRange(_context.Jobs.ToArray());
            //_context.DeveloperUsers.RemoveRange(_context.DeveloperUsers.ToArray());
            //_context.Users.RemoveRange(_context.Users.ToArray());


            //await Technologies().ContinueWith(async t => await DeveloperUser());
            await EmployeeUsers().ContinueWith(async t => await Jobs());

           /// await Jobs();
            // await Developers();
            //  await Jobs();
            // await DeveloperUsers();






        }

        private async Task DeveloperUser()
        {
            if (_manager.FindByEmailAsync("jaroslaw@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "jaroslaw@gmail.com",
                    UserName = "jarekKox",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "JAroslaw ",
                        BirthDate = new DateTime(1942, 03, 05),
                        City = "Lublin",
                        Level = Level.Regular,
                        Experience = 2,
                        Province = "małopolskie",
                        Surname = "Kowalski",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        IsFinishedUniversity = IsFinishedUniversity.Finished,
                        Technology = await _context.Technologies.FindAsync(2),
                        Photo = "/image/photo/profil-large.jpg",
                        UserId = _context.Users.Find(user.Id).Id,

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
                ;
            }
            if (_manager.FindByEmailAsync("Janek@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "Janek@gmail.com",
                    UserName = "janek123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Janek",
                        BirthDate = new DateTime(1992, 08, 05),
                        City = "Kielce",
                        Level = Level.Junior,
                        Experience = 2,
                        Province = "swietokrzyskie",
                        Surname = "Kowalski",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        IsFinishedUniversity = IsFinishedUniversity.Finished,
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "python"),
                        Photo = "/image/photo/profil-large.jpg",
                        UserId = _context.Users.Find(user.Id).Id,

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("JanKowalski@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "JanKowalski@gmail.com",
                    UserName = "janekKowalski123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Jan",
                        BirthDate = new DateTime(1992, 08, 05),
                        City = "Kielce",
                        Level = Level.Senior,
                        Experience = 2,
                        Province = "swietokrzyskie",
                        Surname = "Kowalski",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        IsFinishedUniversity = IsFinishedUniversity.NotFinished,
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "python"),
                        Photo = "/image/photo/profil-large.jpg",
                        UserId = _context.Users.Find(user.Id).Id,

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("Paweł@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "Paweł@gmail.com",
                    UserName = "Pawel123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Paweł",
                        BirthDate = new DateTime(1992, 08, 05),
                        City = "Warszawa",
                        Level = Level.Senior,
                        Experience = 2,
                        Province = "mazowieckie",
                        Surname = "Nowak",
                        University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
                        IsFinishedUniversity = IsFinishedUniversity.InProgress,
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "sql"),
                        Photo = "/image/photo/profil2.jpg",
                        UserId = _context.Users.Find(user.Id).Id,


                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("Marcin@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "Marcin@gmail.com",
                    UserName = "Marcin123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Marcin",
                        BirthDate = new DateTime(1992, 08, 05),
                        City = "Poznań",
                        Level = Level.Regular,
                        Experience = 7,
                        Province = "wielkopolskie",
                        Surname = "Turek",
                        University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
                        IsFinishedUniversity = IsFinishedUniversity.InProgress,
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "c#"),
                        Photo = "/image/photo/profil3.jpg",
                        UserId = _context.Users.Find(user.Id).Id,

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("Karol@com.pl").Result == null)
            {
                User user = new User()
                {
                    Email = "Karol@com.pl",
                    UserName = "Karol123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Karol",
                        BirthDate = new DateTime(1992, 08, 05),
                        City = "Opole",
                        Level = Level.Regular,
                        Experience = 4,
                        Province = "śląskie",
                        Surname = "Biracz",
                        University = " ",
                        IsFinishedUniversity = IsFinishedUniversity.NotFinished,
                        Photo = "/image/photo/profil4.jpg",
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "javascript"),
                        UserId = _context.Users.Find(user.Id).Id,


                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("Piotrek@com.pl").Result == null)
            {
                User user = new User()
                {
                    Email = "Piotrek@com.pl",
                    UserName = "Piotrek123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Piotrek",
                        BirthDate = new DateTime(1992, 08, 05),
                        City = "Opole",
                        Level = Level.Junior,

                        Experience = 4,
                        Province = "śląskie",
                        Surname = "Olak",
                        University = " ",
                        IsFinishedUniversity = IsFinishedUniversity.NotFinished,
                        Photo = "/image/photo/profil4.jpg",
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "html/css"),
                        UserId = _context.Users.Find(user.Id).Id,

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("Heniek@com.pl").Result == null)
            {
                User user = new User()
                {
                    Email = "Heniek@com.pl",
                    UserName = "Heniek123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Heniek",
                        BirthDate = new DateTime(1992, 08, 05),
                        City = "Opole",
                        Level = Level.Junior,
                        Experience = 4,
                        Province = "śląskie",
                        Surname = "Tomczak",
                        University = " Univerek",
                        IsFinishedUniversity = IsFinishedUniversity.Finished,
                        Photo = "/image/photo/profil4.jpg",
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "python"),
                        UserId = _context.Users.Find(user.Id).Id,

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
        }

  
        private async Task Technologies()
        {
            if (!_context.Technologies.Any())
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
                    new Technology() {Name = "C++"}
                };
                await _context.Technologies.AddRangeAsync(technology);
                await _context.SaveChangesAsync();
            }


        }

        private async Task EmployeeUsers()
        {
            if (_manager.FindByEmailAsync("vive@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "vive@gmail.com",
                    UserName = "ViveCompany",
                    PhoneNumber = "66526323",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    EmployeeUser emp = new EmployeeUser()
                    {
                        Name = "Władysław",
                        City = "Kielce",
                        Province = "świętokrzyskie",
                        Surname = "Sołkiewicz",
                        Company = "Vive",
                        UserId = _context.Users.Find(user.Id).Id,
                    };

                    await _context.EmployeeUsers.AddAsync(emp);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Employee.ToString());
                }
                ;
            }
            if (_manager.FindByEmailAsync("infover@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "infover@gmail.com",
                    UserName = "InfoverSa",
                    PhoneNumber = "66526323",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    EmployeeUser emp = new EmployeeUser()
                    {
                        Name = "Tomasz",
                        City = "Kraków",
                        Province = "świętokrzyskie",
                        Surname = "Bińczak",
                        Company = "infover",
                        UserId = _context.Users.Find(user.Id).Id,
                    };

                    await _context.EmployeeUsers.AddAsync(emp);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Employee.ToString());
                }
                ;
            }

        }



 
        private async Task Jobs()
        {
            if (!_context.Jobs.Any())
            {
                List<Job> jobs = new List<Job>()
                {
                    new Job()
                    {
                        Title = "Developer Javascript frotned",
                        Descritpion =
                            "Opis stanowiska:\r\n\r\nOsoba zatrudniona na stanowisku programisty będzie uczestniczyć w projektach tworzenia i dalszego rozwijania oprogramowania oraz opracowanych przez Xerrex systemów informatycznych związanych z archiwizacją, obiegiem oraz przetwarzaniem wersji papierowej dokumentów na postać cyfrową.",
                        Price = 5000,
                        IsHidden = false,
                        Technology = _context.Technologies.Where(t => t.Name.ToLower() == "javascript")
                            .ToList()
                            .FirstOrDefault(),
                        EmployeeUserId = 5

                    },
                    new Job()
                    {
                        Title = "Administracja znajomość sql",
                        Descritpion =
                            "Fideltronik to grupa firm w branży elektronicznej z polskim kapitałem posiadająca zakłady produkcyjne i biura inżynierskie w Suchej Beskidzkiej (siedziba główna), Krakowie, Gdańsku oraz w Szwecji i Norwegii. Realizujemy pełny zakres usług produkcyjnych i inżynierskich w zakresie elektroniki (EMS -Electronics Manufacturing Services), jak również tworzymy oprogramowanie wspierające zarządzanie wybranymi procesami w przedsiębiorstwach.\r\n\r\nFideltronik powstał w roku 1987 i dzięki strategii dynamicznego rozwoju jesteśmy obecnie największym polskim dostawcą EMS oraz mamy wysoką pozycję w Europie, a nasze produkty trafiają do odbiorców na całym świecie. Wśród naszych klientów są firmy globalne o znanych markach.",
                        Price = 8000,
                        IsHidden = false,
                        Technology = _context.Technologies.Where(t => t.Name.ToLower() == "sql")
                            .ToList()
                            .FirstOrDefault(),
                        EmployeeUserId = 5


                    },
                    new Job()
                    {
                        Title = "Młodszy programista baz danych",
                        Descritpion =
                            "Technology Center in Gdansk is a part of ​UTC Climate, Controls & Security\'s fire safety, security, building automation, heating, ventilation, air conditioning and refrigeration systems and services promote safer, smarter  and sustainable buildings. With over 200 employees we develop new products for different units from CCS group.\r\n\r\nLenel Systems, the global leader in advanced security systems, is the one of the unit supported in Gdansk. We serve top Fortune 100 companies to effectively and efficiently protect buildings, people and assets: ",
                        Price = 2211,
                        IsHidden = false,
                        Technology = _context.Technologies.Where(t => t.Name.ToLower() == "sql")
                            .ToList()
                            .FirstOrDefault(),
                        EmployeeUserId = 6

                    },
                    new Job()
                    {
                        Title = "Developer backend c#",
                        Descritpion =
                            "Technology Center in Gdansk is a part of ​UTC Climate, Controls & Security\'s fire safety, security, building automation, heating, ventilation, air conditioning and refrigeration systems and services promote safer, smarter  and sustainable buildings. With over 200 employees we develop new products for different units from CCS group.\r\n\r\nLenel Systems, the global leader in advanced security systems, is the one of the unit supported in Gdansk. We serve top Fortune 100 companies to effectively and efficiently protect buildings, people and assets: ",
                        Price = 4332,
                        IsHidden = false,
                        Technology = _context.Technologies.Where(t => t.Name.ToLower() == "c#")
                            .ToList()
                            .FirstOrDefault(),
                        EmployeeUserId = 6

                    },
                };

                await _context.Jobs.AddRangeAsync(jobs);
                await _context.SaveChangesAsync();
             
                
            }
        }

    }
}