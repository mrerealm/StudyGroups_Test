using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentsDemo1.Models;
using StudentsDemo1.Repositories;

namespace StudentsDemo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGroupController : ControllerBase
    {
        private readonly IStudentRankRepository _studentRankRepository;

        public StudentGroupController(IStudentRankRepository studentRankRepository)
        {
            _studentRankRepository = studentRankRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Student Grouping Test");
        }

        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody]string value)
        {
            if (String.IsNullOrWhiteSpace(value)) return BadRequest();

            try
            {
                // deserialise string array into model
                var list = StudentRank.Deserialize(value);

                // store into repository (why?)
                _studentRankRepository.Add(list);

                // get all student's rank
                var studentRanks = _studentRankRepository.Get();

                var groups = new StringBuilder();
                // for each student get neighbours list
                foreach (var student in studentRanks)
                {
                    if (!groups.ToString().Contains(student.Name))
                    {
                        var group = _studentRankRepository.GetGroup(student);
                        groups.Append($"{{'{String.Join<string>("','", group)}'}}");
                    }
                }

                // serialise list
                var res = $"{{{groups.ToString()}}}";
                return Ok(res);

            }
            catch
            {
                return BadRequest("Invalid input format");
            }
        }
    }
}
