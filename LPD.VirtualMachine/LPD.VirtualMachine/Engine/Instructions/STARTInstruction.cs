using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The START instruction.
    /// </summary>
    [Instruction(START)]
    public class STARTInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Sets the stack position to -1 (initial value).
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">Not used.</param>
        protected override void SpecificExecute(ExecutionContext context, string[] parameters)
        {
            context.Memory.StackRegion.Seek(-1);
        }
    }
}
