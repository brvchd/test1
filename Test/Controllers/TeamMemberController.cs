using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.DTOs.Response;
using Test.Services;

namespace Test.Controllers
{
    [Route("api/teammember")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly IDbTask _task;
        public TeamMemberController(IDbTask task)
        {
            _task = task;
        }
        
        [HttpGet]
        public IActionResult getTeamMember(int id)
        {
            var result = new TeamMemberResponce();
            try
            {
                result = _task.getTeamMember(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult deleteProject(string name)
        {
            try
            {
                deleteProject(name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}