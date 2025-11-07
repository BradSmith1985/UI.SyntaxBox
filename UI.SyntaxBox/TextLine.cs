using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UI.SyntaxBox
{
    /// <summary>
    /// Represents a line of text.
    /// </summary>
    public readonly struct TextLine
    {
        /// <summary>
        /// The line number (0-based).
        /// </summary>
        public readonly int LineNumber;
        /// <summary>
        /// The start index of the line relative to the entire text.
        /// </summary>
        public readonly int StartIndex;
        /// <summary>
        /// The text of the line.
        /// </summary>
        public readonly string Text;
        /// <summary>
        /// The end index of the line relative to the entire text.
        /// </summary>
        public int EndIndex => StartIndex + Text?.Length ?? 0;

        /// <summary>
        /// Initialises a new instance of the <see cref="TextLine"/> structure using the specified values.
        /// </summary>
        /// <param name="LineNumber"></param>
        /// <param name="StartIndex"></param>
        /// <param name="Text"></param>
        public TextLine(int LineNumber, int StartIndex, string Text) {
            this.LineNumber = LineNumber;
            this.StartIndex = StartIndex;
            this.Text = Text;
        }
    }
}
