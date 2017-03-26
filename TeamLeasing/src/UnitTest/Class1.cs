using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamLeasing.DAL;
using TeamLeasing.Services;
using TeamLeasing.Services.Mail;

namespace UnitTest
{
    [TestClass]
    public class Class1
    {
        private TeamLeasingContext Context { get; }
        public IConfigurationRoot _config;
        public DbContextOptions opiton;

        public Class1()
        {
           
            Context = new TeamLeasingContext(_config, opiton);
        }


        [TestMethod]

        public void Test()
        {
            SendEmail s = new SendEmail(new MessageModel());
            s.EmailMessage = s.CreateMessage( "michalmazur3@gmail.com", "dupa", "cos");
            s.Send(s.EmailMessage);



            //EmailService.SendEmailAsync("michalmazur3@gmail.com", "dupa", "addads");

        }

        [TestMethod]
        public void TestDatabase()
        {
            var dev = Context.Developers.ToList();

        }
    }
}
