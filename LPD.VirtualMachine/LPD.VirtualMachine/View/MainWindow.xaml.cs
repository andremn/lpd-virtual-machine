using LPD.VirtualMachine.Engine;
using LPD.VirtualMachine.Engine.HAL;
using static LPD.VirtualMachine.Properties.Resources;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System;
using System.Threading.Tasks;
using LPD.VirtualMachine.Properties;

namespace LPD.VirtualMachine.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private const string OpenDialogDefaultFilter = "Arquivo de assembly|*.asmd|Todos os arquivos|*.*";

        private string _selectedFilePath;
        private bool _isStartButtonVisible = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        /// <summary>
        /// Starts and completes the animation for the StartButton control.
        /// </summary>
        private void DoStartButtonAnimation()
        {
            Storyboard sb = FindResource("StartButtonStoryboard") as Storyboard;

            sb.Completed += delegate
            {
                StartButton.RenderTransform = new ScaleTransform();
                StartButton.Margin = new Thickness(50, 13, 0, 0);
                StartButton.Width = 250d;
                sb.Stop();
                _isStartButtonVisible = true;
            };
            sb.Begin();
        }

        /// <summary>
        /// Stops the anitmation for the StartButton control.
        /// </summary>
        private void StopDragEnterAnimation()
        {
            Storyboard sb = FindResource("DragEndAnimation") as Storyboard;

            sb.Completed += delegate
            {
                LoadButton.RenderTransform = new ScaleTransform();
                sb.Stop(this);
            };
            (FindResource("DragBeginAnimation") as Storyboard).Stop(this);
            sb.Begin(this, true);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (App.FileFromArgument != null)
            {
                _selectedFilePath = App.FileFromArgument;
                DoStartButtonAnimation();
            }            
        }

        /// <summary>
        /// Occurs when the button "Carregar" is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private void OnLoadButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = OpenDialogDefaultFilter
            };

            bool? result = openFileDialog.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return;
            }

            _selectedFilePath = openFileDialog.FileName;

            if (!_isStartButtonVisible)
            {
                DoStartButtonAnimation();
            }
        }

        /// <summary>
        /// Prepares the virtual machine to start the execution.
        /// </summary>
        /// <returns>The window that will show the execution information.</returns>
        private async Task<ExecutionWindow> PrepareExecutionAsync()
        {
            Memory memory = null;

            await Task.Run(async () =>
            {
                int virtualMachineSize = (int)Settings.Default[App.SystemMemorySettingKey];
                InstructionSet instructionsCollection = InstructionSet.CreateFromFile(_selectedFilePath);
                Memory mem = Memory.CreateMemory(virtualMachineSize, instructionsCollection);

                if (mem.StackRegion.Count <= 0)
                {
                    await this.ShowMessageAsync(ProgramTooBigErrorMessage, ProgramTooBigErrorTitle);
                    return;
                }

                memory = mem;
            });
                        
            if (memory == null)
            {
                //We don't have memory :'(
                return null;
            }

            ExecutionWindow executionWindow;
            ExecutionContext currentContext = new ExecutionContext()
            {
                ProgramCounter = new ProgramCounter(CPU.InitialProgramCounter),
                Memory = memory
            };

            executionWindow = new ExecutionWindow(_selectedFilePath, App.ProgramNameFromArgument, currentContext);
            currentContext.InputProvider = executionWindow as IInputProvider;
            currentContext.OutputProvider = executionWindow as IOutputProvider;
            return executionWindow;
        }

        /// <summary>
        /// Occurs when the button "Iniciar" is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private async void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                ExecutionWindow executionWindow = await PrepareExecutionAsync();

                if (executionWindow != null)
                {
                    executionWindow.ShowDialog();
                }
            }
            catch (OutOfMemoryException)
            {
                
            }
        }

        /// <summary>
        /// Occurs when the button "Configurações" is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private void OnSettingsButtonClick(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }

        /// <summary>
        /// Occurs when the item being dragged is drop over the LoadButton button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The data of the event.</param>
        private async void OnLoadButtonDrop(object sender, DragEventArgs e)
        {
            StopDragEnterAnimation();

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                _selectedFilePath = file;
                DoStartButtonAnimation();
                await PrepareExecutionAsync();
            }
        }

        /// <summary>
        /// Occurs when an item is dragged over the LoadButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The data of the event.</param>
        private void OnLoadButtonDragEnter(object sender, DragEventArgs e)
        {
            if (e.OriginalSource.GetType() != typeof(Image))
            {
                return;
            }

            (FindResource("DragBeginAnimation") as Storyboard).Begin(this, true);
        }

        /// <summary>
        /// Occurs when an item is no more being dragged over the LoadButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The data of the event.</param>
        private void OnLoadButtonDragLeave(object sender, DragEventArgs e)
        {
            if (e.OriginalSource.GetType() != typeof(Image))
            {
                return;
            }

            StopDragEnterAnimation();
        }
    }
}
