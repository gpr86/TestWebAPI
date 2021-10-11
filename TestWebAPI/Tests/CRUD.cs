using NUnit.Framework;
using TestWebAPI.Model;
using TestWebAPI.API;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System;

namespace TestWebAPI.Tests
{
    [TestFixture]
    public class CRUD
    {
        List<string> addedWorkers;
        WorkersResource workers;

        [OneTimeSetUp]
        public void Setup()
        {
            workers = new WorkersResource(new WebAPI());
            addedWorkers = new List<string>();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            workers.Delete(addedWorkers);
        }

        [TestCase(TestName = "TC100_WORKER_CREATE")]
        public void CreateWorker()
        {
            var expected = new WorkerFaker().Generate();
            var response = workers.Create(expected);
            var actual = response.Data;

            if (actual?.Id != null)
                addedWorkers.Add(actual.Id);

            using (new AssertionScope())
            {
                // response
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

                // data
                actual.Id.Should().NotBeNullOrWhiteSpace();
                actual.Should().BeEquivalentTo(expected, options => options.Excluding(p => p.Id));
            }
        }

        [TestCase(TestName = "TC101_WORKER_READ_ALL")]
        public void GetWorkers()
        {
            var expected = new WorkerFaker().Generate(3);

            // create
            var created = workers.Create(expected).Select(x => x.Data);
            var ids = created.Select(x => x.Id);
            addedWorkers.AddRange(ids);

            // read
            var response = workers.Read();
            var actual = response.Data.Where(x => ids.Contains(x.Id));

            using (new AssertionScope())
            {
                // response
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                // data
                actual.Should().BeEquivalentTo(expected, options => options.Excluding(p => p.Id));

            }
        }

        [TestCase(TestName = "TC102_WORKER_READ_SINGLE")]
        public void GetWorker()
        {
            var expected = new WorkerFaker().Generate();

            // create
            var created = workers.Create(expected).Data;
            addedWorkers.Add(created.Id);

            // read
            var response = workers.Read(created.Id);
            var actual = response.Data;

            using (new AssertionScope())
            {
                // response
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                // data
                actual.Should().BeEquivalentTo(expected, options => options.Excluding(p => p.Id));
            }
        }

        [TestCase(TestName = "TC103_WORKER_UPDATE")]
        public void UpdateWorker()
        {
            var wkrf = new WorkerFaker();
            var wkr1 = wkrf.Generate();
            var wkr2 = wkrf.Generate();

            // create
            var id = workers.Create(wkr1).Data.Id;
            addedWorkers.Add(id);

            // update
            var response = workers.Update(id, wkr2);

            // read
            var actual = workers.Read(id).Data;
            var expected = wkr2;
            var expectedId = id;

            using (new AssertionScope())
            {
                // response
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

                // data
                actual.Id.Should().Be(expectedId);
                actual.Should().BeEquivalentTo(expected, options => options.Excluding(p => p.Id));
            }
        }

        [TestCase(TestName = "TC104_WORKER_DELETE")]
        public void DeleteWorker()
        {
            var wkr = new WorkerFaker().Generate();

            // create
            var id = workers.Create(wkr).Data.Id;
            addedWorkers.Add(id);

            // delete
            var deleteResponse = workers.Delete(id);

            // read
            var readResponse = workers.Read(id);

            var actual = readResponse.Data;
            var expected = new Worker();

            using (new AssertionScope())
            {
                // response
                deleteResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
                readResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

                // data
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}