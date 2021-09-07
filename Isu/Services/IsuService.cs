using System.Collections.Generic;
using System.Linq;
using Isu.Entities;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly uint _maxStudentsCount;

        private readonly List<Group> _groups = new List<Group>();
        private int _currentId = 1;

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
            var student = new Student(name, _currentId++, group);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _groups
                .SelectMany(group => @group.Students)
                .Single(student => student.Id == id);
        }

        public Student FindStudent(string name)
        {
            return _groups
                .SelectMany(group => @group.Students)
                .SingleOrDefault(student => student.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            Group group = FindGroup(groupName);
            return @group != null ? @group.Students.ToList() : new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _groups
                .Where(group => group.Course == courseNumber)
                .SelectMany(group => group.Students)
                .ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _groups
                .SingleOrDefault(group => group.Name == new GroupName(groupName));
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups
                .Where(group => group.Course == courseNumber)
                .ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            Group containingGroup = _groups
                .Single(group => group.Students.Contains(student));

            containingGroup.DeleteStudent(student);
            newGroup.AddStudent(student);
            student.CurrentGroup = newGroup;
        }
    }
}