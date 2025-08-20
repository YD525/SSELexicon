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

        private readonly double _Speed = 60;

        public ScanAnimator(TranslateTransform scanTransform, FrameworkElement processBar, double speed = 60)
        {
            _ScanTransform = scanTransform;
            _ProcessBar = processBar;
            _Speed = speed;

            _ProcessBar.SizeChanged += (s, e) => RestartAnimation();
        }

        public void Start()
        {
            if (_Animation != null) return;
            CreateAndStartAnimation();
        }
        public void Stop()
        {
            _ScanTransform.BeginAnimation(TranslateTransform.XProperty, null);
            _Animation = null;
        }

        private void RestartAnimation()
        {
            Stop();
            CreateAndStartAnimation();
        }

        private void CreateAndStartAnimation()
        {
            double from = -30;
            double to = _ProcessBar.ActualWidth;
            if (to <= 0) return;

            double distance = to - from;
            double durationSeconds = distance / _Speed;

            _Animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = TimeSpan.FromSeconds(durationSeconds),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            //15 FPS Limit
            Timeline.SetDesiredFrameRate(_Animation, 15);

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
            Grid MainGrid = DeFine.RowStyleWin.CreatLine(Height, new PhoenixEngine.TranslateManage.TranslationUnit(Engine.GetModName(), Key, Type, SourceText, TransText));
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

        public static void NodeCallCallback(string NodeName, bool? WorkState)
        {
            try
            {
                if (!LeftMenuIsShow) return;

                DeFine.WorkingWin.Dispatcher.Invoke(() =>
                {
                    var IndicatorOn = (Style)Application.Current.FindResource("IndicatorOnStyle");
                    var IndicatorOff = (Style)Application.Current.FindResource("IndicatorOffStyle");

                    ContentControl? LightControl = NodeName switch
                    {
                        "PreTranslate" => DeFine.WorkingWin.PreTranslateLight,
                        "Gemini" => DeFine.WorkingWin.GeminiLight,
                        "ChatGpt" => DeFine.WorkingWin.ChatGptLight,
                        "Cohere" => DeFine.WorkingWin.CohereLight,
                        "DeepSeek" => DeFine.WorkingWin.DeepSeekLight,
                        "Baichuan" => DeFine.WorkingWin.BaichuanLight,
                        "LMLocalAI" => DeFine.WorkingWin.LMLocalAILight,
                        "DeepL" => DeFine.WorkingWin.DeepLLight,
                        "Google" => DeFine.WorkingWin.GoogleLight,
                        _ => null
                    };

                    if (LightControl == null) return;

                    LightControl.Style = IndicatorOn;
                    _LastOnTime[NodeName] = DateTime.UtcNow;

                    if (_NodeTimers.TryGetValue(NodeName, out var OldTimer))
                    {
                        OldTimer.Stop();
                    }

                    var MinOn = TimeSpan.FromMilliseconds(200);
                    
                    var MaxOn = TimeSpan.FromSeconds(1);

                    var Timer = new DispatcherTimer
                    {
                        Interval = MaxOn
                    };

                    Timer.Tick += (s, e) =>
                    {
                        LightControl.Style = IndicatorOff;
                        Timer.Stop();
                    };

                    _NodeTimers[NodeName] = Timer;
                    Timer.Start();
                });
            }
            catch { }
        }
    }
}
