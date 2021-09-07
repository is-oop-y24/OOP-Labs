namespace Isu.Entities
{
    public class Student
    {
        public Student(string name, int id, Group group)
        {
            Name = name;
            Id = id;
            CurrentGroup = group;
        }

        public string Name { get; }
        public int Id { get; }

        public Group CurrentGroup { get; set; }
    }
}