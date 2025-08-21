using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using SSELex.TranslateManage;
using SSELex.ConvertManager;
using SSELex.SkyrimManage;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Shapes;
using static SSELex.TranslateManage.TranslatorExtend;
using PhoenixEngine.EngineManagement;
using System.Windows.Threading;
using System.Windows.Documents;
using PhoenixEngine.TranslateManage;

namespace SSELex.UIManage
{
    // Copyright (C) 2025 YD525
    // Licensed under the GNU GPLv3
    // See LICENSE for details
    //https://github.com/YD525/YDSkyrimToolR/

    public class ScanAnimator
    {
        private readonly TranslateTransform _ScanTransform;
        private readonly FrameworkElement _ProcessBar;
        private DoubleAnimation? _Animation;

        private readonly double _Speed = 120;
        private double _PendingTo; 

        public ScanAnimator(TranslateTransform scanTransform, FrameworkElement processBar, double speed = 120)
        {
            _ScanTransform = scanTransform;
            _ProcessBar = processBar;
            _Speed = speed;
        }

        public void UpdateAnimationTarget()
        {
            _PendingTo = _ProcessBar.ActualWidth;
        }

        public void Start()
        {
            if (_Animation != null) return;
            StartNewCycle();
        }

        public void Stop()
        {
            _ScanTransform.BeginAnimation(TranslateTransform.XProperty, null);
            _Animation = null;
        }

        private void StartNewCycle()
        {
            double from = -30;
            double to = _PendingTo > 0 ? _PendingTo : _ProcessBar.ActualWidth;
            if (to <= 0) return;

            double distance = to - from;
            double durationSeconds = distance / _Speed;

            _Animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(durationSeconds),
                AutoReverse = false,               
                RepeatBehavior = new RepeatBehavior(1), 
                FillBehavior = FillBehavior.Stop
            };

            Timeline.SetDesiredFrameRate(_Animation, 15);

            _Animation.Completed += (s, e) =>
            {
                _ScanTransform.BeginAnimation(TranslateTransform.XProperty, null);
                _Animation = null;

                StartNewCycle();
            };

            _ScanTransform.BeginAnimation(TranslateTransform.XProperty, _Animation);
        }
    }

    public class UIHelper
    {
        public static void ShowButton(Border NormalButton, bool Enable)
        {
            if (Enable)
            {
                NormalButton.Cursor = Cursors.Hand;
                NormalButton.IsEnabled = true;
                NormalButton.Opacity = 1;
            }
            else
            {
                NormalButton.Cursor = null;
                NormalButton.Opacity = 0.5;
                NormalButton.IsEnabled = false;
            }
        }

        public static Grid SelectLine = null;
  
        public static double DefLineHeight = 42;
        public static double DefFontSize = 15;

        public static double MeasureTextWidth(string Text, double FontSize, FontFamily FontFamily)
        {
            var Typeface = new Typeface(FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

            var FormattedText = new FormattedText(
                Text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                Typeface,
                FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return FormattedText.Width;
        }

        public static TextBox FakeTextBox = new TextBox();
        public static FakeGrid CreatFakeLine(string Type, string Key, string SourceText, string TransText, double Score)
        {
            double AutoHeight = DefLineHeight;

            FontFamily FontFamily = FakeTextBox.FontFamily;

            double ActualTextWidth = MeasureTextWidth(SourceText, FakeTextBox.FontSize, FontFamily);

            var GetTextWidthRange = (DeFine.WorkingWin.ActualWidth / 3) - 135;

            if (ActualTextWidth > GetTextWidthRange)
            {
                AutoHeight = 82;
            }

            return new FakeGrid(AutoHeight, Type, Key, SourceText, TransText, Score);
        }

        public static Grid CreatLine(FakeGrid Item)
        {
            return CreatLine(Item.Height, Item.Type, Item.Key, Item.SourceText, Item.TransText, Item.Score);
        }

        public static Grid CreatLine(double Height, string Type, string Key, string SourceText, string TransText, double Score)
        {
            Grid MainGrid = DeFine.RowStyleWin.CreatLine(Height, new PhoenixEngine.TranslateManage.TranslationUnit(Engine.GetModName(), Key, Type, SourceText, TransText,"",Engine.From,Engine.To));
            return MainGrid;
        }


        private static CancellationTokenSource? AutoCancelSelectIDETrd;
        public static Thread AutoSelectIDETrd = null;
        public static void SelectLineFromIDE(string GetKey)
        {
            try
            {
                if (AutoSelectIDETrd != null)
                {
                    CancelAutoSelect();
                }
            }
            catch { }

            AutoCancelSelectIDETrd = new CancellationTokenSource();
            var Token = AutoCancelSelectIDETrd.Token;

            AutoSelectIDETrd = new Thread(() =>
            {
                try
                {
                    string[] Params = GetKey.Split(',');
                    if (Params.Length > 1)
                    {
                        Task.Delay(200, Token).Wait(Token);

                        Token.ThrowIfCancellationRequested();

                        foreach (var Item in DeFine.WorkingWin.GlobalPexReader.HeuristicEngine.DStringItems)
                        {
                            if (Item.Key.Contains(","))
                            {
                                if (Item.Key.Equals(Params[0] + "," + Params[1]))
                                {
                                    DeFine.ActiveIDE.Dispatcher.Invoke(() =>
                                    {
                                        int lineOffset = DeFine.ActiveIDE.Document.Text.IndexOf(Item.SourceLine);
                                        if (lineOffset == -1) return;

                                        int relativeOffset = Item.SourceLine.IndexOf("\"" + Item.Str + "\"");
                                        if (relativeOffset == -1) return;

                                        int absoluteOffset = lineOffset + relativeOffset;
                                        DeFine.ActiveIDE.ScrollToLine(DeFine.ActiveIDE.Document.GetLineByOffset(absoluteOffset).LineNumber);
                                        DeFine.ActiveIDE.Select(absoluteOffset, ("\"" + Item.Str + "\"").Length);
                                    });
                                }
                            }

                        }
                    }
                }
                catch (OperationCanceledException)
                {

                }
            });

            AutoSelectIDETrd.Start();
        }

        public static void CancelAutoSelect()
        {
            AutoCancelSelectIDETrd?.Cancel();
        }

        private static readonly Dictionary<string, DispatcherTimer> _NodeTimers = new();
        private static readonly Dictionary<string, DateTime> _LastOnTime = new();

        public static bool LeftMenuIsShow = false;

        public static void NodeCallCallback(PlatformType Sign)
        {
            try
            {
                if (!LeftMenuIsShow) return;

                DeFine.WorkingWin.Dispatcher.Invoke(() =>
                {
                    var IndicatorOn = (Style)Application.Current.FindResource("IndicatorOnStyle");
                    var IndicatorOff = (Style)Application.Current.FindResource("IndicatorOffStyle");

                    var platformToLightMap = new Dictionary<PlatformType, ContentControl>
            {
                { PlatformType.ChatGpt, DeFine.WorkingWin.ChatGptLight },
                { PlatformType.Gemini, DeFine.WorkingWin.GeminiLight },
                { PlatformType.Cohere, DeFine.WorkingWin.CohereLight },
                { PlatformType.DeepSeek, DeFine.WorkingWin.DeepSeekLight },
                { PlatformType.LMLocalAI, DeFine.WorkingWin.LMLocalAILight },
                { PlatformType.Baichuan, DeFine.WorkingWin.BaichuanLight },
                { PlatformType.DeepL, DeFine.WorkingWin.DeepLLight },
                { PlatformType.GoogleApi, DeFine.WorkingWin.GoogleLight },
                { PlatformType.PhoenixEngine, DeFine.WorkingWin.PreTranslateLight }
            };

                    if (!platformToLightMap.TryGetValue(Sign, out var lightControl) || lightControl == null)
                    {
                        return;
                    }

                    lightControl.Style = IndicatorOn;
                    _LastOnTime[Sign.ToString()] = DateTime.UtcNow;

                    if (_NodeTimers.TryGetValue(Sign.ToString(), out var oldTimer))
                    {
                        oldTimer.Stop();
                    }

                    var timer = new DispatcherTimer
                    {
                        Interval = TimeSpan.FromMilliseconds(500)
                    };

                    timer.Tick += (s, e) =>
                    {
                        lightControl.Style = IndicatorOff;
                        timer.Stop();
                    };

                    _NodeTimers[Sign.ToString()] = timer;
                    timer.Start();
                });
            }
            catch { }
        }
    }
}
