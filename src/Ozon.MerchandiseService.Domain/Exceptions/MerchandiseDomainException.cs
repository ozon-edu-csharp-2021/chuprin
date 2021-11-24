using System;

namespace Ozon.MerchandiseService.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class MerchandiseDomainException : Exception
    {
        public MerchandiseDomainException()
        { }

        public MerchandiseDomainException(string message)
            : base(message)
        { }

        public MerchandiseDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
