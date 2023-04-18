using System;
using System.Globalization;
using System.Reflection;
using GRF.IO;
using TokeiLibrary;
using Utilities;

namespace RrfParser.Core {
	/// <summary>
	/// Contains all the configuration information
	/// The ConfigAsker shouldn't be used manually to store variable,
	/// make a new property instead. The properties should also always
	/// have a default value.
	/// </summary>
	public static class RrfParserConfiguration {
		private static ConfigAsker _configAsker;

		public static ConfigAsker ConfigAsker {
			get { return _configAsker ?? (_configAsker = new ConfigAsker(GrfPath.Combine(Configuration.ApplicationPath, "config.txt"))); }
			set { _configAsker = value; }
		}

		#region Program's configuration and information

		public static string PublicVersion {
			get { return "1.0.0.1"; }
		}

		public static string Author {
			get { return "Tokeiburu"; }
		}

		public static string ProgramName {
			get { return "Rrf Parser"; }
		}

		public static string RealVersion {
			get { return Assembly.GetEntryAssembly().GetName().Version.ToString(); }
		}

		public static int PatchId {
			get { return Int32.Parse(ConfigAsker["[RrfParser - Patch ID]", "0"]); }
			set { ConfigAsker["[RrfParser - Patch ID]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		#endregion

		public static string ProgramDataPath {
			get { return GrfPath.Combine(Configuration.ApplicationDataPath, ProgramName); }
		}

		public static bool CopyToClipboard {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - CopyToClipboard]", "true"]); }
			set { ConfigAsker["[RrfParser - CopyToClipboard]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool DisplayPacketTimers {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - DisplayPacketTimers]", "true"]); }
			set { ConfigAsker["[RrfParser - DisplayPacketTimers]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool ShowRawPackets {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - ShowRawPackets]", "false"]); }
			set { ConfigAsker["[RrfParser - ShowRawPackets]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool RevertInstanceRenames {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - RevertInstanceRenames]", "false"]); }
			set { ConfigAsker["[RrfParser - RevertInstanceRenames]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool Mes {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - Mes]", "true"]); }
			set { ConfigAsker["[RrfParser - Mes]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool ScStart {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - ScStart]", "true"]); }
			set { ConfigAsker["[RrfParser - ScStart]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool AfterCast {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - AfterCast]", "true"]); }
			set { ConfigAsker["[RrfParser - AfterCast]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool SpiritBall {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - SpiritBall]", "false"]); }
			set { ConfigAsker["[RrfParser - SpiritBall]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool SkillId {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - SkillId]", "true"]); }
			set { ConfigAsker["[RrfParser - SkillId]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool ClifSkillNoDamage {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - ClifSkillNoDamage]", "true"]); }
			set { ConfigAsker["[RrfParser - ClifSkillNoDamage]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool StatusUpdated {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - StatusUpdated]", "true"]); }
			set { ConfigAsker["[RrfParser - StatusUpdated]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool GroundUnit {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - GroundUnit]", "true"]); }
			set { ConfigAsker["[RrfParser - GroundUnit]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool PosEffect {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - PosEffect]", "true"]); }
			set { ConfigAsker["[RrfParser - PosEffect]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool SkillDamage {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - SkillDamage]", "false"]); }
			set { ConfigAsker["[RrfParser - SkillDamage]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool Generic {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - Generic]", "true"]); }
			set { ConfigAsker["[RrfParser - Generic]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool MapAnnounces {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - MapAnnounces]", "true"]); }
			set { ConfigAsker["[RrfParser - MapAnnounces]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool MonsterHp {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - MonsterHp]", "false"]); }
			set { ConfigAsker["[RrfParser - MonsterHp]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool DamageLog {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - DamageLog]", "true"]); }
			set { ConfigAsker["[RrfParser - DamageLog]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool HpSpRecovery {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - HpSpRecovery]", "false"]); }
			set { ConfigAsker["[RrfParser - HpSpRecovery]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool FixPos {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - FixPos]", "false"]); }
			set { ConfigAsker["[RrfParser - FixPos]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool UnitWalk {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - UnitWalk]", "false"]); }
			set { ConfigAsker["[RrfParser - UnitWalk]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool UnitWalkFile {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - UnitWalkFile]", "true"]); }
			set { ConfigAsker["[RrfParser - UnitWalkFile]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool Showevent {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - Showevent]", "true"]); }
			set { ConfigAsker["[RrfParser - Showevent]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool MobCasting {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - Mobcasting]", "false"]); }
			set { ConfigAsker["[RrfParser - Mobcasting]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static bool SkillFailed {
			get { return Boolean.Parse(ConfigAsker["[RrfParser - SkillFailed]", "false"]); }
			set { ConfigAsker["[RrfParser - SkillFailed]"] = value.ToString(CultureInfo.InvariantCulture); }
		}

		public static string ReplaySimulationTickFrom {
			get { return ConfigAsker["[RrfParser - ReplaySimulationTickFrom]", "0"]; }
			set { ConfigAsker["[RrfParser - ReplaySimulationTickFrom]"] = value; }
		}

		public static string ReplaySimulationTickTo {
			get { return ConfigAsker["[RrfParser - ReplaySimulationTickTo]", "0"]; }
			set { ConfigAsker["[RrfParser - ReplaySimulationTickTo]"] = value; }
		}

		public static string ReplaySimulationTickAid {
			get { return ConfigAsker["[RrfParser - ReplaySimulationTickAid]", "0"]; }
			set { ConfigAsker["[RrfParser - ReplaySimulationTickAid]"] = value; }
		}

	}
}
