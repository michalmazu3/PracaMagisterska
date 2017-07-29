using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamLeasing.Infrastructure;
using TeamLeasing.Infrastructure.Extension;
using TeamLeasing.Models;

namespace TeamLeasing.DAL
{
    public class TeamLeasingSeedData
    {
        private OptimizedDbManager _manager { get; set; }
        private TeamLeasingContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TeamLeasingSeedData(TeamLeasingContext context, OptimizedDbManager manager, RoleManager<IdentityRole> roleManager)
        {
            _manager = manager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            try
            {
                var tasks = Enumerable.Range(0, 10000).Select(async p =>
                {
                    var result = await _context.DeveloperUsers.ToListAsync();
                    Debug.WriteLine(result);

                });
                Task.WhenAll(tasks).Wait();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }




            // _context.Technologies.RemoveRange(_context.Technologies.ToArray());
            //_context.Developers.RemoveRange(_context.Developers.ToArray());
            //_context.Jobs.RemoveRange(_context.Jobs.ToArray());
            //_context.DeveloperUsers.RemoveRange(_context.DeveloperUsers.ToArray());
            //_context.EmployeeUsers.RemoveRange(_context.EmployeeUsers.ToArray());
            //_context.Users.RemoveRange(_context.Users.ToArray());
            await Technologies().ContinueWith(async t =>
            {
                await DeveloperUser();
                await EmployeeUsers();
                //.ContinueWith(async tt => await Jobs())
                //.ContinueWith(async v => await DeveloperUserJob());

            });
        }
      
        private async Task DeveloperUserJob()
        {
            if (!_context.DeveloperUserJob.Any())
            {
                List<DeveloperUserJob> developerUserJobs = new List<DeveloperUserJob>();
                developerUserJobs.Add(new DeveloperUserJob()
                {
                    DeveloperUser = _manager.GetDeveloperUser(w => w.Id == 25).Result.FirstOrDefault(),
                    Job = _manager.GetJob(w => w.Id == 36).Result.FirstOrDefault(),
                });
                developerUserJobs.Add(new DeveloperUserJob()
                {
                    DeveloperUser = _manager.GetDeveloperUser(w => w.Id == 25).Result.FirstOrDefault(),
                    Job = _manager.GetJob(w => w.Id == 37).Result.FirstOrDefault(),
                });
                developerUserJobs.Add(new DeveloperUserJob()
                {
                    DeveloperUser = _manager.GetDeveloperUser(w => w.Id == 25).Result.FirstOrDefault(),
                    Job = _manager.GetJob(w => w.Id == 38).Result.FirstOrDefault(),
                });
                developerUserJobs.Add(new DeveloperUserJob()
                {
                    DeveloperUser = _manager.GetDeveloperUser(w => w.Id == 26).Result.FirstOrDefault(),
                    Job = _manager.GetJob(w => w.Id == 36).Result.FirstOrDefault(),
                });
                developerUserJobs.Add(new DeveloperUserJob()
                {
                    DeveloperUser = _manager.GetDeveloperUser(w => w.Id == 26).Result.FirstOrDefault(),
                    Job = _manager.GetJob(w => w.Id == 37).Result.FirstOrDefault(),
                });
                developerUserJobs.Add(new DeveloperUserJob()
                {
                    DeveloperUser = _manager.GetDeveloperUser(w => w.Id == 26).Result.FirstOrDefault(),
                    Job = _manager.GetJob(w => w.Id == 38).Result.FirstOrDefault(),
                });

                await _context.DeveloperUserJob.AddRangeAsync(developerUserJobs);
                await _context.SaveChangesAsync();
            }
        }

private async Task DeveloperUser()
{
    if (_manager.FindByEmailAsync("jaroslaw@gmail.com").Result == null)
    {
        User user = new User()
        {
            Email = "jaroslaw@gmail.com",
            UserName = "jarekKow",
            PhoneNumber = "2342342523",
        };
        var result = await _manager.CreateAsync(user, "Qazqaz12#");
        if (result.Succeeded)
        {
            DeveloperUser dev = new DeveloperUser()
            {
                Name = "Jaroslaw ",
                BirthDate = new DateTime(1942, 03, 05),
                City = "Lublin",
                Level = Enums.Level.Regular,
                Experience = 2,
                Province = Enums.Province.Lubelskie.ToString(),
                Surname = "Kowalski",
                University = "Politechnika świetokrzyska II stopien Informatyka",
                IsFinishedUniversity = Enums.IsFinishedUniversity.Finished,
                Technology = await _context.Technologies.FindAsync(2),
                Photo = "/uploadfile/photo/Kowalski_Jaroslaw.jpg",
                UserId = _context.Users.Find(user.Id).Id,
                Cv = "/uploadfile/cv/cv.pdf",
                About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."

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
                        Level = Enums.Level.Junior,
                        Experience = 2,
                        Province = Enums.Province.Dolnoslaskie.ToString(),
                        Surname = "Kowalski",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.Finished,
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "python"),
                        Photo = "/uploadfile/photo/Kowlaski_Janek.jpg",
                        UserId = _context.Users.Find(user.Id).Id,
                        Cv = "/uploadfile/cv/cv.pdf",

                        About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("ArkadiuszNowak@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "ArkadiuszNowak@gmail.com",
                    UserName = "ArkadiuszNow",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Arkadiusz",
                        BirthDate = new DateTime(1992, 08, 05),
                        City = "Kielce",
                        Level = Enums.Level.Senior,
                        Experience = 2,
                        Province = Enums.Province.Mazowieckie.ToString(),
                        Surname = "Nowak",
                        University = "Politechnika świetokrzyska II stopien Informatyka",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.NotFinished,
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "python"),
                        Photo = "/uploadfile/photo/Arkadiusz_Nowak.jpg",
                        UserId = _context.Users.Find(user.Id).Id,
                        Cv = "/uploadfile/cv/cv.pdf",

                        About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("Pawel@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "Pawel@gmail.com",
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
                        Level = Enums.Level.Senior,
                        Experience = 2,
                        Province = Enums.Province.Mazowieckie.ToString(),
                        Surname = "Nowak",
                        University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.InProgress,
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "sql"),
                        Photo = "/uploadfile/photo/Nowak_Pawel.jpg",
                        UserId = _context.Users.Find(user.Id).Id,
                        Cv = "/uploadfile/cv/cv.pdf",

                        About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."


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
                        Level = Enums.Level.Regular,
                        Experience = 7,
                        Province = Enums.Province.Wielkopolskie.ToString(),
                        Surname = "Turek",
                        University = "Politechnika świetokrzyska II stopien Informatyka (w trakcie)",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.InProgress,
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "c#"),
                        Photo = "/uploadfile/photo/Turek_Marcin.jpg",
                        UserId = _context.Users.Find(user.Id).Id,
                        Cv = "/uploadfile/cv/cv.pdf",

                        About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."

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
                        Level = Enums.Level.Regular,
                        Experience = 4,
                        Province = Enums.Province.Slaskie.ToString(),
                        Surname = "Biracz",
                        University = " ",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.NotFinished,
                        Photo = "/uploadfile/photo/Biracz_Karol.jpg",
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "javascript"),
                        UserId = _context.Users.Find(user.Id).Id,
                        Cv = "/uploadfile/cv/cv.pdf",

                        About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."


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
                        Level = Enums.Level.Junior,
                        Cv = "/uploadfile/cv/cv.pdf",

                        Experience = 4,
                        Province = Enums.Province.Slaskie.ToString(),
                        Surname = "Olak",
                        University = " ",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.NotFinished,
                        Photo = "/uploadfile/photo/Olak_Piotrek.jpg",
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "html/css"),
                        UserId = _context.Users.Find(user.Id).Id,
                        About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."

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
                        Level = Enums.Level.Junior,
                        Experience = 4,
                        Province = Enums.Province.Dolnoslaskie.ToString(),
                        Surname = "Tomczak",
                        University = " Uniwersystet Polsko-Japoński w Warszawie",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.Finished,
                        Photo = "/uploadfile/photo/Tomczak_Heniek.jpg",
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "python"),
                        UserId = _context.Users.Find(user.Id).Id,
                        Cv = "/uploadfile/cv/cv.pdf",

                        About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("Bartosz@com.pl").Result == null)
            {
                User user = new User()
                {
                    Email = "Bartosz@com.pl",
                    UserName = "Bartosz123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Bartosz",
                        BirthDate = new DateTime(1983, 02, 05),
                        City = "Warszawa",
                        Level = Enums.Level.Regular,
                        Experience = 3,
                        Province = Enums.Province.Mazowieckie.ToString(),
                        Surname = "Nowicki",
                        University = " Uniwersystet Polsko-Japoński w Warszawie",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.Finished,
                        Photo = "/uploadfile/photo/Nowicki_Bartosz.jpg",
                        Technology = await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "java"),
                        UserId = _context.Users.Find(user.Id).Id,
                        Cv = "/uploadfile/cv/cv.pdf",

                        About = "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            };
            if (_manager.FindByEmailAsync("Ludwik@com.pl").Result == null)
            {
                User user = new User()
                {
                    Email = "Ludwik@com.pl",
                    UserName = "Ludwik123",
                    PhoneNumber = "2342342523",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    DeveloperUser dev = new DeveloperUser()
                    {
                        Name = "Ludwik",
                        BirthDate = new DateTime(1986, 08, 05),
                        City = "Kraków",
                        Level = Enums.Level.Senior,
                        Experience = 7,
                        Province = Enums.Province.Dolnoslaskie.ToString(),
                        Surname = "Boski",
                        University = "Akademia Górniczo Hutnicza",
                        IsFinishedUniversity = Enums.IsFinishedUniversity.Finished,
                        Photo = "/uploadfile/photo/Ludwik_Boski.jpg",
                        Technology =
                            await _context.Technologies.FirstOrDefaultAsync(f => f.Name.ToLower() == "javascript"),
                        UserId = _context.Users.Find(user.Id).Id,
                        Cv = "/uploadfile/cv/cv.pdf",

                        About =
                            "Jestem z zawodu i z zamiłowania programistą. Czuję się pewnie pisząc w JavaScripcie i TypeScripcie (głównie Node.js, choć także po stronie przeglądarki), w Javie, a na wcześniejszych etapach kariery programowałem również w PHP. Chętnie uczę się nowych technik programowania, szybko przyzwyczajam się do nowości w światku technologii. Eksperymentuję z nowymi narzędziami i językami programowania. Od czasu do czasu prowadzę prelekcje na spotkaniach meet.js w Gdańsku, zdarzyło mi się też prowadzić kilka warsztatów dotyczących programowania sterowanego testami(TDD).Jak mam natchnienie i nadmiar czasu, to dzielę się doświadczeniami i przemyśleniami na niniejszym blogu lub na kanale YouTube."

                    };

                    await _context.DeveloperUsers.AddAsync(dev);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Developer.ToString());
                }
            }
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
            if (_manager.FindByEmailAsync("komputerCompany@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "komputerCompany@gmail.com",
                    UserName = "komputerCompany",
                    PhoneNumber = "66526323",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    EmployeeUser emp = new EmployeeUser()
                    {
                        Name = "Władysław",
                        City = "Kielce",
                        Province = Enums.Province.Swietokrzyskie.ToString(),
                        Surname = "Sołkiewicz",
                        Company = "komputerCompany",
                        UserId = _context.Users.Find(user.Id).Id,
                    };

                    await _context.EmployeeUsers.AddAsync(emp);
                    await _context.SaveChangesAsync();
                    await _manager.AddToRoleAsync(_context.Users.Find(user.Id), Roles.Employee.ToString());
                }
                ;
            }
            if (_manager.FindByEmailAsync("netDevelopment@gmail.com").Result == null)
            {
                User user = new User()
                {
                    Email = "netDevelopment@gmail.com",
                    UserName = "netDevelopment",
                    PhoneNumber = "66526323",
                };
                var result = await _manager.CreateAsync(user, "Qazqaz12#");
                if (result.Succeeded)
                {
                    EmployeeUser emp = new EmployeeUser()
                    {
                        Name = "Tomasz",
                        City = "Kraków",
                        Province = Enums.Province.Malopolskie.ToString(),
                        Surname = "Bińczak",
                        Company = "netDevelopment",
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
                        EmployeeUserId =1,
                        EmploymentType =Enums.EmploymentType.UoP.GetAttribute().Name,
                        Level = Enums.Level.Regular,

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
                        EmployeeUserId =1,
                        EmploymentType =Enums.EmploymentType.UoP.GetAttribute().Name


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
                        EmployeeUserId =2,
                        EmploymentType =Enums.EmploymentType.B2B.GetAttribute().Name,
                        Level = Enums.Level.Junior

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
                        EmployeeUserId = 2,
                        EmploymentType =Enums.EmploymentType.Any.GetAttribute().Name,


                    },
                };

                await _context.Jobs.AddRangeAsync(jobs);
                await _context.SaveChangesAsync();


            }
        }

    }
}