using UnityEngine;
using System.Collections;
/**
激励，自身攻击力提升5%
 **/
public class PassiveSkill_JiLi : PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Start;
	}

	public override void activeSkill()
	{
		AttackBuff atkbuff = new AttackBuff ();
		atkbuff.duration = 3600;
		atkbuff.affectValue = (float)(parentController.getAttack () * 0.05);
		gameCtrl.attachBuffToEntityController (atkbuff, parentController);
	}
}

