using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShuShan;

namespace ShuShan
{
	public enum Em_SixDP
	{
		力,
		敏,
		体,
		悟,
		意,
		魅
	}
	public enum Em_ModifierType
	{
		Normal,
		Fight
	}
	public enum Em_SectJobKind
    {
		None,
		Rest,
		Expedition,
		Practice

    }
	public enum Em_FightPropertyType
	{
		
		力量,
		敏捷,
		体质,
		智力,
		意志,
		魅力
		
	}
	public enum Em_NpcPropertyKind
    {
		Normal,
		SixDP,
		TalentP,
		EthicP
    }

	public enum Em_TagType
    {
		None,
		Sect,
		Npc,
		Item,
		Tech
    }

	public enum Em_ItemType
	{
		None,
		Treasure,
		Food,
		Water,
		Medicine,
		Joy,
		Weapon,
		Armor
	}

	public enum Em_NpcResourceType
	{
		Food,
		Water,
		Hp,
		Joy
	}

	public enum Em_FeatureType
	{
		None,
		性格,
		天赋,
		经历
	}

	public enum Em_EffectScopeKind
    {
		None,
		World,
		Sect,
		Npc,
		Npcs,
		Tile,
		Filed,
		Location
    }
	public enum Em_OptionType
	{
		None,
		XLua
	}
	public enum Em_Event
	{
		None,
		EnterFight,
		YearPass
	}
	public enum g_emScene
	{
        None,
        加载界面,
        开始界面,
		选人界面,
		大地图界面,
		宗门界面,
		战斗界面

    }


	public enum g_emCardKind
    {
		None,
		法术,
		被动,
		法宝,
		体术,
		本命法宝,
		阵法,
		丹药,
		状态
	}
	public enum g_emTileKind
	{
		None,
		山地,
		丘陵,
		平原,
		浅滩,
		水域
	}

	public enum g_emLingElement
    {
		金,
		木,
		水,
		火,
		土,
		无,
		None
    }
	public enum Em_ElementKind
	{
		金,
		木,
		水,
		火,
		土,
		魂,
		风,
		雷,
		无,
		None
	}
	public enum g_emNpcSkillType
	{
		// Token: 0x04000EAB RID: 3755
		None,
		// Token: 0x04000EAC RID: 3756
		Fight,
		// Token: 0x04000EAD RID: 3757
		Qi,
		// Token: 0x04000EAE RID: 3758
		SocialContact,
		// Token: 0x04000EAF RID: 3759
		Medicine,
		// Token: 0x04000EB0 RID: 3760
		Cooking,
		// Token: 0x04000EB1 RID: 3761
		Building,
		// Token: 0x04000EB2 RID: 3762
		Farming,
		// Token: 0x04000EB3 RID: 3763
		Mining,
		// Token: 0x04000EB4 RID: 3764
		Art,
		// Token: 0x04000EB5 RID: 3765
		Manual,
		// Token: 0x04000EB6 RID: 3766
		DouFa,
		// Token: 0x04000EB7 RID: 3767
		DanQi,
		// Token: 0x04000EB8 RID: 3768
		Fabao,
		// Token: 0x04000EB9 RID: 3769
		FightSkill,
		// Token: 0x04000EBA RID: 3770
		Barrier,
		// Token: 0x04000EBB RID: 3771
		Zhen,
		// Token: 0x04000EBC RID: 3772
		_Count
	}
	public enum g_emNpcBasePropertyType
	{
		// Token: 0x04000E8E RID: 3726
		Perception,
		// Token: 0x04000E8F RID: 3727
		Physique,
		// Token: 0x04000E90 RID: 3728
		Charisma,
		// Token: 0x04000E91 RID: 3729
		Intelligence,
		// Token: 0x04000E92 RID: 3730
		Luck,
		// Token: 0x04000E93 RID: 3731
		_FUNCTIONBEGIN,
		// Token: 0x04000E94 RID: 3732
		Pain,
		// Token: 0x04000E95 RID: 3733
		Consciousness,
		// Token: 0x04000E96 RID: 3734
		Meridian,
		// Token: 0x04000E97 RID: 3735
		Movement,
		// Token: 0x04000E98 RID: 3736
		Operation,
		// Token: 0x04000E99 RID: 3737
		Feeling,
		// Token: 0x04000E9A RID: 3738
		Count
	}
}
