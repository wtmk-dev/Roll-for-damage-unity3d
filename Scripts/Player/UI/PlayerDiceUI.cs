using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiceUI : MonoBehaviour {

    public static readonly string TAG = "PlayerDiceUI";
    private int diceBoxCount = 3;
    private List<GameObject> goDiceBoxs = new List<GameObject>();
    private List<DieBoxUI> diceSets = new List<DieBoxUI>();

    void Awake(){
        goDiceBoxs = new List<GameObject>();
        var count = 0;
        do{
            Vector3 pos = transform.position;
            pos.x += 100f * count;
            GameObject clone = Instantiate( Resources.Load( DieBoxUI.TAG ), pos, Quaternion.identity ) as GameObject;
            clone.transform.parent = gameObject.transform;
            goDiceBoxs.Add( clone );
            clone.SetActive( false );
            count++;
        }while( count < diceBoxCount );
    }

    public void Init( List<Die> diceSets ){
        //Debug.Log( "hello" );
        var count = 0;

        foreach( GameObject go in goDiceBoxs ){
            DieBoxUI diceUI = go.GetComponent<DieBoxUI>();
            diceUI.Init( diceSets[ count ] );
            var offset = 10 * count;
            go.SetActive( true );
            count++;
        }
       // Debug.Log( diceSets.Count );
    }
	
}
