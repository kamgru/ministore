namespace MiniStore.Common
{
    public class PagingSettings
    {
        public int Page { get; }
        public int Count { get; }

        public PagingSettings(int page, int count)
        {
            Page = page;
            Count = count;
        }
    }
}