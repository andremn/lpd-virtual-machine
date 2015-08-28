using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The JMPF instruction.
    /// </summary>
    [Instruction(JMPF)]
    class JMPFInstruction : JumpableInstruction
    {
        /// <summary>
        /// Jumps to the specified address if the stack's top position is zero.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The address to jump.</param>
        /// <returns>The address to jump if the stack's top position is zero or the next instruction address.</returns>
        protected override int SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int address = parameters[0];
            int value;
            Stack stack = context.Memory.StackRegion;

            value = stack.Load();
            stack.Down();

            if (value == 0)
            {
                return address;
            }

            return context.ProgramCounter.Current + 1;
        }
    }
}
