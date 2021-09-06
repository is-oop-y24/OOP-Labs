using System.Linq;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private const int _maxStunedtsCount = 25;
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService(_maxStunedtsCount);
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3200");
            Student student = _isuService.AddStudent(group, "student");
            Assert.IsTrue(student.CurrentGroup == group);
            Assert.IsTrue(group.Students.Contains(student));
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group @group = _isuService.AddGroup("M3200");
            Assert.Catch<IsuException>(() =>
            {
                foreach (int number in Enumerable.Range(1, _maxStunedtsCount + 1))
                {
                    _isuService.AddStudent(@group, $"student_{number}");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M32000");
            });
            
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M320");
            });
            
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("32001");
            });
            
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("M3g00");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group1 = _isuService.AddGroup("M3200");
            Group group2 = _isuService.AddGroup("M3201");
            Student student = _isuService.AddStudent(group1, "student");
            _isuService.ChangeStudentGroup(student, group2);
            Assert.AreEqual(student.CurrentGroup, group2);
        }
    }
}