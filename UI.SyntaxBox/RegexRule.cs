using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace UI.SyntaxBox
{
    /// <summary>
    /// Syntax rule that uses a regular expression pattern.
    /// </summary>
    public class RegexRule : ISyntaxRule
    {
        private Regex _regex = null;

        #region ISyntaxRule members
        // ...................................................................
        /// <inheritdoc />
        public int RuleId { get; set; }
        // ...................................................................
        /// <summary>
        /// The driver operation to apply (Line | Block | FullText).
        /// </summary>
        public DriverOperation Op { get; set; } = DriverOperation.None;
        // ...................................................................
        /// <summary>
        /// Matches the rule against the provided text.
        /// Used internally, shouldn't be called by user code.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public IEnumerable<FormatInstruction> Match(string Text)
        {
            var regex = this.GetRegex();
            var matches = regex.Matches(Text);

            foreach (Match match in matches)
            {
                yield return (new FormatInstruction(
                    this.RuleId,
                    match.Index,
                    match.Length,
                    this.Background,
                    this.Foreground,
                    this.Outline,
                    (this.TextDecorations.Count > 0) ? this.TextDecorations : null
                ));
            }
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
        /// The regex pattern to match.
        /// </summary>
        public string Pattern { get; set; }
        // ...................................................................
        /// <summary>
        /// Initialises a new instance of the <see cref="RegexRule"/> class using default values.
        /// </summary>
        public RegexRule() {
            TextDecorations = new TextDecorationCollection();
        }
        // ...................................................................
        #endregion

        #region Private members
        // ...................................................................
        private Regex GetRegex()
        {
            if (this._regex == null)
                this._regex = new Regex(this.Pattern);

            return (this._regex);
        }
        // ...................................................................
        #endregion
    }
}
