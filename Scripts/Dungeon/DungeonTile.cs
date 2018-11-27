using System.Collections;
using System.Collections.Generic;

public class DungeonTile {
	public static bool exitPlaced = false;
	private static int TYPE_MAX = 5;

	public enum Type { FLYING, FLESH, ARMORED, LOOT, TRAP, EXIT, ENTER }
	public Type type;
	
	public int ID{get;set;}

	public DungeonTile(){
		Randomize();
		ID = 0;
	}

	public void SetExit(){
		type = Type.EXIT;
		exitPlaced = true;
	}

	public void Randomize(){
		if( type != Type.EXIT ){
			SetRandomType();
		}
	}

	private void SetRandomType(){
		int roll = DungeonController.RandomNumber( 0, TYPE_MAX );
		switch( roll ){
			case 0:
			type = Type.FLYING;
			break;
			case 1:
			type = Type.LOOT;
			break;
			case 2:
			type = Type.TRAP;
			break;
			case 3:
			type = Type.FLESH;
			break;
			case 4:
			type = Type.ARMORED;
			break;
		}
	}

}