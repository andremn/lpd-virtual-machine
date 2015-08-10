using LPD.VirtualMachine.Engine;
using LPD.VirtualMachine.Engine.HAL;
using MahApps.Metro.Controls;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace LPD.VirtualMachine.View
{
    /// <summary>
    /// Interaction logic for ExecutionWindow.xaml
    /// </summary>
    public partial class ExecutionWindow : MetroWindow, IInputProvider, IOutputProvider
    {
        private int _nextInputValue;
        private ManualResetEvent _mre = new ManualResetEvent(false);

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionWindow"/> class with the specified program name.
        /// <param name="programName">The name of the program to be executed.</param>
        /// </summary>
        public ExecutionWindow(string programName)
        {
            InitializeComponent();
            Loaded += OnWindowLoaded;
            Title += programName;
        }

        /// <summary>
        /// Reads the next input value.
        /// </summary>
        /// <returns>The next input value.</returns>
        public int ReadInputValue()
        {
            TextCompositionManager.AddTextInputHandler(this, OnTextComposition);
            //Since the CPU execution is not done on the UI thread, this will not block the UI
            _mre.WaitOne();
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
            this.OutputTextBlock.Inlines.Add(line);
            this.OutputTextBlock.Inlines.Add("\n");
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
            _mre.Set();
        }

        /// <summary>
        /// Occurs when the window is loaded and displayed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event's info.</param>
        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            CPU.Instance.BeginExecution();
        }
    }
}
