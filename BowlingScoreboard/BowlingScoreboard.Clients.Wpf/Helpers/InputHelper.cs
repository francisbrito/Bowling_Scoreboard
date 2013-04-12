using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BowlingScoreboard.Clients.Wpf.Helpers
{
    public static class InputHelper
    {
        // Yes, this is a little bit over-simplistic, but hey, I can get away with it. ;)
        public const string UrlRegex 
            = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

        public const string NetworkFileRegex 
            = @"^((\\\\[a-zA-Z0-9-]+\\[a-zA-Z0-9`~!@#$%^&(){}'._-]+([ ]+[a-zA-Z0-9`~!@#$%^&(){}'._-]+)*)|([a-zA-Z]:))(\\[^ \\/:*?""<>|]+([ ]+[^ \\/:*?""<>|]+)*)*\\?$";

        public const string LocalFileRegex 
            = @"^(?:[a-zA-Z]\:|\\\\[\w\.]+\\[\w.]+)\\(?:[\w]+\\)*\w([\w.])+$";

        public static bool IsInputEmpty(string input)
        {
            return string.IsNullOrWhiteSpace(input.Trim());
        }

        public static bool IsUrl(string input)
        {
            return Regex.IsMatch(input, UrlRegex);
        }

        public static bool IsNetworkFile(string input)
        {
            return Regex.IsMatch(input, NetworkFileRegex);
        }

        public static bool IsLocalFile(string input)
        {
            return Regex.IsMatch(input, LocalFileRegex);
        }

        public static string ValidateInput(string input)
        {
            var errors = new StringBuilder();

            // TODO: Add more validations.

            if (IsInputEmpty(input))
            {
                errors.AppendLine("Please provide path or URL.");
            }
            else if (!IsUrl(input) && !IsNetworkFile(input) && !(IsLocalFile(input)))
            {
                errors.AppendLine("I dont think I know how to get there...");
            }

            return errors.ToString();
        }
    }
}
