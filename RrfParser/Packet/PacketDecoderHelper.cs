using System;
using System.Collections.Generic;
using RrfParser.Core;
using Utilities;

namespace RrfParser.Packet {
	public static class PacketDecoderHelper {
		public static HashSet<uint> Spawned = new HashSet<uint>();

		public static void AddMonster(PacketStream ps, uint id, int class_, int x, int y, int dir, string name, int isBoss) {
			if ((class_ > 1001 && class_ < 4000) || class_ > 20000) {
				Data.Mob(id);

				if (Data.Mobs[id].X == 0) {
					Data.Mobs[id].X = x;
					Data.Mobs[id].Y = y;
					Data.Mobs[id].Dir = dir;
					Data.Mobs[id].View = class_;
					Data.Mobs[id].Map = Data.LastMap;
					Data.Mobs[id].Name = name ?? "";
					Data.Mobs[id].Time = ps.Delay;
					Data.Mobs[id].IsBoss = isBoss == -1 ? -1 : isBoss;
				}
				else {
					if (Data.Mobs[id].IsBoss == -1) {
						Data.Mobs[id].IsBoss = isBoss;
					}
				}
			}

			// Don't show a monster spawn more than once
			if (Spawned.Add(id)) {
				Data.AppendLine(ps, String.Format("monster \"{0}\", {1}, {2}, \"{4}\", {3}, 1; // dID: {5} ", Data.LastMap, x, y, class_, name, id), ScriptConfigSetting.Generic);
			}
			else {
				Data.AppendLine(ps, String.Format("// (already spawned) monster \"{0}\", {1}, {2}, \"{4}\", {3}, 1; // dID: {5} ", Data.LastMap, x, y, class_, name, id), ScriptConfigSetting.Generic);
			}
		}

		public static void AddMonsterHp(uint id, int maxHp) {
			Data.Mob(id);
			Data.Mobs[id].HP = maxHp;
		}

		public static void AddMonsterAMotion(uint id, int motion) {
			if (motion > 0) {
				Data.Mob(id).AMotion = motion;
			}
		}

		public static void AddMonsterDMotion(uint id, int motion) {
			if (motion > 0) {
				Data.Mob(id).DMotion = motion;
			}
		}


		public static void AddMonsterDamage(uint mobGid, uint playerId, int damage, PacketStream ps) {
			if (damage > 1) {
				Data.Mob(mobGid);
				Data.AppendLine(ps, Data.Mobs[mobGid].View + " - " + playerId + " : [ATTACK] #" + damage, ScriptConfigSetting.DamageLog);
				Data.Mobs[mobGid].MinDamage = Math.Min(damage, Data.Mobs[mobGid].MinDamage == 0 ? Int32.MaxValue : Data.Mobs[mobGid].MinDamage);
				Data.Mobs[mobGid].MaxDamage = Math.Max(damage, Data.Mobs[mobGid].MaxDamage);
			}
		}

		public static void AddMonsterSkill(uint id, int skillId, int skillLv, long packetTick, PacketStream ps) {
			Data.Mob(id);

			Data.AppendLine(ps, Data.Mobs[id].View + " : [SKILL USED " + skillId + "]", ScriptConfigSetting.DamageLog);

			if (!Data.Mobs[id].LatestSkillIds.ContainsKey(skillId)) {
				Data.Mobs[id].LatestSkillIds[skillId] = new Skill { Id = skillId, p_StartTick = packetTick, p_Tick = packetTick, Level =  skillLv };
			}
			else {
				var skill = Data.Mobs[id].LatestSkillIds[skillId];

				if (skill.Level <= 0) {
					skill.Level = Math.Max(skillLv, skill.Level);
				}

				skill.p_Count++;
				skill.p_EndTick = packetTick;
				if (skill.p_StartTick == 0) {
					skill.p_StartTick = packetTick;
					skill.p_Count = 1;
				}
				if (skill.p_Rate == 0) {
					skill.p_Rate = packetTick;
				}
				else {
					skill.p_Rate = Math.Min(skill.p_Rate, packetTick - skill.p_Tick);
				}
				skill.p_Tick = packetTick;
			}
		}

		public static void AddMonsterSkill2(PacketStream ps, uint id, uint dId, int skillId, int skillLv, int tick) {
			Data.Mob(id);
			uint utick = (uint)tick;

			Data.Mobs[id].LatestSkill = skillId;

			Data.AppendLine(ps, Data.Mobs[id].View + " - " + dId + " : [SKILL USED " + skillId + "] Lv" + skillLv, ScriptConfigSetting.DamageLog);

			if (!Data.Mobs[id].LatestSkillIds.ContainsKey(skillId)) {
				Data.Mobs[id].LatestSkillIds[skillId] = new Skill {Id = skillId, Count = 1, Tick = utick, Level = skillLv, Rate = Int32.MaxValue, StartTick = utick};
			}
			else {
				var skill = Data.Mobs[id].LatestSkillIds[skillId];

				if (skill.Level <= 0) {
					skill.Level = Math.Max(skillLv, skill.Level);
				}

				skill.Count++;
				skill.EndTick = utick;
				if (skill.StartTick == 0) {
					skill.StartTick = utick;
					skill.Count = 1;
				}
				if (skill.Rate == 0) {
					skill.Rate = utick;
				}
				else {
					skill.Rate = Math.Min(skill.Rate, utick - skill.Tick);
				}
				skill.Tick = utick;
			}
		}

		public static void AddMonsterSkillEmote(uint id, int skillId, int skillLv, int latestEmote) {
			Data.Mob(id);

			if (!Data.Mobs[id].LatestSkillIds.ContainsKey(skillId)) {
				return;
			}
			else {
				var skill = Data.Mobs[id].LatestSkillIds[skillId];
				skill.Emote = latestEmote;
			}
		}

		public static void AddMonsterSkillLevel(uint id, int skillId, int skillLv) {
			Data.Mob(id);

			if (!Data.Mobs[id].LatestSkillIds.ContainsKey(skillId)) {
				return;
			}
			else {
				var skill = Data.Mobs[id].LatestSkillIds[skillId];
				skill.Level = skillLv;
			}
		}

		public static void AddMonsterCasttime(uint id, int skillId, int casttime, long packetTick) {
			Data.Mob(id);

			Data.Mobs[id].LatestSkill = skillId;

			if (!Data.Mobs[id].LatestSkillIds.ContainsKey(skillId)) {
				Data.Mobs[id].LatestSkillIds[skillId] = new Skill { Id = skillId, Count = 1, Casttime = casttime };
			}
			else {
				Data.Mobs[id].LatestSkillIds[skillId].Casttime = casttime;
				var skill = Data.Mobs[id].LatestSkillIds[skillId];

				skill.p_Count++;
				skill.p_EndTick = packetTick;
				if (skill.p_StartTick == 0) {
					skill.p_StartTick = packetTick;
					skill.p_Count = 1;
				}
				if (skill.p_Rate == 0) {
					skill.p_Rate = packetTick;
				}
				else {
					skill.p_Rate = Math.Min(skill.p_Rate, packetTick - skill.p_Tick);
				}
				skill.p_Tick = packetTick;
			}
		}

		public static void AddMonsterTick(uint id, uint tick) {
			if (tick > 0) {
				Data.Mob(id);

				if (Data.Mobs[id].Tick == 0 || tick <= Data.Mobs[id].Tick)
					Data.Mobs[id].Tick = tick;
				else {
					int tickDiff = (int)(tick - Data.Mobs[id].Tick);

					if (Data.Mobs[id].ADelay == 0) {
						Data.Mobs[id].ADelay = int.MaxValue;
					}

					if (tickDiff < Data.Mobs[id].AMotion) {
					}
					else {
						//Data.Mobs[id].ADelay = (int)Math.Min(Data.Mobs[id].ADelay, tickDiff);
						Data.Mobs[id].ADelays.Add(tickDiff);
					}

					Data.Mobs[id].Tick = tick;
				}
			}
		}

		public static void AddMonsterSkillDamage(PacketStream ps, uint id, uint dId, int skillId, int damage, int div) {
			Data.Mob(id);

			if (damage < 0)
				return;

			Data.AppendLine(ps, Data.Mobs[id].View + " - " + dId + " : [SKILL " + skillId + "] #" + damage + "/" + div, ScriptConfigSetting.DamageLog);

			if (Data.Mobs[id].LatestSkillIds[skillId].MinDamage == 0) {
				Data.Mobs[id].LatestSkillIds[skillId].MinDamage = damage;
				Data.Mobs[id].LatestSkillIds[skillId].MaxDamage = damage;
				Data.Mobs[id].LatestSkillIds[skillId].Div = div;
			}
			else {
				if (div > Data.Mobs[id].LatestSkillIds[skillId].Div)
					Data.Mobs[id].LatestSkillIds[skillId].Div  = div;

				if (damage < Data.Mobs[id].LatestSkillIds[skillId].MinDamage && damage > Data.Mobs[id].LatestSkillIds[skillId].MaxDamage / 2) {
					Data.Mobs[id].LatestSkillIds[skillId].MinDamage = damage;
				}

				if (damage > Data.Mobs[id].LatestSkillIds[skillId].MaxDamage) {
					Data.Mobs[id].LatestSkillIds[skillId].MaxDamage = damage;
				}
			}
		}

		public static void AddMonsterDrop(int id, uint lastDead, double lastDeadTimer, int nameid) {
			int view;

			if (!Data.MobDropsIds.Add(id))
				return;

			if (Data.Mobs.ContainsKey(lastDead)) {
				view = Data.Mobs[Data.LastDead].View;
			}
			else {
				return;
			}

			if (!Data.MobDrops.ContainsKey(view))
				Data.MobDrops[view] = new TkDictionary<int, int>();

			if (!Data.MobDrops[view].ContainsKey(nameid))
				Data.MobDrops[view][nameid] = 1;
			else
				Data.MobDrops[view][nameid]++;
		}

		public static void AddMonsterDead(int view) {
			if (!Data.MobDead.ContainsKey(view)) {
				Data.MobDead[view] = 1;
			}
			else {
				Data.MobDead[view]++;
			}
		}
	}
}
