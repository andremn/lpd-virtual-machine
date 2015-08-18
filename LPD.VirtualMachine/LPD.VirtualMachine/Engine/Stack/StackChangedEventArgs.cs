using System;

namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Event data for when the stack changes.
    /// </summary>
    public class StackChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StackChangedReason"/>
        /// </summary>
        /// <param name="reason">Why the stack has changed.</param>
        public StackChangedEventArgs(StackChangedReason reason)
        {
            Reason = reason;
        }

        /// <summary>
        /// Gets or sets why the stack has changed.
        /// </summary>
        public StackChangedReason Reason { get; set; }

        /// <summary>
        /// Gets or sets the index that was affected.
        /// </summary>
        public int? Index { get; set; }
    }
}
