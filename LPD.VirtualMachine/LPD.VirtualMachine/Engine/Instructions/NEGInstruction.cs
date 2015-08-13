using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The NEG instruction.
    /// </summary>
    [Instruction(NEG)]
    class NEGInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Realizes an logical NOT operation on the first two values in the stack.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">Not used.</param>
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int first;
            Stack stack = context.Memory.StackRegion;

            first = stack.Load();
            stack.Store(1 - first);
        }
    }
}
