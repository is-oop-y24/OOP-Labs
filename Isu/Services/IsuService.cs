using System.Collections.Generic;
using System.Linq;
using Isu.Entities;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly uint _maxStudentsCount;

        private readonly List<Group> _groups = new List<Group>();
        private int _currentID = 1;

        public IsuService(uint maxStudentsCount)
        {
            _maxStudentsCount = maxStudentsCount;
        }

        public Group AddGroup(string name)
        {
            var group = new Group(groupName: new GroupName(name), _maxStudentsCount);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(name, _currentID++, group);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            IEnumerable<Student> result = from @group in _groups
                from student in @group.Students
                where student.ID == id
                select student;

            return result.Single();
        }

        public Student FindStudent(string name)
        {
            IEnumerable<Student> result = from @group in _groups
                from student in @group.Students
                where student.Name == name
                select student;

            return result.SingleOrDefault();
        }

        public List<Student> FindStudents(string groupName)
        {
            Group group = FindGroup(groupName);
            return @group != null ? @group.Students.ToList() : new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            IEnumerable<Student> result = from @group in _groups
                where @group.Name.Course == courseNumber
                from student in @group.Students
                select student;

            return result.ToList();
        }

        public Group FindGroup(string groupName)
        {
            IEnumerable<Group> result = from @group in _groups
                where @group.Name.GetName() == groupName
                select @group;

            return result.SingleOrDefault();
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            IEnumerable<Group> result = from @group in _groups
                where @group.Name.Course == courseNumber
                select @group;

            return result.ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            IEnumerable<Group> groupQuery = from @group in _groups
                where @group.Students.Contains(student)
                select @group;

            Group containingGroup = groupQuery.Single();
            containingGroup.DeleteStudent(student);
            newGroup.AddStudent(student);
            student.CurrentGroup = newGroup;
        }
    }
}