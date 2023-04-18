using System;
using System.Runtime.InteropServices;
using GRF.IO;

namespace RrfParser.Packet {
	public static class PacketStructures {
		[StructLayout(LayoutKind.Sequential, Pack=1)]
		public struct packet_status_change {
			public Int16 PacketType;
			public Int16 index;
			public UInt32 AID;
			public byte state;
			public UInt32 Total;
			public UInt32 Left;
			public Int32 val1;
			public Int32 val2;
			public Int32 val3;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct packet_status_change2 {
			public Int16 PacketType;
			public Int16 index;
			public UInt32 AID;
			public byte state;
			public UInt32 Left;
			public Int32 val1;
			public Int32 val2;
			public Int32 val3;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct packet_unit_walking {
			public Int16 PacketType;
			public Int16 PacketLength;
			public byte objecttype;
			public UInt32 AID;
			public UInt32 GID;
			public Int16 speed;
			public Int16 bodyState;
			public Int16 healthState;
			public Int32 effectState;
			public Int16 job;
			public UInt16 head;
			public UInt32 weapon;
			public UInt32 shield;
			public UInt16 accessory;
			public UInt32 moveStartTime;
			public UInt16 accessory2;
			public UInt16 accessory3;
			public Int16 headpalette;
			public Int16 bodypalette;
			public Int16 headDir;
			public UInt16 robe;
			public UInt32 GUID;
			public Int16 GEmblemVer;
			public Int16 honor;
			public Int32 virtue;
			public byte isPKModeON;
			public byte sex;
			public fixed byte MoveData[6];
			public byte xSize;
			public byte ySize;
			public Int16 clevel;
			public Int16 font;
			public Int32 maxHP;
			public Int32 HP;
			public byte isBoss;
			public UInt16 body;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct packet_idle_unit {
			public Int16 PacketType;
			public Int16 PacketLength;
			public byte objecttype;
			public UInt32 AID;
			public UInt32 GID;
			public Int16 speed;
			public Int16 bodyState;
			public Int16 healthState;
			public Int32 effectState;
			public Int16 job;
			public UInt16 head;
			public UInt32 weapon;
			public UInt32 shield;
			public UInt16 accessory;
			public UInt16 accessory2;
			public UInt16 accessory3;
			public Int16 headpalette;
			public Int16 bodypalette;
			public Int16 headDir;
			public UInt16 robe;
			public UInt32 GUID;
			public Int16 GEmblemVer;
			public Int16 honor;
			public Int32 virtue;
			public byte isPKModeON;
			public byte sex;
			public fixed byte PosDir[3];
			public byte xSize;
			public byte ySize;
			public byte state;
			public Int16 clevel;
			public Int16 font;
			public Int32 maxHP;
			public Int32 HP;
			public byte isBoss;
			public UInt16 body;
			//public fixed char name [24];
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct packet_idle_unit_spawn {
			public Int16 PacketType;
			public Int16 PacketLength;
			public byte objecttype;
			public UInt32 AID;
			public UInt32 GID;
			public Int16 speed;
			public Int16 bodyState;
			public Int16 healthState;
			public Int32 effectState;
			public Int16 job;
			public UInt16 head;
			public UInt32 weapon;
			public UInt32 shield;
			public UInt16 accessory;
			public UInt16 accessory2;
			public UInt16 accessory3;
			public Int16 headpalette;
			public Int16 bodypalette;
			public Int16 headDir;
			public UInt16 robe;
			public UInt32 GUID;
			public Int16 GEmblemVer;
			public Int16 honor;
			public Int32 virtue;
			public byte isPKModeON;
			public byte sex;
			public fixed byte PosDir[3];
			public byte xSize;
			public byte ySize;
			public Int16 clevel;
			public Int16 font;
			public Int32 maxHP;
			public Int32 HP;
			public byte isBoss;
			public UInt16 body;
			//public fixed char name [24];
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct PACKET_ZC_NOTIFY_GROUNDSKILL_0x0117 {
			public Int16 PacketType;
			public UInt16 SKID;
			public UInt32 AID;
			public Int16 level;
			public Int16 xPos;
			public Int16 yPos;
			public UInt32 startTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PACKET_ZC_USE_SKILL_0x09cb {
			public Int16 PacketType;
			public UInt16 SKID;
			public Int32 level;
			public UInt32 targetAID;
			public UInt32 srcAID;
			public byte result;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PACKET_ZC_USE_SKILL_0x011a {
			public Int16 PacketType;
			public UInt16 SKID;
			public Int32 level;
			public UInt32 targetAID;
			public UInt32 srcAID;
			public byte result;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public unsafe struct QuestItemHunt {
			public UInt32 huntIdent;
			public UInt32 huntIdent2;
			public UInt32 mobType;
			public UInt32 mob_id;
			public UInt16 levelMin;
			public UInt16 levelMax;
			public UInt16 huntCount;
			public UInt16 maxCount;
			public fixed byte mobName[24];
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PACKET_ZC_NOTIFY_SKILL {
			public Int16 PacketType;
			public UInt16 SKID;
			public UInt32 AID;
			public UInt32 targetID;
			public UInt32 startTime;
			public Int32 attackMT;
			public Int32 attackedMT;
			public Int32 damage;
			public Int16 level;
			public Int16 count;
			public byte action;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public class QuestItem {
			public Int32 QuestID;
			public byte Active;
			public Int32 quest_svrTime;
			public Int32 quest_endTime;
			public Int16 hunting_count;
			public QuestItemHunt[] Hunts;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PACKET_ZC_USESKILL_ACK_0x013e {
			public Int16 packetType;
			public UInt32 srcId;
			public UInt32 dstId;
			public UInt16 x;
			public UInt16 y;
			public UInt16 skillId;
			public UInt32 element;
			public UInt32 delayTime;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PACKET_ZC_DISPEL_0x01b9 {
			public Int16 packetType;
			public UInt32 srcId;
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PACKET_ZC_USESKILL_ACK {
			public Int16 packetType;
			public UInt32 srcId;
			public UInt32 dstId;
			public UInt16 x;
			public UInt16 y;
			public UInt16 skillId;
			public UInt32 element;
			public UInt32 delayTime;
			public byte disposable;
		};

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PACKET_ZC_SKILL_POSTDELAY {
			public Int16 PacketType;
			public UInt16 SKID;
			public UInt32 DelayTM;
		};

		public static T Assign<T>(ByteReader reader) {
			reader.Position = 0;
			byte[] bytes = reader.Bytes(Marshal.SizeOf(typeof(T)));
			GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
			T p = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();
			reader.Position = 2;
			return p;
		}

		public static T Assign2<T>(ByteReader reader) {
			byte[] bytes = reader.Bytes(Marshal.SizeOf(typeof(T)));
			GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
			T p = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
			handle.Free();
			return p;
		}
	}
}
