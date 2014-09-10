using UnityEngine;
using System.Collections;
/**
激刃，攻击时降低敌人体魄10% 
duration : 10s
 **/
public class PassiveSkill_JiRen: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{
		NinJaController enemy = data as NinJaController;
		DefendBuff buff = new DefendBuff ();
		buff.duration = 10f;
		buff.affectValue = (-enemy.getDefence() * 0.1f);
		gameCtrl.attachBuffToEntityController (buff, enemy);

	}
}

