using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace C_Pad_Software
{
    public partial class FileSelection : UserControl, IItemSlot
    {
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        
        private MainWindow Mw { get; set; }
        public FileSelection(MainWindow mw)
        {
            InitializeComponent();
            
            this.Mw = mw;
        }

        public bool IsChecked()
        {
            return Checked.IsChecked == true;
        }

        public object GetInput()
        {
            return _openFileDialog;
        }

        public void OnMoveRight(object sender, RoutedEventArgs routedEventArgs)
        {
            Mw.ItemSlotMoveRight(this);
        }

        public void OnMoveLeft(object sender, RoutedEventArgs routedEventArgs)
        {
            Mw.ItemSlotMoveLeft(this);
        }

        private void ChooseFileButton(object sender, RoutedEventArgs e)
        {
            if (_openFileDialog.ShowDialog() == true)
                ButtonText.Text = _openFileDialog.SafeFileName;
        }
        
        public void SetChecked(bool check)
        {
            Checked.IsChecked = check;
        }
        
        public void CheckChanged(object sender, RoutedEventArgs e)
        {
            Mw.CheckAll.IsChecked = Mw.AllInputsChecked(this);
            Mw.CheckAll.IsChecked = Mw.CheckAll.IsChecked == true ? !Checked.IsChecked : false;
        }

        public void SetRight(bool val)
        {
            Right.IsEnabled = val;
        }

        public void SetLeft(bool val)
        {
            Left.IsEnabled = val;
        }
    }
}