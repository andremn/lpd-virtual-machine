namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Reasons why the stack has changed.
    /// </summary>
    public enum StackChangedReason
    {
        /// <summary>
        /// The stack was totally cleared.
        /// </summary>
        Cleared,
        /// <summary>
        /// An item was inserted.
        /// </summary>
        Inserted,
        /// <summary>
        /// An item was removed from the stack.
        /// </summary>
        Popped,
        /// <summary>
        /// An item was added to the stack.
        /// </summary>
        Pushed
    }
}
