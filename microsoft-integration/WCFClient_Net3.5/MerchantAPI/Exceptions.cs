using System;
using System.Collections.Generic;

namespace Earthport.MerchantAPI
{
    /// <summary>
    /// The exception that is thrown when the Earthport Merchant Web Service responds with an error.
    /// </summary>
    /// <remarks>
    /// ServiceErrorException is thrown when a web service operation is invoked and and it returns with an error (i.e. an ack value of 'failure'). 
    /// </remarks>
    public class ServiceErrorException : Exception
    {
        public ServiceErrorException(string message, ErrorResponseType serviceErrorResponse)
            : base(message)
        {
            _serviceErrorResponse = serviceErrorResponse;
        }

        /// <summary>
        /// Details of the error
        /// </summary>
        public ErrorResponseType ServiceErrorResponse
        {
            get { return _serviceErrorResponse; }
        }

        private ErrorResponseType _serviceErrorResponse;
    }
}
