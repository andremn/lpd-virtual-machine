using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The JMP instruction.
    /// </summary>
    [Instruction(JMP)]
    class JMPInstruction : JumpableInstruction
    {
        /// <summary>
        /// Jumps to the specified address.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The address to jump.</param>
        /// <returns>The address to jump.</returns>
        protected override int SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int address = parameters[0];

            return address;
        }
    }
}
