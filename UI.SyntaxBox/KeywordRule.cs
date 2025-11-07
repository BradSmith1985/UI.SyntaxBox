using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace UI.SyntaxBox
{
    /// <summary>
    /// This rule is used to match a list of keywords wit input text.
    /// It is ~10x faster than using regex for the same purpose.
    /// </summary>
    public class KeywordRule : ISyntaxRule
    {
        private AhoCorasickSearch _engine = null;

        #region ISyntaxRule members
        // ...................................................................
        /// <inheritdoc />
        public int RuleId { get; set; }
        // ...................................................................
        /// <inheritdoc />
        public DriverOperation Op { get; set; } = DriverOperation.Line;
        // ...................................................................
        /// <summary>
        /// Matches the rule against the provided text.
        /// Used internally, shouldn't be called by user code.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public IEnumerable<FormatInstruction> Match(string Text)
        {
            var engine = this.GetEngine();
            var matched = engine.FindAll(Text).ToList();
            var instructions = matched
                .Select((x) => new FormatInstruction(
                    this.RuleId,
                    x.Position,
                    x.Length,
                    this.Background,
                    this.Foreground,
                    this.Outline,
                    this.TextDecorations
                )).ToList();
            return (instructions);
                
        }
        // ...................................................................
        #endregion

        #region Public members
        // ...................................................................
        /// <summary>
        /// Background brush
        /// </summary>
        public Brush Background { get; set; }
        // ...................................................................
        /// <summary>
        /// Foreground brush.
        /// </summary>
        public Brush Foreground { get; set; }
        // ...................................................................
        /// <summary>
        /// Outline pen
        /// </summary>
        public Pen Outline { get; set; }
        /// <summary>
        /// Decorations
        /// </summary>
        public TextDecorationCollection TextDecorations { get; set; }
        // ...................................................................
        /// <summary>
        /// Comma-separated list of keywords to match.
        /// </summary>
        public string Keywords { get; set; }
        // ...................................................................
        /// <summary>
        /// Whether to match whole words only.
        /// </summary>
        /// <remarks>
        /// The default is <see langword="true"/>.
        /// </remarks>
        public bool WholeWordsOnly { get; set; } = true;
        // ...................................................................
        #endregion

        #region Private members
        // ...................................................................
        private AhoCorasickSearch GetEngine()
        {
            if (this._engine == null)
            {
                var keywordList = (this.Keywords ?? String.Empty)
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select((x) => x.Trim())
                    .ToList();
                this._engine = new AhoCorasickSearch(
                    keywordList,
                    this.WholeWordsOnly);
            }
            return (this._engine);
        }
        // ...................................................................
        #endregion
    }
}
