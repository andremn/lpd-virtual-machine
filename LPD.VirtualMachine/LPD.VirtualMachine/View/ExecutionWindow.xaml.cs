using LPD.VirtualMachine.Engine;
using LPD.VirtualMachine.Engine.HAL;
using LPD.VirtualMachine.ViewModel;
using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using static System.IO.Path;

namespace LPD.VirtualMachine.View
{
    /// <summary>
    /// Interaction logic for ExecutionWindow.xaml
    /// </summary>
    public partial class ExecutionWindow : MetroWindow, IInputProvider, IOutputProvider, IProgramExecutor
    {
        private int _nextInputValue;
        private ManualResetEventSlim _manualResetSlim = new ManualResetEventSlim(false);
        private EventWaitHandle _executionSynchronizer;

        /// <summary>
        /// Gets the current execution context.
        /// </summary>
        public Engine.ExecutionContext Context { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionWindow"/> class with the specified program name.
        /// <param name="filePath">The path of the program going to be executed.</param>
        /// <param name="context">The execution context.</param>
        /// </summary>
        public ExecutionWindow(string filePath, Engine.ExecutionContext context)
        {
            InitializeComponent();
            Loaded += OnWindowLoaded;
            Title += GetFileNameWithoutExtension(filePath);
            InstructionsDataGrid.DataContext = ConvertInstructionsToInstructionViewModel(filePath);
            Context = context;
        }

        /// <summary>
        /// Called when the caller finished executing the current instruction.
        /// </summary>
        public void OnInstructionReadyToExecute()
        {
            _executionSynchronizer.WaitOne();
        }

        /// <summary>
        /// Called when the caller finished its execution.
        /// </summary>
        public void OnFinished()
        {

        }

        /// <summary>
        /// Called when the caller finished its executin due to a faltal error.
        /// </summary>
        /// <param name="error">The error data.</param>
        public void OnFatalError(object error)
        {

        }

        /// <summary>
        /// Reads the next input value.
        /// </summary>
        /// <returns>The next input value.</returns>
        public int ReadInputValue()
        {
            TextCompositionManager.AddTextInputHandler(this, OnTextComposition);
            //Since the CPU execution is not done on the UI thread, this will not block the UI
            _manualResetSlim.Wait();
            return _nextInputValue;
        }

        /// <summary>
        /// Writes the specified value to th output.
        /// </summary>
        /// <param name="value">The value to output.</param>
        public void Print(int value)
        {
            Dispatcher.Invoke(() => AppendLineToOutput(value.ToString()));
        }

        private void AppendLineToOutput(string line)
        {
            OutputListView.Items.Add(line);
        }

        /// <summary>
        /// Occurs when the user press a key.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The data of the event.</param>
        private void OnTextComposition(object sender, TextCompositionEventArgs e)
        {
            _nextInputValue = int.Parse(e.Text);
            TextCompositionManager.RemoveTextInputHandler(this, OnTextComposition);
            _manualResetSlim.Set();
            _manualResetSlim.Reset();
        }

        /// <summary>
        /// Occurs when the window is loaded and displayed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event's info.</param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _executionSynchronizer = new EventWaitHandle(false, EventResetMode.AutoReset);
            InstructionsDataGrid.SelectedIndex = 0;
            CPU.Instance.BeginExecution(this);
        }

        private IList<InstructionViewModel> ConvertInstructionsToInstructionViewModel(string filePath)
        {
            string[] instructions = InstructionSet.CreateFromFile(filePath, false).Instructions;
            IList<InstructionViewModel> instructionViewModel = new List<InstructionViewModel>(instructions.Length);

            for (uint i = 0; i < instructions.Length; i++)
            {
                string instruction = instructions[i];

                instructionViewModel.Add(new InstructionViewModel()
                {
                    Comment = "Instrução",
                    //Removes various spaces and tabs and replace them by two tabs.
                    Content = Regex.Replace(instruction, @"(?:\s+|\t+)", "\t\t"),
                    LineNumber = i
                });
            }

            return instructionViewModel;
        }

        private void OnNextInstructionButtonClick(object sender, RoutedEventArgs e)
        {
            int currentInstructionAddress = Context.ProgramCounter.Current + 1;

            InstructionsDataGrid.SelectedIndex = currentInstructionAddress;
            InstructionsDataGrid.ScrollIntoView(InstructionsDataGrid.Items[currentInstructionAddress]);
            _executionSynchronizer.Set();
        }
    }
}
