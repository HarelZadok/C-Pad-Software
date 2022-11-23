using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace C_Pad_Software
{
    public partial class KeySelection : UserControl, IItemSlot
    {
        public struct AInput
        {
            public ushort Key;
            public bool Released;
        }

        public AInput Input;
        private enum BTN
        {
            BACKSPACE = 0x08,
            TAB,
            ENTER = 0x0D,
            SHIFT = 0x10,
            CONTROL,
            ALT,
            PAUSE,
            CAPS,
            ESCAPE = 0x1B,
            SPACE = 0x20,
            PAGE_UP,
            PAGE_DOWN,
            HOME,
            LEFT,
            UP,
            RIGHT,
            DOWN,
            SELECT,
            PRINT = 0x2A,
            EXECUTE,
            PRTSCRN,
            INSERT,
            DEL,
            HELP,
            KEY_0 = 0x30,
            KEY_1,
            KEY_2,
            KEY_3,
            KEY_4,
            KEY_5,
            KEY_6,
            KEY_7,
            KEY_8,
            KEY_9,
            KEY_A = 0x41,
            KEY_B,
            KEY_C,
            KEY_D,
            KEY_E,
            KEY_F,
            KEY_G,
            KEY_H,
            KEY_I,
            KEY_J,
            KEY_K,
            KEY_L,
            KEY_M,
            KEY_N,
            KEY_O,
            KEY_P,
            KEY_Q,
            KEY_R,
            KEY_S,
            KEY_T,
            KEY_U,
            KEY_V,
            KEY_W,
            KEY_X,
            KEY_Y,
            KEY_Z,
            LWIN = 0x5B,
            RWIN,
            APPS,
            SLEEP = 0x5F,
            NUMPAD_0,
            NUMPAD_1,
            NUMPAD_2,
            NUMPAD_3,
            NUMPAD_4,
            NUMPAD_5,
            NUMPAD_6,
            NUMPAD_7,
            NUMPAD_8,
            NUMPAD_9,
            MULTIPLY,
            ADD,
            SEPARATOR,
            SUBTRACT,
            DOT,
            DIVIDE,
            F1,
            F2,
            F3,
            F4,
            F5,
            F6,
            F7,
            F8,
            F9,
            F10,
            F11,
            F12,
            F13,
            F14,
            F15,
            F16,
            F17,
            F18,
            F19,
            F20,
            F21,
            F22,
            F23,
            F24,
            NUMLOCK = 0x90,
            SCROLL,
            LSHIFT = 0xA0,
            RSHIFT,
            LCONTROL,
            RCONTROL,
            LALT,
            RALT,
            VOLUME_MUTE = 0xAD,
            VOLUME_DOWN,
            VOLUME_UP,
            MEDIA_NEXT,
            MEDIA_PREVIOUS,
            MEDIA_STOP,
            MEDIA_PLAY_PAUSE
        }

        private MainWindow Mw { get; set; }
        
        public KeySelection(MainWindow mw)
        {
            InitializeComponent();
            
            this.Mw = mw;

            Box.SelectedItem = Box.Items[0];

            Input.Key = 0x08;
            Input.Released = false;
        }

        private void OnKeyboardButtonSelected(object sender, RoutedEventArgs e)
        {
            if (Box.SelectedItem == null)
                return;
            
            foreach (var val in Enum.GetValues(typeof(BTN)))
            {
                if (((ComboBoxItem)Box.SelectedItem).Content.Equals(val.ToString()))
                {
                    
                    Input.Key = (ushort)(BTN)val;
                }
            }
        }

        private void OnReleaseChanged(object sender, RoutedEventArgs e)
        {
            Input.Released = (bool)Release.IsChecked;
        }

        private void OnClickChanged(object sender, RoutedEventArgs e)
        {
            if (Release == null)
                return;
            
            if (Click.IsChecked == true)
            {
                Release.IsChecked = false;
                Release.IsEnabled = false;
            }
            else
            {
                Release.IsEnabled = true;
            }
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

        private void OnKeyRecord(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this)!.KeyDown += RegisterClick;
        }
        
        private void RegisterClick(object o, KeyEventArgs args)
        {
            string val = "";
            switch (args.Key)
            {
                case Key.Back:
                    val = "BACKSPACE";
                    break;
                case Key.Tab:
                    val = "TAB";
                    break;
                case Key.Enter:
                    val = "ENTER";
                    break;
                case Key.Pause:
                    val = "PAUSE";
                    break;
                case Key.CapsLock:
                    val = "CAPS";
                    break;
                case Key.Escape:
                    val = "ESCAPE";
                    break;
                case Key.Space:
                    val = "SPACE";
                    break;
                case Key.PageUp:
                    val = "PAGE_UP";
                    break;
                case Key.PageDown:
                    val = "PAGE_DOWN";
                    break;
                case Key.Home:
                    val = "HOME";
                    break;
                case Key.Left:
                    val = "LEFT";
                    break;
                case Key.Up:
                    val = "UP";
                    break;
                case Key.Right:
                    val = "RIGHT";
                    break;
                case Key.Down:
                    val = "DOWN";
                    break;
                case Key.Select:
                    val = "SELECT";
                    break;
                case Key.Print:
                    val = "PRINT";
                    break;
                case Key.Execute:
                    val = "EXECUTE";
                    break;
                case Key.PrintScreen:
                    val = "PRTSCRN";
                    break;
                case Key.Insert:
                    val = "INSERT";
                    break;
                case Key.Delete:
                    val = "DEL";
                    break;
                case Key.Help:
                    val = "HELP";
                    break;
                case Key.D0:
                    val = "KEY_0";
                    break;
                case Key.D1:
                    val = "KEY_1";
                    break;
                case Key.D2:
                    val = "KEY_2";
                    break;
                case Key.D3:
                    val = "KEY_3";
                    break;
                case Key.D4:
                    val = "KEY_4";
                    break;
                case Key.D5:
                    val = "KEY_5";
                    break;
                case Key.D6:
                    val = "KEY_6";
                    break;
                case Key.D7:
                    val = "KEY_7";
                    break;
                case Key.D8:
                    val = "KEY_8";
                    break;
                case Key.D9:
                    val = "KEY_9";
                    break;
                case Key.A:
                    val = "KEY_A";
                    break;
                case Key.B:
                    val = "KEY_B";
                    break;
                case Key.C:
                    val = "KEY_C";
                    break;
                case Key.D:
                    val = "KEY_D";
                    break;
                case Key.E:
                    val = "KEY_E";
                    break;
                case Key.F:
                    val = "KEY_F";
                    break;
                case Key.G:
                    val = "KEY_G";
                    break;
                case Key.H:
                    val = "KEY_H";
                    break;
                case Key.I:
                    val = "KEY_I";
                    break;
                case Key.J:
                    val = "KEY_J";
                    break;
                case Key.K:
                    val = "KEY_K";
                    break;
                case Key.L:
                    val = "KEY_L";
                    break;
                case Key.M:
                    val = "KEY_M";
                    break;
                case Key.N:
                    val = "KEY_N";
                    break;
                case Key.O:
                    val = "KEY_O";
                    break;
                case Key.P:
                    val = "KEY_P";
                    break;
                case Key.Q:
                    val = "KEY_Q";
                    break;
                case Key.R:
                    val = "KEY_R";
                    break;
                case Key.S:
                    val = "KEY_S";
                    break;
                case Key.T:
                    val = "KEY_T";
                    break;
                case Key.U:
                    val = "KEY_U";
                    break;
                case Key.V:
                    val = "KEY_V";
                    break;
                case Key.W:
                    val = "KEY_W";
                    break;
                case Key.X:
                    val = "KEY_X";
                    break;
                case Key.Y:
                    val = "KEY_Y";
                    break;
                case Key.Z:
                    val = "KEY_Z";
                    break;
                case Key.LWin:
                    val = "LWIN";
                    break;
                case Key.RWin:
                    val = "RWIN";
                    break;
                case Key.Apps:
                    val = "APPS";
                    break;
                case Key.Sleep:
                    val = "SLEEP";
                    break;
                case Key.NumPad0:
                    val = "NUMPAD_0";
                    break;
                case Key.NumPad1:
                    val = "NUMPAD_1";
                    break;
                case Key.NumPad2:
                    val = "NUMPAD_2";
                    break;
                case Key.NumPad3:
                    val = "NUMPAD_3";
                    break;
                case Key.NumPad4:
                    val = "NUMPAD_4";
                    break;
                case Key.NumPad5:
                    val = "NUMPAD_5";
                    break;
                case Key.NumPad6:
                    val = "NUMPAD_6";
                    break;
                case Key.NumPad7:
                    val = "NUMPAD_7";
                    break;
                case Key.NumPad8:
                    val = "NUMPAD_8";
                    break;
                case Key.NumPad9:
                    val = "NUMPAD_9";
                    break;
                case Key.Multiply:
                    val = "MULTIPLY";
                    break;
                case Key.OemPlus:
                case Key.Add:
                    val = "ADD";
                    break;
                case Key.OemComma:
                case Key.Separator:
                    val = "SEPARATOR";
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    val = "SUBTRACT";
                    break;
                case Key.OemPeriod:
                    val = "DOT";
                    break;
                case Key.OemQuestion:
                case Key.Divide:
                    val = "DIVIDE";
                    break;
                case Key.F1:
                    val = "F1";
                    break;
                case Key.F2:
                    val = "F2";
                    break;
                case Key.F3:
                    val = "F3";
                    break;
                case Key.F4:
                    val = "F4";
                    break;
                case Key.F5:
                    val = "F5";
                    break;
                case Key.F6:
                    val = "F6";
                    break;
                case Key.F7:
                    val = "F7";
                    break;
                case Key.F8:
                    val = "F8";
                    break;
                case Key.F9:
                    val = "F9";
                    break;
                case Key.F10:
                    val = "F10";
                    break;
                case Key.F11:
                    val = "F11";
                    break;
                case Key.F12:
                    val = "F12";
                    break;
                case Key.F13:
                    val = "F13";
                    break;
                case Key.F14:
                    val = "F14";
                    break;
                case Key.F15:
                    val = "F15";
                    break;
                case Key.F16:
                    val = "F16";
                    break;
                case Key.F17:
                    val = "F17";
                    break;
                case Key.F18:
                    val = "F18";
                    break;
                case Key.F19:
                    val = "F19";
                    break;
                case Key.F20:
                    val = "F20";
                    break;
                case Key.F21:
                    val = "F21";
                    break;
                case Key.F22:
                    val = "F22";
                    break;
                case Key.F23:
                    val = "F23";
                    break;
                case Key.F24:
                    val = "F24";
                    break;
                case Key.NumLock:
                    val = "NUMLOCK";
                    break;
                case Key.Scroll:
                    val = "SCROLL";
                    break;
                case Key.LeftShift:
                    val = "LSHIFT";
                    break;
                case Key.RightShift:
                    val = "RSHIFT";
                    break;
                case Key.LeftCtrl:
                    val = "LCONTROL";
                    break;
                case Key.RightCtrl:
                    val = "RCONTROL";
                    break;
                case Key.LeftAlt:
                    val = "LALT";
                    break;
                case Key.RightAlt:
                    val = "RALT";
                    break;
                case Key.VolumeMute:
                    val = "VOLUME_MUTE";
                    break;
                case Key.VolumeDown:
                    val = "VOLUME_DOWN";
                    break;
                case Key.VolumeUp:
                    val = "VOLUME_UP";
                    break;
                case Key.MediaNextTrack:
                    val = "MEDIA_NEXT";
                    break;
                case Key.MediaPreviousTrack:
                    val = "MEDIA_PREVIOUS";
                    break;
                case Key.MediaStop:
                    val = "MEDIA_STOP";
                    break;
                case Key.MediaPlayPause:
                    val = "MEDIA_PLAY_PAUSE";
                    break;
            }

            for (int i = 0; i < Box.Items.Count; i++)
            {
                if (((ComboBoxItem)Box.Items[i]).Content.Equals(val))
                {
                    Box.SelectedIndex = i;
                    break;
                }
            }
            
            Window.GetWindow(this)!.KeyDown -= RegisterClick;
        }
    }
}