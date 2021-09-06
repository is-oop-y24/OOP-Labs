using System.Runtime.InteropServices;

namespace Isu.Services
{
    public class CourseNumber
    {
        private readonly int _number;

        public CourseNumber(int number)
        {
            _number = number;
        }

        public int ToInt()
        {
            return _number;
        }
    }
}