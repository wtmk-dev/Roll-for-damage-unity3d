using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDungeonUI : MonoBehaviour {

	private PlayerController controller;
	public GameObject goButton;
	public GameObject goStats;
	public GameObject goRolls;
	public List<GameObject> gameObjects;
	private TextMeshProUGUI stats;
	private TextMeshProUGUI rolls;
	private Button button;
	private Text buttonText;
	private bool hasRolled;

	private void OnEnable() {
		if( controller != null ){
			SubEvents();
		}
	}

	private void OnDisable() {
		if( controller != null ){
			UnsubEvents();
		}
	}

	public void Init( PlayerController controller ){
		this.controller = controller;
		LoadObjects();
		SubEvents();
		Debug.Log( "Player Dungeon Ui init" );
	}

	private void LoadObjects(){
		gameObjects = new List<GameObject>();

		stats = goStats.GetComponent<TextMeshProUGUI>();
		gameObjects.Add( goStats );
		goStats.SetActive( false );

		rolls = goRolls.GetComponent<TextMeshProUGUI>();
		gameObjects.Add( goRolls );
		goRolls.SetActive( false );

		button = goButton.GetComponent<Button>();
		buttonText = goButton.GetComponentInChildren<Text>();
		gameObjects.Add( goButton );
		goButton.SetActive( false );
	}

	private void SubEvents(){
		//controller.OnPlayerCreated += PlayerCreated;
	}

	private void UnsubEvents(){
		//controller.OnPlayerCreated -= PlayerCreated;
	}

	public void SetDiceRolled( List<int> rolls ){
		if( !goRolls.activeSelf ){
			goRolls.SetActive( true );
		}
		string rolltxt = "";
		foreach( int roll in rolls ){
			rolltxt += "[ " + roll + " ] ";
		}
		this.rolls.SetText( rolltxt );
	}

	public void UpdateStats( int blood, int ap, int mp, int stamina, int level ){
		if( !goStats.activeSelf ){
			goStats.SetActive( true );
		}
		string txt = "Blood: " + blood + "\n" +
					 "AP: " + ap + "\n" +
					 "MP: " + mp + "\n" +
					 "Stamina: " + stamina + "\n" +
					 "Level: " + level + "\n";
		this.stats.SetText( txt );
	}

	public void Button(){
		if( !hasRolled ){
			controller.RollForDamage();
			hasRolled = true;
			goButton.SetActive( false );
		}
	}

	public void ResetRoll(){
		if( hasRolled ){
			hasRolled = false;
			rolls.text = "";
			goButton.SetActive( true );
		}
	}

	public void Reset(){
		Toggel( false );
		hasRolled = false;
		Debug.Log( "PlayerDungeon UI Off" );
	}

	private void Toggel( bool isActive ){
		foreach( GameObject go in gameObjects ){
			go.SetActive( isActive );
		}
	}


}
