using System;

namespace CargoApi.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }
    }
}
