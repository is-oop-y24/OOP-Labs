using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Isu.Entities;
using Isu.Services;

namespace IsuExtra
{
    public class GsaProfile
    {
        public Student Student { get; }
        public Shedule Shedule { get; }
        private readonly List<GsaGroup> _gsaGroups = new List<GsaGroup>();

        public GsaProfile(Student student, Shedule shedule)
        {
            Student = student;
            Shedule = shedule;
        }

        internal void RegisterToGroup(GsaGroup group)
        {
            if (_gsaGroups.Count == 2)
                throw new Exception("Student cannot be registered for more than 2 courses.");
            if (_gsaGroups.Exists(@group => @group.Course == group.Course))
                throw new Exception("Student is already registered to another group of this course.");
            if (Student.CurrentGroup.Name.MfTag == group.Course.MfTag)
                throw new Exception("Student cannot register to his faculty's GSA.");
            if (Shedule.IsCrossed(group.Shedule))
                throw new Exception("Student's shedule is crossed with group's shedule.");
            
            _gsaGroups.Add(group);
        }

        internal void CancelRegistration(GsaGroup gsaGroup)
        {
            if (!_gsaGroups.Remove(gsaGroup))
                throw new Exception("Student is not registered to this course.");
        }

        public ReadOnlyCollection<GsaGroup> GsaGroups => _gsaGroups.AsReadOnly();
    }
}