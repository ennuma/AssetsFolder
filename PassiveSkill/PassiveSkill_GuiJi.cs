using UnityEngine;
using System.Collections;
/**
诡计，攻击时一定几率使敌人进入混乱状态，持续3S（随机方向自动行动）
50% now
 **/
public class PassiveSkill_GuiJi: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{
		float rnd = Random.Range (0, 100);
		if (rnd < 50) {
			NinJaController enemy = data as NinJaController;
			WildBuff wildbuff = new WildBuff ();
			wildbuff.duration = 3;
			gameCtrl.attachBuffToEntityController (wildbuff, enemy);
		}

	}
}

