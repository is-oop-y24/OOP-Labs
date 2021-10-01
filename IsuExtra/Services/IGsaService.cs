using System.Collections.Generic;
using System.Collections.ObjectModel;
using Isu.Entities;
using Isu.Services;

namespace IsuExtra
{
    public interface IGsaService
    {
        ReadOnlyCollection<GsaCourse> GsaCourses { get; }

        void AddGsa(MfTag mfTag, string name);
        void SignStudent(GsaProfile gsaProfile, GsaGroup gsaGroup);
        void CancelRegistration(GsaProfile gsaProfile, GsaGroup gsaGroup);
        List<Student> NotRegisteredStudents(Group group);
    }
}