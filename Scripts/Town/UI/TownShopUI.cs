using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownShopUI : MonoBehaviour {

	public static readonly string TAG = "TownShopUI";
    [SerializeField]
	private List<Sprite> faceSprites;
	private int diceBoxCount = 3;
	private List<GameObject> goFaces = new List<GameObject>();
	private List<FaceUI> itemsForSale = new List<FaceUI>();

	void Awake(){
        goFaces = new List<GameObject>();
        var count = 0;
        do{
            Vector3 pos = transform.position;
            pos.y += 100f * count;
            GameObject clone = Instantiate( Resources.Load( FaceUI.STORE_TAG ), pos, Quaternion.identity ) as GameObject;
            clone.transform.parent = gameObject.transform;
            goFaces.Add( clone );
            clone.SetActive( false );
            count++;
        }while( count < diceBoxCount );

    }

	public void Init( List<Face> diceSets ){
        Debug.Log( "hello" );
        Debug.Log( diceSets.Count );
        for( int i = 0; i < diceSets.Count; i++ ){
            FaceUI faceUI = goFaces[ i ].GetComponent<FaceUI>();
            Debug.Log( faceUI.nameId );
            faceUI.Init( diceSets[i], faceSprites[i] );
            int offset = 10 * i;
            goFaces[ i ].SetActive( true );
        }
    }


}
