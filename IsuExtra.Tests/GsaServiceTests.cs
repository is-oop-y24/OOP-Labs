using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class GsaServiceTests
    {
        private IGsaService _gsaService;
        
        [SetUp]
        public void Setup()
        {
            _gsaService = new GsaService();
        }

        [Test]
        public void AddGsa_GsaIsAdded()
        {
            const string gsaName = "gsa";
            const char mfTag = 'M';
            _gsaService.AddGsa(new MfTag(mfTag), gsaName);
            GsaCourse course = _gsaService.GsaCourses.FirstOrDefault(course => course.Name == gsaName);
            Assert.IsFalse(course == null);
            Assert.AreEqual(mfTag, course.MfTag.GetCharTag());
        }

        [Test]
        public void RegisterStudent_StudentIsRegistered()
        {
            const string studentName = "studentName";
            const string groupName = "M3200";
            const char mfTag = 'G';
            const string gsaName = "gsa2";
            const string gsaGroupName = "groupName";
            const int id = 1;
            var shedule = new Shedule();
            var student = new Student(studentName, id, new Group(new GroupName(groupName), 30));
            GsaProfile profile = _gsaService.CreateProfile(student, shedule);
            _gsaService.AddGsa(new MfTag(mfTag), gsaName);
            GsaCourse gsaCourse = _gsaService.GsaCourses.First(gsa => gsa.Name == gsaName);
            gsaCourse.AddGroup(gsaGroupName, shedule);
            GsaGroup gsaGroup = gsaCourse.Groups.First();
            _gsaService.SignStudent(profile, gsaCourse.Groups.First());
            Assert.IsTrue(gsaGroup.Students.Contains(student));
            Assert.IsTrue(profile.GsaGroups.Contains(gsaGroup));
        }

        [Test]
        public void CancelRegistration_StudentIsNotInTheGroup()
        {
            const string studentName = "studentName";
            const string groupName = "M3200";
            const char mfTag = 'G';
            const string gsaName = "gsa2";
            const string gsaGroupName = "groupName";
            const int id = 1;
            var shedule = new Shedule();
            var student = new Student(studentName, id, new Group(new GroupName(groupName), 30));
            GsaProfile profile = _gsaService.CreateProfile(student, shedule);
            _gsaService.AddGsa(new MfTag(mfTag), gsaName);
            GsaCourse gsaCourse = _gsaService.GsaCourses.First(gsa => gsa.Name == gsaName);
            gsaCourse.AddGroup(gsaGroupName, shedule);
            GsaGroup gsaGroup = gsaCourse.Groups.First();
            _gsaService.SignStudent(profile, gsaCourse.Groups.First());
            
            _gsaService.CancelRegistration(profile, gsaGroup);
            Assert.IsFalse(gsaGroup.Students.Contains(student));
            Assert.IsFalse(profile.GsaGroups.Contains(gsaGroup));
        }

        [Test]
        public void GetNotRegisteredStudents_RightListGotten()
        {
            const string groupName = "M3200";
            const char mfTag = 'G';
            const string gsaName = "gsa2";
            const string gsaGroupName = "groupName";
            
            var shedule = new Shedule();
            _gsaService.AddGsa(new MfTag(mfTag), gsaName);
            GsaCourse gsaCourse = _gsaService.GsaCourses.First(gsa => gsa.Name == gsaName);
            gsaCourse.AddGroup(gsaGroupName, shedule);
            GsaGroup gsaGroup = gsaCourse.Groups.First();

            var students = new List<GsaProfile>();
            var group = new Group(new GroupName(groupName), 30);
            foreach (int num in Enumerable.Range(1, 30))
            {
                var student = new Student($"Student_{num}", num, group);
                group.AddStudent(student);
                students.Add(_gsaService.CreateProfile(student, shedule));
            }
            
            foreach (int num in Enumerable.Range(1, 15))
            {
                _gsaService.SignStudent(students[num-1], gsaGroup);
            }

            List<Student> notRegistered = _gsaService.NotRegisteredStudents(group);
            Assert.AreEqual(15, notRegistered.Count);
        }
    }
}