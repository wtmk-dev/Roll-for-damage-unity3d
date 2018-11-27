using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonView : MonoBehaviour {
	public static readonly string TAG = "DungeonView";

	[SerializeField]
	private List<Sprite> tileSprites = new List<Sprite>();
	private DungeonController controller;
	private GameObject goTile;
	private GameObject goTileSelectUI;
	private List<GameObject> uiPrefabs = new List<GameObject>();
	private List<GameObject> goMap = new List<GameObject>();
	private List<GameObject> avaliableUI = new List<GameObject>();
	private List<DungeonTile> map = new List<DungeonTile>();
	private int mapX = 4;
	private int mapY = 4;
	private Vector3 tilePos = new Vector3();
	private Vector3 tileUIpos = new Vector3();

	void Awake(){
		goTile = Resources.Load("Tile") as GameObject;
		goTileSelectUI = Resources.Load("TileSelectedUI") as GameObject;
		tilePos = gameObject.transform.position;
		tileUIpos = new Vector3 ( -475f, 240f, 0f );
	}

	public void Init( DungeonController controller ){
		this.controller = controller; 
		BuildMap();
		SpawnUI();
	}	

	public void DisplayMap( bool isActive = true ){
		foreach( GameObject go in goMap ){
			go.SetActive( isActive );
		}

		DungeonTileUI.isSelecting = false;
	}

	public void TileSelected( int id, DungeonTile.Type type, int fightCost, bool isExit ){
		Sprite currentSprite = GetSprite( type );
		FlipTile(  goMap[id], currentSprite );
		DisplayTileSelection( avaliableUI[0], currentSprite, fightCost, isExit ); 
	}

	public void ResolveTile( int id ){
		FlipTile( goMap[id], tileSprites[2] );
	}

	public void RunSuccessful(){
		Debug.Log( "I know youre sad bud but she didn't want you" + 
				   " the code dont just want you it needs you to exist" );

	}

	private void FlipTile( GameObject tile, Sprite currentSprite ){
		if( currentSprite != null ){
			tile.GetComponent<Image>().sprite = currentSprite;
		}else{
			Debug.Log( "ERROR: Sprite is null" );
		}
	}

	private void DisplayTileSelection( GameObject tile, Sprite currentSprite, int fightCost, bool isExit ){
		if( currentSprite != null ){
			Debug.Log( tile );
			tile.GetComponent<TileSelectedUI>().SetSelection( currentSprite, GetFightCostTxt( fightCost ), isExit );
			tile.SetActive( true );
		}

	}

	private Sprite GetSprite( DungeonTile.Type type ){
		Sprite currentSprite = null;
		
		if( type == DungeonTile.Type.EXIT ){
			currentSprite = tileSprites[0];
		}
		else if( type == DungeonTile.Type.FLESH ){
			currentSprite = tileSprites[1];
		}
		else if( type == DungeonTile.Type.TRAP ){
			currentSprite = tileSprites[5];
		}
		else if( type == DungeonTile.Type.FLYING ){
			currentSprite = tileSprites[6];
		}
		else if( type == DungeonTile.Type.LOOT ){
			currentSprite = tileSprites[4];
		}
		else if( type == DungeonTile.Type.ARMORED ){
			currentSprite = tileSprites[7];
		}

		return currentSprite;
	}

	private string GetFightCostTxt( int fightCost ){
		string cost = "AC: " + fightCost;
		return cost;
	}

	private void BuildMap(){	
		var count = 0;
		goMap = new List<GameObject>();
		for ( int i = 0; i < mapX; i++ ) {
			for ( int j = 0; j < mapY; j++ ){
				tilePos = gameObject.transform.position;
				tilePos.x -= 100 * i;
				tilePos.y += 100 * j;
				int[] pos = { i, j };
				SpawnTile(tilePos, count, pos );
				count++;
			}
		}

		DungeonTileUI.isSelecting = true;
	}

	private void SpawnTile( Vector3 pos, int id, int[] xy ){
		GameObject clone = Instantiate(goTile, pos, Quaternion.identity);
		clone.transform.parent = gameObject.transform;
		
		DungeonTileUI dtUI = clone.GetComponent<DungeonTileUI>();
		controller.BuildMap( id, dtUI, xy );
		
		goMap.Add( clone );
		clone.SetActive( false );
	}

	private void SpawnUI(  ){
		//selection screen
		GameObject goTileUI = Instantiate( goTileSelectUI, transform.position, Quaternion.identity );
		goTileUI.transform.position += tileUIpos;
		goTileUI.transform.parent = gameObject.transform;
		TileSelectedUI tileUI = goTileUI.GetComponent<TileSelectedUI>();
		tileUI.Init( controller );
		avaliableUI.Add( goTileUI );
		goTileUI.SetActive( false );
		//...
		//
	}

	public void ResetTiles(){
		foreach( GameObject go in goMap ){
			FlipTile( go, tileSprites[3] );
		}
	}

	public void Reset(){
		DisplayMap( false );
	}

}
