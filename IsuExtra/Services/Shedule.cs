using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace IsuExtra
{
    public class Shedule
    {
        private List<Lesson> _lessons = new List<Lesson>();

        public void AddLesson(Lesson lesson)
        {
            if (_lessons.Exists(@lesson => @lesson.IsCrossed(lesson)))
                throw new Exception("New lesson crosses current shedule.");

            _lessons.Add(lesson);
        }

        public bool IsCrossed(Shedule other)
        {
            return _lessons.Exists(
                lesson => other._lessons.Exists(otherLesson => otherLesson.IsCrossed(lesson)));
        }
    }
}