using LPD.VirtualMachine.Engine;
using LPD.VirtualMachine.Engine.HAL;
using LPD.VirtualMachine.Properties;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static System.IO.Path;
using System;

namespace LPD.VirtualMachine.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private const string OpenDialogDefaultFilter = "Arquivo de assembly|*.asmd|Todos os arquivos|*.*";

        private string _selectedFilePath;
        private Window _executionWindow;
        private bool _isStartButtonVisible = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
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
            PrepareExecution();
        }

        private async void PrepareExecution()
        {
            _executionWindow = new ExecutionWindow(GetFileNameWithoutExtension(_selectedFilePath));

            int virtualMachineSize = (int)Settings.Default[App.SystemMemorySettingKey];
            InstructionSet instructionsCollection = await InstructionSet.CreateFromFileAsync(_selectedFilePath);
            Memory memory = Memory.CreateMemory(virtualMachineSize, instructionsCollection);
            ExecutionContext context = new ExecutionContext()
            {
                ProgramCounter = new ProgramCounter(CPU.InitialProgramCounter),
                InputProvider = (IInputProvider)_executionWindow,
                OutputProvider = (IOutputProvider)_executionWindow,
                Memory = memory
            };

            CPU.Instance.Initialize(context);

            if (!_isStartButtonVisible)
            {
                DoStartButtonAnimation();
            }
        }

        /// <summary>
        /// Occurs when the button "Iniciar" is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            _executionWindow.ShowDialog();
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

        private void OnLoadButtonDrop(object sender, DragEventArgs e)
        {
            StopDragEnterAnimation();

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string file = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                _selectedFilePath = file;
                PrepareExecution();
            }
        }

        private void OnLoadButtonDragEnter(object sender, DragEventArgs e)
        {
            if (e.OriginalSource.GetType() != typeof(Image))
            {
                return;
            }

            (FindResource("DragBeginAnimation") as Storyboard).Begin(this, true);
        }

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
