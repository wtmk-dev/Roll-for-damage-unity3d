using System.Collections;
using System.Collections.Generic;


public class Weapon {

	public enum Type { BLUNT, SLASH, PIERCE }
	public Type type;
	public bool isRanged = false;
	public int damage = 0;

	public Weapon( Type type, bool isRanged ){
		this.type = type; this.isRanged = isRanged;
		damage = 1;
	}

}