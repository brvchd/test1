using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.DTOs.Response;

namespace Test.Services
{
    public interface IDbTask
    {
        public TeamMemberResponce getTeamMember(int id);
    }
}
