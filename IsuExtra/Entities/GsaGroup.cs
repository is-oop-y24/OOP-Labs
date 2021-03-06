using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Isu.Entities;

namespace IsuExtra
{
    public class GsaGroup
    {
        private List<Student> _students = new List<Student>();

        public GsaGroup(string name, GsaCourse course, Shedule shedule)
        {
            Name = name;
            Course = course;
            Shedule = shedule;
        }

        public string Name { get; }
        public ReadOnlyCollection<Student> Students => _students.AsReadOnly();
        public GsaCourse Course { get; }
        public Shedule Shedule { get; }

        internal void AddStudent(GsaProfile gsaProfile)
        {
            _students.Add(gsaProfile.Student);
        }

        internal void RemoveStudent(GsaProfile gsaProfile)
        {
            _students.Remove(gsaProfile.Student);
        }
    }
}