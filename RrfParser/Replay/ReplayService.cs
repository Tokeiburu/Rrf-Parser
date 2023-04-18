using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ErrorManager;
using GRF.IO;
using RrfParser.Core;
using Utilities;
using Utilities.Services;

namespace RrfParser.Replay {
	/// <summary>
	///	Credits:
	/// https://github.com/Dia
	/// </summary>
	public interface IReplayService {
		RrfParser.Replay.Replay LoadReplay(Stream fs);
		void WriteReplay(RrfParser.Replay.Replay replay, Stream newReplayFile);
		byte[] WriteReplay(RrfParser.Replay.Replay replay);
		void EditReplay(RrfParser.Replay.Replay replay);
		void EditReplayV2(RrfParser.Replay.Replay replay);
		void EditReplayV3(RrfParser.Replay.Replay replay);
	}
	public class ReplayService2 : IReplayService {

		public RrfParser.Replay.Replay LoadReplay(Stream fs) {
			using (var br = new BinaryReader(fs)) {
				//reading header
				var replay = ReadReplayHeaders(br);

				switch (replay.Version) {
					case 5:
						LoadReplayV5(replay, br, replay.Size);
						break;
				}

				return replay;
			}
		}

		public void WriteReplay(RrfParser.Replay.Replay replay, Stream newReplayFile) {
			throw new NotImplementedException();
		}

		public byte[] WriteReplay(RrfParser.Replay.Replay replay) {
			throw new NotImplementedException();
		}

		public void WriteReplay(RrfParser.Replay.Replay replay, string outputFilename) {
			GrfPath.Delete(outputFilename);

			using (BinaryWriter writer = new BinaryWriter(File.Create(outputFilename))) {
				WriteReplayHeaders(replay, writer);
				WriteReplayV5(replay, writer);
			}
		}

		private RrfParser.Replay.Replay ReadReplayHeaders(BinaryReader br) {
			var replay = new RrfParser.Replay.Replay();
			replay.Header = br.ReadBytes(100);
			replay.Version = br.ReadByte();
			replay.Sig = br.ReadBytes(3);
			var year = br.ReadInt16();
			var month = br.ReadByte();
			var day = br.ReadByte();
			replay.DateUnused = br.ReadByte();
			var hour = br.ReadByte();
			var minute = br.ReadByte();
			var second = br.ReadByte();
			replay.Date = new DateTime(year, month, day, hour, minute, second);
			replay.Size = br.BaseStream.Length;
			
			return replay;
		}

		private void WriteReplayHeaders(RrfParser.Replay.Replay replay, BinaryWriter writer) {
			writer.Write(replay.Header);
			writer.Write(replay.Version);
			writer.Write(replay.Sig);
			writer.Write((Int16)replay.Date.Year);
			writer.Write((byte)replay.Date.Month);
			writer.Write((byte)replay.Date.Day);
			writer.Write((byte)replay.DateUnused);
			writer.Write((byte)replay.Date.Hour);
			writer.Write((byte)replay.Date.Minute);
			writer.Write((byte)replay.Date.Second);
		}

		/// <summary>
		/// Credits:
		/// https://github.com/Dia
		/// </summary>
		/// <param name="br"></param>
		/// <param name="filesize"></param>
		private void LoadReplayV5(RrfParser.Replay.Replay replay, BinaryReader br, long filesize) {
			for (var i = 0; i < 24; i++) {
				var container = new ChunkContainer {
					IsEncrypted = false
				};
				container.Data = new List<Chunk>();
				container.ContainerType = (ContainerType)br.ReadUInt16();
				container.Length = br.ReadInt32();
				container.RealLength = container.Length;
				container.Offset = br.ReadInt32();

				replay.ChunkContainers.Add(container);

				if (container.RealLength == 0) {
					container.RealLength = (int)filesize - container.Offset;
				}

				var lastOffset = br.BaseStream.Position;
				br.BaseStream.Seek(container.Offset, SeekOrigin.Begin);
				var content = br.ReadBytes(container.RealLength);

				if (container.ContainerType == ContainerType.PacketStream) {
					var ptr = 0;
					using (var mschunk = new MemoryStream(content)) {
						using (var brchunk = new BinaryReader(mschunk)) {
							while (ptr < container.RealLength) {
								var packet = new Chunk();
								packet.Id = brchunk.ReadInt32();
								packet.Time = brchunk.ReadInt32();
								packet.Length = brchunk.ReadUInt16();
								packet.Data = brchunk.ReadBytes(packet.Length);
								packet.Data = Crypt(replay.Date, packet.Length, packet.Data);
								packet.Header = (ushort)((packet.Data[1] << 8) | packet.Data[0]);
								container.Data.Add(packet);
								ptr += packet.Length + 10;
							}
						}
					}
				}
				else {
					content = Crypt(replay.Date, container.Length, content);
					var ptr = 0;
					using (var mschunk = new MemoryStream(content)) {
						using (var brchunk = new BinaryReader(mschunk)) {
							while (ptr < container.Length) {
								var chunk = new Chunk();
								chunk.Id = brchunk.ReadInt16();
								chunk.Length = brchunk.ReadInt32();
								chunk.Data = brchunk.ReadBytes(chunk.Length);
								container.Data.Add(chunk);
								ptr += chunk.Length + 6;
							}
						}
					}
				}

				br.BaseStream.Seek(lastOffset, SeekOrigin.Begin);
			}

			var replayContainer = replay.ChunkContainers.FirstOrDefault(p => p.ContainerType == ContainerType.Items);

			if (replayContainer != null) {
				foreach (var chunk in replayContainer.Data) {
					ByteReader r = new ByteReader(chunk.Data);

					while (r.CanRead && r.Position + 172 < r.Length) {
						r.Forward(22);
						int pos = r.Int16() - 2; // 22
						r.Forward(18);
						int equipped = r.Int32(); // 42
						r.Forward(6);
						int qty = r.Int16(); // 52
						r.Forward(28);
						int card0 = r.Int32(); // 82
						int card1 = r.Int32(); // 86
						int card2 = r.Int32(); // 90
						int card3 = r.Int32(); // 94
						r.Forward(6);
						int nameid = r.Int32(); // 104
						r.Forward(26);
						int refine = r.Byte(); // 134
						r.Forward(37);

						BL.AddItem(pos, nameid, qty, refine, card0, card1, card2, card3);

						if (equipped > 0) {
							BL.PreAll.AppendLine(String.Format("+{0} {1} [{2} {3} {4} {5}]", refine, nameid, card0, card1, card2, card3) + String.Format(" // @item2 {0} 1 1 {1} 0 {2} {3} {4} {5}", nameid, refine, card0, card1, card2, card3));
						}
					}
				}
			}

			replayContainer = replay.ChunkContainers.FirstOrDefault(p => p.ContainerType == ContainerType.Session);

			Data.PlayerAid = 0;

			if (replayContainer != null) {
				Data.PlayerAid = BitConverter.ToUInt32(replayContainer.Data[1].Data, 0);
				Data.PlayerAidByte = replayContainer.Data[1].Data;
			}

			replayContainer = replay.ChunkContainers.FirstOrDefault(p => p.ContainerType == ContainerType.ReplayData);
			
			string name = "Tokei";
			
			if (replayContainer != null) {
				// Extract map name
				int length = 0;

				for (int i = 0; i < replayContainer.Data[5].Data.Length; i++, length++) {
					if (replayContainer.Data[5].Data[i] == 0)
						break;
				}

				Data.LastMap = EncodingService.Korean.GetString(replayContainer.Data[5].Data, 0, length);

				length = 0;
			
				for (int i = 0; i < replayContainer.Data[4].Data.Length; i++, length++) {
					if (replayContainer.Data[4].Data[i] == 0)
						break;
				}
			
				name = EncodingService.Korean.GetString(replayContainer.Data[4].Data, 0, length);
			}

			//string newname = "PlayerNamePlayerNamePlayerNamePlayerName";
			//var byteName = EncodingService.Korean.GetBytes(name);
			//byteName = new byte[] { 0x07, 0x80, 0xa6, 0x00 };
			////byteName = new byte[] { 0x08, 0x80, 0xa6, 0x00 };
			//var byteNewName = EncodingService.Korean.GetBytes(newname.Substring(0, byteName.Length));
			//
			//for (int index1 = 0; index1 < replay.ChunkContainers.Count; index1++) {
			//	var container = replay.ChunkContainers[index1];
			//	for (int index = 0; index < container.Data.Count; index++) {
			//		var chunk = container.Data[index];
			//		try {
			//			var res = _find(chunk.Data, byteName);
			//
			//			for (int i = 0; i < res.Count; i++) {
			//				Buffer.BlockCopy(byteNewName, 0, chunk.Data, res[i], byteNewName.Length);
			//			}
			//		}
			//		catch (Exception err) {
			//			ErrorHandler.HandleException(err);
			//		}
			//	}
			//}

			Data.LastZeny = 0;

			try {
				// Attempt to fetch zeny
				replayContainer = replay.ChunkContainers.FirstOrDefault(p => p.ContainerType == ContainerType.Session);

				if (replayContainer != null) {
					Data.LastZeny = BitConverter.ToInt32(replayContainer.Data[42].Data, 0);
				}
			}
			catch {
			}

			Z.F();
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

		private void WriteReplayV5(RrfParser.Replay.Replay replay, BinaryWriter writer) {
			long oldPosition = writer.BaseStream.Position;

			for (var i = 0; i < 24; i++) {
				writer.BaseStream.Seek(oldPosition, SeekOrigin.Begin);
				var container = replay.ChunkContainers[i];
				writer.Write((Int16)container.ContainerType);
				writer.Write(container.Length);
				writer.Write(container.Offset);
				oldPosition = writer.BaseStream.Position;

				try {
					if (writer.BaseStream.Position < container.Offset) {
						writer.BaseStream.Seek(0, SeekOrigin.End);
					}

					while (writer.BaseStream.Position < container.Offset) {
						writer.Write((byte)0);
					}

					if (container.Offset < writer.BaseStream.Position) {
						writer.BaseStream.Seek(container.Offset, SeekOrigin.Begin);
					}

					if (container.Offset == 0)
						continue;

					if (container.Offset == 46068) {
						Z.F();
					}

					Console.WriteLine("BaseStream.Position = " + writer.BaseStream.Position);

					if (container.ContainerType == ContainerType.PacketStream) {
						foreach (var chunk in container.Data) {
							writer.Write(chunk.Id);
							writer.Write(chunk.Time);
							writer.Write((UInt16)chunk.Length);
							writer.Write(Crypt(replay.Date, chunk.Length, chunk.Data));
							//writer.Write(chunk.Data);
						}
					}
					else {
						using (var mschunk = new MemoryStream()) {
							using (var brchunk = new BinaryWriter(mschunk)) {
								foreach (var chunk in container.Data) {
									brchunk.Write((Int16)chunk.Id);
									brchunk.Write(chunk.Length);
									brchunk.Write(chunk.Data);
								}

								var content = mschunk.ToArray();
								writer.Write(Crypt(replay.Date, container.Length, content));
								//writer.Write(content);
							}
						}
					}
				}
				catch (Exception err) {
					Z.F(err);
				}
			}
		}
		
		public void EditReplay(RrfParser.Replay.Replay replay) {
			foreach (var container in replay.ChunkContainers) {
				if (container.ContainerType != ContainerType.PacketStream) {
					/*if (container.ContainerType != ContainerType.ReplayData &&
					    container.ContainerType != ContainerType.Quests &&
					    container.ContainerType != ContainerType.Session &&
					    container.ContainerType != ContainerType.Unknown_20 &&
					    container.ContainerType != ContainerType.Unknown_21 &&
					    container.ContainerType != ContainerType.Unknown_22 &&
					    container.ContainerType != ContainerType.Unknown_23 &&
					    container.ContainerType != ContainerType.Unknown_24 &&
					    container.ContainerType != ContainerType.UnknownContainingPet &&
					    container.ContainerType != ContainerType.Efst &&
					    container.ContainerType != ContainerType.Status &&
					    container.ContainerType != ContainerType.GroupAndFriends &&
					    container.ContainerType != ContainerType.Unknown_10 &&
					    container.ContainerType != ContainerType.Unknown_12 &&
					    container.ContainerType != ContainerType.Unknown_13 &&
					    container.ContainerType != ContainerType.Unknown_16 &&
					    container.ContainerType != ContainerType.Unknown_15 &&
					    container.ContainerType != ContainerType.Unknown_18 &&
					    container.ContainerType != ContainerType.Unknown_19 &&
					    container.ContainerType != ContainerType.LastContainerType &&
						container.ContainerType != ContainerType.None)*/
					if (container.ContainerType == ContainerType.Items || container.ContainerType == ContainerType.InitialPackets ||
						container.ContainerType == ContainerType.Quests) {
						container.Length = 0;
					}
					foreach (var chunk in container.Data) {
						/*if (container.ContainerType != ContainerType.ReplayData &&
						    container.ContainerType != ContainerType.Quests &&
						    container.ContainerType != ContainerType.Session &&
						    container.ContainerType != ContainerType.Unknown_20 &&
						    container.ContainerType != ContainerType.Unknown_21 &&
						    container.ContainerType != ContainerType.Unknown_22 &&
						    container.ContainerType != ContainerType.Unknown_23 &&
						    container.ContainerType != ContainerType.Unknown_24 &&
						    container.ContainerType != ContainerType.UnknownContainingPet &&
						    container.ContainerType != ContainerType.Efst &&
						    container.ContainerType != ContainerType.Status &&
						    container.ContainerType != ContainerType.Unknown_10 &&
						    container.ContainerType != ContainerType.Unknown_12 &&
						    container.ContainerType != ContainerType.Unknown_13 &&
						    container.ContainerType != ContainerType.Unknown_16 &&
						    container.ContainerType != ContainerType.Unknown_15 &&
						    container.ContainerType != ContainerType.Unknown_18 &&
						    container.ContainerType != ContainerType.Unknown_19 &&
						    container.ContainerType != ContainerType.GroupAndFriends &&
						    container.ContainerType != ContainerType.LastContainerType &&
						    container.ContainerType != ContainerType.None)
						{
							chunk.Data = new byte[chunk.Length];
							break;
						}*/
						if (container.ContainerType == ContainerType.Items || container.ContainerType == ContainerType.InitialPackets ||
							container.ContainerType == ContainerType.Quests) {
							chunk.Data = new byte[chunk.Length];
							break;
						}/*
						switch ((ReplayOpCodes) chunk.Id)
						{
							case ReplayOpCodes.Money:
								chunk.Data = BitConverter.GetBytes(0);
								break;
							case ReplayOpCodes.InventoryItems:
								chunk.Data = new byte[chunk.Length];
								break;
							case ReplayOpCodes.QuestInfo:
								chunk.Data = new byte[chunk.Length];
								break;
							case ReplayOpCodes.EquippedItems:
								chunk.Data = new byte[chunk.Length];
								break;
							case ReplayOpCodes.EquippedShadowItems:
								chunk.Data = new byte[chunk.Length];
								break;
							case ReplayOpCodes.CartItems:
								chunk.Data = new byte[chunk.Length];
								break;
							case ReplayOpCodes.EfstInfo:
								chunk.Data = new byte[chunk.Length];
								break;
							case ReplayOpCodes.Charactername:
								var buffer = new byte[64];
								var newName = Encoding.ASCII.GetBytes("Redacted");
								Array.Copy(newName, buffer, newName.Length);
								chunk.Data = buffer;
								break;
							default:
								break;
						}*/
					}
				}
				else {
					/*foreach (var chunk in container.Data)
					{
						if ((PacketHeader) chunk.Header ==
						    PacketHeader.HEADER_ZC_NOTIFY_PLAYERMOVE)
						{
							chunk.Data = EditPacketMoveLocation(chunk.Data, 198,
								224, 200, 228);
						}
					}*/

					/*foreach (var chunk in container.Data)
					{
						switch ((PacketHeader) chunk.Header.Value)
						{
							case PacketHeader.HEADER_ZC_INVENTORY_ITEMLIST_NORMAL_V5:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_INVENTORY_ITEMLIST_NORMAL:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_INVENTORY_ITEMLIST_EQUIP:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_INVENTORY_ITEMLIST_EQUIP_V5:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_INVENTORY_ITEMLIST_EQUIP_V6:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_EQUIPMENT_ITEMLIST:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_EQUIPMENT_ITEMLIST2:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_EQUIPMENT_ITEMLIST3:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_PAR_CHANGE:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_COUPLESTATUS:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_MSG_STATE_CHANGE3:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_MSG_STATE_CHANGE2:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_CLOSE_DIALOG:
								chunk.Data = new byte[chunk.Length];
								break;
							case PacketHeader.HEADER_ZC_SKILL_DISAPPEAR:
								chunk.Data = new byte[chunk.Length];
								break;
							default:
								if (!Enum.IsDefined(typeof(PacketHeader), (PacketHeader) chunk.Header.Value))
								{
										Console.WriteLine("Deleted");
										chunk.Data = new byte[chunk.Length];
									break;
								}
								break;
						}
					}*/
				}
			}
		}

		public void EditReplayV2(RrfParser.Replay.Replay replay) {
			foreach (var container in replay.ChunkContainers) {
				if (container.ContainerType != ContainerType.PacketStream) {
					if (container.ContainerType == ContainerType.Items || container.ContainerType == ContainerType.InitialPackets ||
						container.ContainerType == ContainerType.Quests || container.ContainerType == ContainerType.Status) {
						container.Length = 0;
					}
					foreach (var chunk in container.Data) {
						if (container.ContainerType == ContainerType.Items || container.ContainerType == ContainerType.InitialPackets ||
							container.ContainerType == ContainerType.Quests || container.ContainerType == ContainerType.Status) {
							chunk.Data = new byte[chunk.Length];
							break;
						}
					}
				}
			}
		}

		public void EditReplayV3(RrfParser.Replay.Replay replay) {
			foreach (var container in replay.ChunkContainers) {
				if (container.ContainerType != ContainerType.PacketStream) {
					if (container.ContainerType != ContainerType.ReplayData) {
						container.Length = 0;
					}
					foreach (var chunk in container.Data) {
						if (container.ContainerType != ContainerType.ReplayData) {
							chunk.Data = new byte[chunk.Length];
							break;
						}
					}
				}
			}
		}


		private void WritePacketChunk(BinaryWriter bw, Chunk chunk) {
			bw.Write(chunk.Id);
			bw.Write(chunk.Time);
			bw.Write((ushort)chunk.Length);
			bw.Write(chunk.Data);
		}

		private byte[] EditPacketMoveLocation(byte[] data, byte x0, byte y0, byte x1, byte y1) {
			var sx0 = 8;
			var sy0 = 8;
			data[6] = (byte)(x0 >> 2);
			data[7] = (byte)((x0 << 6) | ((y0 >> 4) & 0x3f));
			data[8] = (byte)((y0 << 4) | ((x1 >> 6) & 0x0f));
			data[9] = (byte)((x1 << 2) | ((y1 >> 8) & 0x03));
			data[10] = (byte)y1;
			data[11] = (byte)(sx0 << 4 | sy0 & 0xf);
			return data;
		}

		private byte[] Crypt(DateTime date, int size, byte[] buffer) {
			var offset = 0;

			if (buffer == null)
				return new byte[] { };

			var realKey1 = GetKey1(date) >> 5;
			var realKey2 = GetKey2(date) >> 3;
			var ret = new byte[size];
			using (var ms = new MemoryStream(buffer)) {
				using (var msw = new MemoryStream(ret)) {
					using (var br = new BinaryReader(ms)) {
						using (var bw = new BinaryWriter(msw)) {
							for (var cursor = 0; cursor < size / 4; cursor++) {
								var tempOld = br.ReadInt32();
								var temp = tempOld ^ (realKey1 + (cursor + 1)) * realKey2;
								bw.Write(temp);
								offset += 4;
							}
							//Debug.Print("{0}", size - offset);
							bw.Write(br.ReadBytes(size - offset));
						}
					}
				}
			}

			return ret;
		}

		private int GetKey1(DateTime date) {
			var b = new byte[4];
			using (var ms = new MemoryStream()) {
				using (var bw = new BinaryWriter(ms)) {
					bw.Write((short)date.Year);
					bw.Write((byte)date.Month);
					bw.Write((byte)date.Day);
				}

				b = ms.ToArray();
			}

			using (var ms = new MemoryStream(b)) {
				using (var br = new BinaryReader(ms)) {
					return br.ReadInt32();
				}
			}
		}

		private int GetKey2(DateTime date) {
			var b = new byte[4];
			using (var ms = new MemoryStream()) {
				using (var bw = new BinaryWriter(ms)) {
					bw.Write((byte)0);
					bw.Write((byte)date.Hour);
					bw.Write((byte)date.Minute);
					bw.Write((byte)date.Second);
				}

				b = ms.ToArray();
			}

			using (var ms = new MemoryStream(b)) {
				using (var br = new BinaryReader(ms)) {
					return br.ReadInt32();
				}
			}
		}
	}

}