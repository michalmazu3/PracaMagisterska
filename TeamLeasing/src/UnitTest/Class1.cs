using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeamLeasing.Services;

namespace UnitTest
{
    [TestClass]
    public class Class1
    {
        public Class1()
        {
           

        }


        [TestMethod]

        public void Test()
        {
            SendEmail s = new SendEmail(new MessageModel());
            s.EmailMessage = s.CreateMessage( "michalmazur3@gmail.com", "dupa", "cos");
            s.Send(s.EmailMessage);



            //EmailService.SendEmailAsync("michalmazur3@gmail.com", "dupa", "addads");

        }
    }
}
