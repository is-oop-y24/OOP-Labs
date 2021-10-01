using System.Collections.Generic;
using Isu.Entities;
using Isu.Services;

namespace IsuExtra
{
    public interface IGsaService
    {
        void AddGsa(MfTag mfTag, string name);
        void SignStudent(GsaProfile student, GsaGroup gsaGroup);
        void CancelRegistration(GsaProfile student, GsaGroup gsaGroup);
        List<Student> NotRegisteredStudents(Group group);
    }
}