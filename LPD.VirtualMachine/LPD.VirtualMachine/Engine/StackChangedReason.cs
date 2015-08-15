using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Reasons why the stack has changed.
    /// </summary>
    public enum StackChangedReason
    {
        /// <summary>
        /// An item was added to the stack.
        /// </summary>
        Pushed,
        /// <summary>
        /// 
        /// </summary>
        Popped
    }
}
