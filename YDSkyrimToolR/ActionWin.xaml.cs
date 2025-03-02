using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace YDSkyrimToolR
{
    /*
* @Author: YD525
* @GitHub: https://github.com/YD525/YDSkyrimToolR
* @Date: 2025-02-06
*/
    /// <summary>
    /// Interaction logic for ActionWin.xaml
    /// </summary>
    public partial class ActionWin : Window
    {
        public ActionWin()
        {
            InitializeComponent();
        }

       
        public static int SelectState = -1;
        public static int Show(string Tittle, string Msg, MsgAction OneAction, MsgType OneType,int SetHight = 300)
        {
            SelectState = -1;
            Thread NewCreatForm = new Thread(

             new ThreadStart(
             () =>
             {
                 ActionWin NMessageBoxExtend = new ActionWin();

                 NMessageBoxExtend.Dispatcher.Invoke(new Action(() => {
                     NMessageBoxExtend.Title = Tittle;
                     NMessageBoxExtend.Caption.Content = Tittle;
                     NMessageBoxExtend.CurrentMsg.Text = Msg;
                     NMessageBoxExtend.Height = SetHight;

                     if (OneAction == MsgAction.Yes || OneAction == MsgAction.Null)
                     {
                         NMessageBoxExtend.Confirm.Visibility = Visibility.Visible;
                         NMessageBoxExtend.Confirm.Margin = new Thickness(0, 0, 0, 0);
                         NMessageBoxExtend.Cancel.Visibility = Visibility.Collapsed;
                         NMessageBoxExtend.Confirm.Margin = new Thickness(0, 0, 0, 0);
                     }
                     else
                     if (OneAction == MsgAction.YesNo)
                     {
                         NMessageBoxExtend.Confirm.Visibility = Visibility.Visible;
                         NMessageBoxExtend.Confirm.Margin = new Thickness(25, 0, 25, 0);
                         NMessageBoxExtend.Cancel.Visibility = Visibility.Visible;
                         NMessageBoxExtend.Confirm.Margin = new Thickness(25, 0, 25, 0);
                     }

                     NMessageBoxExtend.Show();
                     NMessageBoxExtend.Activate();
                     NMessageBoxExtend.Topmost = true;

                     if (OneType == MsgType.Waring)
                     {
                       
                     }
                 }));


                 NMessageBoxExtend.Closed += (Sender, E) =>
                 NMessageBoxExtend.Dispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
                 Dispatcher.Run();

             }));

            NewCreatForm.SetApartmentState(ApartmentState.STA);
            NewCreatForm.IsBackground = true;
            NewCreatForm.Start();

            while (SelectState == -1)
            {
                Thread.Sleep(10);
            }

            return SelectState;
        }

        private void OneConfirm(object sender, MouseButtonEventArgs e)
        {
            SelectState = 1;
            this.Close();
        }

        private void OneCancel(object sender, MouseButtonEventArgs e)
        {
            SelectState = 0;
            this.Close();
        }

        public bool IsLeftMouseDown = false;

        private void WinHead_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                IsLeftMouseDown = true;
            }

            if (IsLeftMouseDown)
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.DragMove();
                    }));

                    IsLeftMouseDown = false;
                }
                catch { }
            }
        }
    }

    public enum MsgAction
    {
        Null = 0, Yes = 1, YesNo = 2
    }

    public enum MsgType
    {
        Null = 0, Info = 1, Waring = 2, Error = 3
    }
}
