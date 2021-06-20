using System;

namespace Core.Exceptions
{

    public class TransactionScopeException : Exception
    {
        public TransactionScopeException(string message) : base(message)
        {

        }
    }
}