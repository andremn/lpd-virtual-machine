using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace LPD.VirtualMachine.View
{
    /// <summary>
    /// Interaction logic for CustomInputDialog.xaml
    /// </summary>
    public partial class CustomInputDialog : BaseMetroDialog
    {
        /// <summary>
        /// Gets the regular expression used to verify if a text is a integer number.
        /// </summary>

        public const string IntegerNumberRegexExpression = @"^-?[0-9]\d*(\d+)?$";

        /// <summary>
        /// The <see cref="TaskCompletionSource{int}"/> used to wait the user input.
        /// </summary>
        private TaskCompletionSource<int> _taskCompletionSource;

        /// <summary>
        /// Creates a new instance of the <see cref="CustomInputDialog"/> class with the specified title.
        /// </summary>
        /// <param name="title">The dialog's title.</param>
        public CustomInputDialog(string title)
        {
            InitializeComponent();
            Title = title;
        }

        /// <summary>
        /// Shows the dialog in the specified <see cref="MetroWindow"/>.
        /// </summary>
        /// <param name="window">The <see cref="MetroWindow"/> to host the dialog.</param>
        /// <returns></returns>
        public async Task<int> ShowDialogAsync(MetroWindow window)
        {
            int value;

            _taskCompletionSource = new TaskCompletionSource<int>();
            InputTextBox.Text = string.Empty;
            KeyDown += OnCustomInputDialogKeyDown;
            //Shows the dialog.
            await DialogManager.ShowMetroDialogAsync(window, this);
            //Forces the focus to the textbox.
            InputTextBox.Focus();
            //Waits for the user to click "ok" or hit Enter.
            value = await _taskCompletionSource.Task;
            //Hides the dialog.
            await DialogManager.HideMetroDialogAsync(window, this);
            //Returns...
            return value;
        }

        /// <summary>
        /// Sets the result and releases the TaskCompletionSource.
        /// </summary>
        private void SetResult()
        {
            _taskCompletionSource.TrySetResult(int.Parse(InputTextBox.Text));
        }

        /// <summary>
        /// Occurs when the "OkButton" button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The data of the event.</param>
        private void OnOkButtonButtonClick(object sender, RoutedEventArgs e)
        {
            SetResult();
        }

        /// <summary>
        /// Occurs when an key is hit.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The data of the event.</param>
        private void OnCustomInputDialogKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                KeyDown -= OnCustomInputDialogKeyDown;
                SetResult();
            }
        }

        private void OnInputTextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));

                if (!TextBoxTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void OnInputTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var text = e.Text;

            e.Handled = text == "-" ? InputTextBox.Text.Contains('-') : !TextBoxTextAllowed(text);
        }

        private bool TextBoxTextAllowed(string text)
        {
            return new Regex(IntegerNumberRegexExpression).IsMatch(text);
        }
    }
}
