
namespace Home.TypeFactory
{
    using System;

    public class HttpException : Exception
    {
        public HttpException(string message, string description) : base(message)
        {
            this.Description = description;
        }

        public string Description { get; private set; }
    }
}
