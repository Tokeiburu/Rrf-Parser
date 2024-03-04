using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ErrorManager;
using RrfParser.Packet;

namespace RrfParser.Replay {
	public class ReadPackets {
		public PacketStream PacketStream { get; set; }
		public List<Chunk> Chunks = new List<Chunk>();

		public void ReadPath(string path) {
			try {
				ReplayService2 replayService = new ReplayService2();
				var replay = replayService.LoadReplay(File.OpenRead(path));

				var mainContainers = replay.ChunkContainers.OrderByDescending(p => p.Data.Count).ToList();

				for (int i = 0; i < mainContainers.Count; i++) {
					if (mainContainers[i].Data.Count > 0 && mainContainers[i].Data[0].Header == null) {
						mainContainers.RemoveAt(i);
						i--;
					}
					else {
						break;
					}
				}

				List<Chunk> chunks = new List<Chunk>();

				var initialPacket = mainContainers.FirstOrDefault(p => p.ContainerType == ContainerType.InitialPackets);

				if (initialPacket != null)
					chunks.AddRange(initialPacket.Data);

				chunks.AddRange(mainContainers.First(p => p.ContainerType == ContainerType.PacketStream).Data);
				chunks = chunks.Where(p => p.Length > 0).ToList();

				for (int i = 0; i < chunks.Count; i++) {
					chunks[i].Id = i;
				}

				Chunks = chunks;
				PacketStream = new PacketStream(chunks);
			}
			catch (Exception err) {
				ErrorHandler.HandleException(err);
			}
		}
	}
}
