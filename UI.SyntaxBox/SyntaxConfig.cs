using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.SyntaxBox
{
    /// <summary>
    /// Impements a regex-based syntax driver that is configurable directly
    /// within XAML.
    /// </summary>
    public class SyntaxConfig : List<ISyntaxRule>, ISyntaxDriver
    {
        private Lazy<Dictionary<DriverOperation, List<ISyntaxRule>>> _ruleIndex;

        #region Public members
        // ...................................................................
        /// <summary>
        /// Initializes a new instance of the <see cref="SyntaxConfig"/> class using default values.
        /// </summary>
        public SyntaxConfig()
        {
            // Lazy-build an index of all rules so we can get the right
            // kind of rule quickly.
            this._ruleIndex = new Lazy<Dictionary<DriverOperation, List<ISyntaxRule>>>(
                () =>
                    {
                        // Set the ID of each rule to it's position.
                        // This is used for sorting the matches later.
                        for (int i = 0; i < this.Count; i++)
                            this[i].RuleId = i;

                        return this.GroupBy((rule) => rule.Op)
                            .ToDictionary(
                                (group) => group.Key,
                                (group) => group.ToList());
                    },
                System.Threading.LazyThreadSafetyMode.PublicationOnly);
        }
        // ...................................................................
        #endregion

        #region ISyntaxDriver members
        // ...................................................................
        /// <summary>
        /// Gets a combination of bit-flags that describe the combined abilities of the syntax rules.
        /// </summary>
        public DriverOperation Abilities => this
            .Select((rule) => rule.Op)
            ?.Aggregate(DriverOperation.None, (a, b) => a | b)
            ?? DriverOperation.None;
        // ...................................................................
        /// <summary>
        /// Matches the specified text against the syntax rules.
        /// </summary>
        /// <param name="Operation"></param>
        /// <param name="Text"></param>
        /// <returns>Sequence containing the resulting instructions.</returns>
        public IEnumerable<FormatInstruction> Match(DriverOperation Operation, string Text)
        {
            if (!this._ruleIndex.Value.TryGetValue(Operation, out List<ISyntaxRule> rules))
                return Enumerable.Empty<FormatInstruction>();

            List<FormatInstruction> matches = rules
                .SelectMany((rule) => rule.Match(Text))
                .ToList();

            return (matches);
        }
        // ...................................................................
        #endregion
    }
}
