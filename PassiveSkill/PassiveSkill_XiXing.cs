using UnityEngine;
using System.Collections;
/**
吸星，攻击时吸收伤害5%转换为生命。
 **/
public class PassiveSkill_XiXing: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{
		//NinJaController enemy = data as NinJaController;
		parentController.health += parentController.attack*0.05f;
	}
}

