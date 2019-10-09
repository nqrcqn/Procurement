using System;

namespace POEApi.Infrastructure
{
    public class ForumThreadException : Exception
    {
        public ForumThreadException()
            : base() { }

        public string errormessage = "";

        public ForumThreadException(string message)
            : base(message)
        {
            errormessage = message;
        }
    }
}
