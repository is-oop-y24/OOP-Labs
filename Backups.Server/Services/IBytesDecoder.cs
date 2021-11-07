namespace Backups.Server
{
    public interface IBytesDecoder
    {
        public BytesData Encode<TData>(TData obj);
        public TData Decode<TData>(BytesData bytesData);
    }
}