namespace Isu.Entities
{
    public class Student
    {
        public Student(string name, int id, Group group)
        {
            Name = name;
            ID = id;
            CurrentGroup = group;
        }

        public string Name { get; }
        public int ID { get; }

        public Group CurrentGroup { get; set; }
    }
}