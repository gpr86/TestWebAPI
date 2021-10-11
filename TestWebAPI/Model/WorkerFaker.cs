using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace TestWebAPI.Model
{
    class WorkerFaker : Faker<Worker>
    {
        public WorkerFaker()
        {
            RuleFor(w => w.FirstName, f => f.Name.FirstName());
            RuleFor(w => w.LastName, f => f.Name.LastName());
            RuleFor(w => w.Prefix, f => f.Name.Prefix());
            RuleFor(w => w.Suffix, f => f.Name.Suffix());
            RuleFor(w => w.JobTitle, f => f.Name.JobTitle());
            RuleFor(w => w.JobDescriptor, f => f.Name.JobDescriptor());
            RuleFor(w => w.JobArea, f => f.Name.JobArea());
            RuleFor(w => w.JobType, f => f.Name.JobType());
            RuleFor(w => w.PhoneNumber, f => f.Phone.PhoneNumber());
            RuleFor(w => w.DateOfEmployment, f => f.Date.PastOffset(10).DateTime);
        }
    }
}
