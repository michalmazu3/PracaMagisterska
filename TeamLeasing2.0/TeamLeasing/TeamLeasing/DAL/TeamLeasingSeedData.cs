﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            //  _context.Technologies.RemoveRange(_context.Technologies.ToArray());
            _context.Developers.RemoveRange(_context.Developers.ToArray());
            _context.Jobs.RemoveRange(_context.Jobs.ToArray());
            _context.DeveloperUsers.RemoveRange(_context.DeveloperUsers.ToArray());
            _context.Users.RemoveRange(_context.Users.ToArray());
            await _context.SaveChangesAsync().ContinueWith(async t =>
            {
                //   await Technologies();
                // await Developers();
                //  await Jobs();
                // await DeveloperUsers();
                await User();

            });

           

        }

        private async Task User()
        {
         
            DeveloperUser dev = new DeveloperUser()
                {
                    Name = "Jan User",
                    BirthDate = new DateTime(1992, 08, 05),
                    City = "Kielce",
                    Level = Level.Junior,
                    Experience = 2,
                    Province = "swietokrzyskie",
                    Surname = "Kowalski",
                    University = "Politechnika świetokrzyska II stopien Informatyka",
                    IsFinishedUniversity = IsFinishedUniversity.Finished,
                    // Technology = await _context.Technologies.FindAsync(162),
                    TechnologyId = 2,
                    Photo = "/image/photo/profil-large.jpg"

                };
            User user = new User()
            {
                Email = "michal1@gmail.com",
                UserName = "michal1",
                DeveloperUser = dev
            };
            var result = await _manager.CreateAsync(user, "Michal123$");
            await _context.DeveloperUsers.AddAsync(dev);

            //DeveloperUser dev1 = new DeveloperUser()
            //{
            //    Name = "Jan",
            //    BirthDate = new DateTime(1992, 08, 05),
            //    City = "Kielce",
            //    Level = Level.Junior,
            //    Experience = 2,
            //    Province = "swietokrzyskie",
            //    Surname = "Kowalski",
            //    University = "Politechnika świetokrzyska II stopien Informatyka",
            //    IsFinishedUniversity = IsFinishedUniversity.Finished,
            //    TechnologyId = 1,
            //    Photo = "/image/photo/profil-large.jpg"
            //};
            //User user1 = new User()
            //{
            //    Email = "jan@com.pl",
            //    UserName = "jan",
            //    DeveloperUser = dev1,

            //};
            //await _manager.CreateAsync(user1, "Jankowalski123$");
            //await _context.DeveloperUsers.AddAsync(dev);

            //DeveloperUser dev2 = new DeveloperUser()
            //{
            //    Name = "Paweł",
            //    BirthDate = new DateTime(1992, 08, 05),
            //    City = "Warszawa",
            //    Level = Level.Senior,
            //    Experience = 2,
            //    Province = "mazowieckie",
            //    Surname = "Nowak",
            //    University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
            //    IsFinishedUniversity = IsFinishedUniversity.InProgress,
            //    TechnologyId = 4,
            //    Photo = "/image/photo/profil2.jpg"

            //};
            //User  user2 = new User()
            //{
            //    Email = "pawel@com.pl",
            //    UserName = "pawel",
            //    DeveloperUser = dev2,

            //};
            //await _manager.CreateAsync(user2, "Qaz123#");
            //await _context.DeveloperUsers.AddAsync(dev);

            //dev = new DeveloperUser()
            //{
            //    Name = "Marcin",
            //    BirthDate = new DateTime(1992, 08, 05),
            //    City = "Poznań",
            //     Level = Level.Regular,
            //     Experience = 7,
            //    Province = "wielkopolskie",
            //    Surname = "Turek",
            //    University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
            //    IsFinishedUniversity = IsFinishedUniversity.InProgress,
            //    TechnologyId = 5,
            //    Photo = "/image/photo/profil3.jpg"
            //};
            //await _manager.CreateAsync(new User()
            //{
            //    Email = "Marcin@com.pl",
            //    UserName = "marcinturek",
            //    DeveloperUser = dev,

            //}, "Marcinturek123$");
            //await _context.DeveloperUsers.AddAsync(dev);
            //dev = new DeveloperUser()
            //{
            //    Name = "Karol",
            //    BirthDate = new DateTime(1992, 08, 05),
            //    City = "Opole",
            //    Email = "Karol@com.pl",
            //    Level = Level.Regular,
            //    Password = "karollolek",
            //    Experience = 4,
            //    Province = "śląskie",
            //    Surname = "Biracz",
            //    University = " ",
            //    IsFinishedUniversity = IsFinishedUniversity.NotFinished,
            //    Photo = "/image/photo/profil4.jpg",
            //    Technology = _context.Technologies.Where(t => t.Name.ToLower() == "javascript")
            //            .ToList()
            //            .FirstOrDefault()
            //};
            //await _manager.CreateAsync(new User()
            //{
            //    Email = "jan@com.pl",
            //    UserName = "jan",
            //    DeveloperUser = dev,

            //}, "Jankowalski123$");
            //await _context.DeveloperUsers.AddAsync(dev);
            //dev = new DeveloperUser()
            //{
            //    Name = "Piotrek",
            //    BirthDate = new DateTime(1992, 08, 05),
            //    City = "Opole",
            //    Email = "Karol@com.pl",
            //    Level = Level.Junior,
            //    Password = "karollolek",
            //    Experience = 4,
            //    Province = "śląskie",
            //    Surname = "Olak",
            //    University = " ",
            //    IsFinishedUniversity = IsFinishedUniversity.NotFinished,
            //    Photo = "/image/photo/profil4.jpg",
            //    Technology = _context.Technologies.Where(t => t.Name.ToLower() == "python").ToList().FirstOrDefault()
            //};
            //await _manager.CreateAsync(new User()
            //{
            //    Email = "jan@com.pl",
            //    UserName = "jan",
            //    DeveloperUser = dev,

            //}, "Jankowalski123$");
            //await _context.DeveloperUsers.AddAsync(dev);
            //dev = new DeveloperUser()
            //{
            //    Name = "Heniek",
            //    BirthDate = new DateTime(1992, 08, 05),
            //    City = "Opole",
            //    Email = "Karol@com.pl",
            //    Level = Level.Junior,
            //    Password = "karollolek",
            //    Experience = 4,
            //    Province = "śląskie",
            //    Surname = "Tomczak",
            //    University = " Univerek",
            //    IsFinishedUniversity = IsFinishedUniversity.Finished,
            //    Photo = "Uniwerek",
            //    Technology = _context.Technologies.Where(t => t.Name.ToLower() == "python").ToList().FirstOrDefault()
            //};
            //await _manager.CreateAsync(new User()
            //{
            //    Email = "jan@com.pl",
            //    UserName = "jan",
            //    DeveloperUser = dev,

            //}, "Jankowalski123$");
            //await _context.DeveloperUsers.AddAsync(dev);

            await _context.SaveChangesAsync();

        }

        private async Task DeveloperUsers()
        {
            List<DeveloperUser> developerUsers = new List<DeveloperUser>()
                {
                    new DeveloperUser()
                    {
                        Name = "Jan User",
                        BirthDate = new DateTime(1992,08,05),
                        City = "Kielce",
                        Level = Level.Junior,
                        Experience = 2,
                        Province = "swietokrzyskie",
                        Surname = "Kowalski",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        IsFinishedUniversity = IsFinishedUniversity.Finished,
                       // Technology = _context.Technologies.ToList()[0],
                        Photo =  "/image/photo/profil-large.jpg"

                    },
                    new DeveloperUser()
                    {
                        Name = "Paweł User",
                        BirthDate = new DateTime(1992,08,05),
                        City = "Warszawa",
                        Level = Level.Senior,
                        Experience = 2,
                        Province = "mazowieckie",
                        Surname = "Nowak",
                        University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
                        IsFinishedUniversity = IsFinishedUniversity.InProgress,
                      //  Technology = _context.Technologies.ToList()[3],
                        Photo =  "/image/photo/profil2.jpg"

                    },
                };

            await _context.AddRangeAsync(developerUsers);
            await _context.SaveChangesAsync();
        }
        private async Task Technologies()
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

        private async Task Developers()
        {

            List<Developer> developer = new List<Developer>()
                {
                    new Developer()
                    {
                        Name = "Jan",
                        BirthDate = new DateTime(1992,08,05),
                        City = "Kielce",
                        Email = "jan@com.pl",
                        Level = Level.Junior,
                        Password = "jankowalski",
                        Experience = 2,
                        Province = "swietokrzyskie",
                        Surname = "Kowalski",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        IsFinishedUniversity = IsFinishedUniversity.Finished,
                        Technology = _context.Technologies.ToList()[0],
                        Photo =  "/image/photo/profil-large.jpg"

                    },
                    new Developer()
                    {
                        Name = "Paweł",
                        BirthDate = new DateTime(1992,08,05),
                        City = "Warszawa",
                        Email = "jan@com.pl",
                        Level = Level.Senior,
                        Password = "pawelnowak",
                        Experience = 2,
                        Province = "mazowieckie",
                        Surname = "Nowak",
                        University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
                        IsFinishedUniversity = IsFinishedUniversity.InProgress,
                        Technology = _context.Technologies.ToList()[3],
                        Photo =  "/image/photo/profil2.jpg"

                    },
                      new Developer()
                    {
                        Name = "Marcin",
                        BirthDate = new DateTime(1992,08,05),
                        City = "Poznań",
                        Email = "Marcin@com.pl",
                        Level = Level.Regular,
                        Password = "marcinturek",
                        Experience = 7,
                        Province = "wielkopolskie",
                        Surname = "Turek",
                        University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
                        IsFinishedUniversity = IsFinishedUniversity.InProgress,
                        Technology = _context.Technologies.Where(t=>t.Name.ToLower()=="sql").ToList().FirstOrDefault(),
                        Photo =  "/image/photo/profil3.jpg"
                      },
                        new Developer()
                    {
                        Name = "Karol",
                        BirthDate = new DateTime(1992,08,05),
                        City = "Opole",
                        Email = "Karol@com.pl",
                        Level = Level.Regular,
                        Password = "karollolek",
                        Experience = 4,
                        Province = "śląskie",
                        Surname = "Biracz",
                        University = " ",
                        IsFinishedUniversity = IsFinishedUniversity.NotFinished,
                        Photo =  "/image/photo/profil4.jpg",
                        Technology = _context.Technologies.Where(t=>t.Name.ToLower()=="javascript").ToList().FirstOrDefault()
                    },
                    new Developer(){  Name = "Piotrek",
                        BirthDate = new DateTime(1992,08,05),
                        City = "Opole",
                        Email = "Karol@com.pl",
                        Level = Level.Junior,
                        Password = "karollolek",
                        Experience = 4,
                        Province = "śląskie",
                        Surname = "Olak",
                        University = " ",
                        IsFinishedUniversity = IsFinishedUniversity.NotFinished,
                        Photo =  "/image/photo/profil4.jpg",
                        Technology = _context.Technologies.Where(t=>t.Name.ToLower()=="python").ToList().FirstOrDefault()
                    },
                    new Developer(){  Name = "Heniek",
                        BirthDate = new DateTime(1992,08,05),
                        City = "Opole",
                        Email = "Karol@com.pl",
                        Level = Level.Junior,
                        Password = "karollolek",
                        Experience = 4,
                        Province = "śląskie",
                        Surname = "Tomczak",
                        University = " Univerek",
                        IsFinishedUniversity = IsFinishedUniversity.Finished,
                        Photo =  "Uniwerek",
                        Technology = _context.Technologies.Where(t=>t.Name.ToLower()=="python").ToList().FirstOrDefault()
                    }
                };
            await _context.Developers.AddRangeAsync(developer);
            await _context.SaveChangesAsync();
        }

        private async Task Jobs()
        {

            List<Job> jobs = new List<Job>()
            {
                new Job()
                {
                    Title = "Developer Javascript frotned",
                    Descritpion = "Opis stanowiska:\r\n\r\nOsoba zatrudniona na stanowisku programisty będzie uczestniczyć w projektach tworzenia i dalszego rozwijania oprogramowania oraz opracowanych przez Xerrex systemów informatycznych związanych z archiwizacją, obiegiem oraz przetwarzaniem wersji papierowej dokumentów na postać cyfrową.",
                    Price = 5000,
                    IsHidden = false,
                   Technology = _context.Technologies.Where(t=>t.Name.ToLower()=="javascript").ToList().FirstOrDefault()

                },
                 new Job()
                {
                     Title = "Administracja znajomość sql",
                     Descritpion = "Fideltronik to grupa firm w branży elektronicznej z polskim kapitałem posiadająca zakłady produkcyjne i biura inżynierskie w Suchej Beskidzkiej (siedziba główna), Krakowie, Gdańsku oraz w Szwecji i Norwegii. Realizujemy pełny zakres usług produkcyjnych i inżynierskich w zakresie elektroniki (EMS -Electronics Manufacturing Services), jak również tworzymy oprogramowanie wspierające zarządzanie wybranymi procesami w przedsiębiorstwach.\r\n\r\nFideltronik powstał w roku 1987 i dzięki strategii dynamicznego rozwoju jesteśmy obecnie największym polskim dostawcą EMS oraz mamy wysoką pozycję w Europie, a nasze produkty trafiają do odbiorców na całym świecie. Wśród naszych klientów są firmy globalne o znanych markach.",
                     Price = 8000,
                    IsHidden = false,
                   Technology = _context.Technologies.Where(t=>t.Name.ToLower()=="sql").ToList().FirstOrDefault()

                },
                  new Job()
                {
                      Title = "Młodszy programista baz danych",
                      Descritpion = "Technology Center in Gdansk is a part of ​UTC Climate, Controls & Security\'s fire safety, security, building automation, heating, ventilation, air conditioning and refrigeration systems and services promote safer, smarter  and sustainable buildings. With over 200 employees we develop new products for different units from CCS group.\r\n\r\nLenel Systems, the global leader in advanced security systems, is the one of the unit supported in Gdansk. We serve top Fortune 100 companies to effectively and efficiently protect buildings, people and assets: ",
                     Price = 2211,
                    IsHidden = false,
                   Technology = _context.Technologies.Where(t=>t.Name.ToLower()=="sql").ToList().FirstOrDefault()

                },
                   new Job()
                {     Title = "Developer backend c#",
                      Descritpion = "Technology Center in Gdansk is a part of ​UTC Climate, Controls & Security\'s fire safety, security, building automation, heating, ventilation, air conditioning and refrigeration systems and services promote safer, smarter  and sustainable buildings. With over 200 employees we develop new products for different units from CCS group.\r\n\r\nLenel Systems, the global leader in advanced security systems, is the one of the unit supported in Gdansk. We serve top Fortune 100 companies to effectively and efficiently protect buildings, people and assets: ",
                     Price = 4332,
                    IsHidden = false,
                   Technology = _context.Technologies.Where(t=>t.Name.ToLower()=="c#").ToList().FirstOrDefault()

                },
            };

            await _context.Jobs.AddRangeAsync(jobs);
            await _context.SaveChangesAsync();
        }

    }
}