using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;

namespace IsuExtra
{
    public class GsaService : IGsaService
    {
        private readonly List<GsaCourse> _courses = new List<GsaCourse>();
        private readonly List<GsaProfile> _profiles = new List<GsaProfile>();

        public void AddGsa(MfTag mfTag, string name)
        {
            _courses.Add(new GsaCourse(mfTag, name));
        }

        public GsaProfile CreateProfile(Student student, Shedule shedule)
        {
            if (_profiles.Exists(profile => profile.Student.Id == student.Id))
                throw new GsaException("Student profile is already exist.");

            GsaProfile profile = new GsaProfile(student, shedule);
            _profiles.Add(profile);
            return profile;
        }

        public void SignStudent(GsaProfile gsaProfile, GsaGroup gsaGroup)
        {
            if (gsaProfile.GsaGroups.Count == 2)
                throw new GsaException("Student cannot be registered for more than 2 courses.");
            if (gsaProfile.GsaGroups.FirstOrDefault(@group => @group.Course == group.Course) != null)
                throw new GsaException("Student is already registered to another group of this course.");
            if (gsaProfile.Student.CurrentGroup.Name.MfTag == gsaGroup.Course.MfTag)
                throw new GsaException("Student cannot register to his faculty's GSA.");
            if (gsaProfile.Shedule.IsCrossed(gsaGroup.Shedule))
                throw new GsaException("Student's shedule is crossed with group's shedule.");

            gsaGroup.AddStudent(gsaProfile);
            gsaProfile.RegisterToGroup(gsaGroup);
        }

        public void CancelRegistration(GsaProfile gsaProfile, GsaGroup gsaGroup)
        {
            if (!gsaProfile.GsaGroups.Contains(gsaGroup))
                throw new GsaException("Student is not registered to this group");

            gsaProfile.CancelRegistration(gsaGroup);
            gsaGroup.RemoveStudent(gsaProfile);
        }

        public List<Student> GetRegisteredStudents()
        {
            return _profiles
                .Where(profile => profile.GsaGroups.Count == 2)
                .Select(profile => profile.Student)
                .ToList();
        }

        public List<Student> NotRegisteredStudents(Group @group)
        {
            return group.Students.Except(GetRegisteredStudents()).ToList();
        }
    }
}