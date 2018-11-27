using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreateUI : MonoBehaviour {

	public static readonly string TAG = "PlayerCreateUI";

	[SerializeField]
	private GameObject weponChoice;

	[SerializeField]
	private Button slash, blunt, ranged;

	private PlayerController controller;

	public void Init( PlayerController controller ){
		this.controller = controller;

		slash.onClick.AddListener( SlashPicked );
		blunt.onClick.AddListener( BluntPicked );
		ranged.onClick.AddListener( RangedPicked );

		ToggleWeponChoice( false );
	}

	private void SlashPicked(){
		controller.BuildNewPlayer( Player.Job.SLASH );
	}

	private void BluntPicked(){
		controller.BuildNewPlayer( Player.Job.BLUNT );
	}

	private void RangedPicked(){
		controller.BuildNewPlayer( Player.Job.RANGED );
	}

	public void ToggleWeponChoice( bool isActive ){
		weponChoice.SetActive( isActive );
	}

}
