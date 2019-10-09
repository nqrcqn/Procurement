using System;

namespace POEApi.Infrastructure
{
    public class ForumThreadException : Exception
    {
        public ForumThreadException()
            : base() { }

        public string message = "";

        public ForumThreadException(string errormessage)
            : base(errormessage)
        {
            message = errormessage;
        }
    }
}
