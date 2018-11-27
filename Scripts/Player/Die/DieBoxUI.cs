using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieBoxUI : MonoBehaviour {

	public static readonly string TAG = "DiceBoxUI";
	[SerializeField]
	private List<Sprite> faceSprites;
	public GameObject faceSlot;
	public List<GameObject> currentFaces;

	void Awake(){
		currentFaces = new List<GameObject>();
	}

	public void Init( Die dieSet ){
		for( int i = 0; i < dieSet.faces.Count; i++ ){
			Vector3 pos = transform.transform.position;
			pos.y += -65f * i;
			GameObject face = Instantiate( faceSlot, pos, Quaternion.identity );
			face.GetComponent<Image>().sprite = GetSpriteFromId( dieSet.faces[ i ].ID );
			face.transform.parent = gameObject.transform;
			currentFaces.Add( face );
			face.SetActive( true );
		}
	}

	private Sprite GetSpriteFromId( int id ){
		return faceSprites[ id ];
	}

}
