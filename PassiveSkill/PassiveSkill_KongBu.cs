using UnityEngine;
using System.Collections;
/**
恐怖，攻击时一定几率使敌人进入封锁状态持续1S（不能攻击）
 **/
public class PassiveSkill_KongBu: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{
		NinJaController enemy = data as NinJaController;

		AttackIntervalBuff buff1 = new AttackIntervalBuff ();
		buff1.duration = 1;
		buff1.affectValue = 3600;
		gameCtrl.attachBuffToEntityController (buff1, enemy);

	}
}

