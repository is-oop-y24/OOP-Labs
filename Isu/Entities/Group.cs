using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Isu.Services;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private readonly uint _maxStudentsCount;
        private readonly List<Student> _students = new List<Student>();

        public Group(GroupName groupName, uint maxStudentsCount)
        {
            Name = groupName;
            _maxStudentsCount = maxStudentsCount;
        }

        public GroupName Name { get; }
        public CourseNumber Course => Name.Course;
        public ReadOnlyCollection<Student> Students => _students.AsReadOnly();

        public void AddStudent(Student student)
        {
            if (_students.Count == _maxStudentsCount)
                throw new IsuException();
            _students.Add(student);
        }

        public void DeleteStudent(Student student)
        {
            _students.Remove(student);
        }
    }
}