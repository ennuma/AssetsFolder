using UnityEngine;
using System.Collections;

public class PoisionBuff : Buff
{

	public float poisionIndex;

	public override void elapse(float decBuffTime)
	{
		Debug.Log ("here");
		duration -= decBuffTime;
		parentController.health = Mathf.Max(1,parentController.health-(poisionIndex / 10*decBuffTime));

		if (duration <= 0) {
			gameCtrl.dettachBuffFromEntityController(this,parentController);	
		}
	}


	protected override void Begin()
	{

	}

	protected override void End()
	{

	}
}

