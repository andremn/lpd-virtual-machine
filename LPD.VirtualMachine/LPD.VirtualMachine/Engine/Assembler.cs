using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// A very simple assembler. It only convert labels to memory address.
    /// </summary>
    public static class Assembler
    {
        /// <summary>
        /// Assembles the instructions in the input file to a new file specified as the output file.
        /// This methods simply translates labels to memory addres, so the CPU can execute the program.
        /// </summary>
        /// <param name="inputFilePath">The file to assemble.</param>
        /// <param name="outputFilePath">The file to save the assembled program.</param>
        public static void AssembleFromFile(string inputFilePath, string outputFilePath)
        {
            if (string.IsNullOrEmpty(inputFilePath))
            {
                throw new ArgumentException($"The parameter {nameof(inputFilePath)} is null or empty.");
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                throw new ArgumentException($"The parameter {nameof(outputFilePath)} is null or empty.");
            }
            
            using (StreamReader reader = new StreamReader(File.OpenRead(inputFilePath)))
            {
                string[] instructions = reader.ReadToEnd().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                              .Select(instruction => instruction.TrimEnd())
                                              .ToArray();
                //First step
                IDictionary<string, int> addresses = GetAddresses(instructions);
                //Second step
                instructions = AssembleInstructions(instructions, addresses);
                //Third step
                SaveFile(instructions, outputFilePath);
            }
        }

        /// <summary>
        /// First step: get the addresses of the labels.
        /// </summary>
        /// <param name="instructions">The instructions to get the addresses.</param>
        /// <returns>A collection of label and its address.</returns>
        private static IDictionary<string, int> GetAddresses(string[] instructions)
        {
            Dictionary<string, int> addresses = new Dictionary<string, int>();
            int address = -1;

            foreach (string instruction in instructions)
            {
                string[] splittedInstruction = instruction.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                address++;

                if (splittedInstruction.Length == 1 || splittedInstruction[1] != NULL)
                {
                    continue;
                }

                addresses[splittedInstruction[0]] = address;
            }

            return addresses;
        }

        /// <summary>
        /// Second step: changes the instructions labels to their corresponding addresses.
        /// </summary>
        /// <param name="instructions">The instructions to change the labels to addresses.</param>
        /// <param name="addresses">The collection of address and labels.</param>
        /// <returns>The assembled instructions.</returns>
        private static string[] AssembleInstructions(string[] instructions, IDictionary<string, int> addresses)
        {
            foreach (var address in addresses)
            {
                string instrutionToReplace = instructions[address.Value];

                instructions[address.Value] = instrutionToReplace.Replace(address.Key, address.Value.ToString());
            }

            return instructions;
        }

        /// <summary>
        /// Third step: save the assembled program to a file.
        /// </summary>
        /// <param name="instructions">The assembled instructions.</param>
        /// <param name="path">The path of the file.</param>
        private static void SaveFile(string[] instructions, string path)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path)))
            {
                foreach (string instruction in instructions)
                {
                    writer.WriteLine(instruction);
                }
            }
        }
    }
}
