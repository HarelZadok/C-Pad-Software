using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using WpfSingleInstanceByEventWaitHandle;
using NotifyIcon = System.Windows.Forms.NotifyIcon;
using System.Text.Json;
using System.Threading;
using Brushes = System.Windows.Media.Brushes;

namespace C_Pad_Software
{
    public partial class MainWindow
    {
        private enum RUN_TYPE
        {
            SINGLE = 0,
            LOOP,
            WHILE_HELD
        }
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
        private enum MACRO_TYPE
        {
            KEY,
            FILE,
            DELAY,
            TEXT
        }

        public NotifyIcon m_notifyIcon;
        
        private Stack<UIElement> _contents = new Stack<UIElement>();
        private int _buttonIndex = 0;
        private int _buttonMacroIndex = 0;

        private bool[] _pressedButtonStates = {false, false, false, false};

        private RUN_TYPE _runType = RUN_TYPE.SINGLE;
        private MACRO_TYPE _macroType = MACRO_TYPE.KEY;

        public ArrayList ItemSlots = new ArrayList();

        [DllImport("CPad_Driver.dll")]
        static extern void keyPress(int buttonIndex, RUN_TYPE runType);
        
        [DllImport("CPad_Driver.dll")]
        static extern void addFile(int buttonIndex, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder filePath);
        
        [DllImport("CPad_Driver.dll")]
        static extern void addDelay(int buttonIndex, ulong ns);
        
        [DllImport("CPad_Driver.dll")]
        static extern void addText(int buttonIndex, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder text);
        
        [DllImport("CPad_Driver.dll")]
        static extern void keyRelease(int buttonIndex);
        
        [DllImport("CPad_Driver.dll")]
        static extern bool isKeyHeld(int buttonIndex);
        
        [DllImport("CPad_Driver.dll")]
        static extern void addKeyToButton(int buttonIndex, ushort key, bool released);
        
        [DllImport("CPad_Driver.dll")]
        static extern void clearKeysFromButton(int buttonIndex);

        [DllImport("CPad_Driver.dll")]
        static extern void CPadInit();
        
        [DllImport("CPad_Driver.dll")]
        static extern void setButtonRunType(int buttonIndex, RUN_TYPE runType);
        
        [DllImport("CPad_Driver.dll")]
        static extern void cancelButtonLoop(int buttonIndex);
        
        [DllImport("CPad_Driver.dll")]
        static extern void guiUp();
        
        [DllImport("CPad_Driver.dll")]
        static extern void setTickInterval(uint interval);
        
        [DllImport("CPad_Driver.dll")]
        public static extern bool isConnected();
        
        [DllImport("CPad_Driver.dll")]
        private static extern void CPadClose();

        public static bool IsLinked()
        {
            try
            {
                var t = new Thread(CPadInit);
                t.Start();
                t.Join();
                return true;
            }
            catch (DllNotFoundException e)
            {
                Console.WriteLine(e);
                MessageBox.Show("CPad_Driver.dll is missing or outdated!", "Missing required file", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private struct InputStruct
        {
            public MACRO_TYPE Type { get; set; }
            public bool Click { get; set; }
            public bool Release { get; set; }
            public ushort Button { get; set; }
            public string FileName { get; set; }
            public ulong Timeout { get; set; }
            public int TimeType { get; set; }
            public string Text { get; set; }

            public InputStruct()
            {
                Type = MACRO_TYPE.KEY;
                Click = false;
                Release = false;
                Button = (ushort)BTN.BACKSPACE;
                FileName = "";
                Timeout = 3000000000;
                TimeType = 0;
                Text = "";
            }
        }
        
        private struct DataStruct
        {
            public List<InputStruct>[] Inputs { get; set; }
            public RUN_TYPE[] RunTypes { get; set; }

            public DataStruct()
            {
                RunTypes = new[] { RUN_TYPE.SINGLE, RUN_TYPE.SINGLE, RUN_TYPE.SINGLE, RUN_TYPE.SINGLE };
                
                Inputs = new [] {new List<InputStruct>(), new List<InputStruct>(), new List<InputStruct>(), new List<InputStruct>()};
            }
        }

        private bool initilized = false;

        private DataStruct _ds;
        private static readonly string JSON_FILE_NAME = "preferences.cdata";
        private uint _tickInterval;

        public MainWindow(WaitingForConnection wfc)
        {
            InitializeComponent();

            WpfSingleInstance.Make("CPad Driver Software");

            IsLinked();

            _ds = new DataStruct();
            
            if (!File.Exists(JSON_FILE_NAME))
            {
                File.Create(JSON_FILE_NAME).Close();
                string json = JsonSerializer.Serialize(_ds, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(JSON_FILE_NAME, json);
            }
            

            string jsonString;

            try
            {
                jsonString = File.ReadAllText(JSON_FILE_NAME);
            }
            catch
            {
                string json = JsonSerializer.Serialize(_ds, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(JSON_FILE_NAME, json);
                jsonString = File.ReadAllText(JSON_FILE_NAME);
            }
            
            _ds = JsonSerializer.Deserialize<DataStruct>(jsonString, new JsonSerializerOptions{WriteIndented = true});
            
            RetrieveData(_ds, 0);

            for (int i = 0; i < 4; i++)
            {
                ActivateButton(i);
            }

            _contents.Push(SettingsContent);
            _contents.Push(MacroCreationContent);

            m_notifyIcon = new NotifyIcon();
            m_notifyIcon.BalloonTipText = @"The app has been minimized. Click the tray icon to show.";
            m_notifyIcon.BalloonTipTitle = @"C-Pad Software";
            m_notifyIcon.Text = @"C-Pad Software";
            m_notifyIcon.Icon = new Icon(Application.GetResourceStream(new Uri("pack://application:,,,/img/Logo.ico")).Stream);
            m_notifyIcon.MouseClick += m_notifyIcon_Click;
            m_notifyIcon.Visible = true;
            
            initilized = true;
            
            new Thread(() =>
            {
                while (true)
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (!isConnected())
                        {
                            wfc.Show();
                            wfc.Activate();
                            m_notifyIcon.Visible = false;
                            Close();
                        }
                        
                        B0State.Fill = isKeyHeld(0) ? Brushes.Green : Brushes.Red;
                        B1State.Fill = isKeyHeld(1) ? Brushes.Green : Brushes.Red;
                        B2State.Fill = isKeyHeld(2) ? Brushes.Green : Brushes.Red;
                        B3State.Fill = isKeyHeld(3) ? Brushes.Green : Brushes.Red;
                    });
                    
                    Thread.Sleep(100);
                }
            }){IsBackground = true}.Start();

            guiUp();

            _tickInterval = uint.Parse(TbTickInterval.Text);
            setTickInterval(_tickInterval);
        }

        private void RetrieveData(DataStruct data, int buttonIndex)
        {
            Panel?.Children?.Clear();
            ItemSlots?.Clear();
            
            CbRunType.SelectedIndex = (int)data.RunTypes[buttonIndex];
            
            foreach (var input in data.Inputs[buttonIndex])
            {
                switch (input.Type)
                {
                    case MACRO_TYPE.KEY:
                        KeySelection ks = new KeySelection(this);
                        ks.Click.IsChecked = input.Click;
                        ks.Release.IsChecked = input.Release;
                        foreach (ComboBoxItem val in ks.Box.Items)
                        {
                            if (val.Content.Equals(Enum.GetName(typeof(BTN), input.Button)))
                            {
                                ks.Box.SelectedItem = val;
                                break;
                            }
                        }
                        if (ItemSlots.Count > 0)
                        {
                            (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(true);
                            ks.Left.IsEnabled = true;
                            ks.Right.IsEnabled = false;
                        }
                        else
                        {
                            ks.Left.IsEnabled = false;
                            ks.Right.IsEnabled = false;
                        }
                        Panel.Children.Add(ks);
                        ItemSlots.Add(ks);
                        break;
                    
                    case MACRO_TYPE.FILE:
                        FileSelection fs = new FileSelection(this);
                        ((OpenFileDialog)fs.GetInput()).FileName = input.FileName;
                        fs.ButtonText.Text = input.FileName;
                        if (ItemSlots.Count > 0)
                        {
                            (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(true);
                            fs.Left.IsEnabled = true;
                            fs.Right.IsEnabled = false;
                        }
                        else
                        {
                            fs.Left.IsEnabled = false;
                            fs.Right.IsEnabled = false;
                        }
                        Panel.Children.Add(fs);
                        ItemSlots.Add(fs);
                        break;
                    
                    case MACRO_TYPE.DELAY:
                        DelaySelection ds = new DelaySelection(this);
                        ds.CbTimeSelection.SelectedIndex = 3;
                        ds.TimeInput.Text = input.Timeout.ToString();
                        ds.CbTimeSelection.SelectedIndex = input.TimeType;
                        if (ItemSlots.Count > 0)
                        {
                            (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(true);
                            ds.Left.IsEnabled = true;
                            ds.Right.IsEnabled = false;
                        }
                        else
                        {
                            ds.Left.IsEnabled = false;
                            ds.Right.IsEnabled = false;
                        }
                        Panel.Children.Add(ds);
                        ItemSlots.Add(ds);
                        break;
                    
                    case MACRO_TYPE.TEXT:
                        TextSelection ts = new TextSelection(this);
                        ts.Input.Text = input.Text;
                        if (ItemSlots.Count > 0)
                        {
                            (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(true);
                            ts.Left.IsEnabled = true;
                            ts.Right.IsEnabled = false;
                        }
                        else
                        {
                            ts.Left.IsEnabled = false;
                            ts.Right.IsEnabled = false;
                        }
                        Panel.Children.Add(ts);
                        ItemSlots.Add(ts);
                        break;
                }
            }
        }

        private void m_notifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                MenuOpen(null, null);
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu menu = (ContextMenu)FindResource("NotifierContextMenu");
                menu.IsOpen = !menu.IsOpen;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;

            Hide();
            
            if (m_notifyIcon != null)
                m_notifyIcon.ShowBalloonTip(2000);

            base.OnClosing(e);
        }
        
        private void MenuOpen(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        private void MenuClose(object sender, RoutedEventArgs e)
        {
            CPadClose();
            Environment.Exit(Environment.ExitCode);
        }

        private void CollapseAllContents()
        {
            foreach (var p in _contents)
            {
                p.Visibility = Visibility.Collapsed;
            }
        }
        
        private void OnButtonSimulate(object sender, RoutedEventArgs e)
        {
            if (!IsLinked())
                return;

            keyPress(_buttonIndex, _runType);
        }

        private void ShowMacroCreation(object sender, RoutedEventArgs e)
        {
            CollapseAllContents();
            MacroCreationContent.Visibility = Visibility.Visible;
        }

        private void OnButtonSelected(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            
            _buttonIndex = cb.SelectedIndex;
        }

        private void OnButtonMacroSelected(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            
            _buttonMacroIndex = cb.SelectedIndex;
            
            if (initilized)
                RetrieveData(_ds, _buttonMacroIndex);
        }

        private void ShowSettings(object sender, RoutedEventArgs e)
        {
            CollapseAllContents();
            SettingsContent.Visibility = Visibility.Visible;
        }

        private void OnMacroApplied(object sender, RoutedEventArgs e)
        {
            if (!IsLinked())
                return;

            cancelButtonLoop(_buttonMacroIndex);

            _ds.RunTypes[_buttonMacroIndex] = _runType;
            _ds.Inputs[_buttonMacroIndex].Clear();

            foreach (IItemSlot itemSlot in ItemSlots)
            {
                InputStruct input = new InputStruct();
                
                if (itemSlot is KeySelection keySelection)
                {
                    input.Type = MACRO_TYPE.KEY;
                    input.Button = ((KeySelection.AInput)keySelection.GetInput()).Key;
                    
                    if (keySelection.Click.IsChecked == true)
                        input.Click = true;
                    else
                        input.Release = ((KeySelection.AInput)keySelection.GetInput()).Released;
                }
                else if (itemSlot is FileSelection fileSelection)
                {
                    input.Type = MACRO_TYPE.FILE;
                    input.FileName = ((OpenFileDialog)fileSelection.GetInput()).FileName;
                }
                else if (itemSlot is DelaySelection delaySelection)
                {
                    input.Type = MACRO_TYPE.DELAY;
                    input.TimeType = (int)((ulong)delaySelection.GetInput() % 10);
                    input.Timeout = (ulong)delaySelection.GetInput() / 10;
                }
                else if (itemSlot is TextSelection textSelection)
                {
                    input.Type = MACRO_TYPE.TEXT;
                    input.Text = ((TextBox)textSelection.GetInput()).Text;
                }
                _ds.Inputs[_buttonMacroIndex].Add(input);
            }

            string jsonString = JsonSerializer.Serialize(_ds, new JsonSerializerOptions{WriteIndented = true});
            File.WriteAllText(JSON_FILE_NAME, jsonString);
            
            ActivateButton(_buttonMacroIndex);
        }

        private void ActivateButton(int buttonIndex)
        {
            clearKeysFromButton(buttonIndex);
            
            setButtonRunType(buttonIndex, _ds.RunTypes[buttonIndex]);
            
            foreach (InputStruct itemSlot in _ds.Inputs[buttonIndex])
            {
                if (itemSlot.Type == MACRO_TYPE.KEY)
                {
                    if (itemSlot.Click)
                    {
                        addKeyToButton(buttonIndex, itemSlot.Button, false);
                        addKeyToButton(buttonIndex, itemSlot.Button, true);
                    }
                    else
                    {
                        addKeyToButton(buttonIndex, itemSlot.Button, itemSlot.Release);
                    }
                }
                else if (itemSlot.Type == MACRO_TYPE.FILE)
                {
                    StringBuilder sb = new StringBuilder(itemSlot.FileName);
                    
                    addFile(buttonIndex, sb);
                }
                else if (itemSlot.Type == MACRO_TYPE.DELAY)
                {
                    addDelay(buttonIndex, itemSlot.Timeout);
                }
                else if (itemSlot.Type == MACRO_TYPE.TEXT)
                {
                    StringBuilder sb = new StringBuilder(itemSlot.Text);
                    
                    addText(buttonIndex, sb);
                }
            }
        }
        
        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (_macroType == MACRO_TYPE.KEY)
            {
                KeySelection ks = new KeySelection(this);
                if (ItemSlots.Count > 0)
                {
                    (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(true);
                    ks.Left.IsEnabled = true;
                    ks.Right.IsEnabled = false;
                }
                else
                {
                    ks.Left.IsEnabled = false;
                    ks.Right.IsEnabled = false;
                }
                Panel.Children.Add(ks);
                ItemSlots.Add(ks);
            }
            else if (_macroType == MACRO_TYPE.FILE)
            {
                FileSelection fs = new FileSelection(this);
                if (ItemSlots.Count > 0)
                {
                    (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(true);
                    fs.Left.IsEnabled = true;
                    fs.Right.IsEnabled = false;
                }
                else
                {
                    fs.Left.IsEnabled = false;
                    fs.Right.IsEnabled = false;
                }
                Panel.Children.Add(fs);
                ItemSlots.Add(fs);
            }
            else if (_macroType == MACRO_TYPE.DELAY)
            {
                DelaySelection ds = new DelaySelection(this);
                if (ItemSlots.Count > 0)
                {
                    (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(true);
                    ds.Left.IsEnabled = true;
                    ds.Right.IsEnabled = false;
                }
                else
                {
                    ds.Left.IsEnabled = false;
                    ds.Right.IsEnabled = false;
                }
                Panel.Children.Add(ds);
                ItemSlots.Add(ds);
            }
            else if (_macroType == MACRO_TYPE.TEXT)
            {
                TextSelection ts = new TextSelection(this);
                if (ItemSlots.Count > 0)
                {
                    (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(true);
                    ts.Left.IsEnabled = true;
                    ts.Right.IsEnabled = false;
                }
                else
                {
                    ts.Left.IsEnabled = false;
                    ts.Right.IsEnabled = false;
                }
                Panel.Children.Add(ts);
                ItemSlots.Add(ts);
            }
            
            CheckAll.IsChecked = false;
        }
        
        private void RemoveButton(object sender, RoutedEventArgs e)
        {
            ArrayList indexes = new ArrayList();

            for (int i = ItemSlots.Count - 1; i >= 0; --i)
            {
                if (((IItemSlot)ItemSlots[i]).IsChecked())
                    indexes.Add(i);
            }

            // Remove everything except the first one in case every input was checked 
            // if (ItemSlots.Count == indexes.Count)
            //     indexes.RemoveAt(indexes.Count - 1);
            
            foreach (int index in indexes)
            {
                if (ItemSlots[index] is KeySelection ks)
                    Panel.Children.Remove(ks);
                
                if (ItemSlots[index] is FileSelection fs)
                    Panel.Children.Remove(fs);
                
                if (ItemSlots[index] is DelaySelection ds)
                    Panel.Children.Remove(ds);
                
                if (ItemSlots[index] is TextSelection ts)
                    Panel.Children.Remove(ts);
                
                ItemSlots.RemoveAt(index);
            }
            
            CheckAll.IsChecked = false;

            if (ItemSlots.Count == 0)
                return;

            (ItemSlots[0] as IItemSlot).SetLeft(false);
            (ItemSlots[0] as IItemSlot).SetRight(ItemSlots.Count > 1);
            (ItemSlots[0] as IItemSlot).SetChecked(false);
            
            for (int i = 1; i < ItemSlots.Count - 2; ++i)
            {
                (ItemSlots[i] as IItemSlot).SetLeft(true);
                (ItemSlots[i] as IItemSlot).SetRight(true);
                (ItemSlots[i] as IItemSlot).SetChecked(false);
            }
            
            (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetLeft(ItemSlots.Count > 1);
            (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetRight(false);
            (ItemSlots[ItemSlots.Count - 1] as IItemSlot).SetChecked(false);
        }

        private void OnRunTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            switch (cb.SelectedIndex)
            {
                case 0:
                    _runType = RUN_TYPE.SINGLE;
                    break;
                case 1:
                    _runType = RUN_TYPE.LOOP;
                    break;
                case 2:
                    _runType = RUN_TYPE.WHILE_HELD;
                    break;
            }
        }

        private bool isButtonPressedOnce(int buttonIndex)
        {
            return _pressedButtonStates[buttonIndex];
        }

        private void OnButtonSimulateRelease(object sender, MouseButtonEventArgs e)
        {
            if (IsLinked())
                keyRelease(_buttonIndex);
        }

        private void AlwaysOnTop(object sender, RoutedEventArgs e)
        {
            Topmost = ((MenuItem)sender).IsChecked;
            CbAlwaysOnTop.IsChecked = ((MenuItem)sender).IsChecked;
        }

        private void MacroTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    _macroType = MACRO_TYPE.KEY;
                    break;
                case 1:
                    _macroType = MACRO_TYPE.FILE;
                    break;
                case 2:
                    _macroType = MACRO_TYPE.DELAY;
                    break;
                case 3:
                    _macroType = MACRO_TYPE.TEXT;
                    break;
            }
        }

        public void ItemSlotMoveLeft(IItemSlot itemSlot)
        {
            IItemSlot switched = null;
            if (itemSlot is KeySelection ks)
            {
                int index = Panel.Children.IndexOf(ks);
                
                if (index == 0)
                    return;

                switched = ItemSlots[index - 1] as IItemSlot;

                Panel.Children.RemoveAt(index);
                Panel.Children.Insert(index - 1, ks);

                (ItemSlots[index], ItemSlots[index - 1]) = (ItemSlots[index - 1], ItemSlots[index]);
            }
                
            if (itemSlot is FileSelection fs)
            {
                int index = Panel.Children.IndexOf(fs);
                
                if (index == 0)
                    return;
                
                switched = ItemSlots[index - 1] as IItemSlot;
                
                Panel.Children.RemoveAt(index);
                Panel.Children.Insert(index - 1, fs);

                (ItemSlots[index], ItemSlots[index - 1]) = (ItemSlots[index - 1], ItemSlots[index]);
            }
                
            if (itemSlot is DelaySelection ds)
            {
                int index = Panel.Children.IndexOf(ds);
                
                if (index == 0)
                    return;
                
                switched = ItemSlots[index - 1] as IItemSlot;
                
                Panel.Children.RemoveAt(index);
                Panel.Children.Insert(index - 1, ds);

                (ItemSlots[index], ItemSlots[index - 1]) = (ItemSlots[index - 1], ItemSlots[index]);
            }
            
            if (itemSlot is TextSelection ts)
            {
                int index = Panel.Children.IndexOf(ts);
                
                if (index == 0)
                    return;
                
                switched = ItemSlots[index - 1] as IItemSlot;
                
                Panel.Children.RemoveAt(index);
                Panel.Children.Insert(index - 1, ts);

                (ItemSlots[index], ItemSlots[index - 1]) = (ItemSlots[index - 1], ItemSlots[index]);
            }
            
            itemSlot.SetRight(ItemSlots.IndexOf(itemSlot) != ItemSlots.Count - 1);
            
            itemSlot.SetLeft(ItemSlots.IndexOf(itemSlot) != 0);
            
            switched.SetRight(ItemSlots.IndexOf(switched) != ItemSlots.Count - 1);
            
            switched.SetLeft(ItemSlots.IndexOf(switched) != 0);
        }
        
        public void ItemSlotMoveRight(IItemSlot itemSlot)
        {
            IItemSlot switched = null;
            
            if (itemSlot is KeySelection ks)
            {
                int index = Panel.Children.IndexOf(ks);
                
                if (index >= Panel.Children.Count - 1)
                    return;

                switched = ItemSlots[index + 1] as IItemSlot;
                
                Panel.Children.RemoveAt(index);
                Panel.Children.Insert(index + 1, ks);

                (ItemSlots[index], ItemSlots[index + 1]) = (ItemSlots[index + 1], ItemSlots[index]);
            }
                
            if (itemSlot is FileSelection fs)
            {
                int index = Panel.Children.IndexOf(fs);
                
                if (index >= Panel.Children.Count - 1)
                    return;
                
                switched = ItemSlots[index + 1] as IItemSlot;

                Panel.Children.RemoveAt(index);
                Panel.Children.Insert(index + 1, fs);

                (ItemSlots[index], ItemSlots[index + 1]) = (ItemSlots[index + 1], ItemSlots[index]);
            }
                
            if (itemSlot is DelaySelection ds)
            {
                int index = Panel.Children.IndexOf(ds);
                
                if (index >= Panel.Children.Count - 1)
                    return;
                
                switched = ItemSlots[index + 1] as IItemSlot;
                
                Panel.Children.RemoveAt(index);
                Panel.Children.Insert(index + 1, ds);

                (ItemSlots[index], ItemSlots[index + 1]) = (ItemSlots[index + 1], ItemSlots[index]);
            }

            itemSlot.SetRight(ItemSlots.IndexOf(itemSlot) != ItemSlots.Count - 1);
            
            itemSlot.SetLeft(ItemSlots.IndexOf(itemSlot) != 0);
            
            switched.SetRight(ItemSlots.IndexOf(switched) != ItemSlots.Count - 1);
            
            switched.SetLeft(ItemSlots.IndexOf(switched) != 0);
        }

        private void CheckAllChanged(object sender, RoutedEventArgs e)
        { 
            foreach (IItemSlot o in ItemSlots)
            {
                o.SetChecked(CheckAll.IsChecked != true);
            }
        }

        public bool AllInputsChecked(IItemSlot exception = null)
        {
            foreach (IItemSlot o in ItemSlots)
            {
                if (o == exception)
                    continue;
                if (!o.IsChecked())
                    return false;
            }

            return true;
        }

        private void OnTickIntervalChanged(object sender, TextChangedEventArgs e)
        {
            _tickInterval = uint.Parse(TbTickInterval.Text);
            if (IsLinked())
                setTickInterval(_tickInterval);
        }

        private void OnAlwaysOnTopChanged(object sender, RoutedEventArgs e)
        {
            if (CbAlwaysOnTop.IsChecked != null)
            {
                Topmost = (bool)CbAlwaysOnTop.IsChecked;
                ((GetWindow(this).FindResource("NotifierContextMenu") as ContextMenu).Items[2] as MenuItem).IsChecked = (bool)CbAlwaysOnTop.IsChecked;
            }
        }

        private void OnQuitClicked(object sender, RoutedEventArgs e)
        {
            CPadClose();
            Environment.Exit(Environment.ExitCode);
        }
    }
}