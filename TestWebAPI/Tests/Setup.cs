using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Serilog;

namespace TestWebAPI.Tests
{
    [SetUpFixture]
    public class SetUpClass
    {
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var path = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "log.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(path)
                .CreateLogger();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            // ...
        }
    }
}
