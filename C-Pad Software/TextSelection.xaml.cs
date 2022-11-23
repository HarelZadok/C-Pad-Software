using System.Windows;
using System.Windows.Controls;

namespace C_Pad_Software;

public partial class TextSelection : UserControl, IItemSlot
{
    private MainWindow Mw { get; set; }
    
    public TextSelection(MainWindow mw)
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
        return Input;
    }

    public void OnMoveRight(object sender, RoutedEventArgs routedEventArgs)
    {
        Mw.ItemSlotMoveRight(this);
    }

    public void OnMoveLeft(object sender, RoutedEventArgs routedEventArgs)
    {
        Mw.ItemSlotMoveLeft(this);
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