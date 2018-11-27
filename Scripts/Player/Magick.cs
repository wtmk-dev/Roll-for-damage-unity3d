using System.Collections;
using System.Collections.Generic;


public class Magick
{

	public enum Type { FIRE, WATER, ELECTRIC }
	private Type type;
	private bool isRanged = false;

	public Magick(Type type, bool isRanged)
	{
		this.type = type; this.isRanged = isRanged;
	}

}