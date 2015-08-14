namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Defines how a program can be executed.
    /// </summary>
    public enum ExecutionMode
    {
        /// <summary>
        /// The program is executed to the end without stopping.
        /// </summary>
        Normal,
        /// <summary>
        /// The program is executed instruction by instruction.
        /// </summary>
        Debug
    }
}
