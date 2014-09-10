using UnityEngine;
using System.Collections;
/**
金刚不坏，自身防御力提升5%
 **/
public class PassiveSkill_JinGangBuHuai: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Start;
	}

	public override void activeSkill(Object data)
	{
		DefendBuff atkbuff = new DefendBuff ();
		atkbuff.duration = 3600;
		atkbuff.affectValue = (float)(parentController.getDefence () * 0.05);
		gameCtrl.attachBuffToEntityController (atkbuff, parentController);
	}
}

