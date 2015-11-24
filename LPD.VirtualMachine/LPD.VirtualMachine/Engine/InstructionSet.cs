using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Represents a set of instructions.
    /// </summary>
    public class InstructionSet
    {
        #region Instruction names

        /// <summary>
        /// The START instruction name.
        /// </summary>
        public const string START = "START";

        /// <summary>
        /// The HLT instruction name.
        /// </summary>
        public const string HLT = "HLT";

        /// <summary>
        /// The LDC instruction name.
        /// </summary>
        public const string LDC = "LDC";

        /// <summary>
        /// The LDV instruction name.
        /// </summary>
        public const string LDV = "LDV";

        /// <summary>
        /// The ADD instruction name.
        /// </summary>
        public const string ADD = "ADD";

        /// <summary>
        /// The SUB instruction name.
        /// </summary>
        public const string SUB = "SUB";

        /// <summary>
        /// The MULT instruction name.
        /// </summary>
        public const string MULT = "MULT";

        /// <summary>
        /// The DIVI instruction name.
        /// </summary>
        public const string DIVI = "DIVI";

        /// <summary>
        /// The INV instruction name.
        /// </summary>
        public const string INV = "INV";

        /// <summary>
        /// The AND instruction name.
        /// </summary>
        public const string AND = "AND";

        /// <summary>
        /// The OR instruction name.
        /// </summary>
        public const string OR = "OR";

        /// <summary>
        /// The NEG instruction name.
        /// </summary>
        public const string NEG = "NEG";

        /// <summary>
        /// The CME instruction name.
        /// </summary>
        public const string CME = "CME";

        /// <summary>
        /// The CMA instruction name.
        /// </summary>
        public const string CMA = "CMA";

        /// <summary>
        /// The CMQ instruction name.
        /// </summary>
        public const string CEQ = "CEQ";

        /// <summary>
        /// The CDIF instruction name.
        /// </summary>
        public const string CDIF = "CDIF";

        /// <summary>
        /// The CMEQ instruction name.
        /// </summary>
        public const string CMEQ = "CMEQ";

        /// <summary>
        /// The CMAQ instruction name.
        /// </summary>
        public const string CMAQ = "CMAQ";

        /// <summary>
        /// The STR instruction name.
        /// </summary>
        public const string STR = "STR";

        /// <summary>
        /// The JMP instruction name.
        /// </summary>
        public const string JMP = "JMP";

        /// <summary>
        /// The JMPF instruction name.
        /// </summary>
        public const string JMPF = "JMPF";

        /// <summary>
        /// The NULL instruction name.
        /// </summary>
        public const string NULL = "NULL";

        /// <summary>
        /// The RD instruction name.
        /// </summary>
        public const string RD = "RD";

        /// <summary>
        /// The PRN instruction name.
        /// </summary>
        public const string PRN = "PRN";

        /// <summary>
        /// The ALLOC instruction name.
        /// </summary>
        public const string ALLOC = "ALLOC";

        /// <summary>
        /// The DALLOC instruction name.
        /// </summary>
        public const string DALLOC = "DALLOC";

        /// <summary>
        /// The CALL instruction name.
        /// </summary>
        public const string CALL = "CALL";

        /// <summary>
        /// The RETURN instruction name.
        /// </summary>
        public const string RETURN = "RETURN";

        /// <summary>
        /// The RETURNF instruction name.
        /// </summary>
        public const string RETURNF = "RETURNF";

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionSet"/> class with the specified instruction set.
        /// </summary>
        /// <param name="instructions">The instruction set.</param>
        public InstructionSet(string[] instructions)
        {
            Instructions = instructions;
        }

        /// <summary>
        /// Gets the instructions.
        /// </summary>
        public string[] Instructions { get; }

        /// <summary>
        /// Gets the size of all instructions, in bytes.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Creates a new <see cref="InstructionSet"/> from the instructions in the specified file.
        /// </summary>
        /// <param name="filePath">The location of the file to read the instructions from.</param>
        /// <returns><see cref="InstructionSet"/></returns>
        public static InstructionSet CreateFromFile(string filePath, bool assemble = true)
        {
            InstructionSet collection;
            string tempFile = null;

            if (assemble)
            {
                tempFile = Path.GetTempFileName();
                Assembler.AssembleFromFile(filePath, tempFile);
            }

            using (var fileStream = File.OpenRead(tempFile ?? filePath))
            {
                using (var reader = new StreamReader(fileStream))
                {
                    string[] lines = reader.ReadToEnd().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    lines = lines.Select(line => line.TrimEnd()).ToArray();
                    collection = new InstructionSet(lines);
                    collection.Size = (int)fileStream.Length;
                }
            }

            if (tempFile != null)
            {
                File.Delete(tempFile);
            }

            return collection;
        }
    }
}
