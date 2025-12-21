using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SSELex.UIManagement
{
    public class LogHelper
    {
        private static void SetLog(TextBox Handle, string Msg)
        {
            Handle.Dispatcher.BeginInvoke(new Action(() =>
            {
                Handle.Text = Msg;
                Handle.ScrollToEnd();
            }));
        }

        public static void SetInputLog(string Text)
        {
            if (DeFine.WorkingWin != null)
            {
                SetLog(DeFine.WorkingWin.InputLog, Text);
            }
        }

        public static void SetOutputLog(string Text)
        {
            if (DeFine.WorkingWin != null)
            {
                SetLog(DeFine.WorkingWin.OutputLog, Text);
            }
        }

        public static void SetMainLog(string Text)
        {
            if (DeFine.WorkingWin != null)
            {
                SetLog(DeFine.WorkingWin.MainLog, Text);
            }
        }

        public static void ClearLog()
        {
            if (DeFine.WorkingWin != null)
            {
                DeFine.WorkingWin.Dispatcher.Invoke(new Action(() => {
                    DeFine.WorkingWin.InputLog.Text = DeFine.WorkingWin.OutputLog.Text = DeFine.WorkingWin.MainLog.Text = string.Empty;
                }));
            }
        }
    }
}
