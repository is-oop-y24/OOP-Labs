namespace IsuExtra
{
    public class Lesson
    {
        public Lesson(string name, TimeInterval time)
        {
            Name = name;
            Time = time;
        }

        public bool IsCrossed(Lesson other)
        {
            return Time.IsCrossed(other.Time);
        }
        
        public string Name { get; }
        public TimeInterval Time { get; }
    }
}