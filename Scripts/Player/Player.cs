using System.Collections;
using System.Collections.Generic;

public class Player{

	public enum Job { SLASH, BLUNT, RANGED }
	private Job job; 
	private int blood;
	public int Blood{get{return blood;} set{blood = value;}}
	private int diceCount;
	public int DiceCount{get{return diceCount;}}
	private int exp;
	public int EXP{get{return exp;} set{exp = value;}}
	public int AP{get;set;}
	public int MP{get;set;}
	public int Stamina{get;set;}
	public int Kills{get;set;}
	public int Level{get;set;}
	public int LootRolls{get;set;}
	public int Gold{get;set;}

	private Weapon wepon;
	public Weapon CurrentWepon{ get{ return wepon; } }
	private Magick magick;
	public List<Die> diceSets = new List<Die>();

	public Player( Job job ){
		this.job = job;
		// this.blood = blood; this.diceCount = diceCount; this.exp = exp;
		// Level = 1; AP = 3; MP = 3; Stamina = 2; LootRolls = 0; Gold = 0;
		// wepon = new Weapon( Weapon.Type.SLASH, false );
		
		// diceSets = new List<Die>();
		// diceSets.Add( new Die( 2, Die.DiceSet.LOOT ) );
		// diceSets.Add( new Die( 6, Die.DiceSet.ATTACK ) );
		// diceSets.Add( new Die( 4, Die.DiceSet.EVENT ) );
	}

	public void UpdateLootRolls(){
		LootRolls += Kills;
	}

	public void ResetTempValues(){
		Kills = 0; Level = 0; LootRolls = 0;
	}

}