using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The function return (RETURNF) instruction.
    /// </summary>
    [Instruction(RETURNF)]
    public class RETURNFInstruction : JumpableInstruction
    {
        protected override int SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int retAddress = 0;
            var dallocInstruction = new DALLOCInstruction();
            var stack = context.Memory.StackRegion;
            var retValue = stack.Load();

            stack.Down();
            dallocInstruction.Execute(context, parameters);
            retAddress = stack.Load();
            stack.Store(retValue);

            return retAddress;
        }
    }
}
