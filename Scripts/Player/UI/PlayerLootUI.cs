using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLootUI : MonoBehaviour {

	public static bool isSelecting = false;

	private Player player;
	private PlayerController controller;
	private List<GameObject> gameObjects;
	
	public GameObject goButton;
	public GameObject goRollResult;
	public GameObject goRollsReamaingText;
	public GameObject goWins;
	public GameObject goTitleText;

	private Button button;
	private TextMeshProUGUI rollsReamaing;
	private TextMeshProUGUI winsText;
	private TextMeshProUGUI titleText;
	private Image rollsResultSprite;

	public void Init( Player player, PlayerController controller ){
		LoadObjects();
		this.player = player; this.controller = controller;
		DisplayLootUI( player );
	}

	public void Button(){
		//roll for loot.
		if( player.LootRolls > 0 ){
			isSelecting = true;
			UpdateRemainingRolls( player.LootRolls );
			controller.RollForLoot();
			if( player.LootRolls == 0 ){
				button.GetComponentInChildren<Text>().text = "Exit to town";
				rollsReamaing.text = "Good haul?";
			}
		}
		else{
			controller.ExitLootScreen();
		}
		
	}

	private void UpdateRemainingRolls( int lootRools ){
		rollsReamaing.text = GetNumberOfRollStrings( lootRools );
	}

	private void DisplayLootUI( Player player ){
		goRollResult.SetActive( true );
		goButton.SetActive( true );

		UpdateRemainingRolls( player.LootRolls );
		goRollsReamaingText.SetActive( true );

		goWins.SetActive( true );

		titleText.text = "Loot!";
	}

	private string GetNumberOfRollStrings( int kills ){
		string rolls = kills + " : rolls reamaing";
		return rolls;
	}

	private void LoadObjects(){
		gameObjects = new List<GameObject>();

		button = goButton.GetComponent<Button>();
		gameObjects.Add( goButton );
		goButton.SetActive( false );

		rollsReamaing = goRollsReamaingText.GetComponent<TextMeshProUGUI>();
		gameObjects.Add( goRollsReamaingText );
		goRollsReamaingText.SetActive( false );

		winsText = goWins.GetComponent<TextMeshProUGUI>();
		gameObjects.Add( goWins );
		goWins.SetActive( false ); 

		rollsResultSprite = goRollResult.GetComponent<Image>();
		gameObjects.Add( goRollResult );
		goRollResult.SetActive( false );

		titleText = goTitleText.GetComponent<TextMeshProUGUI>();
		gameObjects.Add( goTitleText );
		//goTitleText.SetActive( false );
	}

	private void Toggel( bool isActive ){
		foreach( GameObject go in gameObjects ){
			go.SetActive( isActive );
		}
	}

	public void Reset(){
		isSelecting = false;
		gameObject.SetActive( false );
	}
}
