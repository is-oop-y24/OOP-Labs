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

        internal void RegisterToGroup(GsaGroup gsaGroup)
        {
            _gsaGroups.Add(gsaGroup);
        }

        internal void CancelRegistration(GsaGroup gsaGroup)
        {
            _gsaGroups.Remove(gsaGroup);
        }

        public ReadOnlyCollection<GsaGroup> GsaGroups => _gsaGroups.AsReadOnly();
    }
}