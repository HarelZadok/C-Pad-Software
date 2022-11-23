using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using WpfSingleInstanceByEventWaitHandle;

namespace C_Pad_Software;

public partial class WaitingForConnection : Window
{
    private DispatcherTimer remainTimer = new DispatcherTimer();
    
    public WaitingForConnection()
    {
        InitializeComponent();
        
        WpfSingleInstance.Make("CPad Driver Software");

        MainWindow mw = null;
        
        new Thread(() =>
        {
            Dispatcher.Invoke(() =>
            {
                mw = new MainWindow(this);
                mw.Show();
                mw.Activate();
            });
        }){IsBackground = true}.Start();

        remainTimer.Tick += delegate(object sender, EventArgs args) {
            if (mw is { IsInitialized: true } && MainWindow.isConnected())
            {
                mw.m_notifyIcon.Visible = true;
                Hide();
            }
        };
        remainTimer.Interval = TimeSpan.FromMilliseconds(100);
        remainTimer.Start();
    }
}