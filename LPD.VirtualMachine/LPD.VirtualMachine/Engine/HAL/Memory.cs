using System;

namespace LPD.VirtualMachine.Engine.HAL
{
    /// <summary>
    /// Represents a memory.
    /// </summary>
    public sealed class Memory
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Memory"/> class with the specified maximum side 
        /// and the instructions to load into it.
        /// </summary>
        /// <param name="maximumSize">The maximum size of the memory; 
        /// that is, the instructions region size plus the stack region size.</param>
        /// <param name="instructions">The instructions to load into the memory.</param>
        /// <returns></returns>
        public static Memory CreateMemory(int maximumSize, InstructionSet instructions)
        {
            Memory memory = new Memory(maximumSize);
            int stackMemory;

            memory.LoadInstructions(instructions.Instructions);
            stackMemory = maximumSize - instructions.Size;
            memory.StackRegion = new Stack(stackMemory < 0 ? 0 : stackMemory);
            return memory;
        }

        private int _maximumSize;
        private Stack _stack;
        private string[] _instructions;

        /// <summary>
        /// Initializes a new instance of the <see cref="Memory"/> class with the 
        /// specified sizes for the instructions and stack regions.
        /// </summary>
        /// <param name="maximumSize">The maximum size, in bytes, of the memory.</param>
        private Memory(int maximumSize)
        {
            _maximumSize = maximumSize;
        }

        /// <summary>
        /// Gets the stack region, that is, the program data region.
        /// </summary>
        public Stack StackRegion
        {
            get { return _stack; }
            private set { _stack = value; }
        }

        /// <summary>
        /// Gets the instructions region.
        /// </summary>
        public string[] InstructionsRegion
        {
            get { return _instructions; }
        }

        /// <summary>
        /// Loads the specified instructions into the memory.
        /// </summary>
        /// <param name="instructions">The instructions to be loaded.</param>
        private void LoadInstructions(string[] instructions)
        {
            _instructions = new string[instructions.Length];
            Array.Copy(instructions, _instructions, instructions.Length);
        }
    }
}