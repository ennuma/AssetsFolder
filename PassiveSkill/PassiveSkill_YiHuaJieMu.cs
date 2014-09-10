using UnityEngine;
using System.Collections;
/**
移花接木，攻击时给敌人周围敌人造成攻击的10%溅射伤害。
 **/
public class PassiveSkill_YiHuaJieMu : PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Attack;
	}

	public override void activeSkill(Object data)
	{
		//TODO
		//NinJaController ct = data as NinJaController;
		//Vector3 cpos = Camera.main.WorldToScreenPoint (ct.gameObject.transform.position);
	}
}

