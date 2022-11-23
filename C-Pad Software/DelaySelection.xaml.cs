using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace C_Pad_Software
{
    public partial class DelaySelection : UserControl, IItemSlot
    {
        private MainWindow Mw { get; set; }
        private int _lastIndex;
        public static ulong Input;
        public DelaySelection(MainWindow mw)
        {
            InitializeComponent();

            this.Mw = mw;
            ulong a = (ulong)Math.Pow(1000, 3 - CbTimeSelection!.SelectedIndex);
            Input = (ulong)(a * double.Parse(TimeInput!.Text));
        }

        public bool IsChecked()
        {
            return Checked.IsChecked == true;
        }

        public object GetInput()
        {
            return (Input * 10) + (ulong)CbTimeSelection!.SelectedIndex;
        }

        public void SetChecked(bool check)
        {
            Checked.IsChecked = check;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void OnMoveRight(object sender, RoutedEventArgs routedEventArgs)
        {
            Mw.ItemSlotMoveRight(this);
        }

        public void OnMoveLeft(object sender, RoutedEventArgs routedEventArgs)
        {
            Mw.ItemSlotMoveLeft(this);
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

        private void TimeChanged(object sender, SelectionChangedEventArgs e)
        {
            double a = Math.Pow(1000, CbTimeSelection.SelectedIndex - _lastIndex);
            TimeInput.Text = (double.Parse(TimeInput.Text) * a).ToString();
            _lastIndex = CbTimeSelection.SelectedIndex;
        }

        private void TimeCountChanged(object sender, TextChangedEventArgs e)
        {
            if (CbTimeSelection != null && TimeInput != null)
            {
                if (string.IsNullOrEmpty(TimeInput.Text))
                    TimeInput.Text = "0";
                
                ulong a = (ulong)Math.Pow(1000, 3 - CbTimeSelection.SelectedIndex);
                Input = (ulong)(a * double.Parse(TimeInput.Text));
            }
        }
    }
}