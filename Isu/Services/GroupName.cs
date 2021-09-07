using System;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class GroupName
    {
        private const int LENGTH = 5;
        private const string SPECIALIZATION = "M3";
        private int _number;

        /// <param name="groupName">Group name in the format M3XYY. Where X - course number, YY - group number.</param>
        public GroupName(string groupName)
        {
            if (groupName.Length != LENGTH)
                throw new IsuException("Group name has incorrect length.");
            
            if (groupName[..2] != SPECIALIZATION)
                throw new IsuException("Specialization is not supported");
            
            if (!char.IsDigit(groupName[2]))
                throw new IsuException("Third symbol in a group name is not a correct course number.");
            Course = new CourseNumber(groupName[2] - '0');
            
            if (!int.TryParse(groupName[3..], out _number))
                throw new IsuException("Last two symbols in a group name is not a correct course number.");
        }

        public CourseNumber Course { get; }

        public string GetName()
        {
            return SPECIALIZATION + Course.GetNumber() + $"{2:_number}";
        }
    }
}