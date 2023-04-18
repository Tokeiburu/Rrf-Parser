using System.Collections.Generic;

namespace RrfParser.Core {
	public static class Constants {
		public static List<string> Emotions = new List<string> {
			"e_gasp",
			"e_what",
			"e_ho",
			"e_lv",
			"e_swt",
			"e_ic",
			"e_an",
			"e_ag",
			"e_cash",
			"e_dots",
			"e_scissors",
			"e_rock",
			"e_paper",
			"e_korea",
			"e_lv2",
			"e_thx",
			"e_wah",
			"e_sry",
			"e_heh",
			"e_swt2",
			"e_hmm",
			"e_no1",
			"e_no",
			"e_omg",
			"e_oh",
			"e_X",
			"e_hlp",
			"e_go",
			"e_sob",
			"e_gg",
			"e_kis",
			"e_kis2",
			"e_pif",
			"e_ok",
			"e_mute",
			"e_indonesia",
			"e_bzz",
			"e_rice",
			"e_awsm",
			"e_meh",
			"e_shy",
			"e_pat",
			"e_mp",
			"e_slur",
			"e_com",
			"e_yawn",
			"e_grat",
			"e_hp",
			"e_philippines",
			"e_malaysia",
			"e_singapore",
			"e_brazil",
			"e_flash",
			"e_spin",
			"e_sigh",
			"e_dum",
			"e_loud",
			"e_otl",
			"e_dice1",
			"e_dice2",
			"e_dice3",
			"e_dice4",
			"e_dice5",
			"e_dice6",
			"e_india",
			"e_luv",
			"e_russia",
			"e_virgin",
			"e_mobile",
			"e_mail",
			"e_chinese",
			"e_antenna1",
			"e_antenna2",
			"e_antenna3",
			"e_hum",
			"e_abs",
			"e_oops",
			"e_spit",
			"e_ene",
			"e_panic",
			"e_whisp",
			"e_yut",
			"e_yut2",
			"e_yut3",
			"e_yut4",
			"e_yut5",
			"e_yut6",
			"e_yut7"
		};

		public static Dictionary<int, string> Effects = new Dictionary<int, string>();

		public static string GetEFST(int id) {
			if (Effects.ContainsKey(id))
				return Effects[id];

			return "" + id;
		}

		static Constants() {
			Effects[0] = "EFST_PROVOKE";
			Effects[1] = "EFST_ENDURE";
			Effects[2] = "EFST_TWOHANDQUICKEN";
			Effects[3] = "EFST_CONCENTRATION";
			Effects[4] = "EFST_HIDING";
			Effects[5] = "EFST_CLOAKING";
			Effects[6] = "EFST_ENCHANTPOISON";
			Effects[7] = "EFST_POISONREACT";
			Effects[8] = "EFST_QUAGMIRE";
			Effects[9] = "EFST_ANGELUS";
			Effects[10] = "EFST_BLESSING";
			Effects[11] = "EFST_CRUCIS";
			Effects[12] = "EFST_INC_AGI";
			Effects[13] = "EFST_DEC_AGI";
			Effects[14] = "EFST_SLOWPOISON";
			Effects[15] = "EFST_IMPOSITIO";
			Effects[16] = "EFST_SUFFRAGIUM";
			Effects[17] = "EFST_ASPERSIO";
			Effects[18] = "EFST_BENEDICTIO";
			Effects[19] = "EFST_KYRIE";
			Effects[20] = "EFST_MAGNIFICAT";
			Effects[21] = "EFST_GLORIA";
			Effects[22] = "EFST_LEXAETERNA";
			Effects[23] = "EFST_ADRENALINE";
			Effects[24] = "EFST_WEAPONPERFECT";
			Effects[25] = "EFST_OVERTHRUST";
			Effects[26] = "EFST_MAXIMIZE";
			Effects[27] = "EFST_RIDING";
			Effects[28] = "EFST_FALCON";
			Effects[29] = "EFST_TRICKDEAD";
			Effects[30] = "EFST_SHOUT";
			Effects[31] = "EFST_ENERGYCOAT";
			Effects[32] = "EFST_BROKENARMOR";
			Effects[33] = "EFST_BROKENWEAPON";
			Effects[34] = "EFST_ILLUSION";
			Effects[35] = "EFST_WEIGHTOVER50";
			Effects[36] = "EFST_WEIGHTOVER90";
			Effects[37] = "EFST_ATTHASTE_POTION1";
			Effects[38] = "EFST_ATTHASTE_POTION2";
			Effects[39] = "EFST_ATTHASTE_POTION3";
			Effects[40] = "EFST_ATTHASTE_INFINITY";
			Effects[41] = "EFST_MOVHASTE_POTION";
			Effects[42] = "EFST_MOVHASTE_INFINITY";
			Effects[43] = "EFST_AUTOCOUNTER";
			Effects[44] = "EFST_SPLASHER";
			Effects[45] = "EFST_ANKLESNARE";
			Effects[46] = "EFST_POSTDELAY";
			Effects[47] = "EFST_NOACTION";
			Effects[48] = "EFST_IMPOSSIBLEPICKUP";
			Effects[49] = "EFST_BARRIER";
			Effects[50] = "EFST_NOEQUIPWEAPON";
			Effects[51] = "EFST_NOEQUIPSHIELD";
			Effects[52] = "EFST_NOEQUIPARMOR";
			Effects[53] = "EFST_NOEQUIPHELM";
			Effects[54] = "EFST_PROTECTWEAPON";
			Effects[55] = "EFST_PROTECTSHIELD";
			Effects[56] = "EFST_PROTECTARMOR";
			Effects[57] = "EFST_PROTECTHELM";
			Effects[58] = "EFST_AUTOGUARD";
			Effects[59] = "EFST_REFLECTSHIELD";
			Effects[60] = "EFST_DEVOTION";
			Effects[61] = "EFST_PROVIDENCE";
			Effects[62] = "EFST_DEFENDER";
			Effects[63] = "EFST_MAGICROD";
			Effects[64] = "EFST_WEAPONPROPERTY";
			Effects[65] = "EFST_AUTOSPELL";
			Effects[66] = "EFST_SPECIALZONE";
			Effects[67] = "EFST_MASK";
			Effects[68] = "EFST_SPEARQUICKEN";
			Effects[69] = "EFST_BDPLAYING";
			Effects[70] = "EFST_WHISTLE";
			Effects[71] = "EFST_ASSASSINCROSS";
			Effects[72] = "EFST_POEMBRAGI";
			Effects[73] = "EFST_APPLEIDUN";
			Effects[74] = "EFST_HUMMING";
			Effects[75] = "EFST_DONTFORGETME";
			Effects[76] = "EFST_FORTUNEKISS";
			Effects[77] = "EFST_SERVICEFORYOU";
			Effects[78] = "EFST_RICHMANKIM";
			Effects[79] = "EFST_ETERNALCHAOS";
			Effects[80] = "EFST_DRUMBATTLEFIELD";
			Effects[81] = "EFST_RINGNIBELUNGEN";
			Effects[82] = "EFST_ROKISWEIL";
			Effects[83] = "EFST_INTOABYSS";
			Effects[84] = "EFST_SIEGFRIED";
			Effects[85] = "EFST_BLADESTOP";
			Effects[86] = "EFST_EXPLOSIONSPIRITS";
			Effects[87] = "EFST_STEELBODY";
			Effects[88] = "EFST_EXTREMITYFIST";
			Effects[89] = "EFST_COMBOATTACK";
			Effects[90] = "EFST_PROPERTYFIRE";
			Effects[91] = "EFST_PROPERTYWATER";
			Effects[92] = "EFST_PROPERTYWIND";
			Effects[93] = "EFST_PROPERTYGROUND";
			Effects[94] = "EFST_MAGICATTACK";
			Effects[95] = "EFST_STOP";
			Effects[96] = "EFST_WEAPONBRAKER";
			Effects[97] = "EFST_PROPERTYUNDEAD";
			Effects[98] = "EFST_POWERUP";
			Effects[99] = "EFST_AGIUP";
			Effects[100] = "EFST_SIEGEMODE";
			Effects[101] = "EFST_INVISIBLE";
			Effects[102] = "EFST_STATUSONE";
			Effects[103] = "EFST_AURABLADE";
			Effects[104] = "EFST_PARRYING";
			Effects[105] = "EFST_LKCONCENTRATION";
			Effects[106] = "EFST_TENSIONRELAX";
			Effects[107] = "EFST_BERSERK";
			Effects[108] = "EFST_SACRIFICE";
			Effects[109] = "EFST_GOSPEL";
			Effects[110] = "EFST_ASSUMPTIO";
			Effects[111] = "EFST_BASILICA";
			Effects[112] = "EFST_GROUNDMAGIC";
			Effects[113] = "EFST_MAGICPOWER";
			Effects[114] = "EFST_EDP";
			Effects[115] = "EFST_TRUESIGHT";
			Effects[116] = "EFST_WINDWALK";
			Effects[117] = "EFST_MELTDOWN";
			Effects[118] = "EFST_CARTBOOST";
			Effects[119] = "EFST_CHASEWALK";
			Effects[120] = "EFST_SWORDREJECT";
			Effects[121] = "EFST_MARIONETTE_MASTER";
			Effects[122] = "EFST_MARIONETTE";
			Effects[123] = "EFST_MOON";
			Effects[124] = "EFST_BLOODING";
			Effects[125] = "EFST_JOINTBEAT";
			Effects[126] = "EFST_MINDBREAKER";
			Effects[127] = "EFST_MEMORIZE";
			Effects[128] = "EFST_FOGWALL";
			Effects[129] = "EFST_SPIDERWEB";
			Effects[130] = "EFST_PROTECTEXP";
			Effects[131] = "EFST_SUB_WEAPONPROPERTY";
			Effects[132] = "EFST_AUTOBERSERK";
			Effects[133] = "EFST_RUN";
			Effects[134] = "EFST_TING";
			Effects[135] = "EFST_STORMKICK_ON";
			Effects[136] = "EFST_STORMKICK_READY";
			Effects[137] = "EFST_DOWNKICK_ON";
			Effects[138] = "EFST_DOWNKICK_READY";
			Effects[139] = "EFST_TURNKICK_ON";
			Effects[140] = "EFST_TURNKICK_READY";
			Effects[141] = "EFST_COUNTER_ON";
			Effects[142] = "EFST_COUNTER_READY";
			Effects[143] = "EFST_DODGE_ON";
			Effects[144] = "EFST_DODGE_READY";
			Effects[145] = "EFST_STRUP";
			Effects[146] = "EFST_PROPERTYDARK";
			Effects[147] = "EFST_ADRENALINE2";
			Effects[148] = "EFST_PROPERTYTELEKINESIS";
			Effects[149] = "EFST_SOULLINK";
			Effects[150] = "EFST_PLUSATTACKPOWER";
			Effects[151] = "EFST_PLUSMAGICPOWER";
			Effects[152] = "EFST_DEVIL1";
			Effects[153] = "EFST_KAITE";
			Effects[154] = "EFST_SWOO";
			Effects[155] = "EFST_STAR2";
			Effects[156] = "EFST_KAIZEL";
			Effects[157] = "EFST_KAAHI";
			Effects[158] = "EFST_KAUPE";
			Effects[159] = "EFST_SMA_READY";
			Effects[160] = "EFST_SKE";
			Effects[161] = "EFST_ONEHANDQUICKEN";
			Effects[162] = "EFST_FRIEND";
			Effects[163] = "EFST_FRIENDUP";
			Effects[164] = "EFST_SG_WARM";
			Effects[165] = "EFST_SG_SUN_WARM";
			Effects[166] = "EFST_SG_MOON_WARM";
			Effects[167] = "EFST_SG_STAR_WARM";
			Effects[168] = "EFST_EMOTION";
			Effects[169] = "EFST_SUN_COMFORT";
			Effects[170] = "EFST_MOON_COMFORT";
			Effects[171] = "EFST_STAR_COMFORT";
			Effects[172] = "EFST_EXPUP";
			Effects[173] = "EFST_GDSKILL_BATTLEORDER";
			Effects[174] = "EFST_GDSKILL_REGENERATION";
			Effects[175] = "EFST_GDSKILL_POSTDELAY";
			Effects[176] = "EFST_RESISTHANDICAP";
			Effects[177] = "EFST_MAXHPPERCENT";
			Effects[178] = "EFST_MAXSPPERCENT";
			Effects[179] = "EFST_DEFENCE";
			Effects[180] = "EFST_SLOWDOWN";
			Effects[181] = "EFST_PRESERVE";
			Effects[182] = "EFST_CHASEWALK2";
			Effects[183] = "EFST_NOT_EXTREMITYFIST";
			Effects[184] = "EFST_CLAIRVOYANCE";
			Effects[185] = "EFST_MOVESLOW_POTION";
			Effects[186] = "EFST_DOUBLECASTING";
			Effects[187] = "EFST_GRAVITATION";
			Effects[188] = "EFST_OVERTHRUSTMAX";
			Effects[189] = "EFST_LONGING";
			Effects[190] = "EFST_HERMODE";
			Effects[191] = "EFST_TAROTCARD";
			Effects[192] = "EFST_HLIF_AVOID";
			Effects[193] = "EFST_HFLI_FLEET";
			Effects[194] = "EFST_HFLI_SPEED";
			Effects[195] = "EFST_HLIF_CHANGE";
			Effects[196] = "EFST_HAMI_BLOODLUST";
			Effects[197] = "EFST_CR_SHRINK";
			Effects[198] = "EFST_WZ_SIGHTBLASTER";
			Effects[199] = "EFST_DC_WINKCHARM";
			Effects[200] = "EFST_RG_CCONFINE_M";
			Effects[201] = "EFST_RG_CCONFINE_S";
			Effects[202] = "EFST_DISABLEMOVE";
			Effects[203] = "EFST_GS_MADNESSCANCEL";
			Effects[204] = "EFST_GS_GATLINGFEVER";
			Effects[205] = "EFST_EARTHSCROLL";
			Effects[206] = "EFST_NJ_UTSUSEMI";
			Effects[207] = "EFST_NJ_BUNSINJYUTSU";
			Effects[208] = "EFST_NJ_NEN";
			Effects[209] = "EFST_GS_ADJUSTMENT";
			Effects[210] = "EFST_GS_ACCURACY";
			Effects[211] = "EFST_NJ_SUITON";
			Effects[212] = "EFST_PET";
			Effects[213] = "EFST_MENTAL";
			Effects[214] = "EFST_EXPMEMORY";
			Effects[215] = "EFST_PERFORMANCE";
			Effects[216] = "EFST_GAIN";
			Effects[217] = "EFST_GRIFFON";
			Effects[218] = "EFST_DRIFT";
			Effects[219] = "EFST_WALLSHIFT";
			Effects[220] = "EFST_REINCARNATION";
			Effects[221] = "EFST_PATTACK";
			Effects[222] = "EFST_PSPEED";
			Effects[223] = "EFST_PDEFENSE";
			Effects[224] = "EFST_PCRITICAL";
			Effects[225] = "EFST_RANKING";
			Effects[226] = "EFST_PTRIPLE";
			Effects[227] = "EFST_DENERGY";
			Effects[228] = "EFST_WAVE1";
			Effects[229] = "EFST_WAVE2";
			Effects[230] = "EFST_WAVE3";
			Effects[231] = "EFST_WAVE4";
			Effects[232] = "EFST_DAURA";
			Effects[233] = "EFST_DFREEZER";
			Effects[234] = "EFST_DPUNISH";
			Effects[235] = "EFST_DBARRIER";
			Effects[236] = "EFST_DWARNING";
			Effects[237] = "EFST_MOUSEWHEEL";
			Effects[238] = "EFST_DGAUGE";
			Effects[239] = "EFST_DACCEL";
			Effects[240] = "EFST_DBLOCK";
			Effects[241] = "EFST_FOOD_STR";
			Effects[242] = "EFST_FOOD_AGI";
			Effects[243] = "EFST_FOOD_VIT";
			Effects[244] = "EFST_FOOD_DEX";
			Effects[245] = "EFST_FOOD_INT";
			Effects[246] = "EFST_FOOD_LUK";
			Effects[247] = "EFST_FOOD_BASICAVOIDANCE";
			Effects[248] = "EFST_FOOD_BASICHIT";
			Effects[249] = "EFST_FOOD_CRITICALSUCCESSVALUE";
			Effects[250] = "EFST_CASH_PLUSEXP";
			Effects[251] = "EFST_CASH_DEATHPENALTY";
			Effects[252] = "EFST_CASH_RECEIVEITEM";
			Effects[253] = "EFST_CASH_BOSS_ALARM";
			Effects[254] = "EFST_DA_ENERGY";
			Effects[255] = "EFST_DA_FIRSTSLOT";
			Effects[256] = "EFST_DA_HEADDEF";
			Effects[257] = "EFST_DA_SPACE";
			Effects[258] = "EFST_DA_TRANSFORM";
			Effects[259] = "EFST_DA_ITEMREBUILD";
			Effects[260] = "EFST_DA_ILLUSION";
			Effects[261] = "EFST_DA_DARKPOWER";
			Effects[262] = "EFST_DA_EARPLUG";
			Effects[263] = "EFST_DA_CONTRACT";
			Effects[264] = "EFST_DA_BLACK";
			Effects[265] = "EFST_DA_MAGICCART";
			Effects[266] = "EFST_CRYSTAL";
			Effects[267] = "EFST_DA_REBUILD";
			Effects[268] = "EFST_DA_EDARKNESS";
			Effects[269] = "EFST_DA_EGUARDIAN";
			Effects[270] = "EFST_DA_TIMEOUT";
			Effects[271] = "EFST_FOOD_STR_CASH";
			Effects[272] = "EFST_FOOD_AGI_CASH";
			Effects[273] = "EFST_FOOD_VIT_CASH";
			Effects[274] = "EFST_FOOD_DEX_CASH";
			Effects[275] = "EFST_FOOD_INT_CASH";
			Effects[276] = "EFST_FOOD_LUK_CASH";
			Effects[277] = "EFST_MER_FLEE";
			Effects[278] = "EFST_MER_ATK";
			Effects[279] = "EFST_MER_HP";
			Effects[280] = "EFST_MER_SP";
			Effects[281] = "EFST_MER_HIT";
			Effects[282] = "EFST_SLOWCAST";
			Effects[283] = "EFST_MAGICMIRROR";
			Effects[284] = "EFST_STONESKIN";
			Effects[285] = "EFST_ANTIMAGIC";
			Effects[286] = "EFST_CRITICALWOUND";
			Effects[287] = "EFST_NPC_DEFENDER";
			Effects[288] = "EFST_NOACTION_WAIT";
			Effects[289] = "EFST_MOVHASTE_HORSE";
			Effects[290] = "EFST_PROTECT_DEF";
			Effects[291] = "EFST_PROTECT_MDEF";
			Effects[292] = "EFST_HEALPLUS";
			Effects[293] = "EFST_S_LIFEPOTION";
			Effects[294] = "EFST_L_LIFEPOTION";
			Effects[295] = "EFST_CRITICALPERCENT";
			Effects[296] = "EFST_PLUSAVOIDVALUE";
			Effects[297] = "EFST_ATKER_ASPD";
			Effects[298] = "EFST_TARGET_ASPD";
			Effects[299] = "EFST_ATKER_MOVESPEED";
			Effects[300] = "EFST_ATKER_BLOOD";
			Effects[301] = "EFST_TARGET_BLOOD";
			Effects[302] = "EFST_ARMOR_PROPERTY";
			Effects[303] = "EFST_REUSE_LIMIT_A";
			Effects[304] = "EFST_HELLPOWER";
			Effects[305] = "EFST_STEAMPACK";
			Effects[306] = "EFST_REUSE_LIMIT_B";
			Effects[307] = "EFST_REUSE_LIMIT_C";
			Effects[308] = "EFST_REUSE_LIMIT_D";
			Effects[309] = "EFST_REUSE_LIMIT_E";
			Effects[310] = "EFST_REUSE_LIMIT_F";
			Effects[311] = "EFST_INVINCIBLE";
			Effects[312] = "EFST_CASH_PLUSONLYJOBEXP";
			Effects[313] = "EFST_PARTYFLEE";
			Effects[314] = "EFST_ANGEL_PROTECT";
			Effects[315] = "EFST_ENDURE_MDEF";
			Effects[316] = "EFST_ENCHANTBLADE";
			Effects[317] = "EFST_DEATHBOUND";
			Effects[318] = "EFST_REFRESH";
			Effects[319] = "EFST_GIANTGROWTH";
			Effects[320] = "EFST_STONEHARDSKIN";
			Effects[321] = "EFST_VITALITYACTIVATION";
			Effects[322] = "EFST_FIGHTINGSPIRIT";
			Effects[323] = "EFST_ABUNDANCE";
			Effects[324] = "EFST_REUSE_MILLENNIUMSHIELD";
			Effects[325] = "EFST_REUSE_CRUSHSTRIKE";
			Effects[326] = "EFST_REUSE_REFRESH";
			Effects[327] = "EFST_REUSE_STORMBLAST";
			Effects[328] = "EFST_VENOMIMPRESS";
			Effects[329] = "EFST_EPICLESIS";
			Effects[330] = "EFST_ORATIO";
			Effects[331] = "EFST_LAUDAAGNUS";
			Effects[332] = "EFST_LAUDARAMUS";
			Effects[333] = "EFST_CLOAKINGEXCEED";
			Effects[334] = "EFST_HALLUCINATIONWALK";
			Effects[335] = "EFST_HALLUCINATIONWALK_POSTDELAY";
			Effects[336] = "EFST_RENOVATIO";
			Effects[337] = "EFST_WEAPONBLOCKING";
			Effects[338] = "EFST_WEAPONBLOCKING_POSTDELAY";
			Effects[339] = "EFST_ROLLINGCUTTER";
			Effects[340] = "EFST_EXPIATIO";
			Effects[341] = "EFST_POISONINGWEAPON";
			Effects[342] = "EFST_TOXIN";
			Effects[343] = "EFST_PARALYSE";
			Effects[344] = "EFST_VENOMBLEED";
			Effects[345] = "EFST_MAGICMUSHROOM";
			Effects[346] = "EFST_DEATHHURT";
			Effects[347] = "EFST_PYREXIA";
			Effects[348] = "EFST_OBLIVIONCURSE";
			Effects[349] = "EFST_LEECHESEND";
			Effects[350] = "EFST_DUPLELIGHT";
			Effects[351] = "EFST_FROSTMISTY";
			Effects[352] = "EFST_FEARBREEZE";
			Effects[353] = "EFST_ELECTRICSHOCKER";
			Effects[354] = "EFST_MARSHOFABYSS";
			Effects[355] = "EFST_RECOGNIZEDSPELL";
			Effects[356] = "EFST_STASIS";
			Effects[357] = "EFST_WUGRIDER";
			Effects[358] = "EFST_WUGDASH";
			Effects[359] = "EFST_WUGBITE";
			Effects[360] = "EFST_CAMOUFLAGE";
			Effects[361] = "EFST_ACCELERATION";
			Effects[362] = "EFST_HOVERING";
			Effects[363] = "EFST_SUMMON1";
			Effects[364] = "EFST_SUMMON2";
			Effects[365] = "EFST_SUMMON3";
			Effects[366] = "EFST_SUMMON4";
			Effects[367] = "EFST_SUMMON5";
			Effects[368] = "EFST_MVPCARD_TAOGUNKA";
			Effects[369] = "EFST_MVPCARD_MISTRESS";
			Effects[370] = "EFST_MVPCARD_ORCHERO";
			Effects[371] = "EFST_MVPCARD_ORCLORD";
			Effects[372] = "EFST_OVERHEAT_LIMITPOINT";
			Effects[373] = "EFST_OVERHEAT";
			Effects[374] = "EFST_SHAPESHIFT";
			Effects[375] = "EFST_INFRAREDSCAN";
			Effects[376] = "EFST_MAGNETICFIELD";
			Effects[377] = "EFST_NEUTRALBARRIER";
			Effects[378] = "EFST_NEUTRALBARRIER_MASTER";
			Effects[379] = "EFST_STEALTHFIELD";
			Effects[380] = "EFST_STEALTHFIELD_MASTER";
			Effects[381] = "EFST_MANU_ATK";
			Effects[382] = "EFST_MANU_DEF";
			Effects[383] = "EFST_SPL_ATK";
			Effects[384] = "EFST_SPL_DEF";
			Effects[385] = "EFST_REPRODUCE";
			Effects[386] = "EFST_MANU_MATK";
			Effects[387] = "EFST_SPL_MATK";
			Effects[388] = "EFST_STR_SCROLL";
			Effects[389] = "EFST_INT_SCROLL";
			Effects[390] = "EFST_LG_REFLECTDAMAGE";
			Effects[391] = "EFST_FORCEOFVANGUARD";
			Effects[392] = "EFST_BUCHEDENOEL";
			Effects[393] = "EFST_AUTOSHADOWSPELL";
			Effects[394] = "EFST_SHADOWFORM";
			Effects[395] = "EFST_RAID";
			Effects[396] = "EFST_SHIELDSPELL_DEF";
			Effects[397] = "EFST_SHIELDSPELL_MDEF";
			Effects[398] = "EFST_SHIELDSPELL_REF";
			Effects[399] = "EFST_BODYPAINT";
			Effects[400] = "EFST_EXEEDBREAK";
			Effects[401] = "EFST_ADORAMUS";
			Effects[402] = "EFST_PRESTIGE";
			Effects[403] = "EFST_INVISIBILITY";
			Effects[404] = "EFST_DEADLYINFECT";
			Effects[405] = "EFST_BANDING";
			Effects[406] = "EFST_EARTHDRIVE";
			Effects[407] = "EFST_INSPIRATION";
			Effects[408] = "EFST_ENERVATION";
			Effects[409] = "EFST_GROOMY";
			Effects[410] = "EFST_RAISINGDRAGON";
			Effects[411] = "EFST_IGNORANCE";
			Effects[412] = "EFST_LAZINESS";
			Effects[413] = "EFST_LIGHTNINGWALK";
			Effects[414] = "EFST_ACARAJE";
			Effects[415] = "EFST_UNLUCKY";
			Effects[416] = "EFST_CURSEDCIRCLE_ATKER";
			Effects[417] = "EFST_CURSEDCIRCLE_TARGET";
			Effects[418] = "EFST_WEAKNESS";
			Effects[419] = "EFST_CRESCENTELBOW";
			Effects[420] = "EFST_NOEQUIPACCESSARY";
			Effects[421] = "EFST_STRIPACCESSARY";
			Effects[422] = "EFST_MANHOLE";
			Effects[423] = "EFST_POPECOOKIE";
			Effects[424] = "EFST_FALLENEMPIRE";
			Effects[425] = "EFST_GENTLETOUCH_ENERGYGAIN";
			Effects[426] = "EFST_GENTLETOUCH_CHANGE";
			Effects[427] = "EFST_GENTLETOUCH_REVITALIZE";
			Effects[428] = "EFST_BLOODYLUST";
			Effects[429] = "EFST_SWING";
			Effects[430] = "EFST_SYMPHONY_LOVE";
			Effects[431] = "EFST_PROPERTYWALK";
			Effects[432] = "EFST_SPELLFIST";
			Effects[433] = "EFST_NETHERWORLD";
			Effects[434] = "EFST_SIREN";
			Effects[435] = "EFST_DEEP_SLEEP";
			Effects[436] = "EFST_SIRCLEOFNATURE";
			Effects[437] = "EFST_COLD";
			Effects[438] = "EFST_GLOOMYDAY";
			Effects[439] = "EFST_SONG_OF_MANA";
			Effects[440] = "EFST_CLOUD_KILL";
			Effects[441] = "EFST_DANCE_WITH_WUG";
			Effects[442] = "EFST_RUSH_WINDMILL";
			Effects[443] = "EFST_ECHOSONG";
			Effects[444] = "EFST_HARMONIZE";
			Effects[445] = "EFST_STRIKING";
			Effects[446] = "EFST_WARMER";
			Effects[447] = "EFST_MOONLIT_SERENADE";
			Effects[448] = "EFST_SATURDAY_NIGHT_FEVER";
			Effects[449] = "EFST_SITDOWN_FORCE";
			Effects[450] = "EFST_ANALYZE";
			Effects[451] = "EFST_LERADS_DEW";
			Effects[452] = "EFST_MELODYOFSINK";
			Effects[453] = "EFST_BEYOND_OF_WARCRY";
			Effects[454] = "EFST_UNLIMITED_HUMMING_VOICE";
			Effects[455] = "EFST_SPELLBOOK1";
			Effects[456] = "EFST_SPELLBOOK2";
			Effects[457] = "EFST_SPELLBOOK3";
			Effects[458] = "EFST_FREEZE_SP";
			Effects[459] = "EFST_GN_TRAINING_SWORD";
			Effects[460] = "EFST_GN_REMODELING_CART";
			Effects[461] = "EFST_GN_CARTBOOST";
			Effects[462] = "EFST_FIXEDCASTINGTM_REDUCE";
			Effects[463] = "EFST_THORNS_TRAP";
			Effects[464] = "EFST_BLOOD_SUCKER";
			Effects[465] = "EFST_SPORE_EXPLOSION";
			Effects[466] = "EFST_DEMONIC_FIRE";
			Effects[467] = "EFST_FIRE_EXPANSION_SMOKE_POWDER";
			Effects[468] = "EFST_FIRE_EXPANSION_TEAR_GAS";
			Effects[469] = "EFST_BLOCKING_PLAY";
			Effects[470] = "EFST_MANDRAGORA";
			Effects[471] = "EFST_ACTIVATE";
			Effects[472] = "EFST_AB_SECRAMENT";
			Effects[473] = "EFST_ASSUMPTIO2";
			Effects[474] = "EFST_TK_SEVENWIND";
			Effects[475] = "EFST_LIMIT_ODINS_RECALL";
			Effects[476] = "EFST_STOMACHACHE";
			Effects[477] = "EFST_MYSTERIOUS_POWDER";
			Effects[478] = "EFST_MELON_BOMB";
			Effects[479] = "EFST_BANANA_BOMB_SITDOWN_POSTDELAY";
			Effects[480] = "EFST_PROMOTE_HEALTH_RESERCH";
			Effects[481] = "EFST_ENERGY_DRINK_RESERCH";
			Effects[482] = "EFST_EXTRACT_WHITE_POTION_Z";
			Effects[483] = "EFST_VITATA_500";
			Effects[484] = "EFST_EXTRACT_SALAMINE_JUICE";
			Effects[485] = "EFST_BOOST500";
			Effects[486] = "EFST_FULL_SWING_K";
			Effects[487] = "EFST_MANA_PLUS";
			Effects[488] = "EFST_MUSTLE_M";
			Effects[489] = "EFST_LIFE_FORCE_F";
			Effects[490] = "EFST_VACUUM_EXTREME";
			Effects[491] = "EFST_SAVAGE_STEAK";
			Effects[492] = "EFST_COCKTAIL_WARG_BLOOD";
			Effects[493] = "EFST_MINOR_BBQ";
			Effects[494] = "EFST_SIROMA_ICE_TEA";
			Effects[495] = "EFST_DROCERA_HERB_STEAMED";
			Effects[496] = "EFST_PUTTI_TAILS_NOODLES";
			Effects[497] = "EFST_BANANA_BOMB";
			Effects[498] = "EFST_SUMMON_AGNI";
			Effects[499] = "EFST_SPELLBOOK4";
			Effects[500] = "EFST_SPELLBOOK5";
			Effects[501] = "EFST_SPELLBOOK6";
			Effects[502] = "EFST_SPELLBOOK7";
			Effects[503] = "EFST_ELEMENTAL_AGGRESSIVE";
			Effects[504] = "EFST_RETURN_TO_ELDICASTES";
			Effects[505] = "EFST_BANDING_DEFENCE";
			Effects[506] = "EFST_SKELSCROLL";
			Effects[507] = "EFST_DISTRUCTIONSCROLL";
			Effects[508] = "EFST_ROYALSCROLL";
			Effects[509] = "EFST_IMMUNITYSCROLL";
			Effects[510] = "EFST_MYSTICSCROLL";
			Effects[511] = "EFST_BATTLESCROLL";
			Effects[512] = "EFST_ARMORSCROLL";
			Effects[513] = "EFST_FREYJASCROLL";
			Effects[514] = "EFST_SOULSCROLL";
			Effects[515] = "EFST_CIRCLE_OF_FIRE";
			Effects[516] = "EFST_CIRCLE_OF_FIRE_OPTION";
			Effects[517] = "EFST_FIRE_CLOAK";
			Effects[518] = "EFST_FIRE_CLOAK_OPTION";
			Effects[519] = "EFST_WATER_SCREEN";
			Effects[520] = "EFST_WATER_SCREEN_OPTION";
			Effects[521] = "EFST_WATER_DROP";
			Effects[522] = "EFST_WATER_DROP_OPTION";
			Effects[523] = "EFST_WIND_STEP";
			Effects[524] = "EFST_WIND_STEP_OPTION";
			Effects[525] = "EFST_WIND_CURTAIN";
			Effects[526] = "EFST_WIND_CURTAIN_OPTION";
			Effects[527] = "EFST_WATER_BARRIER";
			Effects[528] = "EFST_ZEPHYR";
			Effects[529] = "EFST_SOLID_SKIN";
			Effects[530] = "EFST_SOLID_SKIN_OPTION";
			Effects[531] = "EFST_STONE_SHIELD";
			Effects[532] = "EFST_STONE_SHIELD_OPTION";
			Effects[533] = "EFST_POWER_OF_GAIA";
			Effects[534] = "EFST_EL_WAIT";
			Effects[535] = "EFST_EL_PASSIVE";
			Effects[536] = "EFST_EL_DEFENSIVE";
			Effects[537] = "EFST_EL_OFFENSIVE";
			Effects[538] = "EFST_EL_COST";
			Effects[539] = "EFST_PYROTECHNIC";
			Effects[540] = "EFST_PYROTECHNIC_OPTION";
			Effects[541] = "EFST_HEATER";
			Effects[542] = "EFST_HEATER_OPTION";
			Effects[543] = "EFST_TROPIC";
			Effects[544] = "EFST_TROPIC_OPTION";
			Effects[545] = "EFST_AQUAPLAY";
			Effects[546] = "EFST_AQUAPLAY_OPTION";
			Effects[547] = "EFST_COOLER";
			Effects[548] = "EFST_COOLER_OPTION";
			Effects[549] = "EFST_CHILLY_AIR";
			Effects[550] = "EFST_CHILLY_AIR_OPTION";
			Effects[551] = "EFST_GUST";
			Effects[552] = "EFST_GUST_OPTION";
			Effects[553] = "EFST_BLAST";
			Effects[554] = "EFST_BLAST_OPTION";
			Effects[555] = "EFST_WILD_STORM";
			Effects[556] = "EFST_WILD_STORM_OPTION";
			Effects[557] = "EFST_PETROLOGY";
			Effects[558] = "EFST_PETROLOGY_OPTION";
			Effects[559] = "EFST_CURSED_SOIL";
			Effects[560] = "EFST_CURSED_SOIL_OPTION";
			Effects[561] = "EFST_UPHEAVAL";
			Effects[562] = "EFST_UPHEAVAL_OPTION";
			Effects[563] = "EFST_TIDAL_WEAPON";
			Effects[564] = "EFST_TIDAL_WEAPON_OPTION";
			Effects[565] = "EFST_ROCK_CRUSHER";
			Effects[566] = "EFST_ROCK_CRUSHER_ATK";
			Effects[567] = "EFST_FIRE_INSIGNIA";
			Effects[568] = "EFST_WATER_INSIGNIA";
			Effects[569] = "EFST_WIND_INSIGNIA";
			Effects[570] = "EFST_EARTH_INSIGNIA";
			Effects[571] = "EFST_EQUIPED_FLOOR";
			Effects[572] = "EFST_GUARDIAN_RECALL";
			Effects[573] = "EFST_MORA_BUFF";
			Effects[574] = "EFST_REUSE_LIMIT_G";
			Effects[575] = "EFST_REUSE_LIMIT_H";
			Effects[576] = "EFST_NEEDLE_OF_PARALYZE";
			Effects[577] = "EFST_PAIN_KILLER";
			Effects[578] = "EFST_G_LIFEPOTION";
			Effects[579] = "EFST_VITALIZE_POTION";
			Effects[580] = "EFST_LIGHT_OF_REGENE";
			Effects[581] = "EFST_OVERED_BOOST";
			Effects[582] = "EFST_SILENT_BREEZE";
			Effects[583] = "EFST_ODINS_POWER";
			Effects[584] = "EFST_STYLE_CHANGE";
			Effects[585] = "EFST_SONIC_CLAW_POSTDELAY";
			Effects[596] = "EFST_SILVERVEIN_RUSH_POSTDELAY";
			Effects[597] = "EFST_MIDNIGHT_FRENZY_POSTDELAY";
			Effects[598] = "EFST_GOLDENE_FERSE";
			Effects[599] = "EFST_ANGRIFFS_MODUS";
			Effects[600] = "EFST_TINDER_BREAKER";
			Effects[601] = "EFST_TINDER_BREAKER_POSTDELAY";
			Effects[602] = "EFST_CBC";
			Effects[603] = "EFST_CBC_POSTDELAY";
			Effects[604] = "EFST_EQC";
			Effects[605] = "EFST_MAGMA_FLOW";
			Effects[606] = "EFST_GRANITIC_ARMOR";
			Effects[607] = "EFST_PYROCLASTIC";
			Effects[608] = "EFST_VOLCANIC_ASH";
			Effects[609] = "EFST_SPIRITS_SAVEINFO1";
			Effects[610] = "EFST_SPIRITS_SAVEINFO2";
			Effects[611] = "EFST_MAGIC_CANDY";
			Effects[613] = "EFST_ALL_RIDING";
			Effects[615] = "EFST_MACRO";
			Effects[616] = "EFST_MACRO_POSTDELAY";
			Effects[617] = "EFST_BEER_BOTTLE_CAP";
			Effects[618] = "EFST_OVERLAPEXPUP";
			Effects[619] = "EFST_PC_IZ_DUN05";
			Effects[620] = "EFST_CRUSHSTRIKE";
			Effects[621] = "EFST_MONSTER_TRANSFORM";
			Effects[622] = "EFST_SIT";
			Effects[623] = "EFST_ONAIR";
			Effects[624] = "EFST_MTF_ASPD";
			Effects[625] = "EFST_MTF_RANGEATK";
			Effects[626] = "EFST_MTF_MATK";
			Effects[627] = "EFST_MTF_MLEATKED";
			Effects[628] = "EFST_MTF_CRIDAMAGE";
			Effects[629] = "EFST_REUSE_LIMIT_MTF";
			Effects[630] = "EFST_MACRO_PERMIT";
			Effects[631] = "EFST_MACRO_PLAY";
			Effects[632] = "EFST_SKF_CAST";
			Effects[633] = "EFST_SKF_ASPD";
			Effects[634] = "EFST_SKF_ATK";
			Effects[635] = "EFST_SKF_MATK";
			Effects[636] = "EFST_REWARD_PLUSONLYJOBEXP";
			Effects[637] = "EFST_HANDICAPSTATE_NORECOVER";
			Effects[638] = "EFST_SET_NUM_DEF";
			Effects[639] = "EFST_SET_NUM_MDEF";
			Effects[640] = "EFST_SET_PER_DEF";
			Effects[641] = "EFST_SET_PER_MDEF";
			Effects[642] = "EFST_PARTYBOOKING_SEARCH_DEALY";
			Effects[643] = "EFST_PARTYBOOKING_REGISTER_DEALY";
			Effects[644] = "EFST_PERIOD_TIME_CHECK_DETECT_SKILL";
			Effects[645] = "EFST_KO_JYUMONJIKIRI";
			Effects[646] = "EFST_MEIKYOUSISUI";
			Effects[647] = "EFST_ATTHASTE_CASH";
			Effects[648] = "EFST_EQUIPPED_DIVINE_ARMOR";
			Effects[649] = "EFST_EQUIPPED_HOLY_ARMOR";
			Effects[650] = "EFST_2011RWC";
			Effects[651] = "EFST_KYOUGAKU";
			Effects[652] = "EFST_IZAYOI";
			Effects[653] = "EFST_ZENKAI";
			Effects[654] = "EFST_KG_KAGEHUMI";
			Effects[655] = "EFST_KYOMU";
			Effects[656] = "EFST_KAGEMUSYA";
			Effects[657] = "EFST_ZANGETSU";
			Effects[658] = "EFST_PHI_DEMON";
			Effects[659] = "EFST_GENSOU";
			Effects[660] = "EFST_AKAITSUKI";
			Effects[661] = "EFST_TETANY";
			Effects[662] = "EFST_GM_BATTLE";
			Effects[663] = "EFST_GM_BATTLE2";
			Effects[664] = "EFST_2011RWC_SCROLL";
			Effects[665] = "EFST_ACTIVE_MONSTER_TRANSFORM";
			Effects[666] = "EFST_MYSTICPOWDER";
			Effects[667] = "EFST_ECLAGE_RECALL";
			Effects[668] = "EFST_ENTRY_QUEUE_APPLY_DELAY";
			Effects[669] = "EFST_REUSE_LIMIT_ECL";
			Effects[670] = "EFST_M_LIFEPOTION";
			Effects[671] = "EFST_ENTRY_QUEUE_NOTIFY_ADMISSION_TIME_OUT";
			Effects[672] = "EFST_UNKNOWN_NAME";
			Effects[673] = "EFST_ON_PUSH_CART";
			Effects[674] = "EFST_HAT_EFFECT";
			Effects[675] = "EFST_FLOWER_LEAF";
			Effects[676] = "EFST_RAY_OF_PROTECTION";
			Effects[677] = "EFST_GLASTHEIM_ATK";
			Effects[678] = "EFST_GLASTHEIM_DEF";
			Effects[679] = "EFST_GLASTHEIM_HEAL";
			Effects[680] = "EFST_GLASTHEIM_HIDDEN";
			Effects[681] = "EFST_GLASTHEIM_STATE";
			Effects[682] = "EFST_GLASTHEIM_ITEMDEF";
			Effects[683] = "EFST_GLASTHEIM_HPSP";
			Effects[684] = "EFST_HOMUN_SKILL_POSTDELAY";
			Effects[685] = "EFST_ALMIGHTY";
			Effects[686] = "EFST_GVG_GIANT";
			Effects[687] = "EFST_GVG_GOLEM";
			Effects[688] = "EFST_GVG_STUN";
			Effects[689] = "EFST_GVG_STONE";
			Effects[690] = "EFST_GVG_FREEZ";
			Effects[691] = "EFST_GVG_SLEEP";
			Effects[692] = "EFST_GVG_CURSE";
			Effects[693] = "EFST_GVG_SILENCE";
			Effects[694] = "EFST_GVG_BLIND";
			Effects[695] = "EFST_CLIENT_ONLY_EQUIP_ARROW";
			Effects[696] = "EFST_CLAN_INFO";
			Effects[697] = "EFST_JP_EVENT01";
			Effects[698] = "EFST_JP_EVENT02";
			Effects[699] = "EFST_JP_EVENT03";
			Effects[700] = "EFST_JP_EVENT04";
			Effects[701] = "EFST_TELEPORT_FIXEDCASTINGDELAY";
			Effects[702] = "EFST_GEFFEN_MAGIC1";
			Effects[703] = "EFST_GEFFEN_MAGIC2";
			Effects[704] = "EFST_GEFFEN_MAGIC3";
			Effects[705] = "EFST_QUEST_BUFF1";
			Effects[706] = "EFST_QUEST_BUFF2";
			Effects[707] = "EFST_QUEST_BUFF3";
			Effects[710] = "EFST_HANDICAPSTATE_ICEEXPLO";
			Effects[711] = "EFST_FENRIR_CARD";
			Effects[714] = "EFST_PC_STOP";
			Effects[715] = "EFST_FRIGG_SONG";
			Effects[716] = "EFST_OFFERTORIUM";
			Effects[717] = "EFST_TELEKINESIS_INTENSE";
			Effects[718] = "EFST_MOONSTAR";
			Effects[719] = "EFST_STRANGELIGHTS";
			Effects[720] = "EFST_FULL_THROTTLE";
			Effects[721] = "EFST_REBOUND";
			Effects[722] = "EFST_UNLIMIT";
			Effects[723] = "EFST_KINGS_GRACE";
			Effects[724] = "EFST_ITEM_ATKMAX";
			Effects[725] = "EFST_ITEM_ATKMIN";
			Effects[726] = "EFST_ITEM_MATKMAX";
			Effects[727] = "EFST_ITEM_MATKMIN";
			Effects[728] = "EFST_SUPER_STAR";
			Effects[729] = "EFST_HIGH_RANKER";
			Effects[730] = "EFST_DARKCROW";
			Effects[731] = "EFST_2013_VALENTINE1";
			Effects[732] = "EFST_2013_VALENTINE2";
			Effects[733] = "EFST_2013_VALENTINE3";
			Effects[734] = "EFST_ILLUSIONDOPING";
			Effects[735] = "EFST_WIDEWEB";
			Effects[736] = "EFST_CHILL";
			Effects[737] = "EFST_BURNT";
			Effects[738] = "EFST_PCCAFE_PLAY_TIME";
			Effects[739] = "EFST_TWISTED_TIME";
			Effects[740] = "EFST_FLASHCOMBO";
			Effects[741] = "EFST_JITTER_BUFF1";
			Effects[742] = "EFST_JITTER_BUFF2";
			Effects[743] = "EFST_JITTER_BUFF3";
			Effects[744] = "EFST_JITTER_BUFF4";
			Effects[745] = "EFST_JITTER_BUFF5";
			Effects[746] = "EFST_JITTER_BUFF6";
			Effects[747] = "EFST_JITTER_BUFF7";
			Effects[748] = "EFST_JITTER_BUFF8";
			Effects[749] = "EFST_JITTER_BUFF9";
			Effects[750] = "EFST_JITTER_BUFF10";
			Effects[751] = "EFST_CUP_OF_BOZA";
			Effects[752] = "EFST_B_TRAP";
			Effects[753] = "EFST_E_CHAIN";
			Effects[754] = "EFST_E_QD_SHOT_READY";
			Effects[755] = "EFST_C_MARKER";
			Effects[756] = "EFST_H_MINE";
			Effects[757] = "EFST_H_MINE_SPLASH";
			Effects[758] = "EFST_P_ALTER";
			Effects[759] = "EFST_HEAT_BARREL";
			Effects[760] = "EFST_ANTI_M_BLAST";
			Effects[761] = "EFST_SLUGSHOT";
			Effects[762] = "EFST_SWORDCLAN";
			Effects[763] = "EFST_ARCWANDCLAN";
			Effects[764] = "EFST_GOLDENMACECLAN";
			Effects[765] = "EFST_CROSSBOWCLAN";
			Effects[766] = "EFST_PACKING_ENVELOPE1";
			Effects[767] = "EFST_PACKING_ENVELOPE2";
			Effects[768] = "EFST_PACKING_ENVELOPE3";
			Effects[769] = "EFST_PACKING_ENVELOPE4";
			Effects[770] = "EFST_PACKING_ENVELOPE5";
			Effects[771] = "EFST_PACKING_ENVELOPE6";
			Effects[772] = "EFST_PACKING_ENVELOPE7";
			Effects[773] = "EFST_PACKING_ENVELOPE8";
			Effects[774] = "EFST_PACKING_ENVELOPE9";
			Effects[775] = "EFST_PACKING_ENVELOPE10";
			Effects[776] = "EFST_GLASTHEIM_TRANS";
			Effects[777] = "EFST_ZONGZI_POUCH_TRANS";
			Effects[778] = "EFST_HEAT_BARREL_AFTER";
			Effects[779] = "EFST_DECORATION_OF_MUSIC";
			Effects[780] = "EFST_OVERSEAEXPUP";
			Effects[783] = "EFST_BEEF_RIB_STEW";
			Effects[784] = "EFST_PORK_RIB_STEW";
			Effects[785] = "EFST_CHUSEOK_MONDAY";
			Effects[786] = "EFST_CHUSEOK_TUESDAY";
			Effects[787] = "EFST_CHUSEOK_WEDNESDAY";
			Effects[788] = "EFST_CHUSEOK_THURSDAY";
			Effects[789] = "EFST_CHUSEOK_FRIDAY";
			Effects[790] = "EFST_CHUSEOK_WEEKEND";
			Effects[791] = "EFST_ALL_LIGHTGUARD";
			Effects[792] = "EFST_ALL_LIGHTGUARD_COOL_TIME";
			Effects[793] = "EFST_MTF_MHP";
			Effects[794] = "EFST_MTF_MSP";
			Effects[795] = "EFST_MTF_PUMPKIN";
			Effects[796] = "EFST_MTF_HITFLEE";
			Effects[797] = "EFST_MTF_CRIDAMAGE2";
			Effects[798] = "EFST_MTF_SPDRAIN";
			Effects[799] = "EFST_ACUO_MINT_GUM";
			Effects[800] = "EFST_S_HEALPOTION";
			Effects[801] = "EFST_REUSE_LIMIT_S_HEAL_POTION";
			Effects[802] = "EFST_PLAYTIME_STATISTICS";
			Effects[803] = "EFST_GN_CHANGEMATERIAL_OPERATOR";
			Effects[804] = "EFST_GN_MIX_COOKING_OPERATOR";
			Effects[805] = "EFST_GN_MAKEBOMB_OPERATOR";
			Effects[806] = "EFST_GN_S_PHARMACY_OPERATOR";
			Effects[807] = "EFST_SO_EL_ANALYSIS_DISASSEMBLY_OPERATOR";
			Effects[808] = "EFST_SO_EL_ANALYSIS_COMBINATION_OPERATOR";
			Effects[809] = "EFST_NC_MAGICDECOY_OPERATOR";
			Effects[810] = "EFST_GUILD_STORAGE";
			Effects[811] = "EFST_GC_POISONINGWEAPON_OPERATOR";
			Effects[812] = "EFST_WS_WEAPONREFINE_OPERATOR";
			Effects[813] = "EFST_BS_REPAIRWEAPON_OPERATOR";
			Effects[814] = "EFST_GET_MAILBOX";
			Effects[815] = "EFST_JUMPINGCLAN";
			Effects[816] = "EFST_JP_OTP";
			Effects[817] = "EFST_HANDICAPTOLERANCE_LEVELGAP";
			Effects[818] = "EFST_MTF_RANGEATK2";
			Effects[819] = "EFST_MTF_ASPD2";
			Effects[820] = "EFST_MTF_MATK2";
			Effects[825] = "EFST_QSCARABA";
			Effects[826] = "EFST_LJOSALFAR";
			Effects[844] = "EFST_ESSENCE_OF_TIME";
			Effects[865] = "EFST_DRACULA_CARD";
			Effects[867] = "EFST_LIMIT_POWER_BOOSTER";
			Effects[872] = "EFST_TIME_ACCESSORY";
			Effects[873] = "EFST_EP16_DEF";
			Effects[874] = "EFST_NORMAL_ATKED_SP";
			Effects[875] = "EFST_BODYSTATE_STONECURSE";
			Effects[876] = "EFST_BODYSTATE_FREEZING";
			Effects[877] = "EFST_BODYSTATE_STUN";
			Effects[878] = "EFST_BODYSTATE_SLEEP";
			Effects[880] = "EFST_BODYSTATE_STONECURSE_ING";
			Effects[881] = "EFST_BODYSTATE_BURNNING";
			Effects[882] = "EFST_BODYSTATE_IMPRISON";
			Effects[883] = "EFST_HEALTHSTATE_POISON";
			Effects[884] = "EFST_HEALTHSTATE_CURSE";
			Effects[885] = "EFST_HEALTHSTATE_SILENCE";
			Effects[886] = "EFST_HEALTHSTATE_CONFUSION";
			Effects[890] = "EFST_HEALTHSTATE_HEAVYPOISON";
			Effects[891] = "EFST_HEALTHSTATE_FEAR";
			Effects[892] = "EFST_CHERRY_BLOSSOM_CAKE";
			Effects[897] = "EFST_ATTACK_PROPERTY_NOTHING";
			Effects[898] = "EFST_ATTACK_PROPERTY_WATER";
			Effects[899] = "EFST_ATTACK_PROPERTY_GROUND";
			Effects[900] = "EFST_ATTACK_PROPERTY_FIRE";
			Effects[901] = "EFST_ATTACK_PROPERTY_WIND";
			Effects[902] = "EFST_ATTACK_PROPERTY_POISON";
			Effects[903] = "EFST_ATTACK_PROPERTY_SAINT";
			Effects[904] = "EFST_ATTACK_PROPERTY_DARKNESS";
			Effects[905] = "EFST_ATTACK_PROPERTY_TELEKINESIS";
			Effects[906] = "EFST_ATTACK_PROPERTY_UNDEAD";
			Effects[907] = "EFST_RESIST_PROPERTY_NOTHING";
			Effects[908] = "EFST_RESIST_PROPERTY_WATER";
			Effects[909] = "EFST_RESIST_PROPERTY_GROUND";
			Effects[910] = "EFST_RESIST_PROPERTY_FIRE";
			Effects[911] = "EFST_RESIST_PROPERTY_WIND";
			Effects[912] = "EFST_RESIST_PROPERTY_POISON";
			Effects[913] = "EFST_RESIST_PROPERTY_SAINT";
			Effects[914] = "EFST_RESIST_PROPERTY_DARKNESS";
			Effects[915] = "EFST_RESIST_PROPERTY_TELEKINESIS";
			Effects[916] = "EFST_RESIST_PROPERTY_UNDEAD";
			Effects[922] = "EFST_PERIOD_RECEIVEITEM";
			Effects[923] = "EFST_PERIOD_PLUSEXP";
			Effects[924] = "EFST_PERIOD_PLUSJOBEXP";
			Effects[935] = "EFST_DORAM_BUF_01";
			Effects[936] = "EFST_DORAM_BUF_02";
			Effects[893] = "EFST_SU_STOOP";
			Effects[894] = "EFST_CATNIPPOWDER";
			Effects[896] = "EFST_SV_ROOTTWIST";
			Effects[917] = "EFST_BITESCAR";
			Effects[918] = "EFST_ARCLOUSEDASH";
			Effects[919] = "EFST_TUNAPARTY";
			Effects[920] = "EFST_SHRIMP";
			Effects[921] = "EFST_FRESHSHRIMP";
			Effects[933] = "EFST_SUHIDE";
			Effects[937] = "EFST_SPRITEMABLE";
			Effects[950] = "EFST_HISS";
			Effects[952] = "EFST_NYANGGRASS";
			Effects[953] = "EFST_CHATTERING";
			Effects[961] = "EFST_GROOMING";
			Effects[962] = "EFST_PROTECTIONOFSHRIMP";
			Effects[963] = "EFST_EP16_2_BUFF_SS";
			Effects[964] = "EFST_EP16_2_BUFF_SC";
			Effects[965] = "EFST_EP16_2_BUFF_AC";
			Effects[966] = "EFST_GS_MAGICAL_BULLET";
			Effects[976] = "EFST_FALLEN_ANGEL";
			Effects[977] = "EFST_REUSE_LIMIT_MOVEPOINT";
			Effects[938] = "EFST_AID_PERIOD_RECEIVEITEM";
			Effects[939] = "EFST_AID_PERIOD_PLUSEXP";
			Effects[940] = "EFST_AID_PERIOD_PLUSJOBEXP";
			Effects[941] = "EFST_AID_PERIOD_DEADPENALTY";
			Effects[942] = "EFST_AID_PERIOD_ADDSTOREITEMCOUNT";
			Effects[983] = "EFST_AID_PERIOD_RECEIVEITEM_2ND";
			Effects[984] = "EFST_AID_PERIOD_PLUSEXP_2ND";
			Effects[985] = "EFST_AID_PERIOD_PLUSJOBEXP_2ND";
			Effects[988] = "EFST_GLOOM_CARD";
			Effects[989] = "EFST_PHARAOH_CARD";
			Effects[990] = "EFST_KIEL_CARD";
			Effects[995] = "EFST_S_MANAPOTION";
			Effects[996] = "EFST_M_DEFSCROLL";
			Effects[1000] = "EFST_AS_RAGGED_GOLEM_CARD";
			Effects[992] = "EFST_CHEERUP";
			Effects[1001] = "EFST_LHZ_DUN_N1";
			Effects[1002] = "EFST_LHZ_DUN_N2";
			Effects[1003] = "EFST_LHZ_DUN_N3";
			Effects[1004] = "EFST_LHZ_DUN_N4";
			Effects[1013] = "EFST_ALL_STAT_DOWN";
			Effects[1014] = "EFST_GRADUAL_GRAVITY";
			Effects[1015] = "EFST_DAMAGE_HEAL";
			Effects[1016] = "EFST_IMMUNE_PROPERTY_NOTHING";
			Effects[1017] = "EFST_IMMUNE_PROPERTY_WATER";
			Effects[1018] = "EFST_IMMUNE_PROPERTY_GROUND";
			Effects[1019] = "EFST_IMMUNE_PROPERTY_FIRE";
			Effects[1020] = "EFST_IMMUNE_PROPERTY_WIND";
			Effects[1021] = "EFST_IMMUNE_PROPERTY_POISON";
			Effects[1022] = "EFST_IMMUNE_PROPERTY_SAINT";
			Effects[1023] = "EFST_IMMUNE_PROPERTY_DARKNESS";
			Effects[1024] = "EFST_IMMUNE_PROPERTY_TELEKINESIS";
			Effects[1025] = "EFST_IMMUNE_PROPERTY_UNDEAD";
			Effects[1027] = "EFST_SPECIALCOOKIE";
			Effects[1031] = "EFST_ATK_POPCORN";
			Effects[1032] = "EFST_MATK_POPCORN";
			Effects[1033] = "EFST_ASPD_POPCORN";
			Effects[1065] = "EFST_INFINITY_DRINK";
			Effects[1083] = "EFST_HUNTING_EVENT";
			Effects[1084] = "EFST_PERIOD_RECEIVEITEM_2ND";
			Effects[1085] = "EFST_PERIOD_PLUSEXP_2ND";
			Effects[1095] = "EFST_ANCILLA";
			Effects[1035] = "EFST_LIGHTOFMOON";
			Effects[1036] = "EFST_LIGHTOFSUN";
			Effects[1037] = "EFST_LIGHTOFSTAR";
			Effects[1038] = "EFST_LUNARSTANCE";
			Effects[1039] = "EFST_UNIVERSESTANCE";
			Effects[1040] = "EFST_SUNSTANCE";
			Effects[1041] = "EFST_FLASHKICK";
			Effects[1042] = "EFST_NEWMOON";
			Effects[1043] = "EFST_STARSTANCE";
			Effects[1044] = "EFST_DIMENSION";
			Effects[1045] = "EFST_DIMENSION1";
			Effects[1046] = "EFST_DIMENSION2";
			Effects[1047] = "EFST_CREATINGSTAR";
			Effects[1048] = "EFST_FALLINGSTAR";
			Effects[1049] = "EFST_NOVAEXPLOSING";
			Effects[1050] = "EFST_GRAVITYCONTROL";
			Effects[1053] = "EFST_SOULCOLLECT";
			Effects[1054] = "EFST_SOULREAPER";
			Effects[1055] = "EFST_SOULUNITY";
			Effects[1056] = "EFST_SOULSHADOW";
			Effects[1057] = "EFST_SOULFAIRY";
			Effects[1058] = "EFST_SOULFALCON";
			Effects[1059] = "EFST_SOULGOLEM";
			Effects[1060] = "EFST_SOULDIVISION";
			Effects[1061] = "EFST_SOULENERGY";
			Effects[1062] = "EFST_USE_SKILL_SP_SPA";
			Effects[1063] = "EFST_USE_SKILL_SP_SHA";
			Effects[1064] = "EFST_SP_SHA";
			Effects[1107] = "EFST_WEAPONBLOCK_ON";
			Effects[1123] = "EFST_OVERLAPEXPUP2";
			Effects[1121] = "EFST_ASSUMPTIO_BUFF";
			Effects[1122] = "EFST_BASILICA_BUFF";
			Effects[1088] = "EFST_ENSEMBLEFATIGUE";
			Effects[1089] = "EFST_ADAPTATION";
			Effects[1132] = "EFST_SWEETSFAIR_ATK";
			Effects[1133] = "EFST_SWEETSFAIR_MATK";
			Effects[1129] = "EFST_NV_BREAKTHROUGH";
			Effects[1130] = "EFST_HELPANGEL";
			Effects[1131] = "EFST_NV_TRANSCENDENCE";
			Effects[1125] = "EFST_SOULCURSE";
			Effects[1126] = "EFST_SOUND_OF_DESTRUCTION";
			Effects[1135] = "EFST_FLOWER_LEAF2";
			Effects[1136] = "EFST_FLOWER_LEAF3";
			Effects[1137] = "EFST_FLOWER_LEAF4";
			Effects[1141] = "EFST_MISTY_FROST";
			Effects[1142] = "EFST_MAGIC_POISON";
			Effects[1154] = "EFST_LUXANIMA";
			Effects[1159] = "EFST_REUSE_LIMIT_LUXANIMA";
			Effects[1155] = "EFST_BATH_FOAM_A";
			Effects[1156] = "EFST_BATH_FOAM_B";
			Effects[1157] = "EFST_BATH_FOAM_C";
			Effects[1158] = "EFST_AROMA_OIL";
			Effects[1166] = "EFST_RELIEVE_DAMAGE";
			Effects[1167] = "EFST_LOCKON_LASER";
			Effects[1165] = "EFST_HELLS_PLANT_ARMOR";
			Effects[1169] = "EFST_REF_T_POTION";
			Effects[1170] = "EFST_ADD_ATK_DAMAGE";
			Effects[1171] = "EFST_ADD_MATK_DAMAGE";
			Effects[1172] = "EFST_SERVANTWEAPON";
			Effects[1173] = "EFST_SERVANT_SIGN";
			Effects[1174] = "EFST_CHARGINGPIERCE";
			Effects[1175] = "EFST_CHARGINGPIERCE_COUNT";
			Effects[1176] = "EFST_DRAGONIC_AURA";
			Effects[1177] = "EFST_BIG_SCAR";
			Effects[1178] = "EFST_VIGOR";
			Effects[1150] = "EFST_DEADLY_DEFEASANCE";
			Effects[1151] = "EFST_CLIMAX_DES_HU";
			Effects[1152] = "EFST_CLIMAX";
			Effects[1182] = "EFST_CLIMAX_EARTH";
			Effects[1183] = "EFST_CLIMAX_BLOOM";
			Effects[1184] = "EFST_CLIMAX_CRYIMP";
			Effects[1191] = "EFST_CRYSTAL_IMPACT";
			Effects[1202] = "EFST_GUARD_STANCE";
			Effects[1203] = "EFST_ATTACK_STANCE";
			Effects[1204] = "EFST_GUARDIAN_S";
			Effects[1217] = "EFST_REBOUND_S";
			Effects[1218] = "EFST_SHIELD_MASTERY";
			Effects[1219] = "EFST_SPEAR_SWORD_M";
			Effects[1220] = "EFST_HOLY_S";
			Effects[1221] = "EFST_ULTIMATE_S";
			Effects[1222] = "EFST_SPEAR_SCAR";
			Effects[1223] = "EFST_SHIELD_POWER";
			Effects[1160] = "EFST_POWERFUL_FAITH";
			Effects[1161] = "EFST_SINCERE_FAITH";
			Effects[1162] = "EFST_FIRM_FAITH";
			Effects[1190] = "EFST_HOLY_OIL";
			Effects[1230] = "EFST_FIRST_BRAND";
			Effects[1231] = "EFST_SECOND_BRAND";
			Effects[1232] = "EFST_SECOND_JUDGE";
			Effects[1233] = "EFST_THIRD_EXOR_FLAME";
			Effects[1234] = "EFST_FIRST_FAITH_POWER";
			Effects[1326] = "EFST_MASSIVE_F_BLASTER";
			Effects[1192] = "EFST_SHADOW_EXCEED";
			Effects[1193] = "EFST_DANCING_KNIFE";
			Effects[1194] = "EFST_POTENT_VENOM";
			Effects[1195] = "EFST_SHADOW_SCAR";
			Effects[1196] = "EFST_E_SLASH_COUNT";
			Effects[1226] = "EFST_SHADOW_WEAPON";
			Effects[1197] = "EFST_MEDIALE";
			Effects[1198] = "EFST_A_VITA";
			Effects[1199] = "EFST_A_TELUM";
			Effects[1200] = "EFST_PRE_ACIES";
			Effects[1201] = "EFST_COMPETENTIA";
			Effects[1227] = "EFST_RELIGIO";
			Effects[1228] = "EFST_BENEDICTUM";
			Effects[1250] = "EFST_WINDSIGN";
			Effects[1251] = "EFST_CRESCIVEBOLT";
			Effects[1252] = "EFST_CALAMITYGALE";
			Effects[1254] = "EFST_STAGE_MANNER";
			Effects[1255] = "EFST_RETROSPECTION";
			Effects[1256] = "EFST_MYSTIC_SYMPHONY";
			Effects[1257] = "EFST_KVASIR_SONATA";
			Effects[1258] = "EFST_SOUNDBLEND";
			Effects[1259] = "EFST_GEF_NOCTURN";
			Effects[1260] = "EFST_AIN_RHAPSODY";
			Effects[1261] = "EFST_MUSICAL_INTERLUDE";
			Effects[1262] = "EFST_JAWAII_SERENADE";
			Effects[1263] = "EFST_PRON_MARCH";
			Effects[1264] = "EFST_ROSEBLOSSOM";
			Effects[1240] = "EFST_DAGGER_AND_BOW_M";
			Effects[1241] = "EFST_MAGIC_SWORD_M";
			Effects[1242] = "EFST_SHADOW_STRIP";
			Effects[1243] = "EFST_ABYSS_DAGGER";
			Effects[1244] = "EFST_ABYSSFORCEWEAPON";
			Effects[1245] = "EFST_ABYSS_SLAYER";
			Effects[1235] = "EFST_AXE_STOMP";
			Effects[1236] = "EFST_A_MACHINE";
			Effects[1237] = "EFST_D_MACHINE";
			Effects[1238] = "EFST_MT_M_MACHINE_OPERATOR";
			Effects[1239] = "EFST_TWOAXEDEF";
			Effects[1297] = "EFST_ABR_BATTLE_WARIOR";
			Effects[1298] = "EFST_ABR_DUAL_CANNON";
			Effects[1299] = "EFST_ABR_MOTHER_NET";
			Effects[1300] = "EFST_ABR_INFINITY";
			Effects[1270] = "EFST_MAGIC_BOOK_M";
			Effects[1271] = "EFST_SPELL_ENCHANTING";
			Effects[1272] = "EFST_SUMMON_ELEMENTAL_ARDOR";
			Effects[1273] = "EFST_SUMMON_ELEMENTAL_DILUVIO";
			Effects[1274] = "EFST_SUMMON_ELEMENTAL_PROCELLA";
			Effects[1275] = "EFST_SUMMON_ELEMENTAL_TERREMOTUS";
			Effects[1276] = "EFST_SUMMON_ELEMENTAL_SERPENS";
			Effects[1277] = "EFST_FLAMETECHNIC";
			Effects[1278] = "EFST_FLAMETECHNIC_OPTION";
			Effects[1279] = "EFST_FLAMEARMOR";
			Effects[1280] = "EFST_FLAMEARMOR_OPTION";
			Effects[1281] = "EFST_COLD_FORCE";
			Effects[1282] = "EFST_COLD_FORCE_OPTION";
			Effects[1283] = "EFST_CRYSTAL_ARMOR";
			Effects[1284] = "EFST_CRYSTAL_ARMOR_OPTION";
			Effects[1285] = "EFST_GRACE_BREEZE";
			Effects[1286] = "EFST_GRACE_BREEZE_OPTION";
			Effects[1287] = "EFST_EYES_OF_STORM";
			Effects[1288] = "EFST_EYES_OF_STORM_OPTION";
			Effects[1289] = "EFST_EARTH_CARE";
			Effects[1290] = "EFST_EARTH_CARE_OPTION";
			Effects[1291] = "EFST_STRONG_PROTECTION";
			Effects[1292] = "EFST_STRONG_PROTECTION_OPTION";
			Effects[1293] = "EFST_DEEP_POISONING";
			Effects[1294] = "EFST_DEEP_POISONING_OPTION";
			Effects[1295] = "EFST_POISON_SHIELD";
			Effects[1296] = "EFST_POISON_SHIELD_OPTION";
			Effects[1301] = "EFST_ELEMENTAL_VEIL";
			Effects[1247] = "EFST_PROTECTSHADOWEQUIP";
			Effects[1248] = "EFST_RESEARCHREPORT";
			Effects[1249] = "EFST_BO_HELL_DUSTY";
			Effects[1266] = "EFST_ACIDIFIED_ZONE_WATER";
			Effects[1267] = "EFST_ACIDIFIED_ZONE_GROUND";
			Effects[1268] = "EFST_ACIDIFIED_ZONE_WIND";
			Effects[1269] = "EFST_ACIDIFIED_ZONE_FIRE";
			Effects[1205] = "EFST_HANDICAPSTATE_DEEPBLIND";
			Effects[1206] = "EFST_HANDICAPSTATE_DEEPSILENCE";
			Effects[1207] = "EFST_HANDICAPSTATE_LASSITUDE";
			Effects[1208] = "EFST_HANDICAPSTATE_FROSTBITE";
			Effects[1209] = "EFST_HANDICAPSTATE_SWOONING";
			Effects[1210] = "EFST_HANDICAPSTATE_LIGHTNINGSTRIKE";
			Effects[1211] = "EFST_HANDICAPSTATE_CRYSTALLIZATION";
			Effects[1212] = "EFST_HANDICAPSTATE_CONFLAGRATION";
			Effects[1213] = "EFST_HANDICAPSTATE_MISFORTUNE";
			Effects[1214] = "EFST_HANDICAPSTATE_DEADLYPOISON";
			Effects[1215] = "EFST_HANDICAPSTATE_DEPRESSION";
			Effects[1216] = "EFST_HANDICAPSTATE_HOLYFLAME";
			Effects[1315] = "EFST_OVERBRANDREADY";
			Effects[1316] = "EFST_SHIELDSPELL";
			Effects[1318] = "EFST_CLOUD_POISON";
			Effects[1319] = "EFST_SPORE_EXPLOSION_DEBUFF";
			Effects[1310] = "EFST_POISON_MIST";
			Effects[1313] = "EFST_STONE_WALL";
			Effects[1303] = "EFST_HOMUN_TIME";
			Effects[1330] = "EFST_NOEQUIPWEAPON2";
			Effects[1331] = "EFST_NOEQUIPARMOR2";
			Effects[1332] = "EFST_NOEQUIPSHIELD2";
			Effects[1333] = "EFST_NOEQUIPSHOES2";
			Effects[1334] = "EFST_NOEQUIPPENDANT2";
			Effects[1335] = "EFST_NOEQUIPEARING2";
			Effects[1336] = "EFST_NOEQUIPFULL2";
			Effects[1337] = "EFST_CURSE_R_CUBE";
			Effects[1338] = "EFST_CURSE_B_CUBE";
			Effects[1339] = "EFST_KILLING_AURA";
			Effects[1341] = "EFST_TOXIN_OF_MANDARA";
			Effects[1342] = "EFST_GOLDENE_TONE";
			Effects[1343] = "EFST_TEMPERING";
			Effects[1396] = "EFST_NOODLE_FES_1";
			Effects[1397] = "EFST_NOODLE_FES_2";
			Effects[1398] = "EFST_NOODLE_FES_3";
			Effects[1399] = "EFST_NOODLE_FES_4";
			Effects[1400] = "EFST_NOODLE_FES_5";
			Effects[1402] = "EFST_RUSH_QUAKE1";
			Effects[1403] = "EFST_RUSH_QUAKE2";
			Effects[1385] = "EFST_RISING_SUN";
			Effects[1386] = "EFST_NOON_SUN";
			Effects[1387] = "EFST_SUNSET_SUN";
			Effects[1388] = "EFST_RISING_MOON";
			Effects[1389] = "EFST_MIDNIGHT_MOON";
			Effects[1390] = "EFST_DAWN_MOON";
			Effects[1391] = "EFST_STAR_BURST";
			Effects[1392] = "EFST_SKY_ENCHANT";
			Effects[1356] = "EFST_TALISMAN_OF_PROTECTION";
			Effects[1357] = "EFST_TALISMAN_OF_WARRIOR";
			Effects[1358] = "EFST_TALISMAN_OF_MAGICIAN";
			Effects[1359] = "EFST_TALISMAN_OF_FIVE_ELEMENTS";
			Effects[1360] = "EFST_T_FIRST_GOD";
			Effects[1361] = "EFST_T_SECOND_GOD";
			Effects[1362] = "EFST_T_THIRD_GOD";
			Effects[1363] = "EFST_T_FOURTH_GOD";
			Effects[1364] = "EFST_T_FIVETH_GOD";
			Effects[1365] = "EFST_HEAVEN_AND_EARTH";
			Effects[1366] = "EFST_HOGOGONG";
			Effects[1367] = "EFST_MARINE_FESTIVAL";
			Effects[1368] = "EFST_SANDY_FESTIVAL";
			Effects[1369] = "EFST_KI_SUL_RAMPAGE";
			Effects[1370] = "EFST_COLORS_OF_HYUN_ROK_1";
			Effects[1371] = "EFST_COLORS_OF_HYUN_ROK_2";
			Effects[1372] = "EFST_COLORS_OF_HYUN_ROK_3";
			Effects[1373] = "EFST_COLORS_OF_HYUN_ROK_4";
			Effects[1374] = "EFST_COLORS_OF_HYUN_ROK_5";
			Effects[1375] = "EFST_COLORS_OF_HYUN_ROK_6";
			Effects[1376] = "EFST_COLORS_OF_HYUN_ROK_BUFF";
			Effects[1377] = "EFST_TEMPORARY_COMMUNION";
			Effects[1378] = "EFST_BLESSING_OF_M_CREATURES";
			Effects[1379] = "EFST_BLESSING_OF_M_C_DEBUFF";
			Effects[1344] = "EFST_NW_P_F_I";
			Effects[1345] = "EFST_INTENSIVE_AIM";
			Effects[1346] = "EFST_INTENSIVE_AIM_COUNT";
			Effects[1347] = "EFST_GRENADE_FRAGMENT_1";
			Effects[1348] = "EFST_GRENADE_FRAGMENT_2";
			Effects[1349] = "EFST_GRENADE_FRAGMENT_3";
			Effects[1350] = "EFST_GRENADE_FRAGMENT_4";
			Effects[1351] = "EFST_GRENADE_FRAGMENT_5";
			Effects[1352] = "EFST_GRENADE_FRAGMENT_6";
			Effects[1353] = "EFST_AUTO_FIRING_LAUNCHEREFST";
			Effects[1354] = "EFST_HIDDEN_CARD";
			Effects[1355] = "EFST_NW_GRENADE_MASTERY";
			Effects[1380] = "EFST_SHIELDCHAINRUSH";
			Effects[1381] = "EFST_MISTYFROST";
			Effects[1382] = "EFST_GROUNDGRAVITY";
			Effects[1383] = "EFST_BREAKINGLIMIT";
			Effects[1384] = "EFST_RULEBREAK";
			Effects[1393] = "EFST_SHADOW_CLOCK";
			Effects[1394] = "EFST_SHINKIROU_CALL";
			Effects[1395] = "EFST_NIGHTMARE";
			Effects[1415] = "EFST_SBUNSHIN";
			Effects[1418] = "EFST_MTP_W_POTION_100";
			Effects[1425] = "EFST_VR_SPEED";
			Effects[1426] = "EFST_VR_ASPD";
			Effects[1427] = "EFST_VR_MHP";
			Effects[1428] = "EFST_VR_MSP";
			Effects[1429] = "EFST_VR_HIT";
			Effects[1430] = "EFST_VR_DEF";
			Effects[1431] = "EFST_VR_MDEF";
			Effects[1432] = "EFST_VR_BOOK001";
			Effects[1433] = "EFST_VR_BOOK002";
			Effects[1434] = "EFST_VR_BOOK003";
			Effects[1435] = "EFST_VR_BOOK004";
			Effects[1436] = "EFST_REUSE_LIMIT_VR_BOOK";
			Effects[1439] = "EFST_VR_BOOK005";
			Effects[1440] = "EFST_VR_BOOK006";
			Effects[1441] = "EFST_VR_BOOK007";
			Effects[1442] = "EFST_VR_BOOK008";
			Effects[1443] = "EFST_VR_BOOK009";
			Effects[1444] = "EFST_ALL_T_STAT";
			Effects[1445] = "EFST_P_ATK_PLUS";
			Effects[1446] = "EFST_S_MATK_PLUS";
			Effects[1447] = "EFST_C_RATE_PLUS";
			Effects[1448] = "EFST_RESIST_PLUS";
			Effects[1449] = "EFST_PVP_DUN_BUFF";
		}

		public static string GetSeeObjectType(int objecttype) {
			switch (objecttype) {
				case 0:
					return "Saw player named: ";
				case 1:
					return "Saw NPC named: ";
				case 2:
					return "Saw item named: ";
				case 3:
					return "Saw skill named: ";
				case 4:
					return "Saw chat named: ";
				case 5:
					return "Saw monster named: ";
				case 6:
					return "Saw NPC named: ";
				case 7:
					return "Saw pet named: ";
				case 8:
					return "Saw homunculus named: ";
				case 9:
					return "Saw mercenary named: ";
				case 10:
					return "Saw elemental named: ";
			}

			return "Saw object named: ";
		}

		public static string GetEffectState(int effect) {
			switch (effect) {
				case 0:
					return "OPTION_VISIBLE";
				case 0x00000002:
					return "OPTION_HIDE";
				case 0x00000004:
					return "OPTION_CLOAK";
				case 0x00000040:
					return "OPTION_INVISIBLE";
				default:
					return "" + effect;
			}
		}

		public static string GetDmgType(int action) {
			switch (action) {
				case 0: return "DMG_NORMAL";
				case 6: return "DMG_SKILL";
				case 4: return "DMG_ENDURE";
				case 5: return "DMG_SPLASH";
				case 8: return "DMG_MULTI_HIT";
				case 9: return "DMG_MULTI_HIT_ENDURE";
				case 10: return "DMG_CRITICAL";
				case 13: return "DMG_MULTI_HIT_CRIT";
			}

			return "" + action;
		}
	}
}
