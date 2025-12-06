using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace SSELex
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [DllImport("Shcore.dll")] 
        private static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS awareness);
        private enum PROCESS_DPI_AWARENESS 
        { 
            PROCESS_DPI_UNAWARE = 0, 
            PROCESS_SYSTEM_DPI_AWARE = 1, 
            PROCESS_PER_MONITOR_DPI_AWARE = 2 
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try 
            {
                // Force the application into Per-Monitor DPI awareness mode.
                // This prevents Windows from scaling the UI and ensures that all controls
                // render at their native size without being affected by system DPI scaling.
                SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE);
            }
            catch { }

            base.OnStartup(e);
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }
    }

}
