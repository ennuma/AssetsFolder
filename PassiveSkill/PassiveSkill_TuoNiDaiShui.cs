using UnityEngine;
using System.Collections;
/**
拖泥带水，攻击时减低敌人移动速度10%
duration : 10sec
 **/
public class PassiveSkill_TuoNiDaiShui: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{

		NinJaController enemy = data as NinJaController;
		SpeedBuff buff = new SpeedBuff ();
		buff.duration = 10f;
		buff.affectValue = -enemy.getVelocity() * 0.1f;
		gameCtrl.attachBuffToEntityController (buff, enemy);
	}
}

