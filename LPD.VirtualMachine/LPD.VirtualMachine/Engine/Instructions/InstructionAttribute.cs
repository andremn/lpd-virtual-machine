using System;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// Marks an object as a instruction.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class InstructionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionAttribute"/> class providing the name of the instruction.
        /// </summary>
        /// <param name="name"></param>
        public InstructionAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets or sets the instruction's name.
        /// </summary>
        public string Name { get; set; }
    }
}
