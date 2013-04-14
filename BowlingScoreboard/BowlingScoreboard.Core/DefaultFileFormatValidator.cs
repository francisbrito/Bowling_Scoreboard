using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BowlingScoreboard.Core
{
    public class DefaultFileFormatValidator : IFileFormatValidator
    {
        public bool IsValid(string input)
        {
            var result = true;

            var entries = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (entries.Count() != 42)
            {
                result = false;
            }

            return result;
        }
    }
}
