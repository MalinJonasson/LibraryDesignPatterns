namespace Library
{
    public abstract class BookHandler : IBookHandler
    {
        private IBookHandler _nextHandler;

        public void SetNextHandler(IBookHandler handler)
        {
            _nextHandler = handler;
        }

        public virtual void HandleRequest(BookRequest request)
        {
            if (CanHandle(request))
            {
                ProcessRequest(request);
            }
            else if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
        }

        protected abstract bool CanHandle(BookRequest request);
        protected abstract void ProcessRequest(BookRequest request);
    }
}
