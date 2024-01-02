using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Assets.Scripts.Editor
{
    // From: https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/ProjectBrowser/GlobSearchUtilities.cs
    public static class GlobSearchUtilities
    {
        static readonly Regex k_BasicSymbolsRegex = new Regex(@"(?<range>\\\[..+?\])|(?<dstarfold>\\\*\\\*/)|(?<dstar>\\\*\\\*)|(?<star>\\\*)|(?<single>\\\?)");
        static readonly Regex k_ComplexSymbolsRegex = new Regex(@"(?<or>\\\(.+?(?:\\\|.+?)+\\\))");
        static Dictionary<string, Func<string, string>> s_GlobToRegexMatch;

        static GlobSearchUtilities()
        {
            s_GlobToRegexMatch = new Dictionary<string, Func<string, string>>();

            //Match any number of characters, where characters exist - end in a fold.
            s_GlobToRegexMatch.Add("dstarfold", match => "(.+/)?");

            //Match any number of characters
            s_GlobToRegexMatch.Add("dstar", match => ".*");

            //Match any number of non-"/" characters
            s_GlobToRegexMatch.Add("star", match => @"[^/]*");

            //Match a single non-"/" character
            s_GlobToRegexMatch.Add("single", match => @"[^/]");
            s_GlobToRegexMatch.Add("range", match => match.Replace(@"\[", "["));
            s_GlobToRegexMatch.Add("or", match => match.Replace(@"\(", "(").Replace(@"\|", "|").Replace(@"\)", ")"));
        }

        public static string GlobToRegex(string glob)
        {
            // Escape any glob character that could be interpreted in the regex
            var regex = Regex.Escape(glob);

            // Handle basic symbols replacement first
            regex = k_BasicSymbolsRegex.Replace(regex, ReplaceGlobGroups);

            // Complex patterns are replaced in a second pass because they may contain basic symbols that we want to replace first.
            regex = k_ComplexSymbolsRegex.Replace(regex, ReplaceGlobGroups);

            // Add ^ and $ to make sure the search is always done on the full path.
            // Searches like "Editor" should match the same as "**Editor" and looks only for Editor folders or file in any subfolder
            // This is why we are always adding optional folder path at the beginning and option folder ending character in the end.
            return $"^(.+/)?{regex}/?$";
        }

        static string ReplaceGlobGroups(Match match)
        {
            foreach (var replace in s_GlobToRegexMatch)
            {
                if (match.Groups[replace.Key].Success)
                {
                    return replace.Value(match.Value);
                }
            }

            return match.Value;
        }
    }
}
