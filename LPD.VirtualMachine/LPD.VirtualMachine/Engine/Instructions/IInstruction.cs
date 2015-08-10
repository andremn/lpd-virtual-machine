namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// Represents an instruction that is executed by a <see cref="LPD.VirtualMachine.Engine.HAL.CPU"/>.
    /// </summary>
    public interface IInstruction
    {
        /// <summary>
        /// When overriden on a derived class, executes the instruction over a execution context.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The parameters for the instruction.</param>
        void Execute(ExecutionContext context, string[] parameters);
    }
}
