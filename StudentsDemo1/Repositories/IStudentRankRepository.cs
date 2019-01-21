using StudentsDemo1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsDemo1.Repositories
{
    public interface IStudentRankRepository : IRepository<StudentRank>
    {
        HashSet<string> GetGroup(StudentRank instance);
    }
}
