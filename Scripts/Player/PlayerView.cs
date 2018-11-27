using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {

	public static readonly string TAG = "PlayerView";
	private PlayerController controller;
	private List<GameObject> uiScreens = new List<GameObject>();
	private List<GameObject> uiPrefabs = new List<GameObject>();
	private PlayerDungeonUI dungeonUI;
	private PlayerLootUI lootUI;
	private PlayerDiceUI diceUI;
	private PlayerCreateUI createUI;

	void Awake(){ 
		uiPrefabs = new List<GameObject>();
		uiPrefabs.Add( Resources.Load("PlayerDungeonUI") as GameObject );
		uiPrefabs.Add( Resources.Load("PlayerLootUI") as GameObject );
		uiPrefabs.Add( Resources.Load( PlayerDiceUI.TAG ) as GameObject );
		uiPrefabs.Add( Resources.Load( PlayerCreateUI.TAG ) as GameObject );
	}

	public void Init( PlayerController controller ){
		this.controller = controller;
		Debug.Log( "PlayerView Init" );
		GetUiScreens();
		InitUiScreens();
	}

	private void GetUiScreens(){
		foreach( GameObject child in uiPrefabs ){
			GameObject clone = Instantiate( child, transform.position, Quaternion.identity );
			clone.transform.parent = gameObject.transform;
			uiScreens.Add( clone );
			clone.SetActive(false);
		}
	}

	private void InitUiScreens(){
		dungeonUI = uiScreens[ 0 ].GetComponent<PlayerDungeonUI>();
		dungeonUI.Init( controller );

		createUI = uiScreens[ 3 ].GetComponent<PlayerCreateUI>();
		createUI.Init( controller );
	}

	public void UpdatePlayerStats( int blood, int ap, int mp, int stamina, int level ){
		dungeonUI.UpdateStats( blood, ap, mp, stamina, level );
	}

	public void StartNewRun( Player player ){
		uiScreens[0].SetActive( true );
		dungeonUI = uiScreens[0].GetComponent<PlayerDungeonUI>();
		//dungeonUI.Init( player, controller );
	}

	public void EnterCreate(){
		uiScreens[ 3 ].SetActive( true );
		createUI.ToggleWeponChoice( true );
	}

	public void EnterLoot( Player player ){
		uiScreens[1].SetActive( true );
		lootUI = uiScreens[1].GetComponent<PlayerLootUI>();
		lootUI.Init( player, controller );
	}

	public void EnterTown( Player player ){
		uiScreens[2].SetActive( true );
		diceUI = uiScreens[2].GetComponent<PlayerDiceUI>();
		diceUI.Init( player.diceSets );
	}

	public void EnterNextRoom(){
		dungeonUI.ResetRoll();
	}

	public void ExitDungeon(){
		dungeonUI.Reset();
	}

	public void ExitLoot(){
		lootUI.Reset();
	}

	public void SetDiceRolled( List<int> rolls ){
		dungeonUI.SetDiceRolled( rolls );
	}

//Tools
	public void SetGameObjectActive( bool isActive ){
		gameObject.SetActive( isActive );
	}

}
