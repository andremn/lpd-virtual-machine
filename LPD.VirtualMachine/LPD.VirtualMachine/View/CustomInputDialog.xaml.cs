using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace LPD.VirtualMachine.View
{
    /// <summary>
    /// Interaction logic for CustomInputDialog.xaml
    /// </summary>
    public partial class CustomInputDialog : BaseMetroDialog
    {
        private TaskCompletionSource<int> _tcs = new TaskCompletionSource<int>();

        public CustomInputDialog(string title)
        {
            InitializeComponent();
            Title = title;
        }

        public async Task<int> ShowDialogAsync(MetroWindow window)
        {
            await DialogManager.ShowMetroDialogAsync(window, this);
            PART_TextBox.Focus();
            PART_TextBox.Select(0, 0);

            int value = await _tcs.Task;
            await DialogManager.HideMetroDialogAsync(window, this);
            return value;
        }

        private void OnAffirmativeButtonClick(object sender, RoutedEventArgs e)
        {
            _tcs.SetResult(int.Parse(PART_TextBox.Text));
        }
    }
}
