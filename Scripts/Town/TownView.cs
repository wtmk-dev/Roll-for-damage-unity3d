using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownView : MonoBehaviour {

	public static readonly string TAG = "TownView";
	private TownController controller;
	private List<GameObject> uiPrefabs = new List<GameObject>();
	private List<GameObject> avaliableUI = new List<GameObject>();
	private TownShopUI townShopUI;

	void Awake(){
		uiPrefabs = new List<GameObject>();
		uiPrefabs.Add( Resources.Load( TownShopUI.TAG ) as GameObject );
	}

	public void Init( TownController controller ){
		this.controller = controller;
		GetUiScreens();
	}

	private void GetUiScreens(){
		foreach( GameObject child in uiPrefabs ){
			GameObject clone = Instantiate( child, transform.position, Quaternion.identity );
			clone.transform.parent = gameObject.transform;
			avaliableUI.Add( clone );
			clone.SetActive( false );
		}
	}

	public void EnterTown( List<Face> itemsForSale ){
		Debug.Log( itemsForSale );
		avaliableUI[ 0 ].SetActive( true );
		townShopUI = avaliableUI[ 0 ].GetComponent<TownShopUI>();
		townShopUI.Init( itemsForSale );
	}
}
