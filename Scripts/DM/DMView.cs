using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMView : MonoBehaviour {

	[SerializeField]
	private GameObject login;
	[SerializeField]
	private Button newGameButton;

	private DM controller;

	public void Init( DM controller ){
		this.controller = controller;
		newGameButton.onClick.AddListener( NewGame );
	}

	private void ToggleLogin( bool isActive ){
		login.SetActive( isActive );
	}

	private void NewGame(){
		controller.NewGame();
		ToggleLogin( false );
	}
}
