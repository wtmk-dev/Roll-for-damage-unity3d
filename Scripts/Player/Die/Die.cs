using System.Collections;
using System.Collections.Generic;

public class Die {

	public enum DiceSet { LOOT, ATTACK, EVENT }
	public DiceSet diceSet;
	public List<Face> faces = new List<Face>();
	private int faceCount;
	
	
	public Die( int faceCount, DiceSet set ) { 
		this.faceCount = faceCount;
		diceSet = set;
		faces = GetStartingFaces( set );
	}

	private List<Face> GetStartingFaces( DiceSet set ){
		List<Face> startingFaces = new List<Face>();
		
		if ( set == DiceSet.LOOT ){
			Face face = new Face( 0 );
			startingFaces.Add( face );
			Face face1 = new Face( 1 );
			startingFaces.Add( face1 );
		}
		else if ( set == DiceSet.ATTACK ){
			Face face = new Face( 2 );
			startingFaces.Add( face );
			Face face1 = new Face( 3 );
			startingFaces.Add( face1 );
			Face face2 = new Face( 4 );
			startingFaces.Add( face );
			Face face3 = new Face( 2 );
			startingFaces.Add( face1 );
			Face face4 = new Face( 5 );
			startingFaces.Add( face );
			Face face5 = new Face( 5 );
			startingFaces.Add( face1 );
		}
		else if ( set == DiceSet.EVENT ){
			Face face6 = new Face( 9 );
			startingFaces.Add( face6 );
			Face face7 = new Face( 6 );
			startingFaces.Add( face7 );
			Face face8 = new Face( 6 );
			startingFaces.Add( face8 );
			Face face9 = new Face( 7 );
			startingFaces.Add( face9 );
		}

		return startingFaces;
	}

}

public class Face {

	public int ID{get;set;}

	public Face( int id ){
		ID = id;
	}

	public void ActivateFace( Player player ){

		switch( ID ){
			case 0:
			GainExp( player );
			break;
			case 1:
			GainGold( player );
			break;
			case 2:
			GainAtk( player );
			break;
			case 3:
			GainStamina( player );
			break;
			case 4:
			GainBlood( player );
			break;
			case 5:
			Miss();
			break;
			case 6:
			TakeLightDamage( player );
			break;
			case 7:
			TakeMidDamage( player );
			break;
			case 8:
			TakeHighDamage( player );
			break;
			case 9:
			Dodge( player );
			break;
			case 10:
		//	Dodge( player );
			break;
			case 11:
		///	Dodge( player );
			break;
			case 12:
			//Dodge( player );
			break;
			

		}

	}

	private void GainExp ( Player player ){
		//0
		player.EXP += 1 * player.Level;
	}

	private void GainGold ( Player player ){
		//1
		player.Gold += 1 * player.Level;
	}

	private void GainAtk( Player player ){
		//2
		player.AP += 1;
	}

	private void GainStamina( Player player ){
		//3
		player.Stamina += 1;
	}

	private void GainBlood( Player player ){
		//4
		player.Blood += 1;
	}

	private void Miss(){
		//5
	}

	private void TakeLightDamage( Player player ){
		//6
		player.Blood -= 1;
	}

	private void TakeMidDamage( Player player ){
		//7
		player.Blood -= 2;
	}

	private void TakeHighDamage( Player player ){
		//8
		player.Blood -= 3;
	}

	private void Dodge( Player player ){
		//9
	}
}