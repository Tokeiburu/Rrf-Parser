using System;
using System.Collections.Generic;

namespace RrfParser.Replay {
	/// <summary>
	///	Credits:
	/// https://github.com/Dia
	/// </summary>
	public class Replay {
		public Replay() {
			ChunkContainers = new List<ChunkContainer>();
		}
		public string FileName { get; set; }
		public byte[] Header { get; set; }
		public byte Version { get; set; }
		public byte DateUnused { get; set; }
		public byte[] Sig { get; set; }
		public DateTime Date { get; set; }
		public long Size { get; set; }
		public List<ChunkContainer> ChunkContainers { get; set; }
	}
}