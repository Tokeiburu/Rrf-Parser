namespace RrfParser.Core {
	public class Skill {
		public int Id { get; set; }
		public int Level { get; set; }
		public uint Tick { get; set; }
		public uint StartTick { get; set; }
		public uint EndTick { get; set; }
		public uint Rate { get; set; }
		public double Count { get; set; }
		public int Emote { get; set; }
		public int Casttime { get; set; }

		public long p_Tick { get; set; }
		public long p_StartTick { get; set; }
		public long p_EndTick { get; set; }
		public long p_Rate { get; set; }
		public double p_Count { get; set; }
		public int MinDamage { get; set; }
		public int MaxDamage { get; set; }
		public int Div { get; set; }

		public int Cancel { get; set; }
	}
}
