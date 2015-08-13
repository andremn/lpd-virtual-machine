using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The CALL instruction.
    /// </summary>
    [Instruction(CALL)]
    class CALLInstruction : JumpableInstruction
    {
        /// <summary>
        /// Jumps to the specified address and saves the next instruction address in the stack.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The address to jump.</param>
        /// <returns>The address to jump.</returns>
        public override int SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int address = parameters[0];
            Stack stack = context.Memory.StackRegion;

            stack.Up();
            stack.Store(context.ProgramCounter.Current + 1);
            return address;
        }
    }
}
