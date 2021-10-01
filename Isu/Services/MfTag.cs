namespace Isu.Services
{
    public class MfTag
    {
        private char _tag;
        
        public MfTag(char mfTag)
        {
            _tag = mfTag;
        }

        public char GetCharTag()
        {
            return _tag;
        }
    }
}