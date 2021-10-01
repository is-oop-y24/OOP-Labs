using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Isu.Entities;
using Isu.Services;

namespace IsuExtra
{
    public class GsaCourse
    {
        private List<GsaGroup> _groups = new List<GsaGroup>();

        public GsaCourse(MfTag mfTag, string name)
        {
            MfTag = mfTag;
            Name = name;
        }

        public string Name { get; }
        public MfTag MfTag { get; }
        public ReadOnlyCollection<GsaGroup> Groups => _groups.AsReadOnly();

        public void AddGroup(string courseName, Shedule shedule)
        {
            _groups.Add(new GsaGroup(courseName, course: this, shedule));
        }

        public List<Student> GetStudents()
        {
            return _groups
                .SelectMany(@group => @group.Students)
                .ToList();
        }
    }
}