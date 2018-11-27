using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour {

	private static readonly System.Random random = new System.Random(); 
	private static System.Random rng = new  System.Random();  
	private static readonly object syncLock = new object();

	public delegate void PlayerExit();
	public static event PlayerExit OnPlayerExit;
	public delegate void DungeonLevelUp();
	public static event DungeonLevelUp OnDungeonLevelUp;

	private GameObject goView;
	private Player player;
	private DungeonView view;
	private Dictionary<int,DungeonTileUI> map;
	private List<DungeonTile> currentTiles;
	private DungeonTile currentTile;
	private bool isRunning;

	public static int RandomNumber(int min, int max){
		lock(syncLock) { // synchronize
			return random.Next(min, max);
		}
	}

	void OnEnable(){
		GameState.OnScreenChanged += DungeonRun;
		//PlayerController.OnPlayerCreated += LoadPlayer;
	}

	void OnDisable(){
		GameState.OnScreenChanged -= DungeonRun;
		//PlayerController.OnPlayerCreated -= LoadPlayer;
	}

	void Awake() { 
		map = new Dictionary<int, DungeonTileUI>();
	}

	void Start() {
	}

	void Update() { }

	public void Init( DungeonView view ){
		this.view = view;
		view.Init( this );
	}

	public void TileSelected( int id, int x, int y ){
		Debug.Log( "Tile Selected" );
		currentTile = currentTiles[id];
		currentTile.ID = id;

		Debug.Log( currentTile );
		Debug.Log( currentTile.ID );
		//TO:DO CHECK FOR EXIT
		if( currentTile.type == DungeonTile.Type.LOOT ){
			DungeonTileUI.isSelecting = false;
			//gain loot token
		}
		else if( currentTile.type == DungeonTile.Type.TRAP ){
			DungeonTileUI.isSelecting = false;
			//blow up
		}
		else{
			//fight moster or leave 
			int fightCost = GetFightCost( player.CurrentWepon.type, currentTiles[id].type );
			view.TileSelected( id, currentTiles[id].type, fightCost, DisplayExit( currentTile.type ) );
		}

	}

	public void Fight(){
		if( player.AP > 0 ){
			int damage = 0;

			for( int i = 0; i < player.CurrentWepon.damage; i++ ){
				damage += 1;//RandomNumber(1,1);
			}

			player.AP -= GetFightCost( player.CurrentWepon.type, currentTile.type );
			player.Kills++;
		}
		else{
			player.Blood--;
		}

		view.ResolveTile( currentTile.ID );
		DungeonTileUI.isSelecting = false;
		TileSelectedUI.isSelecting = false;
	}

	public void AtemptToRun(){
		if( player.Stamina > 0 ){
			player.Stamina--;
			view.RunSuccessful();
		}
		else{
			//roll to see if player takes damage
		}

		DungeonTileUI.isSelecting = false;
		TileSelectedUI.isSelecting = false;
	}

	public void LevelUp(){
		ShuffleTiles();
		for( int i = 0; i < map.Count; i++ ){
			map[ i ].isActive = true;
		}

		view.ResetTiles();

		DungeonTileUI.isSelecting = false;
		TileSelectedUI.isSelecting = false;

		if( OnDungeonLevelUp != null ){
			OnDungeonLevelUp();
		}
	}

	public void ExitToTown(){
		if( OnPlayerExit != null ){
			OnPlayerExit();
		}
	}

	public void BuildMap( int id, DungeonTileUI dungeonTileUI, int[] xy ){
		dungeonTileUI.Init( this, id, xy );
		map.Add( id, dungeonTileUI );
	}

	private void DungeonRun( GameState.GameScreen screen ){
		if( isRunning && screen == GameState.GameScreen.LOOT ){
			Reset();
		}
		else if( screen == GameState.GameScreen.GAMEOVER ){
			Reset();
		}
		else if( screen != GameState.GameScreen.DUNGEON ){
			return;
		}else{
			isRunning = true;
			StartNewRun();
		}
	}

	private void LoadPlayer( Player player ){
		this.player = player;
	}

	private void StartNewRun(){
		BuildDungeonTiles();
		ActivateTileUI();
		view.DisplayMap();
	}

	private void BuildDungeonTiles(){
		currentTiles = new List<DungeonTile>();
		var count = 0;

		do{
			if( !DungeonTile.exitPlaced ){
				DungeonTile exit = new DungeonTile();
				exit.SetExit();
				currentTiles.Add( exit );
			}
			else{
				DungeonTile tile = new DungeonTile();
				currentTiles.Add( tile );
			}
			count++;
		}while( count < map.Count );
		
	}

	private void ActivateTileUI(){
		for( int i = 0; i < map.Count; i++ ){
			map[i].isActive = true;
		}
	}

	private void ShuffleTiles(){
	 
		int n = currentTiles.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			DungeonTile value = currentTiles[k];  
			currentTiles[k] = currentTiles[n];  
			currentTiles[n] = value;  
		}  
		
	}

	private int GetFightCost( Weapon.Type pWeapon, DungeonTile.Type dType ){
		int cost = 0;

		if( pWeapon == Weapon.Type.SLASH ){
			if( dType == DungeonTile.Type.FLESH ){
				cost = 0;
			}
			else if( dType == DungeonTile.Type.FLYING ){
				cost = 1;
			}
			else if( dType == DungeonTile.Type.ARMORED ){
				cost = 1;
			}
		}
		else if( pWeapon == Weapon.Type.BLUNT ){

		}
		else if( pWeapon == Weapon.Type.PIERCE ){

		}
		
		return cost;
	}

	private bool DisplayExit( DungeonTile.Type type ){
		return type == DungeonTile.Type.EXIT ? true : false;
	}

	private void Reset(){
		isRunning = false;
		view.Reset();
	}

}