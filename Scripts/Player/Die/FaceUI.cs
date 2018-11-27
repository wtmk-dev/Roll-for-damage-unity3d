using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceUI : MonoBehaviour {

	public static readonly string STORE_TAG = "FaceUI";
	[SerializeField]
	private List<Sprite> faceSprites;
	public GameObject faceSlot;
	public string nameId = "faceid";

	public void Init( Face dieSet, Sprite sprite ){
		Debug.Log( "Im a die face" + dieSet );
		faceSlot.SetActive( true );
		faceSlot.GetComponent<Image>().sprite = sprite;
	}

	private Sprite GetSpriteFromId( int id ){
		return faceSprites[ id ];
	}


}
