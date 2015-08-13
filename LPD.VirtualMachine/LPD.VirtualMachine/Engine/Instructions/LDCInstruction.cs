using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The LDC instruction.
    /// </summary>
    [Instruction(LDC)]
    public class LDCInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Stores a constant to the stack.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The constant to be stored.</param>
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int constant = parameters[0];
            Stack stack = context.Memory.StackRegion;

            stack.Up();
            stack.Store(constant);
        }
    }
}
