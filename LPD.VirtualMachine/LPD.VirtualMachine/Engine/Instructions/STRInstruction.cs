using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The STR instruction.
    /// </summary>
    [Instruction(STR)]
    public class STRInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Stores the content of the stack's first position at the specified address.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The address to store the value.</param>
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int index = parameters[0];
            Stack stack = context.Memory.StackRegion;

            stack.StoreAt(index, stack.Load());
            stack.Down();
        }
    }
}
