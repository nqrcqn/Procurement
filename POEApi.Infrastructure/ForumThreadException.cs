using System;

namespace POEApi.Infrastructure
{
    private string errormessage;

    public class ForumThreadException : Exception
    {
        public ForumThreadException()
            : base() { }

        public ForumThreadException(string message)
            : base(message)
        {
            errormessage = message;
        }
    }
    public override string Message
    {
        get
        {
            return errormessage;
        }  
    }
}
