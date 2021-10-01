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
            _gsaService.
        }
    }
}