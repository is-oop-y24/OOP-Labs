using System.Runtime.InteropServices;
using Isu.Tools;

namespace Isu.Services
{
    public class CourseNumber
    {
        private readonly int _number;

        public CourseNumber(int number)
        {
            _number = number;
        }

        public int GetNumber()
        {
            return _number;
        }
    }
}