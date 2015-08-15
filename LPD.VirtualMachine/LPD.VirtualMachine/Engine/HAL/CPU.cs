using LPD.VirtualMachine.Engine.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.HAL
{
    /// <summary>
    /// Represents a virtual Central Processing Unit (CPU).
    /// </summary>
    public sealed class CPU
    {
        /// <summary>
        /// The initial program counter value.
        /// </summary>
        public const int InitialProgramCounter = 0;

        /// <summary>
        /// Statically initializes a new instance of the <see cref="CPU"/> class.
        /// </summary>
        static CPU()
        {
            Instance = new CPU();
        }

        /// <summary>
        /// Gets the current instance of the <see cref="CPU"/> class.
        /// </summary>
        public static readonly CPU Instance;
        
        /// <summary>
        /// The CPU's known instructions.
        /// </summary>
        private Dictionary<string, Type> _knownInstructionTypes;

        /// <summary>
        /// The program executor.
        /// </summary>
        private IProgramExecutor _executor;
                
        /// <summary>
        /// Initializes a new instance of the <see cref="CPU"/> class.
        /// </summary>
        private CPU()
        {
        }

        /// <summary>
        /// Raised when the current execution is done without errors.
        /// </summary>
        public event EventHandler Finished;

        /// <summary>
        /// Starts the execution of instructions that resides in the memory.
        /// The execution is done in a separate thread.
        /// </summary>
        /// <param name="executor">Who is handling the program execution.</param>
        public void BeginExecution(IProgramExecutor executor)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            _executor = executor;
            InitializeTypes();
            //Run all the processing in another thread, so we do not block the UI.
            Task.Factory.StartNew(Execute);
        }

        /// <summary>
        /// Executes all the instructions in the specified memory until the terminte instruction (HLT).
        /// </summary>
        private void Execute()
        {
            ExecutionContext context = _executor.Context;
            string[] instructions = context.Memory.InstructionsRegion;
            
            //Executes the instructions until a 'HLT' instruction is found.
            while (true)
            {
                _executor.OnInstructionExecuting();

                //The first thing we need is to get the program counter from the current context.
                int pc = context.ProgramCounter.Current;

                //Oh noooo!! Some instruction just set the PC to a invalid position.
                //We cannot go on...
                if (pc > instructions.Length)
                {
                    throw new InvalidProgramCounterException($"Program counter at position {pc} is out greater than instructions memory region.");
                }

                //Gets the next instruction from the memory, splitted in instruction name and instruction parameter, if any.
                //Ex: currentInstructionRaw[0] = LDC; currentInstructionRaw[1] = 5 or
                //currentInstructionRaw[0] = ADD; currentInstructionRaw[1] does not exist since the ADD instruction doesn't take parameters
                string[] currentInstructionRaw = instructions[pc].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                //The instruction name is in the first position of the splitted array.
                string currentInstructionName = currentInstructionRaw[0].ToUpper();

                //If an HLT instruction is read from memory...
                if (currentInstructionName == HLT)
                {
                    //... stops everything!
                    break;
                }

                //If the instruction name or parameter is NULL... 
                if (currentInstructionName == NULL || (currentInstructionRaw.Length > 1 && currentInstructionRaw[1] == NULL))
                {
                    //... means we need do nothing or the instruction name is actually an address we may jump in the future.
                    //The only thing we need to do is increment the program counter.
                    context.ProgramCounter.Increment();
                    continue;
                }
                
                Type currentInstructionType;

                //The read instruction exists?
                if (!_knownInstructionTypes.TryGetValue(currentInstructionName, out currentInstructionType))
                {
                    //No!! The instruction is not valid! Time to throw some exception hehehe...
                    throw new InvalidInstructionException($"The instruction {currentInstructionName} is not valid.");
                }
                
                //Gets the instruction. This time is for real!
                IInstruction currentInstruction = (IInstruction)Activator.CreateInstance(currentInstructionType);
                //The parameters are only integers.
                int[] parameters = currentInstructionRaw.Skip(1).Select(parameter => int.Parse(parameter)).ToArray();
                
                //Now the shit gets real...
                //The instruction will be executed... fingers crossed!
                try
                {
                    currentInstruction.Execute(context, currentInstructionRaw.Length > 1 ? parameters : null);
                }
                catch (InvalidInstructionException e)
                {
                    _executor.OnFatalError(e.Message);
                    return;
                }
                
                _executor.OnInstructionExecuted();
            }

            //Let's just tell the guys we're over
            _executor.OnFinished();
        }

        /// <summary>
        /// Initializes all the instructions types in the current assembly.
        /// </summary>
        private void InitializeTypes()
        {
            //Gets all the instructions (objects with the InstructionAttribute) from the current running assembly.
            var typesInAssembly = Assembly.GetAssembly(GetType()).GetTypes();
            //Gets only the type that has the InstructionAttribute attribute.
            //They're the ones we want.
            var instructionTypes = typesInAssembly
                                    .Zip(typesInAssembly.Select(type => type.GetCustomAttribute<InstructionAttribute>()),
                                            (type, attribute) => new Tuple<Type, InstructionAttribute>(type, attribute))
                                    .Where(tuple => tuple.Item2 != null)
                                    .ToList();

            _knownInstructionTypes = new Dictionary<string, Type>(instructionTypes.Count);

            //Copies all the instructions names and types to our dictionary.
            foreach (var tuple in instructionTypes)
            {
                _knownInstructionTypes[tuple.Item2.Name] = tuple.Item1;
            }
        }
    }
}
