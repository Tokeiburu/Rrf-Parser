using System.Collections.Generic;
using System.Linq;
using System.Text;
using RrfParser.Packet;
using Utilities;

namespace RrfParser.Core {
	public static class Data {
		public static Dictionary<string, StringBuilder> _outputStringBuilders = new Dictionary<string, StringBuilder>();

		public static string LastMap { get; set; }
		public static uint LastDead { get; set; }
		public static uint PlayerAid { get; set; }
		public static byte[] PlayerAidByte { get; set; }
		public static int CurrentSp { get; set; }
		public static int CurrentHp { get; set; }
		public static int LastZeny { get; set; }

		public static long TimeStart { get; set; }
		public static bool Preview { get; set; }
		public static double LastDeadTimer { get; set; }
		public static string FilePath { get; set; }

		public static Dictionary<uint, BL> Bls = new Dictionary<uint, BL>();
		public static Dictionary<uint, BL> Mobs = new Dictionary<uint, BL>();
		public static Dictionary<int, TkDictionary<int, int>> MobDrops = new Dictionary<int, TkDictionary<int, int>>();
		public static Dictionary<int, int> MobDead = new Dictionary<int, int>();
		public static HashSet<int> MobDropsIds = new HashSet<int>();
		public static Dictionary<string, BL> BlsNames = new Dictionary<string, BL>();

		public static void AppendLine(PacketStream ps, string line, ScriptConfigSetting config) {
			if (config.Enabled) {
				_outputStringBuilders[config.OutputFile].AppendLine(line + (config.DisplayTimer && RrfParserConfiguration.DisplayPacketTimers ? Time(ps) : ""));
			}
		}

		public static BL Bl(uint id) {
			if (!Bls.ContainsKey(id)) {
				Bls[id] = new BL() { Id = id, Map = LastMap };
			}

			return Bls[id];
		}

		public static BL Mob(uint id) {
			if (!Mobs.ContainsKey(id)) {
				Mobs[id] = new BL() { Id = id, Map = LastMap };
			}

			return Mobs[id];
		}

		public static string GetName(uint id) {
			if (Bls.ContainsKey(id))
				return Bls[id].Name;
			if (Mobs.ContainsKey(id))
				return Mobs[id].Name;
			return "" + id;
		}

		public static int GetClass(uint id) {
			if (Bls.ContainsKey(id))
				return Bls[id].View;
			if (Mobs.ContainsKey(id))
				return Mobs[id].View;
			return (int)id;
		}

		public static string Time(PacketStream ps) {
			if (ps == null)
				return "";
			int s = ((int)ps.Delay / 1000) % 60;
			return " // TICK: " + ps.Delay + " ms, INTERVAL: " + ps.IntervalDelay + ", FORMAT: " + (int)(ps.Delay / 60000) + ":" + (s < 10 ? "0" : "") + s;
		}

		public static void Reset(bool resetAll) {
			LastDead = 0;
			CurrentHp = 0;
			CurrentSp = 0;
			LastDeadTimer = 0;

			if (resetAll) {
				Bls.Clear();
				Mobs.Clear();
				BlsNames.Clear();
			}
			else {
				Mobs.Values.ToList().ForEach(p => p.LatestSkillIds.Clear());
				Mobs.Values.ToList().ForEach(p => p.Damage = 0);
			}

			MobDead.Clear();
			MobDrops.Clear();
			MobDropsIds.Clear();
			PacketDecoderHelper.Spawned.Clear();

			_outputStringBuilders.Clear();

			foreach (var config in ScriptConfigSetting.Configs) {
				_outputStringBuilders[config.OutputFile] = new StringBuilder();
			}
		}
	}
}
