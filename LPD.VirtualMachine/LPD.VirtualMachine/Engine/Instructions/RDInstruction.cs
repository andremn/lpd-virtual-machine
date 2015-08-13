using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The RD instruction.
    /// </summary>
    [Instruction(RD)]
    public class RDInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Reads a value from the input.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">Not used.</param>
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            Stack stack = context.Memory.StackRegion;

            stack.Up();
            stack.Store(context.InputProvider.ReadInputValue());
        }
    }
}
