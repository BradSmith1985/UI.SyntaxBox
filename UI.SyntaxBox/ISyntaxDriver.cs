using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace UI.SyntaxBox
{
    /// <summary>
    /// Defines the behavior for a syntax driver.
    /// </summary>
    public interface ISyntaxDriver
    {
        /// <summary>
        /// Gets the abilities of the driver.
        /// </summary>
        /// <value>
        /// The type of the driver.
        /// </value>
        DriverOperation Abilities { get; }

        /// <summary>
        /// Applies the driver logic to the supplied text. 
        /// This will be called repeatedly 
        /// </summary>
        /// <param name="Operation">The operation.</param>
        /// <param name="Text">The text.</param>
        /// <returns></returns>
        IEnumerable<FormatInstruction> Match(DriverOperation Operation, string Text);
    }

    /// <summary>
    /// Represents the result of a syntax highlighting match.
    /// </summary>
    public readonly struct FormatInstruction
    {
        /// <summary>
        /// Unique ID of the rule that created the instruction.
        /// </summary>
        public readonly int RuleId;
        /// <summary>
        /// Starting character index.
        /// </summary>
        public readonly int FromChar;
        /// <summary>
        /// Length of the formatted text.
        /// </summary>
        public readonly int Length;
        /// <summary>
        /// Background.
        /// </summary>
        public readonly Brush Background;
        /// <summary>
        /// Foreground.
        /// </summary>
        public readonly Brush Foreground;
        /// <summary>
        /// Outline (of the rectangle around the text).
        /// </summary>
        public readonly Pen Outline;
        /// <summary>
        /// Text decorations (e.g. underline).
        /// </summary>
        public readonly TextDecorationCollection TextDecorations;

        /// <summary>
        /// Initialises a new instance of the <see cref="FormatInstruction"/> structure using the specified values.
        /// </summary>
        /// <param name="RuleId"></param>
        /// <param name="FromChar"></param>
        /// <param name="Length"></param>
        /// <param name="Background"></param>
        /// <param name="Foreground"></param>
        /// <param name="Outline"></param>
        /// <param name="TextDecorations"></param>
        public FormatInstruction(int RuleId, int FromChar, int Length, Brush Background = null, Brush Foreground = null, Pen Outline = null, TextDecorationCollection TextDecorations = null) {
            this.RuleId = RuleId;
            this.FromChar = FromChar;
            this.Length = Length;
            this.Background = Background;
            this.Foreground = Foreground;
            this.Outline = Outline;
            this.TextDecorations = TextDecorations;
        }
    }

    /// <summary>
    /// Defines the different abilities of syntax drivers.
    /// </summary>
    [Flags]
    public enum DriverOperation : byte
    {
        /// <summary>
        /// No abilities.
        /// </summary>
        None = 0,

        /// <summary>
        /// Matches single lines.
        /// </summary>
        Line = 1,

        /// <summary>
        /// Matches a block of text/multiline.
        /// </summary>
        Block = 2,

        /// <summary>
        /// Matches on the entire text.
        /// </summary>
        FullText = 4
    }
}
