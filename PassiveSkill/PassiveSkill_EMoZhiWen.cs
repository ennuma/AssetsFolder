using UnityEngine;
using System.Collections;
/**
恶魔之吻，攻击时有较高几率使敌人进入中毒状态，持续10S
50% now
2hp/sec decrement
 **/
public class PassiveSkill_EMoZhiWen: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{
		float rnd = Random.Range (0, 100);
		if (rnd < 50) {
			NinJaController enemy = data as NinJaController;
			PoisionBuff poisionbuff = new PoisionBuff ();
			poisionbuff.duration = 10;
			poisionbuff.poisionIndex = 20;
			gameCtrl.attachBuffToEntityController (poisionbuff, enemy);
		}

	}
}

