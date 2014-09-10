using UnityEngine;
using System.Collections;
/**
李代桃僵，收到伤害时，一定几率反弹敌人攻击。
50% now
 **/
public class PassiveSkill_LiDaiTaoJiang: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Defend;
	}

	public override void activeSkill(Object data)
	{
		float rnd = Random.Range (0, 100);
		if (rnd < 50) {
			NinJaController enemy = data as NinJaController;
			if(parentController == enemy){
				return;
			}
			enemy.takeDamageFromEnemy(enemy);
		}

	}
}

