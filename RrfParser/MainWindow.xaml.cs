using System;
using System.IO;
using System.Linq.Expressions;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using ErrorManager;
using GrfToWpfBridge;
using RrfParser.Core;
using RrfParser.Core.Avalon;
using RrfParser.Packet;
using RrfParser.Replay;
using RrfParser.WPF;
using TokeiLibrary.WPF;
using TokeiLibrary.WPF.Styles.ListView;

namespace RrfParser {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();

			_addSetting(_panel, "Copy to clipboard", () => RrfParserConfiguration.CopyToClipboard);
			_addSetting(_panel, "Display packet timers", () => RrfParserConfiguration.DisplayPacketTimers);
			_addSetting(_panel, "Show raw packets", () => RrfParserConfiguration.ShowRawPackets);
			_addSetting(_panel, "Revert instance renames", () => RrfParserConfiguration.RevertInstanceRenames);
			_addSetting(_panel, ScriptConfigSetting.Generic, "Generic packet", () => RrfParserConfiguration.Generic);
			_addSetting(_panel, ScriptConfigSetting.Mes, "Mes packet", () => RrfParserConfiguration.Mes);
			_addSetting(_panel, ScriptConfigSetting.ScStart, "Status start packet", () => RrfParserConfiguration.ScStart);
			_addSetting(_panel, ScriptConfigSetting.AfterCast, "AfterCast packet", () => RrfParserConfiguration.AfterCast);
			_addSetting(_panel, ScriptConfigSetting.SpiritBall, "SpiritBall packet", () => RrfParserConfiguration.SpiritBall);
			_addSetting(_panel, ScriptConfigSetting.SkillId, "SkillId packet", () => RrfParserConfiguration.SkillId);
			_addSetting(_panel, ScriptConfigSetting.ClifSkillNoDamage, "ClifSkillNoDamage packet", () => RrfParserConfiguration.ClifSkillNoDamage);
			_addSetting(_panel, ScriptConfigSetting.StatusUpdated, "Status updated packet", () => RrfParserConfiguration.StatusUpdated);
			_addSetting(_panel, ScriptConfigSetting.GroundUnit, "GroundUnit packet", () => RrfParserConfiguration.GroundUnit);
			_addSetting(_panel, ScriptConfigSetting.PosEffect, "PosEffect packet", () => RrfParserConfiguration.PosEffect);
			_addSetting(_panel, ScriptConfigSetting.SkillDamage, "SkillDamage packet", () => RrfParserConfiguration.SkillDamage);
			_addSetting(_panel, ScriptConfigSetting.MonsterHp, "MonsterHp packet", () => RrfParserConfiguration.MonsterHp);
			_addSetting(_panel, ScriptConfigSetting.HpSpRecovery, "HpSpRecovery packet", () => RrfParserConfiguration.HpSpRecovery);
			_addSetting(_panel, ScriptConfigSetting.FixPos, "FixPos packet", () => RrfParserConfiguration.FixPos);
			_addSetting(_panel, ScriptConfigSetting.UnitWalk, "UnitWalk packet", () => RrfParserConfiguration.UnitWalk);
			_addSetting(_panel, ScriptConfigSetting.Showevent, "Showevent packet", () => RrfParserConfiguration.Showevent);
			_addSetting(_panel, ScriptConfigSetting.MobCasting, "MobCasting packet", () => RrfParserConfiguration.MobCasting);
			_addSetting(_panel, ScriptConfigSetting.SkillFailed, "SkillFailed packet", () => RrfParserConfiguration.SkillFailed);
			_addSetting(_panel, ScriptConfigSetting.MapAnnounces, "[output] MapAnnounces", () => RrfParserConfiguration.MapAnnounces);
			_addSetting(_panel, ScriptConfigSetting.DamageLog, "[output] DamageLog", () => RrfParserConfiguration.DamageLog);
			_addSetting(_panel, ScriptConfigSetting.UnitWalkFile, "[output] UnitWalk", () => RrfParserConfiguration.UnitWalkFile);

			AvalonLoader.Load(_textEditor);
			AvalonLoader.SetSyntax(_textEditor, "Script");

			_pathBrowserRrf.Loaded += delegate {
				if (_pathBrowserRrf.RecentFiles.Files.Count > 0) {
					_pathBrowserRrf.Text = _pathBrowserRrf.RecentFiles.Files[0];
				}

				_pathBrowserRrf.TextChanged += new TokeiLibrary.WPF.Styles.PathBrowser.PathBrowserEventHandler(_pathBrowserRrf_TextChanged);
			};
		}

		private void _addSetting(StackPanel panel, string content, Expression<Func<bool>> get, Action set = null) {
			CheckBox box = new CheckBox { Content = content, VerticalAlignment = VerticalAlignment.Center, VerticalContentAlignment = System.Windows.VerticalAlignment.Center };
			box.Margin = new Thickness(3, 1, 1, 1);
			panel.Children.Add(box);

			WpfUtils.AddMouseInOutEffectsBox(box);

			if (set != null)
				Binder.Bind(box, get, set, true);
			else
				Binder.Bind(box, get);
		}

		private void _addSetting(StackPanel panel, ScriptConfigSetting config, string content, Expression<Func<bool>> get, Action set = null) {
			CheckBox box = new CheckBox { Content = content, VerticalAlignment = VerticalAlignment.Center, VerticalContentAlignment = System.Windows.VerticalAlignment.Center };
			box.Margin = new Thickness(3, 1, 1, 1);
			panel.Children.Add(box);

			WpfUtils.AddMouseInOutEffectsBox(box);

			if (set != null)
				Binder.Bind(box, get, set, true);
			else
				Binder.Bind(box, get, delegate {
					config.Enabled = get.Compile()();
				}, true);
		}

		private void _pathBrowserRrf_TextChanged(object sender, EventArgs e) {
			try {
				if (!File.Exists(_pathBrowserRrf.Text))
					return;

				var reader = new ReadPackets();
				reader.ReadPath(_pathBrowserRrf.Text);

				var packetParser = new PacketParser();

				if (RrfParserConfiguration.ShowRawPackets) {
					_textEditor.Text = packetParser.ParseRawOutput(reader.PacketStream);
				}
				else {
					_textEditor.Text = packetParser.ParseAsDecodePackets(reader.PacketStream);
				}

				if (RrfParserConfiguration.CopyToClipboard) {
					Clipboard.SetText(_textEditor.Text);
				}

				SystemSounds.Asterisk.Play();
				_pathBrowserRrf.RecentFiles.AddRecentFile(_pathBrowserRrf.Text);
			}
			catch (Exception err) {
				ErrorHandler.HandleException(err);
			}
		}

		private void _btParse_Click(object sender, RoutedEventArgs e) {
			_pathBrowserRrf_TextChanged(null, null);
		}

		private void _menuItemTranslation_Click(object sender, RoutedEventArgs e) {
			WindowProvider.Show(new TranslationHelper(), _menuItemTranslation, this);
		}

		private void _menuItemReplaySimulation_Click(object sender, RoutedEventArgs e) {
			WindowProvider.Show(new ReplaySimulation(this), _menuItemTranslation, this);
		}
	}
}
