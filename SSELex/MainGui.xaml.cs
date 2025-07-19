using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SSELex
{
    /// <summary>
    /// Interaction logic for MainGui.xaml
    /// </summary>
    public partial class MainGui : Window
    {
        #region
        public Storyboard ?BreathingStoryboard = null;

        private void StartBreathingEffect()
        {
            BreathingStoryboard = (Storyboard)FindResource("BreathingEffect");
            Storyboard.SetTarget(BreathingStoryboard, NoteEffect);
            BreathingStoryboard.Begin();
            
        }

        private void StopBreathingEffect()
        {
            BreathingStoryboard?.Stop();
        }

        #endregion

        public MainGui()
        {
            InitializeComponent();
            var Storyboard = (Storyboard)this.Resources["ScanAnimation"];
            Storyboard.Begin(this, true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartBreathingEffect();
            new Thread(() => { 
            MessageBoxExtend.Show(this, "TestTittle", "TestLine", MsgAction.YesNo, MsgType.Info);
            }).Start();
        }

        private void ModTransView_Drop(object sender, DragEventArgs e)
        {

        }

        private void ModTransView_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void ModTransView_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {

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

        private void TransView_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
