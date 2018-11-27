using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public delegate void PlayerToTown();
	public static event PlayerToTown OnPlayerToTown;
	public delegate void PlayerDead();
	public static event PlayerDead OnPlayerDead;

	private Player player = null;
	private GameObject goPlayerView;
	private PlayerView view;
	private bool isInDungeon;

	void OnEnable(){
		GameState.OnScreenChanged += OnScreenChanged;
		DungeonController.OnDungeonLevelUp += LevelUp;
	}

	void OnDisable(){
		GameState.OnScreenChanged -= OnScreenChanged;
		DungeonController.OnDungeonLevelUp -= LevelUp;
	}

	void Update(){
		if( isInDungeon ){
			view.UpdatePlayerStats( player.Blood, player.AP, player.MP, player.Stamina, player.Level );
			Mortality();
		}
	}

	public void Init( PlayerView view ){
		this.view = view;
		view.Init( this );
	}

	private void OnScreenChanged( GameState.GameScreen screen ){
		Debug.Log( screen );
		Debug.Log( "PlayerController: OnScreenChanged" );
		if( screen == GameState.GameScreen.CREATE ){
			EnterCreate();
		}
	}

//Create player
	private void EnterCreate(){
		view.EnterCreate();
	}

	public void BuildNewPlayer( Player.Job job ){
		player = new Player( job );
		Debug.Log( "new player" );
		Debug.Log( player );
	}

	private void EnterLootScreen( Player player ){
		view.EnterLoot( player );
	}

	private void EnterTown(){
		view.EnterTown( player );
	}

	private void EnterGameOver(){
		Debug.Log( "Enter game over" );
	}

	private void ExitDungeon(){
		view.ExitDungeon();
		isInDungeon = false;
	}

	public void ExitLootScreen(){
		player.ResetTempValues();
		view.ExitLoot();
		if( OnPlayerToTown != null ){
			OnPlayerToTown();
		}
	}

	private void LevelUp(){
		player.Level++;
		view.EnterNextRoom();

		DungeonTileUI.isSelecting = true;
	}

	private void Mortality(){
		if( player.Blood < 0 ){
			if( OnPlayerDead != null ){
				OnPlayerDead();
			}
		}
	}

	public void RollForDamage(){
		List<int> rolls = new List<int>();

		for( int i = 0; i < player.DiceCount; i++ ){
			rolls.Add( i );
		}

//		Debug.Log( rolls.Count );
		view.SetDiceRolled( rolls );

		player.Blood++;
		player.AP++;
		player.Stamina++;

		DungeonTileUI.isSelecting = false;
	}

	public void RollForLoot(){
		//check loot die
		player.LootRolls--;
		PlayerLootUI.isSelecting = false;
	}


}
