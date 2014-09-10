using UnityEngine;
using System.Collections;
/**
不屈，生命力低于50%时受到攻击有几率回复生命3%
50% now
 **/
public class PassiveSkill_BuQu: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Defend;
	}

	public override void activeSkill(Object data)
	{
		float rnd = Random.Range (0, 100);
		if (rnd < 50) {
			//NinJaController enemy = data as NinJaController;
			parentController.health += parentController.maxhealth*0.03f;
		}

	}
}

