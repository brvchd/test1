using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Model;

namespace Test.DTOs.Response
{
    public class TeamMemberResponce
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
