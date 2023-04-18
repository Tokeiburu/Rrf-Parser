using System;
using System.Collections.Generic;
using ErrorManager;
using RrfParser.Core;
using RrfParser.Replay;

namespace RrfParser.Packet {
	public class PacketStream {
		private int _position;
		private readonly List<Chunk> _packets;

		public PacketStream(List<Chunk> packets) {
			_packets = packets;
		}

		public void Reset() {
			_position = 0;
			_lastIntervalTick = 0;
		}

		public int PacketId {
			get { return _position; }
			set { _position = value; }
		}

		public bool CanRead {
			get { return PacketId < _packets.Count; }
		}

		public double Delay {
			get {
				if (PacketId > 0) {
					var diff = CurrentTick - Data.TimeStart;
					return diff;
				}

				return 0;
			}
		}

		private double _lastIntervalTick;

		public double IntervalDelay {
			get {
				if (PacketId > 0) {
					var cur = Delay - _lastIntervalTick;
					_lastIntervalTick = Delay;
					return cur;
				}

				return 0;
			}
		}

		public long CurrentTick {
			get {
				try {
					return _packets[PacketId].Time;
				}
				catch (Exception err) {
					ErrorHandler.HandleException(err);
					return 0;
				}
			}
		}

		public Chunk CurrentPacket {
			get { return _packets[_position]; }
		}

		public void NextPacket() {
			_position++;
		}
	}
}