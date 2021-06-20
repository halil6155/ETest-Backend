using System;

namespace Core.Exceptions
{
    public class AdminSecurityException : Exception
    {
        public AdminSecurityException(string message) : base(message)
        {

        }
    }
}