using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonTileUI : MonoBehaviour {

	public static bool isSelecting = false;
	public bool isActive = false;
	public int[] pos;
	private int id;
	private DungeonController controller;
	private Image spriteRenderer;

	public void Init( DungeonController controller, int id, int[] pos ){
		spriteRenderer = GetComponent<Image>();
		this.controller = controller;
		this.id = id;
		this.pos = pos;
		isActive = false;
	}

	public void Select(){
//		Debug.Log( isActive );
//		Debug.Log( isSelecting );
//		Debug.Log( "Selecting" );
		if( isActive && !isSelecting ){
			isActive = false;
			isSelecting = true;
			controller.TileSelected( id, pos[0], pos[1] );
		}
	}

	public void SetSprite( Sprite sprite ){
		spriteRenderer.sprite = sprite;
	}

	public void Reset(){
		isActive = false;
		isSelecting = false;
		Deactivate();
	}

	private void Deactivate(){
		gameObject.SetActive( false );
	}

}
