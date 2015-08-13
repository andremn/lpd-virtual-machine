using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// Represents an instruction that sets the program counter to a specific position.
    /// This class is the base class to the JMP and JMPF instructions.
    /// </summary>
    public abstract class JumpableInstruction : IInstruction
    {
        /// <summary>
        /// Executes the instruction and sets the program counter to a specific position.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The instructions parameters, if any.
        /// Use null if the instruction has no parameters.</param>
        public void Execute(ExecutionContext context, int[] parameters)
        {
            context.ProgramCounter.Jump(SpecificExecute(context, parameters));
        }

        /// <summary>
        /// When overriden in a derived class, executes the instruction for the derived instruction.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The instruction parameters, if any.
        /// Use null if the instruction has no parameters.</param>
        /// <returns>The position to set the program counter. 
        /// In other words, the position to jump.</returns>
        public abstract int SpecificExecute(ExecutionContext context, int[] parameters);
    }
}
