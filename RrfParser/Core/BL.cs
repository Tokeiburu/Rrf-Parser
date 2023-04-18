using System.Collections.Generic;
using System.Text;

namespace RrfParser.Core {
	public class Item {
		public int Nameid;
		public int Amount;
		public int Refine;
		public int[] Cards = new int[4];
	}

	public class BL {
		public static Item[] Items = new Item[200];

		public static StringBuilder PreAll = new StringBuilder();
		private string _name;
		private List<int> _aDelays = new List<int>();
		public int MinDamage = 0;
		public int MaxDamage = 0;
		public int Damage = 0;
		public int Type { get; set; }

		public bool HasName {
			get { return _name != null; }
		}

		public bool IsMob {
			get { return (View > 1001 && View < 4000) || View > 20000; }
		}

		public uint Id { get; set; }

		public string Name {
			get {
				if ((View > 1001 && View < 4000) || View > 20000)
					return _name ?? Id + "";
				if (UseUid) {
					return (_name ?? "") + Id;
				}
				if (_name == null || UseUid)
					return Id + "";
				return _name.Contains("\0") ? "!-ERROR-!" : _name;
			}
			set {
				if (Data.BlsNames.ContainsKey(value)) {
					var bl = Data.BlsNames[value];

					if (bl.Id != Id) {
						UseUid = true;
					}
				}

				_name = value;

				if (!UseUid)
					Data.BlsNames[value] = this;
			}
		}

		public bool UseUid { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public int Dir { get; set; }
		public int View { get; set; }
		public int Speed { get; set; }
		public int Level { get; set; }
		public int HP { get; set; }
		public string Map { get; set; }
		public bool IsHidden { get; set; }
		public int IsBoss { get; set; }
		public int EffectState { get; set; }
		public double Time { get; set; }
		public static bool Cancel { get; set; }
		public int AMotion { get; set; }
		public int DMotion { get; set; }
		public int ADelay { get; set; }

		public List<int> ADelays {
			get { return _aDelays; }
			set { _aDelays = value; }
		}

		public uint Tick { get; set; }
		public int LatestSkill { get; set; }

		public List<Dictionary<int, Skill>> SkillIds = new List<Dictionary<int, Skill>>();
		public int InstanceId { get; set; }

		public Dictionary<int, Skill> LatestSkillIds {
			get {
				if (SkillIds.Count <= InstanceId) {
					SkillIds.Add(new Dictionary<int, Skill>());
				}

				return SkillIds[InstanceId];
			}
		}

		public string CustomDefined { get; set; }

		public override string ToString() {
			StringBuilder builder = new StringBuilder();

			builder.AppendFormat("{0},{1},{2},{3}\tscript\t{4}\t{5},", Map, X, Y, Dir, Name, CustomDefined != "" ? "npc_avail[" + CustomDefined + "]" : View + "");
			builder.AppendLine("{");
			builder.AppendLine("\tend;");
			builder.AppendLine("}");
			return builder.ToString();
		}

		public static void AddItem(int i, int nameid, int amount, int refine, int card0, int card1, int card2, int card3) {
			if (i < 0 || i >= 200)
				return;

			if (Items[i] == null)
				Items[i] = new Item();

			Items[i].Nameid = nameid;
			Items[i].Amount += amount;
			Items[i].Refine = refine;
			Items[i].Cards[0] = card0;
			Items[i].Cards[1] = card1;
			Items[i].Cards[2] = card2;
			Items[i].Cards[3] = card3;
		}

		public static void AddItem(int i, int nameid, int amount) {
			if (i < 0 || i >= 200)
				return;

			if (Items[i] == null)
				Items[i] = new Item();

			Items[i].Nameid = nameid;
			Items[i].Amount += amount;
		}

		public static void DeleteItem(int i, int nameid, int amount) {
			AddItem(i, nameid, -amount);
		}
	}
}