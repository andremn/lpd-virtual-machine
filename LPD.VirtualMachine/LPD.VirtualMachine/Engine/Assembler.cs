﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// A very simple assembler. It only convert labels to memory address.
    /// </summary>
    public static class Assembler
    {
        private static string[] _Instructions;

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
                throw new ArgumentException($"O parametro {nameof(inputFilePath)} é nulo ou vazio.");
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                throw new ArgumentException($"O parametro {nameof(outputFilePath)} é nulo ou vazio.");
            }



            using (StreamReader reader = new StreamReader(File.OpenRead(inputFilePath)))
            {
                string program = reader.ReadToEnd().ToUpper();

                _Instructions = program.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                                                          .Select(instruction => instruction.TrimStart().TrimEnd())
                                                          .ToArray();
                //First step
                IDictionary<string, int> addresses = GetAddresses(program);
                //Second step
                string newProgram = AssembleInstructions(program, addresses);
                //Third step
                SaveFile(newProgram, outputFilePath);
            }
        }

        /// <summary>
        /// First step: get the addresses of the labels.
        /// </summary>
        /// <param name="program">The program to get the addresses.</param>
        /// <returns>A collection of label and its address.</returns>
        private static IDictionary<string, int> GetAddresses(string program)
        {
            Dictionary<string, int> addresses = new Dictionary<string, int>();
            int address = -1;

            foreach (string instruction in _Instructions)
            {
                string[] splittedInstruction = instruction.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                address++;

                if (splittedInstruction.Length == 1 || splittedInstruction[1] != NULL)
                {
                    continue;
                }

                addresses[splittedInstruction[0]] = address;
            }

            return addresses.OrderByDescending(pair => pair.Value).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
        }

        /// <summary>
        /// Second step: changes the instructions labels to their corresponding addresses.
        /// </summary>
        /// <param name="program">The program to change the labels to addresses.</param>
        /// <param name="addresses">The collection of address and labels.</param>
        /// <returns>The assembled program.</returns>
        private static string AssembleInstructions(string program, IDictionary<string, int> addresses)
        {
            var regex = new Regex(@"L\d+$");

            for (int i = 0; i < _Instructions.Length; i++)
            {
                string[] splittedInstruction = _Instructions[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (splittedInstruction.Length > 1)
                {
                    if (regex.IsMatch(splittedInstruction[1]))
                    {
                        var label = splittedInstruction[1];

                        int address;

                        if (!addresses.TryGetValue(label, out address))
                        {
                            continue;
                        }

                        _Instructions[i] = _Instructions[i].Replace(label, address.ToString());
                    }
                }
            }

            return string.Join(Environment.NewLine, _Instructions);
        }

        /// <summary>
        /// Third step: save the assembled program to a file.
        /// </summary>
        /// <param name="newProgram">The assembled program.</param>
        /// <param name="path">The path of the file.</param>
        private static void SaveFile(string newProgram, string path)
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(path)))
            {
                writer.Write(newProgram);
            }
        }
    }
}
