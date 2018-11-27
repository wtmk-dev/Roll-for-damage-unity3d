using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownController : MonoBehaviour {

	private GameObject goView;
	private TownView view;

	void OnEnable(){
		GameState.OnScreenChanged += OnScreenChanged;
	}

	void OnDisable(){
		GameState.OnScreenChanged -= OnScreenChanged;
	}

	void Start(){
		Init();
	}

	private void Init(){
		goView = GameObject.FindGameObjectWithTag( TownView.TAG );
		view = goView.GetComponent<TownView>();
		view.Init( this );
	}

	private void OnScreenChanged( GameState.GameScreen screen ){
		Debug.Log( "Town Controller" );
		Debug.Log( screen );
		if( screen == GameState.GameScreen.TOWN ){
			EnterTown();
		}
	}

	private void EnterTown(){
		Debug.Log( "Entring Town" );
		view.EnterTown( DebugDie() );
	}

	private List<Face> DebugDie(){
		List<Face> faces = new List<Face>();
		for( int i = 0; i < 3; i++ ){
			Face x = new Face( 9 + i );
			faces.Add( x );
		}

		return faces;
	}


	
}
