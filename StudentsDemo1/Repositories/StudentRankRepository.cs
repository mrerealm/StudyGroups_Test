using StudentsDemo1.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsDemo1.Repositories
{
    public class StudentRankRepository : IStudentRankRepository, IDisposable
    {
        private readonly ArrayList _repository = new ArrayList();

        public StudentRank Get(int id)
        {
            return (StudentRank)_repository[id];
        }

        public IEnumerable<StudentRank> Get()
        {
            return _repository.Cast<StudentRank>();
        }

        public StudentRank Add(StudentRank instance)
        {
            _repository.Add(instance);
            return instance;
        }

        public void Add(IEnumerable<StudentRank> instances)
        {
            foreach (var instance in instances) this.Add(instance);
        }

        /// <summary>
        /// For a given student instance, get all the others students in his/her group
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public HashSet<string> GetGroup(StudentRank instance)
        {
            var res = new HashSet<string>();
            foreach (var student in Get())
            {
                if (instance.IsInGroup(student) && !res.Contains(student.Name)) res.Add(student.Name);
            }

            return res;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
