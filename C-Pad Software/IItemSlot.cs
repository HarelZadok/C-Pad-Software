using System.Windows;

namespace C_Pad_Software
{
    public interface IItemSlot
    {
        bool IsChecked();

        object GetInput();

        void SetChecked(bool check);
        
        void CheckChanged(object sender, RoutedEventArgs e);

        void SetRight(bool val);
        
        void SetLeft(bool val);
    }
}