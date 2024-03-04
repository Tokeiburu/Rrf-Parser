using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GRF.IO;
using RrfParser.Core;
using RrfParser.Replay;
using TokeiLibrary;
using Utilities;
using Utilities.Extension;

namespace RrfParser.Packet {
	public class PacketParser {
		public void DecodeAll(PacketStream ps) {
			while (ps.CanRead) {
				Chunk packet = ps.CurrentPacket;
				PacketDecoders.PacketDecoderMethod method;

				try {
					if (PacketDecoders.RecvPacketMethods.TryGetValue(packet.Header ?? 0, out method)) {
						ByteReader reader = new ByteReader(packet.Data);
						reader.UInt16();
						method(reader, ps);
					}
				}
				catch {
					Data.AppendLine(ps, "## EXCEPTION ## for packet 0x" + (packet.Header ?? 0).ToString("x4"), ScriptConfigSetting.Generic);
				}

				ps.NextPacket();
			}
		}

		public string ParseRawOutput(PacketStream ps) {
			var transcript = new StringBuilder();

			while (ps.CanRead) {
				Chunk packet = ps.CurrentPacket;

				transcript.AppendFormat("R{3:0000}-{0} - 0x{1:x4}:{2} ", (int)ps.Delay, packet.Header, (int)ps.IntervalDelay, packet.Id);

				for (int k = 0; k < packet.Data.Length; k++) {
					transcript.AppendFormat("{0:x2}", packet.Data[k]);
				}

				transcript.AppendLine();
				ps.NextPacket();
			}

			return transcript.ToString();
		}

		public string ParseAsDecodePackets(PacketStream ps) {
			string rrfLastMap = Data.LastMap;
			int oldZeny = Data.LastZeny;
			Dictionary<string, string> reverts = new Dictionary<string, string>();

			Data.LastMap = rrfLastMap == "" ? "unknown_map" : rrfLastMap;
			Data.Reset(true);
			// First pass, used to assign NPC IDs with names
			DecodeAll(ps);

			ps.Reset();
			Data.LastMap = rrfLastMap == "" ? "unknown_map" : rrfLastMap;
			Data.LastZeny = oldZeny;
			Data.Reset(false);

			DecodeAll(ps);

			StringBuilder npcs = new StringBuilder();

			npcs.AppendLine("// NPC scripts");

			foreach (var bl in Data.Bls.Values) {
				if (bl.X == 0 && bl.Y == 0 && bl.View == 0)
					continue;

				if (bl.Type == 0x6) {
					if (bl.Map.Contains("@")) {
						int offset = bl.Map.IndexOf("@", System.StringComparison.Ordinal) - 1;
						string header = bl.Map.Substring(0, offset);

						if (!reverts.ContainsKey(header)) {
							reverts["_" + header] = "";
							reverts[header] = "";
						}
					}

					npcs.AppendLine(bl.ToString());
				}
			}

			npcs.AppendLine("// Equipment");
			npcs.AppendLine(BL.PreAll.ToString());
			npcs.AppendLine("// Monster spawned (unique gid)");

			Dictionary<int, BL> mobInfo = new Dictionary<int, BL>();

			foreach (var mob in Data.Mobs.OrderBy(p => p.Value.Time)) {
				if (mob.Value.View == 0)
					continue;

				if (!mobInfo.ContainsKey(mob.Value.View))
					mobInfo[mob.Value.View] = new BL();

				var mobInfoValue = mobInfo[mob.Value.View];
				var mobEntry = mob.Value;

				mobInfoValue.HP = mobEntry.HP > 0 ? mobEntry.HP : mobInfoValue.HP;
				mobInfoValue.Level = mobEntry.Level > 0 ? mobEntry.Level : mobInfoValue.Level;
				mobInfoValue.Speed = mobEntry.Speed > 0 ? mobEntry.Speed : mobInfoValue.Speed;
				mobInfoValue.DMotion = mobEntry.DMotion > 0 ? mobEntry.DMotion : mobInfoValue.DMotion;
				mobInfoValue.AMotion = mobEntry.AMotion > 0 ? mobEntry.AMotion : mobInfoValue.AMotion;
				mobInfoValue.MinDamage = Math.Min(mobEntry.MinDamage == 0 ? Int32.MaxValue : mobEntry.MinDamage, mobInfoValue.MinDamage == 0 ? Int32.MaxValue : mobInfoValue.MinDamage);
				mobInfoValue.MaxDamage = Math.Max(mobEntry.MaxDamage, mobInfoValue.MaxDamage);
				mobInfoValue.IsBoss = Math.Max(mobEntry.IsBoss, mobInfoValue.IsBoss);

				foreach (var skill in mobEntry.LatestSkillIds) {
					if (mobInfoValue.LatestSkillIds.ContainsKey(skill.Key)) {
						// readjust
						mobInfoValue.LatestSkillIds[skill.Key].Casttime = Math.Max(skill.Value.Casttime, mobInfoValue.LatestSkillIds[skill.Key].Casttime);
						mobInfoValue.LatestSkillIds[skill.Key].Div = Math.Max(skill.Value.Div, mobInfoValue.LatestSkillIds[skill.Key].Div);
						mobInfoValue.LatestSkillIds[skill.Key].Emote = Math.Max(skill.Value.Emote, mobInfoValue.LatestSkillIds[skill.Key].Emote);

						if (skill.Value.Level == -1) {
							mobInfoValue.LatestSkillIds[skill.Key].Level = skill.Value.Level;
						}
						else if (skill.Value.Level > 0) {
							mobInfoValue.LatestSkillIds[skill.Key].Level = Math.Max(skill.Value.Level, mobInfoValue.LatestSkillIds[skill.Key].Level);
						}

						if (skill.Value.Count > mobInfoValue.LatestSkillIds[skill.Key].Count) {
							mobInfoValue.LatestSkillIds[skill.Key].Count = skill.Value.Count;
							mobInfoValue.LatestSkillIds[skill.Key].p_Count = skill.Value.p_Count;
							mobInfoValue.LatestSkillIds[skill.Key].Rate = skill.Value.Rate;
							mobInfoValue.LatestSkillIds[skill.Key].p_Rate = skill.Value.p_Rate;
							mobInfoValue.LatestSkillIds[skill.Key].StartTick = skill.Value.StartTick;
							mobInfoValue.LatestSkillIds[skill.Key].p_StartTick = skill.Value.p_StartTick;
							mobInfoValue.LatestSkillIds[skill.Key].EndTick = skill.Value.EndTick;
							mobInfoValue.LatestSkillIds[skill.Key].p_EndTick = skill.Value.p_EndTick;
						}

						if (mobInfoValue.LatestSkillIds[skill.Key].MinDamage == 0) {
							mobInfoValue.LatestSkillIds[skill.Key].MinDamage = skill.Value.MinDamage;
						}
						else if (skill.Value.MinDamage > 0) {
							mobInfoValue.LatestSkillIds[skill.Key].MinDamage = Math.Min(skill.Value.MinDamage, mobInfoValue.LatestSkillIds[skill.Key].MinDamage);
						}

						if (mobInfoValue.LatestSkillIds[skill.Key].MaxDamage == 0) {
							mobInfoValue.LatestSkillIds[skill.Key].MaxDamage = skill.Value.MaxDamage;
						}
						else if (skill.Value.MaxDamage > 0) {
							mobInfoValue.LatestSkillIds[skill.Key].MaxDamage = Math.Max(skill.Value.MaxDamage, mobInfoValue.LatestSkillIds[skill.Key].MaxDamage);
						}

						mobInfoValue.LatestSkillIds[skill.Key].Cancel += skill.Value.Cancel;
					}
					else {
						mobInfoValue.LatestSkillIds[skill.Key] = skill.Value;
					}
				}

				mobInfoValue.ADelays.AddRange(mobEntry.ADelays);

				mobInfoValue.View = mobEntry.View;
				mobInfoValue.Name = mobEntry.Name;

				npcs.AppendFormat("monster \"{0}\", {1}, {2}, \"{4}\", {3}, 1; // dID: {6} - TIME: {5}", mob.Value.Map, mob.Value.X, mob.Value.Y, mob.Value.View, mob.Value.Name, mob.Value.Time, mob.Value.Id);
				npcs.AppendLine();
			}

			string mobConf = "output\\mob_data.conf";

			GrfPath.Delete(mobConf);
			GrfPath.CreateDirectoryFromFile(mobConf);
			StringBuilder mobOutput = new StringBuilder();

			foreach (var mobData in mobInfo.Values.OrderBy(p => p.View)) {
				mobOutput.AppendLine("{");
				mobOutput.AppendLine("\tId: " + mobData.View);
				mobOutput.AppendLine("\tName: " + mobData.Name);
				mobOutput.AppendLine("\tLevel: " + mobData.Level);
				mobOutput.AppendLine("\tSpeed: " + mobData.Speed);
				mobOutput.AppendLine("\tDMotion: " + mobData.DMotion);
				mobOutput.AppendLine("\tAMotion: " + mobData.AMotion);
				mobOutput.AppendLine("\tMinDamage: " + mobData.MinDamage);
				mobOutput.AppendLine("\tMaxDamage: " + mobData.MaxDamage);
				mobOutput.AppendLine("\tIsBoss: " + mobData.IsBoss);

				if (mobData.LatestSkillIds.Count > 0) {
					try {
						uint maxTick = mobData.LatestSkillIds.Max(p => p.Value.EndTick);
						var min = mobData.LatestSkillIds.Where(p => p.Value.StartTick > 0).ToList();
						uint minTick;

						if (min.Count > 0) {
							minTick = mobData.LatestSkillIds.Where(p => p.Value.StartTick > 0).Min(p => p.Value.StartTick);
						}
						else {
							minTick = 0;
						}

						uint tickDiff = maxTick - minTick;

						foreach (int id in mobData.LatestSkillIds.Keys) {
							mobOutput.AppendLine("\tSkillID: " + id);

							Skill skill = mobData.LatestSkillIds[id];

							mobOutput.AppendLine("\t\tCastTime: " + skill.Casttime);
							mobOutput.AppendLine("\t\tSkillLevel: " + skill.Level);
							mobOutput.AppendLine("\t\tEmote: " + skill.Emote);
							mobOutput.AppendLine("\t\tCount: " + (skill.p_Count == 0 ? skill.Count : skill.p_Count));
							mobOutput.AppendLine("\t\tCancel: " + skill.Cancel);
							float chance = 0;
							float chance2 = 0;

							if (skill.Rate > 0 && skill.Count > 0) {
								// Should check the mob end vs the mob beginning
								//float maxAmountUsed = (skill.EndTick - skill.StartTick + skill.Rate) / (float)skill.Rate;
								float maxAmountUsed = (tickDiff) / (float)skill.Rate;
								float realused = (float)skill.Count;

								chance = realused / maxAmountUsed * 100;

								maxAmountUsed = (skill.EndTick - skill.StartTick + skill.Rate) / (float)skill.Rate;
								chance2 = realused / maxAmountUsed * 100;
							}
							else if (skill.p_Rate > 0 && skill.p_Count > 0) {
								float maxAmountUsed = (tickDiff) / (float)skill.p_Rate;
								float realused = (float)skill.p_Count;

								chance = realused / maxAmountUsed * 100;

								maxAmountUsed = (skill.p_EndTick - skill.p_StartTick + skill.p_Rate) / (float)skill.p_Rate;
								chance2 = realused / maxAmountUsed * 100;
							}

							if (Math.Max(1, skill.Div) == 1) {
								mobOutput.AppendLine("\t\tDmg_min: " + skill.MinDamage);
								mobOutput.AppendLine("\t\tDmg_max: " + skill.MaxDamage);
							}
							else {
								mobOutput.AppendLine("\t\tDmg_min: " + skill.MinDamage + " > " + (skill.MinDamage / Math.Max(1, skill.Div)));
								mobOutput.AppendLine("\t\tDmg_max: " + skill.MaxDamage + " > " + (skill.MaxDamage / Math.Max(1, skill.Div)));
							}

							mobOutput.AppendLine("\t\tDiv: " + skill.Div);
						}
					}
					catch {
						Z.F();
					}
				}

				long totalTime = 0;
				int totalItems = 0;

				foreach (var adelay in mobData.ADelays) {
					if (adelay > 3000)
						continue;

					if (adelay < mobData.AMotion) {
						continue;
					}

					totalTime += adelay;
					totalItems++;
				}

				if (totalItems > 0)
					mobData.ADelay = (int)(totalTime / totalItems);

				mobOutput.AppendLine("\tADelay (approximation): " + mobData.ADelay);
				mobOutput.AppendLine("\tHP: " + mobData.HP);

				if (Data.MobDrops.ContainsKey(mobData.View) && Data.MobDead.ContainsKey(mobData.View)) {
					int deaths = Data.MobDead[mobData.View];
					Dictionary<int, double> rates = new Dictionary<int, double>();
					Dictionary<int, double> rates2 = new Dictionary<int, double>();

					foreach (var ele in Data.MobDrops[mobData.View]) {
						rates[ele.Key] = ele.Value / (double)deaths;
					}

					mobOutput.AppendLine("\tMob Death: " + deaths);
					mobOutput.AppendLine("\tDrops: ");

					foreach (var ele in rates) {
						// Sakray, no 6x rates
						//rates2[ele.Key] = (100.0 * ele.Value) / 6.0;
						rates2[ele.Key] = (100.0 * ele.Value) / 1.0;

						if (rates2[ele.Key] >= 16.0) {
							rates2[ele.Key] = 100.0;
						}
						else if (rates2[ele.Key] >= 15.0) {
							rates2[ele.Key] = 20.0;
						}
						else if (rates2[ele.Key] >= 10.0) {
							rates2[ele.Key] = 10.0;
						}

						mobOutput.AppendLine("\t" + ele.Key + "," + (int)(rates2[ele.Key] * 100) + "\tDropped: " + Data.MobDrops[mobData.View][ele.Key]);
					}
				}

				mobOutput.AppendLine("}");
			}

			File.WriteAllText(mobConf, mobOutput.ToString());
			npcs.AppendLine();
			npcs.AppendLine("// Packet output");
			string npc_output = npcs.ToString();

			if (RrfParserConfiguration.RevertInstanceRenames) {
				foreach (var revert in reverts) {
					npc_output = npc_output.ReplaceAll(revert.Key, revert.Value);
				}
			}

			Data._outputStringBuilders[ScriptConfigSetting.Generic.OutputFile].Insert(0, npc_output);
			string output = "";

			foreach (var builder in Data._outputStringBuilders) {
				string path = GrfPath.Combine(Configuration.ApplicationPath, "output\\" + builder.Key);

				GrfPath.Delete(path);
				GrfPath.CreateDirectoryFromFile(path);

				if (builder.Key == ScriptConfigSetting.Generic.OutputFile) {
					output = builder.Value.ToString();

					if (RrfParserConfiguration.RevertInstanceRenames) {
						foreach (var revert in reverts) {
							output = output.ReplaceAll(revert.Key, revert.Value);
						}
					}

					File.WriteAllText(path, output);
				}
				else {
					File.WriteAllText(path, builder.Value.ToString());
				}
			}

			BL.PreAll = new StringBuilder();
			return output;
		}
	}
}
