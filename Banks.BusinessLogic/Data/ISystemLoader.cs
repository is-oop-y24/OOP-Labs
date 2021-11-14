namespace Banks
{
    public interface ISystemLoader
    {
        ICentralBank Load();
        void Save(ICentralBank centralBank);
    }
}