using System;

namespace ECO3_Testing.CustomException
{
    public class ECO3TestException : Exception
    {
        public ECO3TestException(string message) : base(message)
        {

        }
    }
}
