using System;
using System.Collections.Generic;
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
                throw new Exception("Student profile is already exist.");

            GsaProfile profile = new GsaProfile(student, shedule);
            _profiles.Add(profile);
            return profile;
        }

        public void SignStudent(Student student, GsaGroup gsaGroup)
        {
            
        }

        public void CancelRegistration(Student student, GsaGroup gsaGroup)
        {
            throw new System.NotImplementedException();
        }

        public List<Student> NotRegisteredStudents(Group @group)
        {
            throw new System.NotImplementedException();
        }
    }
}