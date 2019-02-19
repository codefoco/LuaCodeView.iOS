using System;
using System.ComponentModel;
using CoreGraphics;
using CYRTextViewKit;
using Foundation;
using UIKit;

namespace LuaCodeViewKit
{
    [Register("LuaCodeView"), DesignTimeVisible(true)]
    public class LuaCodeView : CYRTextView
    {
        public LuaCodeView(IntPtr handle) : base(handle)
        {
        }

        public LuaCodeView()
        {
            Initilize();
        }

        public LuaCodeView(CGRect rect) : base(rect)
        {
            Initilize();
        }

        public string Tab { get; set; }

        private void Initilize()
        {
            Tokens = new[] {
            new CYRToken("string", "\".*?(\"|$)", UIColor.FromRGB (163, 21, 21)),
            new CYRToken("hex", "\\b0x[0-9 a-f]+\\b", UIColor.FromRGB (10, 136, 91)),
            new CYRToken("float", "\\b\\d+\\.?\\d+e[\\+\\-]?\\d+\\b|\\b\\d+\\.\\d+\\b", UIColor.FromRGB (10, 136, 91)),
            new CYRToken("int", "\\b\\d+\\b", UIColor.FromRGB (10, 136, 91)),
            new CYRToken("operator", "[/\\*,\\;:=<>\\+\\-\\^!·≤≥|]", UIColor.FromRGB (0, 0, 0)),
            new CYRToken("round_brackets", "[\\(\\)]", UIColor.FromRGB (0, 0, 0)),
            new CYRToken("square_brackets", "[\\[\\]]", UIColor.FromRGB (0, 0, 0)),
            new CYRToken("reserved_words", "(\\band\\b|\\bbreak\\b|\\bdo\\b|\\belse\\b|\\belseif\\b|\\bend\\b|\\bfalse\\b|\\bfor\\b|\\bfunction\\b|\\bgoto\\b|\\bif\\b|\\bin\\b|\\blocal\\b|\\bnil\\b|\\bnot\\b|\\bor\\b|\\brepeat\\b|\\breturn\\b|\\bthen\\b|\\btrue\\b|\\buntil\\b|\\bwhile\\b)",
                        UIColor.FromRGB (0, 0, 255)),
            new CYRToken("lua_functions", "(\\b_G\\b|\\b_VERSION\\b|\\bassert\\b|\\bcollectgarbage\\b|\\bdofile\\b|\\berror\\b|\\bgetmetatable\\b|\\bipairs\\b|\\bload\\b|\\bloadfile\\b|\\bnext\\b|\\bpairs\\b|\\bpcall\\b|\\bprint\\b|\\brawequal\\b|\\brawget\\b|\\brawlen\\b|\\brawset\\b|\\brequire\\b|\\bselect\\b|\\bsetmetatable\\b|\\btonumber\\b|\\btostring\\b|\\btype\\b|\\bxpcall\\b)",
            UIColor.FromRGB (111, 0, 138)),
            new CYRToken("lua_library_functions", "(\\bcoroutine\\.create\\b|\\bcoroutine\\.isyieldable\\b|\\bcoroutine\\.resume\\b|\\bcoroutine\\.running\\b|\\bcoroutine\\.status\\b|\\bcoroutine\\.wrap\\b|\\bcoroutine\\.yield\\b|\\bcoroutine\\b|\\bdebug\\.debug\\b|\\bdebug\\.gethook\\b|\\bdebug\\.getinfo\\b|\\bdebug\\.getlocal\\b|\\bdebug\\.getmetatable\\b|\\bdebug\\.getregistry\\b|\\bdebug\\.getupvalue\\b|\\bdebug\\.getuservalue\\b|\\bdebug\\.sethook\\b|\\bdebug\\.setlocal\\b|\\bdebug\\.setmetatable\\b|\\bdebug\\.setupvalue\\b|\\bdebug\\.setuservalue\\b|\\bdebug\\.traceback\\b|\\bdebug\\.upvalueid\\b|\\bdebug\\.upvaluejoin\\b|\\bdebug\\b|\\bio\\.close\\b|\\bio\\.flush\\b|\\bio\\.input\\b|\\bio\\.lines\\b|\\bio\\.open\\b|\\bio\\.output\\b|\\bio\\.popen\\b|\\bio\\.read\\b|\\bio\\.stderr\\b|\\bio\\.stdin\\b|\\bio\\.stdout\\b|\\bio\\.tmpfile\\b|\\bio\\.type\\b|\\bio\\.write\\b|\\bio\\b|\\bmath\\.abs\\b|\\bmath\\.acos\\b|\\bmath\\.asin\\b|\\bmath\\.atan\\b|\\bmath\\.ceil\\b|\\bmath\\.cos\\b|\\bmath\\.deg\\b|\\bmath\\.exp\\b|\\bmath\\.floor\\b|\\bmath\\.fmod\\b|\\bmath\\.huge\\b|\\bmath\\.log\\b|\\bmath\\.max\\b|\\bmath\\.maxinteger\\b|\\bmath\\.min\\b|\\bmath\\.mininteger\\b|\\bmath\\.modf\\b|\\bmath\\.pi\\b|\\bmath\\.rad\\b|\\bmath\\.random\\b|\\bmath\\.randomseed\\b|\\bmath\\.sin\\b|\\bmath\\.sqrt\\b|\\bmath\\.tan\\b|\\bmath\\.tointeger\\b|\\bmath\\.type\\b|\\bmath\\.ult\\b|\\bmath\\b|\\bos\\.clock\\b|\\bos\\.date\\b|\\bos\\.difftime\\b|\\bos\\.execute\\b|\\bos\\.exit\\b|\\bos\\.getenv\\b|\\bos\\.remove\\b|\\bos\\.rename\\b|\\bos\\.setlocale\\b|\\bos\\.time\\b|\\bos\\.tmpname\\b|\\bos\\b|\\bpackage\\.config\\b|\\bpackage\\.cpath\\b|\\bpackage\\.loaded\\b|\\bpackage\\.loadlib\\b|\\bpackage\\.path\\b|\\bpackage\\.preload\\b|\\bpackage\\.searchers\\b|\\bpackage\\.searchpath\\b|\\bpackage\\b|\\bstring\\.byte\\b|\\bstring\\.char\\b|\\bstring\\.dump\\b|\\bstring\\.find\\b|\\bstring\\.format\\b|\\bstring\\.gmatch\\b|\\bstring\\.gsub\\b|\\bstring\\.len\\b|\\bstring\\.lower\\b|\\bstring\\.match\\b|\\bstring\\.pack\\b|\\bstring\\.packsize\\b|\\bstring\\.rep\\b|\\bstring\\.reverse\\b|\\bstring\\.sub\\b|\\bstring\\.unpack\\b|\\bstring\\.upper\\b|\\bstring\\b|\\btable\\.concat\\b|\\btable\\.insert\\b|\\btable\\.move\\b|\\btable\\.pack\\b|\\btable\\.remove\\b|\\btable\\.sort\\b|\\btable\\.unpack\\b|\\btable\\b|\\butf8\\.char\\b|\\butf8\\.charpattern\\b|\\butf8\\.codepoint\\b|\\butf8\\.codes\\b|\\butf8\\.len\\b|\\butf8\\.offset\\b|\\butf8\\b)",
            UIColor.FromRGB (43, 145, 175)),
            new CYRToken("comment", "--.*", UIColor.FromRGB (0, 128, 0)),
            };

            Tab = "\t";
        }

        [Export("awakeAfterUsingCoder:")]
        public NSObject AwakeAfterUsingCoder(NSCoder aDecoder)
        {
            return new LuaCodeView(Frame);
        }

        string FindPreviousLine(UITextPosition from)
        {
            nint offset = -1;
            UITextPosition prefixPosition = GetPosition(from, offset);
            if (prefixPosition == null)
                return string.Empty;

            UITextRange range; //[textView textRangeFromPosition: prefixPosition toPosition: from];

            while (prefixPosition != null)
            {
                range = GetTextRange(prefixPosition, from);

                string text = TextInRange(range);
                if (text[0] == '\n')
                    return text.Substring(1);

                offset--;
                prefixPosition = GetPosition(from, offset);
            }

            return string.Empty;
        }

        int FindFirstNonWhitespace(string previousLine)
        {
            for (int i = 0; i < previousLine.Length; i++)
            {
                char ch = previousLine[i];
                if (!char.IsWhiteSpace(ch))
                    return i;
            }
            return -1;
        }

        string CheckAutoIdentation(string previousLine)
        {
            if (previousLine.EndsWith(" do", StringComparison.Ordinal) ||
                previousLine.EndsWith("\tdo", StringComparison.Ordinal) ||
                previousLine.Equals("do", StringComparison.Ordinal) ||
                previousLine.EndsWith(" then", StringComparison.Ordinal) ||
                previousLine.EndsWith("\tthen", StringComparison.Ordinal) ||
                previousLine.Equals("then", StringComparison.Ordinal) ||
                previousLine.EndsWith(" else", StringComparison.Ordinal) ||
                previousLine.EndsWith("\telse", StringComparison.Ordinal) ||
                previousLine.Equals("else", StringComparison.Ordinal) ||
                (
                    (previousLine.Contains("function ") || previousLine.Contains("function(")) && !previousLine.EndsWith(" end", StringComparison.Ordinal)
                ))
                return Tab;

            return string.Empty;
        }

        string FindIdentationPrefixWithRange(UITextPosition from)
        {
            string previousLine = FindPreviousLine(from);

            if (string.IsNullOrEmpty(previousLine))
                return null;

            string autoIdent = CheckAutoIdentation(previousLine);

            int index = FindFirstNonWhitespace(previousLine);
            // No identation
            if (index == 0)
                return autoIdent;
            // Entire line is whitespace
            if (index == -1)
                return previousLine + autoIdent;

            return previousLine.Substring(0, index) + autoIdent;
        }

        [Export("textView:shouldChangeTextInRange:replacementText:")]
        public bool ShouldChangeTextWithReplacementText(UITextView textView, NSRange range, string text)
        {
            if (text != "\n")
                return true;

            // Get the replacement range of the UITextView
            UITextPosition beginning = BeginningOfDocument;
            UITextPosition start = GetPosition(beginning, range.Location);
            UITextPosition end = GetPosition(start, range.Length);
            UITextRange textRange = GetTextRange(start, end);

            string sufix = FindIdentationPrefixWithRange(start);

            if (string.IsNullOrEmpty(sufix))
                return true;

            string identation = "\n" + sufix;
            // Insert that newline character *and* a tab
            // at the point at which the user inputted just the
            // newline character
            ReplaceText(textRange, identation);

            // Update the cursor position accordingly
            NSRange cursor = new NSRange(range.Location + identation.Length, 0);
            SelectedRange = cursor;
            return false;
        }
    }
}
