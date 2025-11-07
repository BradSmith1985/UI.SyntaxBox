using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.SyntaxBox
{
    /// <summary>
    /// A POCO object describing a matched substring.
    /// </summary>
    public readonly struct Substring
    {
        /// <summary>
        /// The first character in the match
        /// </summary>
        public readonly int Position { get; init; }

        /// <summary>
        /// The length of the matched string
        /// </summary>
        public readonly int Length { get; init; }

        /// <summary>
        /// The matched substring value.
        /// </summary>
        public readonly string Value { get; init; }
    }
}
