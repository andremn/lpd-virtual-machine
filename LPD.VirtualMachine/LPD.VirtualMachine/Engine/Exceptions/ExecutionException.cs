﻿using System;

namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Represents an error that occured when an invalid instruction was tried to be executed.
    /// </summary>
    public class ExecutionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionException"/> class.
        /// </summary>
        public ExecutionException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ExecutionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionException"/> class with the specified error message 
        /// and a reference to the inner exception that is the cause to this exception..
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ExecutionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
