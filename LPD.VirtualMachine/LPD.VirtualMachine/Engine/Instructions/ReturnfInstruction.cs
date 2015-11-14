using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPD.VirtualMachine.Engine.Instructions
{
    public class ReturnfInstruction : JumpableInstruction
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
            stack.Down();
            stack.Store(retValue);

            return retAddress;
        }
    }
}
