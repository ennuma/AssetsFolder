using UnityEngine;
using System.Collections;
/**
暗杀，攻击时较低几率造成敌人生命力减半。 
50% now
 **/
public class PassiveSkill_AnSha : PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{
		float rnd = Random.Range (0, 100);
		if (rnd < 50) {
			NinJaController enemy = data as NinJaController;
			enemy.health /= 2;
			//Debug.Log("Assassin");
		}
	}
}

