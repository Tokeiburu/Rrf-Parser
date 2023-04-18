using System;
using System.Linq;
using System.Text.RegularExpressions;
using RrfParser.Core.Avalon;
using TokeiLibrary.WPF.Styles;
using Utilities;
using Utilities.Services;

namespace RrfParser.WPF {
	/// <summary>
	/// Interaction logic for TranslationHelper.xaml
	/// </summary>
	public partial class TranslationHelper : TkWindow {
		public TranslationHelper() {
			InitializeComponent();

			_rtb1.TextChanged += new EventHandler(_rtb1_TextChanged);
			AvalonLoader.Load(_rtb1);
			AvalonLoader.SetSyntax(_rtb1, "Script");

			AvalonLoader.Load(_rtb2);
			AvalonLoader.SetSyntax(_rtb2, "Script");
		}

		private void _rtb1_TextChanged(object sender, EventArgs e) {
			var korean = EncodingService.ConvertStringToKorean(_rtb1.Text);

			korean = Regex.Replace(korean, @"mes ""([^""]+?)"";", "$1");
			korean = Regex.Replace(korean, @"select\(""([^""]+?):""\);", "$1");
			korean = Regex.Replace(korean, @"npctalkself ""([^""]+?)"",.*;", "$1");
			korean = Regex.Replace(korean, @"npctalk ""([^""]+?)"",.*;", "$1");
			korean = Regex.Replace(korean, @"npctalk2 ""([^""]+?)"",.*;", "$1");
			korean = Regex.Replace(korean, @"npcmestalk ""([^""]+?)"",.*;", "$1");
			korean = Regex.Replace(korean, @"select \(""([^""]+?):""\);", "$1");
			korean = Regex.Replace(korean, @"^.*script\t([^""]+?)\t.*", "$1", RegexOptions.Multiline);
			korean = Regex.Replace(korean, @"\t*", "");
			korean = Regex.Replace(korean, @" //.*", "");

			korean = korean.Replace("\r", "");

			var lines = korean.Split('\n').ToList();

			for (int i = 0; i < lines.Count; i++) {
				var oriIndex = i;
				var line = lines[i];
				int index = 0;

				Match match;

				while ((match = Regex.Match(line, ".*\"([^\"]+?)\".*")).Success) {
					var name = line.Substring(match.Groups[1].Index, match.Groups[1].Length);
					line = name;
				}

				while ((match = Regex.Match(line, "<NAVI>([^<]+?)<INFO>(.*)</INFO></NAVI>")).Success) {
					var name = line.Substring(match.Groups[1].Index, match.Groups[1].Length);
					var coords = line.Substring(match.Groups[2].Index, match.Groups[2].Length);

					var navi = "<NAVI>" + name + "<INFO>" + coords + "</INFO></NAVI>";
					lines.Insert(++i, navi);
					lines.Insert(++i, name);

					line = line.Replace(navi, name);
				}

				while (true) {
					try {
						var idS = line.IndexOf("^", index, System.StringComparison.Ordinal);

						if (idS < 0)
							break;

						var idE = line.IndexOf("^", idS + 1, System.StringComparison.Ordinal);

						if (idE < 0)
							break;

						string c1 = line.Substring(idS, 7);
						string c2 = line.Substring(idE, 7);
						string sub = line.Substring(idS + 7, idE - (idS + 7));

						lines.Insert(++i, c1 + c2);
						lines.Insert(++i, sub);

						line = line.Remove(idE, 7);
						line = line.Remove(idS, 7);
						index = idE + 1 - 7;
					}
					catch {
						break;
					}
				}

				lines[oriIndex] = line;
			}

			korean = Methods.Aggregate(lines, "\n");
			_rtb2.Text = korean;
		}
	}
}
