namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// Represents an instruction that increments the program counter before executing itself.
    /// </summary>
    public abstract class IncrementalInstruction : IInstruction
    {
        /// <summary>
        /// Increments the program counter and executes the instruction.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The instructions parameters, if any. 
        /// Use null if the instruction has no parameters.</param>
        public void Execute(ExecutionContext context, string[] parameters)
        {
            context.ProgramCounter.Increment();
            SpecificExecute(context, parameters);
        }

        /// <summary>
        /// When overriden in a derived class, executes the instruction for the derived instruction.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The instruction parameters, if any.
        /// Use null if the instruction has no parameters.</param>
        protected abstract void SpecificExecute(ExecutionContext context, string[] parameters);
    }
}
