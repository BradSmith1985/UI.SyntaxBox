using System.Collections.Generic;
using System.Windows.Media;

namespace UI.SyntaxBox
{
    /// <summary>
    /// Defines the behavior for a syntax highlighting rule.
    /// </summary>
    public interface ISyntaxRule
    {
        /// <summary>
        /// Gets or sets the unique ID of the rule.
        /// </summary>
        int RuleId { get; set; }
        /// <summary>
        /// Gets or sets the ability of the rule.
        /// </summary>
        DriverOperation Op { get; set; }

        /// <summary>
        /// Matches the specified text against the rule.
        /// </summary>
        /// <param name="Text"></param>
        /// <returns>Sequence containing the resulting instructions.</returns>
        IEnumerable<FormatInstruction> Match(string Text);
    }
}