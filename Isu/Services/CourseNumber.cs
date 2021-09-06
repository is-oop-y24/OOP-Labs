using System.Runtime.InteropServices;
using Isu.Tools;

namespace Isu.Services
{
    public class CourseNumber
    {
        private readonly int _number;

        public CourseNumber(int number)
        {
            if (_number < 0)
                throw new IsuException("Group number must be a positive integer.");
            _number = number;
        }

        public int GetNumber()
        {
            return _number;
        }
    }
}