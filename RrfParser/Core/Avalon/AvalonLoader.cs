﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using ICSharpCode.AvalonEdit.Rendering;
using RrfParser;

namespace RrfParser.Core.Avalon {
	public class AvalonLoader {
		static AvalonLoader() {
			string[] syntaxes = {
				"Custom Highlighting", "RrfParser.Core.Avalon.Syntax.CustomHighlighting.xshd",
				"Lua", "RrfParser.Core.Avalon.Syntax.Lua.xshd",
				"Imf", "RrfParser.Core.Avalon.Syntax.Imf.xshd",
				//"DefaultColors", "SDE.Core.Avalon.Syntax.DefaultColors.xshd",
				"Python", "RrfParser.Core.Avalon.Syntax.Python.xshd",
				"DebugDb", "RrfParser.Core.Avalon.Syntax.DebugDb.xshd",
			};

			for (int i = 0; i < syntaxes.Length; i += 2) {
				IHighlightingDefinition customHighlighting;

				using (Stream s = typeof(App).Assembly.GetManifestResourceStream(syntaxes[i + 1])) {
					if (s == null)
						throw new InvalidOperationException("Could not find embedded resource");
					using (XmlReader reader = new XmlTextReader(s)) {
						customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
					}
				}

				HighlightingManager.Instance.RegisterHighlighting(syntaxes[i], new[] { ".cool" }, customHighlighting);
			}
		}

		public static void Load(TextEditor editor) {
			new AvalonDefaultLoading().Attach(editor);
		}

		public static void Select(string preset, ComboBox box) {
			box.Dispatcher.Invoke(new Action(delegate {
				try {
					var result = box.Items.Cast<object>().FirstOrDefault(p => p.ToString() == preset);
					if (result != null) {
						box.SelectedItem = result;
					}
				}
				catch {
				}
			}));
		}

		public static bool IsWordBorder(ITextSource document, int offset) {
			return TextUtilities.GetNextCaretPosition(document, offset - 1, LogicalDirection.Forward, CaretPositioningMode.WordBorder) == offset;
		}

		public static bool IsWholeWord(ITextSource document, int offsetStart, int offsetEnd) {
			int start = TextUtilities.GetNextCaretPosition(document, offsetStart - 1, LogicalDirection.Forward, CaretPositioningMode.WordBorder);

			if (start != offsetStart)
				return false;

			int end = TextUtilities.GetNextCaretPosition(document, offsetStart, LogicalDirection.Forward, CaretPositioningMode.WordBorder);

			if (end != offsetEnd)
				return false;

			return true;
		}

		public static string GetWholeWord(TextDocument document, TextEditor textEditor, LogicalDirection direction = LogicalDirection.Forward) {
			TextArea textArea = textEditor.TextArea;
			TextView textView = textArea.TextView;

			if (textView == null) return null;

			Point pos = textArea.TextView.GetVisualPosition(textArea.Caret.Position, VisualYPosition.LineMiddle) - textArea.TextView.ScrollOffset;
			VisualLine line = textView.GetVisualLine(textEditor.TextArea.Caret.Position.Line);

			if (line != null) {
				int visualColumn = line.GetVisualColumn(pos, textArea.Selection.EnableVirtualSpace);
				int wordStartVc;

				if (line.VisualLength == visualColumn) {
					wordStartVc = line.GetNextCaretPosition(visualColumn, LogicalDirection.Backward, CaretPositioningMode.WordStartOrSymbol, textArea.Selection.EnableVirtualSpace);
				}
				else {
					wordStartVc = line.GetNextCaretPosition(visualColumn + 1, LogicalDirection.Backward, CaretPositioningMode.WordStartOrSymbol, textArea.Selection.EnableVirtualSpace);
				}

				if (wordStartVc == -1)
					wordStartVc = 0;
				int wordEndVc = line.GetNextCaretPosition(wordStartVc, direction, CaretPositioningMode.WordBorderOrSymbol, textArea.Selection.EnableVirtualSpace);
				if (wordEndVc == -1)
					wordEndVc = line.VisualLength;

				if (visualColumn < wordStartVc || (direction == LogicalDirection.Forward && visualColumn > wordEndVc))
					return "";

				if (direction == LogicalDirection.Backward && wordStartVc > wordEndVc) {
					int temp = wordEndVc;
					wordEndVc = wordStartVc;
					wordStartVc = temp;
				}

				int relOffset = line.FirstDocumentLine.Offset;
				int wordStartOffset = line.GetRelativeOffset(wordStartVc) + relOffset;
				int wordEndOffset = line.GetRelativeOffset(wordEndVc) + relOffset;

				return textEditor.TextArea.Document.GetText(wordStartOffset, wordEndOffset - wordStartOffset);
			}

			return null;
		}

		public static ISegment GetWholeWordSegment(TextDocument document, TextEditor textEditor, LogicalDirection direction = LogicalDirection.Forward) {
			TextArea textArea = textEditor.TextArea;
			TextView textView = textArea.TextView;

			if (textView == null) return null;

			Point pos = textArea.TextView.GetVisualPosition(textArea.Caret.Position, VisualYPosition.LineMiddle) - textArea.TextView.ScrollOffset;
			VisualLine line = textView.GetVisualLine(textEditor.TextArea.Caret.Position.Line);

			if (line != null) {
				int visualColumn = line.GetVisualColumn(pos, textArea.Selection.EnableVirtualSpace);
				int wordStartVc;

				if (line.VisualLength == visualColumn) {
					wordStartVc = line.GetNextCaretPosition(visualColumn, LogicalDirection.Backward, CaretPositioningMode.WordStartOrSymbol, textArea.Selection.EnableVirtualSpace);
				}
				else {
					wordStartVc = line.GetNextCaretPosition(visualColumn + 1, LogicalDirection.Backward, CaretPositioningMode.WordStartOrSymbol, textArea.Selection.EnableVirtualSpace);
				}

				if (wordStartVc == -1)
					wordStartVc = 0;
				int wordEndVc = line.GetNextCaretPosition(wordStartVc, direction, CaretPositioningMode.WordBorderOrSymbol, textArea.Selection.EnableVirtualSpace);
				if (wordEndVc == -1)
					wordEndVc = line.VisualLength;

				if (visualColumn < wordStartVc || (direction == LogicalDirection.Forward && visualColumn > wordEndVc))
					return new SimpleSegment();

				if (direction == LogicalDirection.Backward && wordStartVc > wordEndVc) {
					int temp = wordEndVc;
					wordEndVc = wordStartVc;
					wordStartVc = temp;
				}

				int relOffset = line.FirstDocumentLine.Offset;
				int wordStartOffset = line.GetRelativeOffset(wordStartVc) + relOffset;
				int wordEndOffset = line.GetRelativeOffset(wordEndVc) + relOffset;

				return new SimpleSegment(wordStartOffset, wordEndOffset - wordStartOffset);
			}

			return null;
		}

		public static void SetSyntax(TextEditor editor, string ext) {
			if (HighlightingManager.Instance.GetDefinition(ext) == null) {
				IHighlightingDefinition customHighlighting;
				using (Stream s = typeof(App).Assembly.GetManifestResourceStream("RrfParser.Core.Avalon.Syntax." + ext + ".xshd")) {
					if (s == null)
						throw new InvalidOperationException("Could not find embedded resource");
					using (XmlReader reader = new XmlTextReader(s)) {
						customHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
					}
				}

				HighlightingManager.Instance.RegisterHighlighting(ext, new[] { ".cool" }, customHighlighting);
			}

			var def = HighlightingManager.Instance.GetDefinition(ext);

			foreach (var color in def.NamedHighlightingColors) {
				var tb = Application.Current.TryFindResource("Avalon" + ext + color.Name) as TextBlock;

				if (tb != null) {
					color.Foreground = new SimpleHighlightingBrush(tb.Foreground as SolidColorBrush);
					color.FontStyle = tb.FontStyle;
					color.FontWeight = tb.FontWeight;
				}
			}

			var tbDef = Application.Current.TryFindResource("Avalon" + ext + "Default") as TextBlock;

			if (tbDef != null) {
				editor.Foreground = tbDef.Foreground;
				editor.FontWeight = tbDef.FontWeight;
				editor.FontStyle = tbDef.FontStyle;
			}

			editor.SyntaxHighlighting = def;
		}
	}
}