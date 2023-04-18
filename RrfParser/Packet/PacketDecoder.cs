using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GRF.IO;
using RrfParser.Core;
using Utilities;

namespace RrfParser.Packet {
	public static class PacketDecoders {
		public delegate void PacketDecoderMethod(ByteReader reader, PacketStream ps);

		public static Dictionary<ushort, PacketDecoderMethod> RecvPacketMethods = new Dictionary<ushort, PacketDecoderMethod>();

		static PacketDecoders() {
			string name;
			string mes;
			ushort us;

			RecvPacketMethods[0x01f3] = (reader, ps) => {
				uint uid = reader.UInt32();
				int effect = reader.Int32();

				Data.AppendLine(ps, "specialeffect " + effect + ", AREA, \"" + Data.GetName(uid) + "\";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x01de] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.PACKET_ZC_NOTIFY_SKILL>(reader);

				uint sId = p.AID;
				uint dId = p.targetID;

				BL mob = Data.Mob(p.AID);

				if (!mob.IsMob && p.SKID != 429) {
					foreach (var entry in Data.Mobs) {
						if (entry.Value.LatestSkillIds.ContainsKey(p.SKID)) {
							sId = entry.Key;
							mob = entry.Value;
							break;
						}
					}
				}

				if (Data.Mobs.ContainsKey(dId)) {
					BL mob2 = Data.Mob(dId);
					mob2.Damage += p.damage;
				}

				Data.AppendLine(ps, "Skill damage: " + p.SKID + ", skill_lv = " + p.level + ", sdelay = " + p.attackMT + ", rdelay = " + p.attackedMT + ", damage = " + p.damage + ", div = " + p.count + ", type = " + Constants.GetDmgType(p.action) + ", sid = " + p.AID + ", dId = " + p.targetID + "; ", ScriptConfigSetting.SkillDamage);

				if (mob.IsMob) {
					PacketDecoderHelper.AddMonsterSkill2(ps, sId, dId, p.SKID, p.level, (int)p.startTime);
					PacketDecoderHelper.AddMonsterSkillDamage(ps, sId, dId, p.SKID, p.damage, p.count);
				}
			};

			RecvPacketMethods[0x0117] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.PACKET_ZC_NOTIFY_GROUNDSKILL_0x0117>(reader);

				BL mob = Data.Mob(p.AID);

				if (mob.IsMob) {
					PacketDecoderHelper.AddMonsterSkill(p.AID, p.SKID, p.level, ps.CurrentPacket.Time, ps);
				}

				Data.AppendLine(ps, "Pos effect: " + p.SKID + ", " + p.xPos + ", " + p.yPos + ", sid = " + p.AID + ";", ScriptConfigSetting.PosEffect);
			};

			RecvPacketMethods[0x09cb] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.PACKET_ZC_USE_SKILL_0x09cb>(reader);

				BL mob = Data.Mob(p.srcAID);

				if (mob.IsMob) {
					PacketDecoderHelper.AddMonsterSkill(p.srcAID, p.SKID, -1, ps.CurrentPacket.Time, ps);
				}

				Data.AppendLine(ps, "clif_skill_nodamage: " + p.SKID + ", level = " + p.level + ", sId: " + p.srcAID + ", dId: " + p.targetAID + ";", ScriptConfigSetting.ClifSkillNoDamage);
			};

			RecvPacketMethods[0x011a] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.PACKET_ZC_USE_SKILL_0x011a>(reader);

				BL mob = Data.Mob(p.srcAID);

				if (mob.IsMob) {
					PacketDecoderHelper.AddMonsterSkill(p.srcAID, p.SKID, -1, ps.CurrentPacket.Time, ps);
				}

				Data.AppendLine(ps, "clif_skill_nodamage: " + p.SKID + ", level = " + p.level + ", sId: " + p.srcAID + ", dId: " + p.targetAID + ";", ScriptConfigSetting.ClifSkillNoDamage);
			};

			RecvPacketMethods[0x0a27] = (reader, ps) => {
				int type = reader.Int16();
				int val = reader.Int32();

				if (type == 7) {
					Data.AppendLine(ps, "Skill SP Recovery: " + (Data.CurrentSp + val) + "\tDiff: " + val, ScriptConfigSetting.HpSpRecovery);
					Data.CurrentSp += val;
				}
			};

			RecvPacketMethods[0x013e] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.PACKET_ZC_USESKILL_ACK_0x013e>(reader);

				BL mob = Data.Mob(p.srcId);

				if (mob.IsMob) {
					Data.AppendLine(ps, "Mob casting skill " + p.skillId + " with " + p.delayTime + "ms", ScriptConfigSetting.MobCasting);
					PacketDecoderHelper.AddMonsterCasttime(p.srcId, p.skillId, (int)p.delayTime, ps.CurrentPacket.Time);
				}
			};

			RecvPacketMethods[0x01b9] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.PACKET_ZC_DISPEL_0x01b9>(reader);

				BL mob = Data.Mob(p.srcId);

				if (mob.IsMob) {
					if (Data.Mobs[p.srcId].LatestSkill > 0) {
						int skillId = Data.Mobs[p.srcId].LatestSkill;

						var skill = Data.Mobs[p.srcId].LatestSkillIds[skillId];
						skill.Cancel++;
					}
				}
			};

			RecvPacketMethods[0x0a41] = (reader, ps) => {
				uint sId = reader.UInt32();
				int skill_id = reader.Int16();
				int skill_lv = reader.Int16();

				BL mob = Data.Mob(sId);

				if (mob.IsMob) {
					Data.AppendLine(ps, "Mob used skill " + skill_id + "", ScriptConfigSetting.Generic);
					PacketDecoderHelper.AddMonsterSkillLevel(sId, skill_id, skill_lv);
				}
			};

			RecvPacketMethods[0x02e1] = (reader, ps) => {
				uint sId = reader.UInt32();
				uint dId = reader.UInt32();
				uint tick = reader.UInt32();
				int amotion = reader.Int32();
				int dmotion = reader.Int32();

				BL mob = Data.Mob(sId);

				int damage2 = reader.Int32();

				if (!mob.IsMob) {
					Data.AppendLine(ps, "Received damage: " + damage2 + "; ", ScriptConfigSetting.Generic);
				}

				if (mob.IsMob) {
					PacketDecoderHelper.AddMonsterAMotion(sId, amotion);
					PacketDecoderHelper.AddMonsterTick(sId, tick);
					PacketDecoderHelper.AddMonsterDamage(sId, dId, damage2, ps);
				}

				mob = Data.Mob(dId);

				if (mob.IsMob) {
					PacketDecoderHelper.AddMonsterDMotion(dId, dmotion);
					PacketDecoderHelper.AddMonsterTick(dId, tick);
				}
			};

			RecvPacketMethods[0x0110] = (reader, ps) => {
				// Skill failed
				uint skill_id = reader.UInt16();
				int bType = reader.Int32();
				reader.Byte();
				int reason = reader.Byte();

				Data.AppendLine(ps, "Skill " + skill_id + " has failed: " + reason + ";", ScriptConfigSetting.SkillFailed);
			};

			RecvPacketMethods[0x0acb] = (reader, ps) => {
				// Skill failed
				us = reader.UInt16();
				long value = reader.Int64();

				//Data.AppendLine(ps, "Status updated: " + us + ", value = " + value, ScriptConfigSetting.Generic);

				switch (us) {
					case 1:
					case 2:
						break;
					case 22:
						Data.AppendLine(ps, "Next base level exp required: " + value, ScriptConfigSetting.Generic);
						break;
					case 23:
						Data.AppendLine(ps, "Next job level exp required: " + value, ScriptConfigSetting.Generic);
						break;
					//case 22:
					//	//BL.SetBaseExp(value);
					//	break;
					//case 23:
					//	//BL.SetJobExp(value);
					//	break;
				}
			};

			RecvPacketMethods[0x00b1] = (reader, ps) => {
				us = reader.UInt16();
				int value = reader.Int32();

				switch (us) {
					case 20:
						Data.AppendLine(ps, "Zeny -= " + (Data.LastZeny - value) + "; // Current zeny amount: " + value + " (previous amount: " + Data.LastZeny + ")", ScriptConfigSetting.Generic);
						Data.LastZeny = value;
						break;
				}
			};

			RecvPacketMethods[0x0141] = (reader, ps) => {
				us = (ushort)reader.UInt32();
				int value = reader.Int32();
				int value2 = reader.Int32();

				//Data.AppendLine(ps, "Status updated: " + us + ", value = " + value, ScriptConfigSetting.Generic);

				//switch(us) {
				//	case 13:
				//		Data.AppendLine(ps, "Str: " + value + " + " + value2, ScriptConfigSetting.Generic);
				//		break;
				//	case 14:
				//		Data.AppendLine(ps, "Agi: " + value + " + " + value2, ScriptConfigSetting.Generic);
				//		break;
				//	case 15:
				//		Data.AppendLine(ps, "Vit: " + value + " + " + value2, ScriptConfigSetting.Generic);
				//		break;
				//	case 16:
				//		Data.AppendLine(ps, "Int: " + value + " + " + value2, ScriptConfigSetting.Generic);
				//		break;
				//	case 17:
				//		Data.AppendLine(ps, "Dex: " + value + " + " + value2, ScriptConfigSetting.Generic);
				//		break;
				//	case 18:
				//		Data.AppendLine(ps, "Luk: " + value + " + " + value2, ScriptConfigSetting.Generic);
				//		break;
				//}
			};

			RecvPacketMethods[0x00b0] = (reader, ps) => {
				us = reader.UInt16();
				int value = reader.Int32();

				//Data.AppendLine(ps, "Status updated: " + us + ", value = " + value, ScriptConfigSetting.Generic);

				switch (us) {
					case 25:
						//Data.AppendLine(ps, "Max Weight : " + value / 10, ScriptConfigSetting.Generic);
						break;
					case 52:
						//Data.AppendLine(ps, "Crit changed to: " + value, ScriptConfigSetting.Generic);
						break;
					//case 0:
					//	Data.AppendLine(ps, "Speed changed to: " + value, ScriptConfigSetting.Generic);
					//	break;
					case 5:
						//Data.AppendLine(ps, "HP value: " + value + "\tDiff: " + (value - Data.CurrentHp) + ";", ScriptConfigSetting.Generic);
						Data.CurrentHp = value;
						break;
					case 7:
						//Data.AppendLine(ps, "SP Recovery: " + value + "\tDiff: " + (value - Data.CurrentSp) + ";", ScriptConfigSetting.Generic);
						Data.CurrentSp = value;
						break;
					case 11:
						Data.AppendLine(ps, "Base Level increased to " + value, ScriptConfigSetting.Generic);
						break;
					case 55:
						Data.AppendLine(ps, "Job Level increased to " + value, ScriptConfigSetting.Generic);
						//BL.SetJobExp(value);
						break;
					case 6:
						//Data.AppendLine(ps, "HP value: " + value, ScriptConfigSetting.Generic);
						//BL.HPAll.AppendLine("" + value);
						//BL.SetJobLevel(value);
						break;
					case 232:
						//Data.AppendLine(ps, "AP value: " + value, ScriptConfigSetting.Generic);
						break;
					case 8:
						//Data.AppendLine(ps, "SP value: " + value, ScriptConfigSetting.Generic);
						//BL.SPAll.AppendLine("" + value);
						//BL.SetJobLevel(value);
						break;
				}
			};

			RecvPacketMethods[0x0196] = (reader, ps) => {
				// Skill failed
				int index = reader.UInt16();
				uint uid = reader.UInt32();
				byte state = reader.Byte();

				if (state == 0) {
					Data.AppendLine(ps, "Status ended: " + Constants.GetEFST(index) + ", target = " + uid + ", state = " + state, ScriptConfigSetting.StatusUpdated);
				}
				else {
					Data.AppendLine(ps, "Status updated: " + Constants.GetEFST(index) + ", target = " + uid + ", state = " + state, ScriptConfigSetting.StatusUpdated);
				}
			};

			RecvPacketMethods[0x07fb] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.PACKET_ZC_USESKILL_ACK>(reader);

				Data.AppendLine(ps, String.Format("skill_id: {0}, casttime: {1}, property: {2}", p.skillId, p.delayTime, p.element) + ";", ScriptConfigSetting.SkillId);
			};

			RecvPacketMethods[0x043d] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.PACKET_ZC_SKILL_POSTDELAY>(reader);

				Data.AppendLine(ps, String.Format("skill_id: {0}, COOLDOWN: {1}", p.SKID, p.DelayTM) + ";", ScriptConfigSetting.SkillId);
			};

			RecvPacketMethods[0x08c8] = (reader, ps) => {
				uint sId = reader.UInt32();
				uint dId = reader.UInt32();
				uint tick = reader.UInt32();
				int amotion = reader.Int32();
				int dmotion = reader.Int32();
				int damage = reader.Int32();
				reader.Byte();
				reader.Int16();
				byte type = reader.Byte();

				BL mob = Data.Mob(sId);

				if (!mob.IsMob) {
					Data.AppendLine(ps, "Received damage: " + damage + ", " + amotion + ";", ScriptConfigSetting.Generic);
				}

				if (mob.IsMob) {
					PacketDecoderHelper.AddMonsterAMotion(sId, amotion);
					PacketDecoderHelper.AddMonsterTick(sId, tick);
					PacketDecoderHelper.AddMonsterDamage(sId, dId, damage, ps);
				}

				mob = Data.Mob(dId);

				if (mob.IsMob) {
					PacketDecoderHelper.AddMonsterDMotion(dId, dmotion);
					PacketDecoderHelper.AddMonsterTick(dId, tick);
					// Do not log damage here
					//PacketDecoderHelper.AddMonsterDamage(dId, sId, damage, ps);
				}
			};

			RecvPacketMethods[0x01d0] = (reader, ps) => {
				reader.Int32();
				int size = reader.Int16();

				Data.AppendLine(ps, "spirit ball: " + size + ";", ScriptConfigSetting.SpiritBall);
			};

			RecvPacketMethods[0x0192] = (reader, ps) => {
				int x = reader.Int16();
				int y = reader.Int16();
				int type = reader.Int16();
				name = reader.String(16);

				Data.AppendLine(ps, "setcell " + x + ", " + y + ", " + type + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x029b] = (reader, ps) => {
				uint uid = reader.UInt32();
				int atk = reader.Int16();
				int matk = reader.Int16();
				int hit = reader.Int16();
				int cri = reader.Int16();
				int def = reader.Int16();
				int mdef = reader.Int16();
				int flee = reader.Int16();
				int amotion = reader.Int16();
				name = reader.String(24, '\0');
				int lv = reader.Int16();
				int hp = reader.Int32();
				int hp_max = reader.Int32();
				int sp = reader.Int32();
				int sp_max = reader.Int32();
				int lifetime = reader.Int32();
				int faith = reader.Int16();
				int call_count = reader.Int32();
				int kill_count = reader.Int32();
				int range = reader.Int16();

				Data.AppendLine(ps, "MERCENARY uid = " + uid, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY atk = " + atk, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY matk = " + matk, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY hit = " + hit, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY cri = " + cri, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY def = " + def, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY mdef = " + mdef, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY flee = " + flee, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY amotion = " + amotion, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY name = " + name, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY lv = " + lv, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY hp = " + hp, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY hp_max = " + hp_max, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY sp = " + sp, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY sp_max = " + sp_max, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY lifetime = " + lifetime, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY faith = " + faith, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY call_count = " + call_count, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY kill_count = " + kill_count, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "MERCENARY range = " + range, ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0add] = (reader, ps) => {
				int id = reader.Int32();
				int nameid = reader.Int32();

				reader.Position = 13;
				int x = reader.UInt16();
				int y = reader.UInt16();

				Data.AppendLine(ps, "makeitem " + nameid + ", 1, \"" + Data.LastMap + "\"" + ", " + x + ", " + y + ";", ScriptConfigSetting.Generic);

				if (Data.LastDead > 0) {
					if (ps.Delay - Data.LastDeadTimer > 300)
						return;

					PacketDecoderHelper.AddMonsterDrop(id, Data.LastDead, Data.LastDeadTimer, nameid);
				}
			};

			RecvPacketMethods[0x009e] = (reader, ps) => {
				reader.Int32();
				int nameid = reader.UInt16();
				reader.Byte();
				int x = reader.UInt16();
				int y = reader.UInt16();
				Data.AppendLine(ps, "makeitem " + nameid + ", 1, \"" + Data.LastMap + "\"" + ", " + x + ", " + y + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x07fa] = (reader, ps) => {
				ushort reason = reader.UInt16();
				ushort index = reader.UInt16();
				ushort amount = reader.UInt16();

				if (index < BL.Items.Length && BL.Items[index - 2] != null && BL.Items[index - 2].Nameid != 0) {
					Data.AppendLine(ps, "delitem " + BL.Items[index - 2].Nameid + ", " + amount + ";", ScriptConfigSetting.Generic);
				}
				else {
					Data.AppendLine(ps, "delitem [POSITION=" + index + "], " + amount + ";", ScriptConfigSetting.Generic);
				}
			};

			RecvPacketMethods[0x0229] = (reader, ps) => {
				uint uid = reader.UInt32();
				ushort bodyState = reader.UInt16();
				ushort healthState = reader.UInt16();
				int effectState = reader.Int32();
				byte mode = reader.Byte();

				if (effectState == 2) {
					Data.AppendLine(ps, "hideonnpc \"" + Data.GetName(uid) + "\";", ScriptConfigSetting.Generic);
					Data.Bl(uid).IsHidden = true;
				}
				else if (effectState == 4) {
					Data.AppendLine(ps, "cloakonnpcself \"" + Data.GetName(uid) + "\";", ScriptConfigSetting.Generic);
					Data.Bl(uid).IsHidden = true;
				}
				else if (effectState == 0) {
					if (Data.Bl(uid).EffectState == 4) {
						Data.AppendLine(ps, "cloakoffnpcself \"" + Data.GetName(uid) + "\";", ScriptConfigSetting.Generic);
					}
					else {
						Data.AppendLine(ps, "hideoffnpc \"" + Data.GetName(uid) + "\";", ScriptConfigSetting.Generic);
					}
				}
				else {
					Data.AppendLine(ps, String.Format("changeoption \"{0}\", \"{1}\", \"{2}\", \"{3}\";", bodyState, healthState, effectState, uid), ScriptConfigSetting.Generic);
				}

				Data.Bl(uid).EffectState = effectState;
			};

			RecvPacketMethods[0x0b8d] = (reader, ps) => {
				reader.Position = 4;
				int result = reader.Byte();

				if (result == 0)
					return;

				while (reader.CanRead) {
					int type = (int)reader.Int64();
					int points = (int)reader.Int64();

					Data.AppendLine(ps, "REPUTATION: " + type + " - " + points, ScriptConfigSetting.Generic);
				}
			};

			RecvPacketMethods[0x018c] = (reader, ps) => {
				int class_ = reader.Int16();
				int lv = reader.Int16();
				int size = reader.Int16();
				int hp = reader.Int32();
				int def = reader.Int16();
				int race = reader.Int16();
				int mdef = reader.Int16();
				int ele = reader.Int16();

				Data.AppendLine(ps, "SENSE Class = " + class_ + ";", ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "SENSE lv = " + lv + ";", ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "SENSE size = " + size + ";", ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "SENSE hp = " + hp + ";", ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "SENSE def = " + def + ";", ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "SENSE race = " + race + ";", ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "SENSE mdef = " + mdef + ";", ScriptConfigSetting.Generic);
				Data.AppendLine(ps, "SENSE def_ele = " + ele + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x01b3] = (reader, ps) => {
				name = reader.String(64, '\0');
				int id = reader.Byte();

				if (id == 255) {
					Data.AppendLine(ps, "cutin \"\", " + id + ";", ScriptConfigSetting.Generic);
					return;
				}

				mes = "cutin \"" + name + "\", " + id + ";";
				Data.AppendLine(ps, mes, ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x02f0] = (reader, ps) => {
				reader.Int32();
				int ztime = reader.Int32();

				Data.AppendLine(ps, "progressbar \"0x00ff00\", " + ztime + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x09d1] = (reader, ps) => {
				uint uid = reader.UInt32();
				reader.Int32();
				int ztime = reader.Int32();

				Data.AppendLine(ps, "progressbar \"0x00ff00\", " + ztime + ", \"" + Data.GetName(uid) + "\";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0080] = (reader, ps) => {
				Data.LastDead = reader.UInt32();
				Data.LastDeadTimer = ps.Delay;
				int type = reader.Byte();

				if (type == 1) {
					if (Data.LastDead > 10000000) {
						Data.AppendLine(ps, "Player died.", ScriptConfigSetting.Generic);
					}

					if (Data.Mobs.ContainsKey(Data.LastDead)) {
						Data.AppendLine(ps, "Unit dead: " + Data.Mobs[Data.LastDead].Name + ", class: " + Data.Mobs[Data.LastDead].View + " - " + Data.LastDead + ";", ScriptConfigSetting.Generic);
						PacketDecoderHelper.AddMonsterDead(Data.Mobs[Data.LastDead].View);
					}
					else {
						Data.AppendLine(ps, "Unit dead: " + Data.GetName(Data.LastDead) + ", class: " + Data.GetClass(Data.LastDead) + " - " + Data.LastDead + ";", ScriptConfigSetting.Generic);
					}
				}
			};

			RecvPacketMethods[0x0ae2] = (reader, ps) => {
				int type = reader.Byte();
				int value = reader.Int32();

				Data.AppendLine(ps, "uiopen " + type + ", " + value + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0b41] = (reader, ps) => {
				int pos = reader.Int16();
				int amount = reader.Int16();
				int nameid = reader.Int32();

				Data.AppendLine(ps, "getitem " + nameid + ", " + amount + "; // " + pos + " - ", ScriptConfigSetting.Generic);
				BL.AddItem(pos - 2, nameid, amount);
			};

			RecvPacketMethods[0x0a37] = (reader, ps) => {
				int pos = reader.Int16();
				int amount = reader.Int16();
				int nameid = reader.Int32();

				Data.AppendLine(ps, "getitem " + nameid + ", " + amount + "; // " + pos + " - ", ScriptConfigSetting.Generic);
				BL.AddItem(pos - 2, nameid, amount);
			};

			RecvPacketMethods[0x0a0c] = (reader, ps) => {
				int pos = reader.Int16();
				int amount = reader.Int16();
				int nameid = reader.Int16();

				Data.AppendLine(ps, "getitem " + nameid + ", " + amount + "; // " + pos + " - ", ScriptConfigSetting.Generic);
				BL.AddItem(pos - 2, nameid, amount);
			};

			RecvPacketMethods[0x07f6] = (reader, ps) => {
				uint id = reader.UInt32();
				uint exp = reader.UInt32();
				int type = reader.UInt16();
				int isQuest = reader.UInt16();

				if (isQuest == 1) {
					if (type == 1) {
						Data.AppendLine(ps, "getexp " + exp + ", 0;", ScriptConfigSetting.Generic);
					}
					else if (type == 2) {
						Data.AppendLine(ps, "getexp 0, " + exp + ";", ScriptConfigSetting.Generic);
					}
				}
				else if (isQuest == 0) {
					if (type == 1) {
						Data.AppendLine(ps, "getexp " + exp + ", 0;", ScriptConfigSetting.Generic);
					}
					else if (type == 2) {
						Data.AppendLine(ps, "getexp 0, " + exp + ";", ScriptConfigSetting.Generic);
					}
				}
			};

			RecvPacketMethods[0x0acc] = (reader, ps) => {
				uint id = reader.UInt32();
				uint exp = (uint)reader.UInt64();
				int type = reader.UInt16();
				int isQuest = reader.UInt16();

				if (isQuest == 1) {
					if (type == 1) {
						Data.AppendLine(ps, "getexp " + exp + ", 0;", ScriptConfigSetting.Generic);
					}
					else if (type == 2) {
						Data.AppendLine(ps, "getexp 0, " + exp + ";", ScriptConfigSetting.Generic);
					}
				}
				else if (isQuest == 0) {
					if (type == 1) {
						Data.AppendLine(ps, "getexp " + exp + ", 0;", ScriptConfigSetting.Generic);
					}
					else if (type == 2) {
						Data.AppendLine(ps, "getexp 0, " + exp + ";", ScriptConfigSetting.Generic);
					}
				}
			};

			RecvPacketMethods[0x008e] = (reader, ps) => {
				us = reader.UInt16();
				mes = reader.String(us - reader.Position, '\0');
				Data.AppendLine(ps, "npctalk \"" + mes + "\", getcharid(3);", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0109] = (reader, ps) => {
				us = reader.UInt16();
				uint id = reader.UInt32();
				mes = reader.String(us - reader.Position, '\0');

				Data.AppendLine(ps, "npctalk \"" + mes + "\", \"" + Data.GetName(id) + "\";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x008d] = (reader, ps) => {
				us = reader.UInt16();
				uint id = reader.UInt32();
				mes = reader.String(us - reader.Position, '\0');

				Data.AppendLine(ps, "npctalk \"" + mes + "\", \"" + Data.GetName(id) + "\";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x08b3] = (reader, ps) => {
				us = reader.UInt16();
				uint id = reader.UInt32();
				mes = reader.String(us - reader.Position, '\0');

				Data.AppendLine(ps, "showscript " + mes + ", \"" + Data.Bl(id).Name + "\";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x00c0] = (reader, ps) => {
				uint id = reader.UInt32();
				byte effect = reader.Byte();
				Data.AppendLine(ps, "emotion " + Constants.Emotions[effect] + ", 0, \"" + Data.GetName(id) + "\";", ScriptConfigSetting.Generic);

				if (Data.Mobs.ContainsKey(id)) {
					if (Data.Mobs[id].LatestSkill != 0 && Data.Mobs[id].LatestSkillIds.ContainsKey(Data.Mobs[id].LatestSkill)) {
						Data.Mobs[id].LatestSkillIds[Data.Mobs[id].LatestSkill].Emote = effect;
					}
				}
			};

			RecvPacketMethods[0x090f] = (reader, ps) => {
				us = reader.UInt16();
				reader.Forward(1);
				uint id = reader.UInt32();

				reader.Position = 19;
				int class_ = reader.UInt16();

				reader.Position = 55;
				byte[] data = reader.Bytes(3);
				int x = (data[0] << 2) | ((data[1] & 0xC0) >> 6);
				int y = ((data[1] & 0x3f) << 4) | ((data[2] & 0xF0) >> 4);
				int dir = (data[2] & 0xF);

				reader.Position = 73;
				name = reader.String(us - reader.Position);

				PacketDecoderHelper.AddMonster(ps, id, class_, x, y, dir, name, -1);
			};

			RecvPacketMethods[0x09fe] = (reader, ps) => {
				unsafe {
					var p = PacketStructures.Assign<PacketStructures.packet_idle_unit_spawn>(reader);

					reader.Position = Marshal.SizeOf(typeof(PacketStructures.packet_idle_unit_spawn));
					name = reader.String(reader.Length - reader.Position);

					int class_ = p.job;

					int x = (p.PosDir[0] << 2) | ((p.PosDir[1] & 0xC0) >> 6);
					int y = ((p.PosDir[1] & 0x3f) << 4) | ((p.PosDir[2] & 0xF0) >> 4);
					int dir = (p.PosDir[2] & 0xF);

					if (p.objecttype == 9) {
						Data.AppendLine(ps, "Mercenary spawned: \"" + Data.LastMap + "\", " + x + ", " + y + ", \"" + name + "\", " + class_, ScriptConfigSetting.Generic);
						return;
					}

					PacketDecoderHelper.AddMonster(ps, p.AID, class_, x, y, dir, name, p.isBoss);
				}
			};

			RecvPacketMethods[0x0915] = (reader, ps) => {
				us = reader.UInt16();
				short type = reader.Byte();
				uint id = reader.UInt32();
				reader.Forward(46);

				byte[] data = reader.Bytes(3);
				int x = (data[0] << 2) | ((data[1] & 0xC0) >> 6);
				int y = ((data[1] & 0x3f) << 4) | ((data[2] & 0xF0) >> 4);
				int dir = (data[2] & 0xF);

				reader.Forward(-39);
				int class_ = reader.UInt16();
				reader.Forward(39 - 2);

				reader.Forward(6 + 10);
				name = reader.String(us - reader.Position);

				switch (type) {
					case 0x8:
						break;
					case 0x0:
					case 0x6:
						break;
					case 0x5:
						PacketDecoderHelper.AddMonster(ps, id, class_, x, y, dir, name, -1);
						return;
				}

				Data.Bl(id);
				Data.Bls[id].Name = name ?? "";

				if (Data.Bls[id].X == 0) {
					Data.Bls[id].X = x;
					Data.Bls[id].Y = y;
					Data.Bls[id].Dir = dir;
					Data.Bls[id].View = class_;
				}

				Data.Bls[id].Type = type;
				Data.AppendLine(ps, Constants.GetSeeObjectType(type) + Data.Bls[id].Name, ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0446] = (reader, ps) => {
				uint id = reader.UInt32();
				int x = reader.Int16();
				int y = reader.Int16();
				int effect = reader.Int16();
				int type = reader.Int16();

				string effectS = effect + "";

				switch (effect) {
					case 9999: effectS = "QTYPE_NONE"; break;
					case 0: effectS = "QTYPE_QUEST"; break;
					case 1: effectS = "QTYPE_QUEST2"; break;
					case 6: effectS = "QTYPE_CLICKME"; break;
					case 7: effectS = "QTYPE_DAILYQUEST"; break;
				}

				Data.AppendLine(ps, "showevent " + effectS + ", " + type + ", getcharid(0), \"" + Data.GetName(id) + "\"; // " + x + "," + y, ScriptConfigSetting.Showevent);
			};

			RecvPacketMethods[0x0adf] = (reader, ps) => {
				uint id = reader.UInt32();
				reader.Int32();

				name = reader.String(24, '\0');
				string posname = reader.String(24, '\0');

				if (posname == "")
					return;

				Data.Bl(id);
				Data.Bls[id].Name = name ?? "";
				Data.AppendLine(ps, "setposname \"" + name + "\", \"" + posname + "\";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x09ff] = (reader, ps) => {
				unsafe {
					var p = PacketStructures.Assign<PacketStructures.packet_idle_unit>(reader);
					string custom = "";

					reader.Position = Marshal.SizeOf(typeof(PacketStructures.packet_idle_unit));
					name = reader.String(reader.Length - reader.Position);

					if ((p.job < 30 || (p.job >= 4000 && p.job < 5000)) && p.objecttype == 0x6) {
						string t =
							p.job + "," +
							p.sex + "," +
							p.head + "," +
							p.headpalette + "," +
							p.weapon + "," +
							p.shield + "," +
							p.accessory + "," +
							p.accessory2 + "," +
							p.accessory3 + "," +
							"0," +
							p.bodypalette + "";

						custom = t;
						Data.AppendLine(ps, "CUSTOM_DEFINED_NPC id = " + p.AID + ", npc_avail: " + t, ScriptConfigSetting.Generic);
					}

					int x = (p.PosDir[0] << 2) | ((p.PosDir[1] & 0xC0) >> 6);
					int y = ((p.PosDir[1] & 0x3f) << 4) | ((p.PosDir[2] & 0xF0) >> 4);
					int dir = (p.PosDir[2] & 0xF);

					switch (p.objecttype) {
						case 0x8:
							break;
						case 0x0:
						case 0x6:
							break;
						case 0x5:
							PacketDecoderHelper.AddMonster(ps, p.AID, p.job, x, y, dir, name, p.isBoss);
							return;
					}

					Data.Bl(p.AID);
					Data.Bls[p.AID].Name = name ?? "";

					if (Data.Bls[p.AID].X == 0) {
						Data.Bls[p.AID].X = x;
						Data.Bls[p.AID].Y = y;
						Data.Bls[p.AID].Dir = dir;
						Data.Bls[p.AID].View = p.job;
						Data.Bls[p.AID].CustomDefined = custom;
					}

					Data.Bls[p.AID].Type = p.objecttype;
					Data.AppendLine(ps, Constants.GetSeeObjectType(p.objecttype) + Data.Bls[p.AID].Name + " - " + Constants.GetEffectState(p.effectState) + ", x = " + x + ", y = " + y, ScriptConfigSetting.Generic);
					Data.Bls[p.AID].EffectState = p.effectState;
				}
			};

			RecvPacketMethods[0x0858] = (reader, ps) => {
				us = reader.UInt16();
				reader.Forward(1);
				uint id = reader.UInt32();
				reader.Forward(46);
				byte[] data = reader.Bytes(3);

				int x = (data[0] << 2) | ((data[1] & 0xC0) >> 6);
				int y = ((data[1] & 0x3f) << 4) | ((data[2] & 0xF0) >> 4);
				int dir = (data[2] & 0xF);

				reader.Forward(-39);
				int class_ = reader.UInt16();
				reader.Forward(39 - 2);

				Data.Bl(id);
				Data.Bls[id].X = x;
				Data.Bls[id].Y = y;
				Data.Bls[id].Dir = dir;
				Data.Bls[id].View = class_;

				reader.Forward(6);
				name = reader.String(us - reader.Position);

				Data.Bls[id].Name = name;
			};

			RecvPacketMethods[0x0977] = (reader, ps) => {
				uint uid = reader.UInt32();
				int hp = reader.Int32();
				int maxHp = reader.Int32();

				PacketDecoderHelper.AddMonsterHp(uid, maxHp);
				Data.AppendLine(ps, "Monster \"" + Data.GetName(uid) + "\" now at " + hp + " / " + maxHp, ScriptConfigSetting.MonsterHp);
			};

			RecvPacketMethods[0x01b0] = (reader, ps) => {
				uint uid = reader.UInt32();
				byte type = reader.Byte();
				int newClass = reader.Int32();

				Data.AppendLine(ps, "classchange_pc " + newClass + ", " + type + ", getnpcid(0, \"" + Data.GetName(uid) + "\");", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x08fe] = (reader, ps) => {
				reader.Int16();
				int i = 0;

				while (reader.CanRead) {
					int quest_id = reader.Int32();
					int mob_id = reader.Int32();
					int mob_count = reader.Int16();
					int mob_current = reader.Int16();

					Data.AppendLine(ps, String.Format("Quest updated: {0}, {1}, {2} / {3}", quest_id, mob_id, mob_current, mob_count), ScriptConfigSetting.Generic);
					i++;
				}
			};

			RecvPacketMethods[0x0ac7] = (reader, ps) => {
				name = reader.String(16, '\0');
				int x = reader.UInt16();
				int y = reader.UInt16();
				Data.LastMap = name.Replace(".gat", "");
				Data.AppendLine(ps, "warp \"" + Data.LastMap + "\", " + x + ", " + y + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0144] = (reader, ps) => {
				uint id = reader.UInt32();
				int type = reader.Int32();
				int x = reader.Int32();
				int y = reader.Int32();
				int pointerId = reader.Byte();
				int color = reader.Int32();

				Data.AppendLine(ps, String.Format("viewpoint {0}, {1}, {2}, {3}, 0x{4:x6};", type, x, y, pointerId, color), ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0b0c] = (reader, ps) => {
				int quest_id = reader.Int32();
				byte state = reader.Byte();
				uint qiTime = reader.UInt32();
				uint qdTime = reader.UInt32();
				int obj_count = reader.Int16();
				uint duration = qdTime - qiTime;

				Data.AppendLine(ps, "setquest " + quest_id + "; // State = " + state + ", Time = " + duration, ScriptConfigSetting.Generic);

				for (int i = 0; i < obj_count; i++) {
					reader.Int32();
					reader.Int32();
					//reader.Int16();
					int mob_type = reader.Int32();
					int mob_id = reader.Int32();
					int min = reader.Int16();
					int max = reader.Int16();
					int mob_count = reader.Int16();
					name = reader.String(24, '\0');

					Data.AppendLine(ps, String.Format("Objective added: {0}, {1}, {2} / {3} / {4} / type: {7} / min: {5} / max: {6}", quest_id, mob_id, 0, mob_count, name, min, max, mob_type), ScriptConfigSetting.Generic);
				}
			};

			RecvPacketMethods[0x09f9] = RecvPacketMethods[0x02b3] = (reader, ps) => {
				uint id = reader.UInt32();
				byte state = reader.Byte();
				uint qiTime = reader.UInt32();
				uint qdTime = reader.UInt32();
				us = reader.UInt16();
				uint duration = qdTime - qiTime;

				reader.Forward(90);

				reader.Position = 0;

				StringBuilder g = new StringBuilder();

				for (int i = 0; i < reader.Length; i++) {
					g.Append(reader.Byte().ToString("X2"));
				}

				Data.AppendLine(ps, "setquest " + id + "; // State = " + state + ", Time = " + duration, ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x02b4] = (reader, ps) => {
				uint id = reader.UInt32();
				Data.AppendLine(ps, "completequest " + id + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x02b7] = (reader, ps) => {
				//uint id = reader.UInt32();
				//byte active = reader.Byte();
				//Data.AppendLine(ps, "hidequest " + id + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x00c6] = (reader, ps) => {
				us = reader.UInt16();
				int total = (us - 4) / 11;

				StringBuilder b = new StringBuilder();
				b.Append("-	shop	UNKNOWN	-1,");

				while (reader.CanRead) {
					int value = reader.Int32();
					int discount = reader.Int32();
					reader.Byte();
					uint nameid = reader.UInt32();

					b.Append(nameid + ":" + value + ",");
				}

				Data.AppendLine(ps, b.ToString(), ScriptConfigSetting.GenericNoTime);
			};

			RecvPacketMethods[0x09d5] = (reader, ps) => {
				us = reader.UInt16();
				int total = (us - 4) / 11;

				StringBuilder b = new StringBuilder();
				b.Append("-	marketshop	UNKNOWN_MARKET_SHOP	-1,");

				while (reader.CanRead) {
					uint nameid = reader.UInt32();
					reader.Byte();
					int value = reader.Int32();
					int quantity = reader.Int32();
					reader.Int16();

					b.Append(nameid + ":" + value + ":" + quantity + ",");
				}

				Data.AppendLine(ps, b.ToString(), ScriptConfigSetting.GenericNoTime);
			};

			RecvPacketMethods[0x0b56] = (reader, ps) => {
				us = reader.UInt16();

				reader.Position = 8;

				Data.AppendLine(ps, "barter_clear \"#UNKNOWN_BARTER_SHOP\";", ScriptConfigSetting.GenericNoTime);

				while (reader.CanRead) {
					uint nameid = reader.UInt32();
					reader.UInt16();
					reader.Int32();
					reader.Int32();
					reader.Int32();
					int zeny = reader.Int32();
					int items_count = reader.Int32();

					StringBuilder b = new StringBuilder();
					b.Append(String.Format("barter_add_ex \"#UNKNOWN_BARTER_SHOP\", {0}, 1, {1}", nameid, zeny));

					for (int i = 0; i < items_count; i++) {
						int sub_nameid = reader.Int32();
						int sub_refine = reader.Int16();
						int sub_amount = reader.Int32();
						reader.UInt16();
						b.Append(String.Format(", {0}, {1}, {2}", sub_nameid, sub_amount, sub_refine));
					}

					Data.AppendLine(ps, b.ToString() + ";", ScriptConfigSetting.GenericNoTime);
				}
			};

			RecvPacketMethods[0x0b79] = (reader, ps) => {
				us = reader.UInt16();

				reader.Position = 8;

				Data.AppendLine(ps, "barter_clear \"#UNKNOWN_BARTER_SHOP\";", ScriptConfigSetting.GenericNoTime);

				while (reader.CanRead) {
					uint nameid = reader.UInt32();
					reader.UInt16();
					reader.Int32();
					reader.Int32();
					reader.Int32();
					int zeny = reader.Int32();
					reader.Int16();	// unknown
					reader.Int32();	// unknown
					int items_count = reader.Int32();

					StringBuilder b = new StringBuilder();
					b.Append(String.Format("barter_add_ex \"#UNKNOWN_BARTER_SHOP\", {0}, 1, {1}", nameid, zeny));

					for (int i = 0; i < items_count; i++) {
						int sub_nameid = reader.Int32();
						int sub_refine = reader.Int16();
						int sub_amount = reader.Int32();
						reader.UInt16();
						b.Append(String.Format(", {0}, {1}, {2}", sub_nameid, sub_amount, sub_refine));
					}

					Data.AppendLine(ps, b.ToString() + ";", ScriptConfigSetting.GenericNoTime);
				}
			};

			RecvPacketMethods[0x0b78] = (reader, ps) => {
				us = reader.UInt16();

				StringBuilder b = new StringBuilder();
				b.Append("-	shop	UNKNOWN_BARTER_SHOP	-1,");

				while (reader.CanRead) {
					uint nameid = reader.UInt32();
					reader.Byte();
					reader.Int32();
					int cost_id = reader.Int32();
					int quantity = reader.Int32();
					reader.Int32();
					reader.Int32();
					reader.Forward(6);

					b.Append(nameid + ":" + nameid + ":" + cost_id + ":" + quantity + ",");
				}

				Data.AppendLine(ps, b.ToString(), ScriptConfigSetting.GenericNoTime);

				reader.Position = 4;

				Data.AppendLine(ps, "barter_clear \"#UNKNOWN_BARTER_SHOP\";", ScriptConfigSetting.GenericNoTime);

				while (reader.CanRead) {
					uint nameid = reader.UInt32();
					reader.Byte();
					reader.Int32();
					int cost_id = reader.Int32();
					int quantity = reader.Int32();
					reader.Int32();
					reader.Int32();
					reader.Forward(6);

					Data.AppendLine(ps, String.Format("barter_add \"#UNKNOWN_TRADE_SHOP\", {0}, 1, {1}, {2};", nameid, cost_id, quantity), ScriptConfigSetting.GenericNoTime);
				}
			};

			RecvPacketMethods[0x0b0e] = (reader, ps) => {
				us = reader.UInt16();

				StringBuilder b = new StringBuilder();
				b.Append("-	shop	UNKNOWN_TRADE_SHOP	-1,");

				while (reader.CanRead) {
					uint nameid = reader.UInt32();
					reader.Byte();
					reader.Int32();
					int cost_id = reader.Int32();
					int quantity = reader.Int32();
					reader.Int32();
					reader.Int32();

					b.Append(nameid + ":" + nameid + ":" + cost_id + ":" + quantity + ",");
				}

				Data.AppendLine(ps, b.ToString(), ScriptConfigSetting.GenericNoTime);

				reader.Position = 4;

				Data.AppendLine(ps, "barter_clear \"#UNKNOWN_BARTER_SHOP\";", ScriptConfigSetting.GenericNoTime);

				while (reader.CanRead) {
					uint nameid = reader.UInt32();
					reader.Byte();
					reader.Int32();
					int cost_id = reader.Int32();
					int quantity = reader.Int32();
					reader.Int32();
					reader.Int32();

					Data.AppendLine(ps, String.Format("barter_add \"#UNKNOWN_TRADE_SHOP\", {0}, 1, {1}, {2};", nameid, cost_id, quantity), ScriptConfigSetting.GenericNoTime);
				}
			};

			RecvPacketMethods[0xb09] = (reader, ps) => {
				try {
					// Map warps, compress items because the indexes don't work anymore
					BL.Items = new Item[200];

					us = reader.UInt16();
					int type = reader.Byte();

					while (reader.CanRead) {
						int idx = reader.UInt16();
						int nameid = reader.Int32();
						reader.Byte();
						int amount = reader.Int32();
						int refine = reader.Byte();
						int[] cards = reader.ArrayInt32(4);
						reader.Forward(34 - (2 + 4 + 4 + 1 + 1 + 4 * 4));

						Data.AppendLine(ps, String.Format("Misc: {1} x{0}  -  {2} |\t@item {1} {0}", amount, nameid, idx), ScriptConfigSetting.GenericNoTime);
						BL.AddItem(idx - 2, nameid, amount);
						// rand options
					}
				}
				catch (Exception err) {
					Z.F(err);
				}
			};

			RecvPacketMethods[0xb39] = (reader, ps) => {
				try {
					us = reader.UInt16();
					int type = reader.Byte();

					while (reader.CanRead) {
						int pos = reader.Position;

						int idx = reader.UInt16();
						int nameid = reader.Int32();
						int wtype = reader.Byte();
						int equip = reader.Int32();
						reader.Int32();	// unknown
						//int refine = reader.Byte();
						int[] cards = reader.ArrayInt32(4);

						reader.Position = pos + 65;
						int refine = reader.Byte();
						reader.Position = pos + 68;

						string equipped = equip > 0 ? "EQ: " : "";

						Data.AppendLine(ps, String.Format(equipped + "+{0} {1} [{2} {3} {4} {5}]  -  {6} | @item2 {7} 1 1 {8} 0 {9} {10} {11} {12}", refine, nameid, cards[0], cards[1], cards[2], cards[3], idx, nameid, refine, cards[0], cards[1], cards[2], cards[3]), ScriptConfigSetting.Generic);
						BL.AddItem(idx - 2, nameid, 1, refine, cards[0], cards[1], cards[2], cards[3]);
					}
				}
				catch (Exception err) {
					Z.F(err);
				}
			};

			RecvPacketMethods[0x0991] = (reader, ps) => {
				for (int i = 0; i < 200; i++) {
					BL.Items[i] = new Item();
				}

				reader.UInt16();

				while (reader.CanRead) {
					try {

						ushort index = reader.UInt16();
						ushort nameid = reader.UInt16();
						if (index - 2 >= 200)
							return;
						BL.Items[index - 2].Nameid = nameid;
						reader.Forward(20);
					}
					catch (Exception err) {
						Z.F(err);
					}
				}
			};

			RecvPacketMethods[0x0aff] = (reader, ps) => {
				us = reader.UInt16();

				int count = reader.Int32();

				for (int k = 0; k < count; k++) {
					PacketStructures.QuestItem quest = new PacketStructures.QuestItem();
					quest.QuestID = reader.Int32();
					quest.Active = reader.Byte();
					reader.Int32();
					reader.Int32();
					quest.hunting_count = reader.Int16();

					if (quest.hunting_count > 0) {
						Data.AppendLine(ps, "Hunting Quest: " + quest.QuestID, ScriptConfigSetting.Generic);

						quest.Hunts = new PacketStructures.QuestItemHunt[quest.hunting_count];

						for (int l = 0; l < quest.hunting_count; l++) {
							quest.Hunts[l] = PacketStructures.Assign2<PacketStructures.QuestItemHunt>(reader);
							Data.AppendLine(ps, "    Mob ID: " + quest.Hunts[l].mob_id + ", " + quest.Hunts[l].huntCount + " / " + quest.Hunts[l].maxCount, ScriptConfigSetting.Generic);
						}
					}
				}
			};

			RecvPacketMethods[0x043f] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.packet_status_change2>(reader);

				Data.AppendLine(ps, "sc_start4 " + Constants.GetEFST(p.index) + ", " + p.Left + ", " + p.val1 + ", " + p.val2 + ", " + p.val3 + ", 0, 10000, 1; // id = " + p.AID + "; ", ScriptConfigSetting.ScStart);
			};

			RecvPacketMethods[0x0983] = (reader, ps) => {
				var p = PacketStructures.Assign<PacketStructures.packet_status_change>(reader);

				if (p.index == 46) {
					Data.AppendLine(ps, "AFTER-CAST delay: " + p.Left + "; ", ScriptConfigSetting.AfterCast);
				}

				Data.AppendLine(ps, "sc_start4 " + Constants.GetEFST(p.index) + ", " + p.Left + ", " + p.val1 + ", " + p.val2 + ", " + p.val3 + ", 0, 10000, 1; // id = " + p.AID + ", total = " + p.Total + "; ", ScriptConfigSetting.ScStart);
			};

			RecvPacketMethods[0x00b4] = (reader, ps) => {
				us = reader.UInt16();
				uint id = reader.UInt32();
				mes = reader.String(us - reader.Position, '\0');
				Data.AppendLine(ps, "mes \"" + mes + "\";", ScriptConfigSetting.Mes);
			};

			RecvPacketMethods[0x0b0d] = (reader, ps) => {
				uint id = reader.UInt32();
				int effect = reader.Int32();

				Data.AppendLine(ps, "removeeffect " + effect + ", AREA, \"" + Data.GetName(id) + "\";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x00b5] = (reader, ps) => {
				uint id = reader.UInt32();
				Data.AppendLine(ps, "next;", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x00b6] = (reader, ps) => {
				uint id = reader.UInt32();
				Data.AppendLine(ps, "close;", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x00b7] = (reader, ps) => {
				us = reader.UInt16();
				uint id = reader.UInt32();
				Data.AppendLine(ps, "select(\"" + reader.String(us - reader.Position, '\0') + "\");", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0091] = (reader, ps) => {
				name = reader.String(16, '\0');
				int x = reader.UInt16();
				int y = reader.UInt16();
				Data.LastMap = name.Replace(".gat", "");
				Data.AppendLine(ps, "warp \"" + Data.LastMap + "\", " + x + ", " + y + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0086] = (reader, ps) => {

			};

			RecvPacketMethods[0x0152] = (reader, ps) => {
				us = reader.UInt16();
				reader.Forward(8);
				reader.Forward(us - reader.Position);
			};

			RecvPacketMethods[0x02cb] = (reader, ps) => {
				name = reader.String(61, '\0');
				us = reader.UInt16();
				Data.AppendLine(ps, "// instance created: \"" + name + "\"", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x02cd] = (reader, ps) => {
				name = reader.String(61, '\0');
				int limit1 = reader.Int32();
				int limit2 = reader.Int32();

				int totalTimer = Math.Abs(limit2 - limit1);
				Data.AppendLine(ps, "// instance timer: \"" + name + "\", total time = " + totalTimer + " seconds | " + (totalTimer / 60) + " minutes", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x02cc] = (reader, ps) => {
				us = reader.UInt16();
				Data.AppendLine(ps, "// instance timer added", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0092] = (reader, ps) => {
				name = reader.String(16, '\0');
				Data.LastMap = name.Replace(".gat", "");
			};

			RecvPacketMethods[0x01c3] = (reader, ps) => {
				us = reader.UInt16();
				int color = reader.Int32();
				int fontType = reader.UInt16();
				int fontSize = reader.UInt16();
				int fontAlign = reader.UInt16();
				int fontY = reader.UInt16();
				string message = reader.String(us - reader.Position, '\0');

				mes = "mapannounce \"" + Data.LastMap + "\", \"" + message + String.Format("\", bc_map, 0x{0:x6}, {1}, {2}, {3}, {4};", color, fontType, fontSize, fontAlign, fontY);

				Data.AppendLine(ps, mes, ScriptConfigSetting.Generic);
				Data.AppendLine(ps, mes, ScriptConfigSetting.MapAnnounces);
			};

			RecvPacketMethods[0x0914] = (reader, ps) => {
				us = reader.UInt16();
				short type = reader.Byte();
				uint id = reader.UInt32();
				int speed = reader.UInt16();
				reader.Forward(8);
				int class_ = reader.UInt16();

				reader.Position = 59;

				byte[] data = reader.Bytes(3);

				int x = (data[0] << 2) | ((data[1] & 0xC0) >> 6);
				int y = ((data[1] & 0x3f) << 4) | ((data[2] & 0xF0) >> 4);
				int dir = (data[2] & 0xF);

				reader.Position = 67;

				int level = reader.UInt16();

				reader.Position = 71;

				int hp = reader.Int32();

				Data.Mob(id);

				reader.Position = 80;
				name = reader.String(us - reader.Position);

				switch (type) {
					case 0x8:
						break;
					case 0x0:
					case 0x6:
						break;
					case 0x5:
						if (Data.Mobs[id].X == 0) {
							PacketDecoderHelper.AddMonster(ps, id, class_, x, y, dir, name, -1);
						}
						break;
				}

				Data.Mobs[id].Speed = speed;
				Data.Mobs[id].Level = level;
				Data.Mobs[id].HP = hp < 0 ? 0 : hp;
				Data.Mobs[id].Name = name;
			};

			RecvPacketMethods[0x09dd] = (reader, ps) => {
				us = reader.UInt16();
				short type = reader.Byte();
				uint id = reader.UInt32();
				reader.Forward(4);
				int speed = reader.UInt16();
				reader.Forward(8);
				int class_ = reader.UInt16();

				reader.Position = 59;

				byte[] data = reader.Bytes(3);

				int x = (data[0] << 2) | ((data[1] & 0xC0) >> 6);
				int y = ((data[1] & 0x3f) << 4) | ((data[2] & 0xF0) >> 4);
				int dir = (data[2] & 0xF);

				reader.Position = 65;

				int level = reader.UInt16();

				reader.Position = 69;

				int hp = reader.Int32();

				reader.Position = 78;
				name = reader.String(us - reader.Position);

				switch (type) {
					case 0x8:
						break;
					case 0x0:
					case 0x6:
						Data.Bl(id);
						Data.Bls[id].Name = name ?? "";

						if (Data.Bls[id].X == 0) {
							Data.Bls[id].X = x;
							Data.Bls[id].Y = y;
							Data.Bls[id].Dir = dir;
							Data.Bls[id].View = class_;
						}

						Data.Bls[id].Type = type;
						Data.AppendLine(ps, Constants.GetSeeObjectType(type) + Data.Bls[id].Name, ScriptConfigSetting.Generic);
						break;
					case 0x5:
						Data.Mob(id);
						PacketDecoderHelper.AddMonster(ps, id, class_, x, y, dir, name, -1);

						Data.Mobs[id].Speed = speed;
						Data.Mobs[id].Level = level;
						Data.Mobs[id].HP = hp < 0 ? 0 : hp;
						Data.Mobs[id].Name = name;
						break;
				}
			};

			RecvPacketMethods[0x0b33] = (reader, ps) => {
				int skill_id = reader.Int16();
				int inf = reader.Int32();
				int level = reader.Int16();
				int sp = reader.Int16();
				int range2 = reader.Int16();
				int flag = reader.Byte();
				int level2 = reader.Int16();

				Data.AppendLine(ps, "Levelup: " + skill_id + ", inf: " + inf + ", lv: " + level + ", sp: " + sp + ", r2: " + range2 + ", flag: " + flag + ", lv2: " + level2 + ";", ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x0120] = (reader, ps) => {
				us = reader.UInt16();

				Data.AppendLine(ps, "Ground unit removed: " + us + ";", ScriptConfigSetting.GroundUnit);
			};

			RecvPacketMethods[0x0088] = (reader, ps) => {
				uint id = reader.UInt32();
				int x = reader.Int16();
				int y = reader.Int16();

				Data.AppendLine(ps, "Fix pos: x = " + x + ", y = " + y + ", id = " + id + ";", ScriptConfigSetting.FixPos);
			};

			RecvPacketMethods[0x09ca] = (reader, ps) => {
				us = reader.UInt16();
				uint id = reader.UInt32();
				int src_id = reader.Int32();
				int x = reader.Int16();
				int y = reader.Int16();
				int unit_id = reader.Int32();
				int range = reader.Byte();
				int visible = reader.Byte();
				int level = reader.Byte();

				Data.AppendLine(ps, "Ground unit: x = " + x + ", y = " + y + ", unit_id = " + unit_id + ", range = " + range + ", visible = " + visible + ", lv = " + level + ", src_id = " + src_id + ", id = " + id + ";", ScriptConfigSetting.GroundUnit);
			};

			RecvPacketMethods[0x09fd] = (reader, ps) => {
				unsafe {
					var p = PacketStructures.Assign<PacketStructures.packet_unit_walking>(reader);

					reader.Position = Marshal.SizeOf(typeof(PacketStructures.packet_unit_walking));
					name = reader.String(reader.Length - reader.Position);

					int speed = p.speed;
					int class_ = p.job;
					int x = (p.MoveData[0] << 2) | ((p.MoveData[1] & 0xC0) >> 6);
					int y = ((p.MoveData[1] & 0x3f) << 4) | ((p.MoveData[2] & 0xF0) >> 4);
					int dir = (p.MoveData[2] & 0xF);

					int x2 = ((p.MoveData[2] & 0x0f) << 6) | (p.MoveData[3] >> 2);
					int y2 = ((p.MoveData[3] & 0x03) << 8) | (p.MoveData[4]);

					int level = p.clevel;
					int hp = p.maxHP;

					Data.Mob(p.AID);

					switch (p.objecttype) {
						case 0x9:
							Data.Mobs[p.AID].View = class_;
							break;
						case 0x0:
						case 0x6:
							// NPC
							break;
						case 0x5:
							PacketDecoderHelper.AddMonster(ps, p.AID, class_, x, y, dir, name, p.isBoss);
							break;
					}

					Data.Mobs[p.AID].Speed = speed;
					Data.Mobs[p.AID].Level = level;
					Data.Mobs[p.AID].HP = hp < 0 ? 0 : hp;
					Data.Mobs[p.AID].Name = name;

					// Don't record walk for players
					if (p.objecttype == 0)
						return;

					Data.AppendLine(ps, "unitwalk getnpcid(0, \"" + Data.GetName(p.AID) + "\"), " + x2 + ", " + y2 + "; Speed: " + speed + " - ", ScriptConfigSetting.UnitWalk);
					Data.AppendLine(ps, "unitwalk getnpcid(0, \"" + Data.GetName(p.AID) + "\"), " + x2 + ", " + y2 + "; Speed: " + speed + " - ", ScriptConfigSetting.UnitWalkFile);
				}
			};

			RecvPacketMethods[0x09db] = (reader, ps) => {
				us = reader.UInt16();
				short type = reader.Byte();
				uint id = reader.UInt32();
				reader.Forward(4);
				int speed = reader.UInt16();
				reader.Forward(8);
				int class_ = reader.UInt16();

				reader.Position = 63;

				byte[] data = reader.Bytes(3);

				int x = (data[0] << 2) | ((data[1] & 0xC0) >> 6);
				int y = ((data[1] & 0x3f) << 4) | ((data[2] & 0xF0) >> 4);
				int dir = (data[2] & 0xF);

				reader.Position = 71;

				int level = reader.UInt16();

				reader.Position = 75;

				int hp = reader.Int32();

				Data.Mob(id);

				reader.Position = 84;
				name = reader.String(us - reader.Position);

				switch (type) {
					case 0x8:
						break;
					case 0x0:
					case 0x6:
						break;
					case 0x5:
						PacketDecoderHelper.AddMonster(ps, id, class_, x, y, dir, name, -1);
						break;
				}

				Data.Mobs[id].Speed = speed;
				Data.Mobs[id].Level = level;
				Data.Mobs[id].HP = hp < 0 ? 0 : hp;
				Data.Mobs[id].Name = name;
			};

			RecvPacketMethods[0x02c2] = (reader, ps) => {
				us = reader.UInt16();
				name = reader.String(us - reader.Position, '\0');
			};

			RecvPacketMethods[0x01d4] = (reader, ps) => {
				Data.AppendLine(ps, "input .@s$;" + Data.Time(ps), ScriptConfigSetting.Generic);
			};

			RecvPacketMethods[0x08ae] = (reader, ps) => {
				int cmd = reader.Int16();

				switch (cmd) {
					case 0x038e:
						reader.Int16();
						string map = reader.String(24, '\0');
						reader.Position = 42;
						int x = reader.Int32();
						int y = reader.Int32();
						int mob_id = reader.Int32();
						Data.AppendLine(ps, "[client input] navigateto \"" + map + "\", " + x + ", " + y + ", " + 0 + ", " + 1 + ", " + mob_id + ";", ScriptConfigSetting.Generic);
						break;
					case 0x0386:
						us = reader.UInt16();
						reader.Position = 18;
						Data.AppendLine(ps, "[client input] // Input: " + reader.String(us - reader.Position, '\0'), ScriptConfigSetting.Generic);
						break;
				}
			};

			RecvPacketMethods[0x08e2] = (reader, ps) => {
				reader.Byte();
				byte flag = reader.Byte();
				byte hideWindow = reader.Byte();
				string map = reader.String(16, '\0');
				int x = reader.Int16();
				int y = reader.Int16();
				int mob_id = reader.Int16();

				Data.AppendLine(ps, "navigateto \"" + map + "\", " + x + ", " + y + ", " + flag + ", " + hideWindow + ", " + mob_id + ";", ScriptConfigSetting.Generic);
			};
		}
	}
}
