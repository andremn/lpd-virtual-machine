using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The INV instruction.
    /// </summary>
    [Instruction(INV)]
    class INVInstruction: IncrementalInstruction
    {
        /// <summary>
        /// Inverts the signal of the first value in the stack.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">Not used.</param>
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int first;            
            Stack stack = context.Memory.StackRegion;

            first = stack.Load();   
            stack.Store(-first);
        }
    }
}
