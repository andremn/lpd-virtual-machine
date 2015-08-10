using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The NULL instruction.
    /// </summary>
    [Instruction(NULL)]
    public class NULLInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="context">Not used..</param>
        /// <param name="parameters">Not used.</param>
        protected override void SpecificExecute(ExecutionContext context, string[] parameters)
        {
        }
    }
}
