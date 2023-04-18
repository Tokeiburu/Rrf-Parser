using System.Collections.Generic;

namespace RrfParser.Core {
	public sealed class ScriptConfigSetting {
		public static List<ScriptConfigSetting> Configs = new List<ScriptConfigSetting>();

		public bool Enabled { get; set; }
		public bool DisplayTimer { get; set; }
		public string OutputFile { get; set; }

		public static ScriptConfigSetting Mes { get; set; }
		public static ScriptConfigSetting ScStart { get; set; }
		public static ScriptConfigSetting AfterCast { get; set; }
		public static ScriptConfigSetting SpiritBall { get; set; }
		public static ScriptConfigSetting SkillId { get; set; }
		public static ScriptConfigSetting ClifSkillNoDamage { get; set; }
		public static ScriptConfigSetting StatusUpdated { get; set; }
		public static ScriptConfigSetting GroundUnit { get; set; }
		public static ScriptConfigSetting PosEffect { get; set; }
		public static ScriptConfigSetting SkillDamage { get; set; }
		public static ScriptConfigSetting Generic { get; set; }
		public static ScriptConfigSetting GenericNoTime { get; set; }
		public static ScriptConfigSetting MapAnnounces { get; set; }
		public static ScriptConfigSetting MonsterHp { get; set; }
		public static ScriptConfigSetting DamageLog { get; set; }
		public static ScriptConfigSetting HpSpRecovery { get; set; }
		public static ScriptConfigSetting FixPos { get; set; }
		public static ScriptConfigSetting UnitWalk { get; set; }
		public static ScriptConfigSetting UnitWalkFile { get; set; }
		public static ScriptConfigSetting Showevent { get; set; }
		public static ScriptConfigSetting MobCasting { get; set; }
		public static ScriptConfigSetting SkillFailed { get; set; }

		private ScriptConfigSetting() {
			Enabled = true;
			DisplayTimer = true;
			OutputFile = "output.txt";
			Configs.Add(this);
		}

		static ScriptConfigSetting() {
			Mes = new ScriptConfigSetting();
			ScStart = new ScriptConfigSetting();
			AfterCast = new ScriptConfigSetting();
			SpiritBall = new ScriptConfigSetting();
			SkillId = new ScriptConfigSetting();
			ClifSkillNoDamage = new ScriptConfigSetting();
			StatusUpdated = new ScriptConfigSetting();
			GroundUnit = new ScriptConfigSetting();
			PosEffect = new ScriptConfigSetting();
			SkillDamage = new ScriptConfigSetting();
			Generic = new ScriptConfigSetting();
			GenericNoTime = new ScriptConfigSetting();
			MapAnnounces = new ScriptConfigSetting();
			MonsterHp = new ScriptConfigSetting();
			DamageLog = new ScriptConfigSetting();
			HpSpRecovery = new ScriptConfigSetting();
			FixPos = new ScriptConfigSetting();
			UnitWalk = new ScriptConfigSetting();
			UnitWalkFile = new ScriptConfigSetting();
			Showevent = new ScriptConfigSetting();
			MobCasting = new ScriptConfigSetting();
			SkillFailed = new ScriptConfigSetting();

			GenericNoTime.DisplayTimer = false;
			MapAnnounces.OutputFile = "mapannounces.txt";
			DamageLog.OutputFile = "damage_log.txt";
			HpSpRecovery.OutputFile = "hpsp_changes.txt";
			UnitWalkFile.OutputFile = "unit_walk.txt";
			HpSpRecovery.Enabled = false;
		}
	}
}
