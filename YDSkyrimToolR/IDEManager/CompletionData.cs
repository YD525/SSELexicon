using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YDSkyrimToolR.IDEManager
{
    public class CompletionData : ICompletionData
    {
        public CompletionData(string text)
        {
            Text = text;
        }

        public ImageSource Image => null;

        public string Text { get; }

        public object Content => Text;

        public object Description => "";//"ZH:" +  LanguageHelper.Translate(this.Text, LanguageHelper.LanguageType.en, LanguageHelper.LanguageType.zh);

        /// <inheritdoc />
        public double Priority { get; }

        public void Complete(TextArea textArea, ISegment completionSegment,
            EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, Text);
        }


    }
}
