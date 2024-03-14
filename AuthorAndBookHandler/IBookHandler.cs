namespace Library
{
    public interface IBookHandler
    {
        void SetNextHandler(IBookHandler handler);
        void HandleRequest(BookRequest request);
    }
}
