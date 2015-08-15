namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Contract for a program executor.
    /// </summary>
    public interface IProgramExecutor
    {
        /// <summary>
        /// Gets the execution context.
        /// </summary>
        ExecutionContext Context { get; }

        /// <summary>
        /// Called when the caller finishing executing all the instructions without issues.
        /// </summary>
        void OnFinished();

        /// <summary>
        /// Called when a fatal error ocurred in the caller.
        /// </summary>
        /// <param name="error"></param>
        void OnFatalError(string error);

        /// <summary>
        /// Called when the caller is about to execute an instruction.
        /// </summary>
        void OnInstructionExecuting();

        /// <summary>
        /// Called when the caller is ready to execute an instruction.
        /// </summary>
        void OnInstructionExecuted();
    }
}
