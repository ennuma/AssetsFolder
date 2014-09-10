using UnityEngine;
using System.Collections;
/**
瞒天过海，单位死亡是有几率复活，生命力20%
50% now
 **/
public class PassiveSkill_ManTianGuoHai : PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Dead;
	}

	public override void activeSkill(Object data)
	{
		float rnd = Random.Range (0, 100);
		if (rnd < 50) {
			parentController.health = (float)parentController.maxhealth*0.2f;
			parentController.revive();
			//Debug.Log("revive");
		}
	}
}

