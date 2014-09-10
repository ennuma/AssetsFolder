using UnityEngine;
using System.Collections;
/**
策反，攻击时较低几率使敌人攻击敌方。
 **/
public class PassiveSkill_CeFan: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{
		NinJaController enemy = data as NinJaController;
		BetrayBuff buff = new BetrayBuff ();
		buff.duration = 3.0f;
		gameCtrl.attachBuffToEntityController (buff, enemy);	
	}
}

