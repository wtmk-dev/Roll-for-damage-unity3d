using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DM : MonoBehaviour {

	private GameObject dmView;
	private DMView view;
	private GameObject player;
	private PlayerController playerController;
	private PlayerView playerView;
	private GameObject town;
	private TownController townController;
	private TownView townView;
	private GameObject dungeon;
	private DungeonController dungeonController;
	private DungeonView dungeonView;
	private List<GameObject> componets;

	private GameState gameScreenStats;

	private void OnEnable() {
		LoadComponets();
		BuildViews();
		BuildComponets();
		gameScreenStats = new GameState();
	}

	private void LoadComponets(){
		dmView = Resources.Load( "DMView" ) as GameObject;
		player = Resources.Load( "Player" ) as GameObject;
		town = Resources.Load( "Town" ) as GameObject;
		dungeon = Resources.Load( "Dungeon" ) as GameObject;
	}

	private void BuildViews(){
		dmView = Instantiate( dmView, dmView.transform.position, Quaternion.identity );
		view = dmView.GetComponent<DMView>();
		view.Init( this );
		playerView = dmView.GetComponentInChildren<PlayerView>();
		townView = dmView.GetComponentInChildren<TownView>();
		dungeonView = dmView.GetComponentInChildren<DungeonView>();
	}

	private void BuildComponets(){
		componets = new List<GameObject>();
		
		player = Instantiate( player, transform.position, Quaternion.identity );
		player.transform.parent = transform;
		playerController = player.GetComponent<PlayerController>();
		playerController.Init( playerView );
		componets.Add( player );
		
		town = Instantiate( town, transform.position, Quaternion.identity );
		town.transform.parent = transform;
		componets.Add( town );
		
		dungeon = Instantiate( dungeon, transform.position, Quaternion.identity );
		dungeon.transform.parent = transform;
		dungeonController = dungeon.GetComponent<DungeonController>();
		dungeonController.Init( dungeonView );
		componets.Add( dungeon );

		SetActiveComponets( false );
	}

	private void SetActiveComponets( bool isActive ){
		foreach (GameObject go in componets ){
			go.SetActive( isActive );
		}
	}

	public void NewGame(){
		player.SetActive( true );
		gameScreenStats.ChangeScreen( GameState.GameScreen.CREATE );
		Debug.Log( gameScreenStats.currentScreen );
	}
}
