using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileSelectedUI : MonoBehaviour {

	public static bool isSelecting = false;
	private bool isExit = false;
	private DungeonController controller;
	public GameObject goRunButton;
	public GameObject goFightButton;
	public GameObject goFightCostText;
	public GameObject goRunCostText;
	public GameObject goImage;

	private List<GameObject> gameObjects = new List<GameObject>();

	private Button fightButton;
	private Button runButton;
	private TextMeshProUGUI fightCostText;
	private TextMeshProUGUI runCostText;
	private Image sprite;

	public void Init( DungeonController controller ){
		gameObjects = new List<GameObject>();
		this.controller = controller;
		SetUpComponets();
	}

	public void SetSelection( Sprite currentSprite, string fightCost, bool isExit ){
		Debug.Log( "SetSelection" );
		
		sprite.sprite = currentSprite;
		fightCostText.text = fightCost;
		
		fightButton.GetComponentInChildren<Text>().text = "Fight!";
		runButton.GetComponentInChildren<Text>().text = "Run!";
		
		if( !goFightCostText.activeSelf ){
			goFightCostText.SetActive( true );
		}
		
		if( isExit ){
			this.isExit = isExit;
			goFightButton.GetComponentInChildren<Text>().text = "Leave?";
			goRunButton.GetComponentInChildren<Text>().text = "Go deeper!";
			goFightCostText.SetActive( false );
		}
	}

	public void FightButton(){
		if( !isSelecting ){
			isSelecting = true;
			if( !isExit ){
				controller.Fight();
				gameObject.SetActive( false );
			}
			else{
//				Debug.Log( "exit the dungon" );
				controller.ExitToTown();
				Reset();
				gameObject.SetActive( false );
			}
		}
	}

	public void RunButton(){
		if( !isSelecting ){
			isSelecting = true;
			if( !isExit ){
				controller.AtemptToRun();
				gameObject.SetActive( false );
			}
			else{
//				Debug.Log( "Set up level 2" );
				controller.LevelUp();
				Reset();
				gameObject.SetActive( false );
			}
			
		}
	}

	public void Toggle( bool isActive ){
		foreach( GameObject go in gameObjects ){
			go.SetActive( isActive );
		}
	}

	private void SetUpComponets(){
		runButton = goRunButton.GetComponent<Button>();
		gameObjects.Add( goRunButton );

		runCostText = goRunCostText.GetComponent<TextMeshProUGUI>();
		gameObjects.Add( goRunCostText );
	
		fightButton = goFightButton.GetComponent<Button>();
		gameObjects.Add( goFightButton );

		fightCostText = goFightCostText.GetComponent<TextMeshProUGUI>();
		gameObjects.Add( goFightCostText );

		sprite = goImage.GetComponent<Image>();
		gameObjects.Add( goImage );
	}

	private void Reset(){
		isExit = false;
	}

	
}
