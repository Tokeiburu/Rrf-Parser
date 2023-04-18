using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ErrorManager;
using GRF.IO;
using GrfToWpfBridge;
using RrfParser.Core;
using RrfParser.Core.Avalon;
using RrfParser.Packet;
using RrfParser.Replay;
using TokeiLibrary.WPF.Styles;
using Utilities;
using Utilities.Extension;
using Utilities.Services;

namespace RrfParser.WPF {
	/// <summary>
	/// Interaction logic for ReplaySimulation.xaml
	/// </summary>
	public partial class ReplaySimulation : TkWindow {
		private readonly MainWindow _window;

		public ReplaySimulation() {
			InitializeComponent();
		}

		public ReplaySimulation(MainWindow window) {
			_window = window;
			InitializeComponent();

			AvalonLoader.Load(_textEditor);
			AvalonLoader.SetSyntax(_textEditor, "Script");

			Binder.Bind(_tbAid, () => RrfParserConfiguration.ReplaySimulationTickAid, v => RrfParserConfiguration.ReplaySimulationTickAid = v);
			Binder.Bind(_tbTickTo, () => RrfParserConfiguration.ReplaySimulationTickTo, v => RrfParserConfiguration.ReplaySimulationTickTo = v);
			Binder.Bind(_tbTickFrom, () => RrfParserConfiguration.ReplaySimulationTickFrom, v => RrfParserConfiguration.ReplaySimulationTickFrom = v);
		}

		private List<int> _find(byte[] array, byte[] search) {
			List<int> indexes = new List<int>();

			for (int i = 0; i <= array.Length - search.Length; i++) {
				if (array[i] == search[0]) {
					bool found = true;

					for (int j = 0; j < search.Length && found; j++) {
						if (array[i + j] != search[j]) {
							found = false;
						}
					}

					if (found)
						indexes.Add(i);
				}
			}

			return indexes;
		}

		private void _buttonToScript_Click(object sender, RoutedEventArgs e) {
			try {
				int aid = Int32.Parse(_tbAid.Text);
				byte[] aid_b = BitConverter.GetBytes(aid);
				int tickFrom = Int32.Parse(_tbTickFrom.Text);
				int tickTo = Int32.Parse(_tbTickTo.Text);

				var reader = new ReadPackets();
				reader.ReadPath(_window._pathBrowserRrf.Text);

				if (tickTo == 0) {
					tickTo = reader.Chunks.Max(p => p.Time);
				}

				var chunks = reader.Chunks.Where(p => p.Time >= tickFrom && p.Time < tickTo).OrderBy(p => p.Time).ToList();
				var ps = new PacketStream(chunks);

				StringBuilder v = new StringBuilder();
				int previous_tick = 0;

				v.AppendLine("-	script	REPLAY_SIMULATION	-1,{");

				while (ps.CanRead) {
					var chunk = ps.CurrentPacket;

					StringBuilder b = new StringBuilder();

					// Convert PlayerAid to given Aid for known packets
					if (chunk.Header != null) {
						if (Data.PlayerAid != 0 && PacketDecoders.RecvPacketMethods.ContainsKey(chunk.Header.Value)) {
							byte[] replayAid = Data.PlayerAidByte;

							var res = _find(chunk.Data, replayAid);

							for (int i = 0; i < res.Count; i++) {
								Buffer.BlockCopy(aid_b, 0, chunk.Data, res[i], replayAid.Length);
							}
						}
					}

					// Special handling for changing map/zone
					if (chunk.Header == 0x0ac7 && chunk.Length >= 22) {
						byte[] clif_changemap = new byte[22];

						Buffer.BlockCopy(chunk.Data, 0, clif_changemap, 0, 22);
						clif_changemap[0] = 0x91;
						clif_changemap[1] = 0x00;

						// Extract map name
						string name = EncodingService.DisplayEncoding.GetString(clif_changemap, 2, 16, '\0');

						if (name.Contains("@")) {
							int offset = name.IndexOf("@", System.StringComparison.Ordinal) - 1;
							string header = name.Substring(0, offset);
							name = name.Replace(header, "");
							name += '\0';
							byte[] name_b = EncodingService.DisplayEncoding.GetBytes(name);
							Buffer.BlockCopy(name_b, 0, clif_changemap, 2, name_b.Length);
						}

						for (int i = 0; i < clif_changemap.Length; i++) {
							b.Append(clif_changemap[i].ToString("x2"));
						}
					}
					else {
						if (chunk.Header == 0x0091) {
							// Extract map name
							string name = EncodingService.DisplayEncoding.GetString(chunk.Data, 2, 16, '\0');

							if (name.Contains("@")) {
								int offset = name.IndexOf("@", System.StringComparison.Ordinal) - 1;
								string header = name.Substring(0, offset);
								name = name.Replace(header, "");
								name += '\0';
								byte[] name_b = EncodingService.DisplayEncoding.GetBytes(name);
								Buffer.BlockCopy(name_b, 0, chunk.Data, 2, name_b.Length);
							}
						}

						for (int i = 0; i < chunk.Data.Length; i++) {
							b.Append(chunk.Data[i].ToString("x2"));
						}
					}

					var chunk_time = chunk.Time;

					if (chunk.Time == tickFrom) {
						chunk_time = tickFrom + 1;
					}

					if (previous_tick != chunk_time) {
						v.AppendLine("\tend;");
						v.AppendLine("OnTimer" + (chunk_time - tickFrom) + ":");
						v.AppendLine("\tattachrid(" + aid + ");");
					}

					v.Append("\tsendpacket(\"");
					v.Append(b.ToString());
					v.AppendLine("\");");

					previous_tick = chunk_time;

					ps.NextPacket();
				}

				v.AppendLine("\tend;");
				v.AppendLine("OnInit:");
				v.AppendLine("\tinitnpctimer;");
				v.AppendLine("\tend;");
				v.AppendLine("}");

				Clipboard.SetText(v.ToString());
				_textEditor.Text = v.ToString();
			}
			catch (Exception err) {
				ErrorHandler.HandleException(err);
			}
		}
	}
}
