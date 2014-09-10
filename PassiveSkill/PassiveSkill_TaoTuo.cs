using UnityEngine;
using System.Collections;
/**
逃脱，自身移动速度提升5%
 **/
public class PassiveSkill_TaoTuo : PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Start;
	}

	public override void activeSkill(Object data)
	{
		SpeedBuff atkbuff = new SpeedBuff ();
		atkbuff.duration = 3600;
		atkbuff.affectValue = (float)(parentController.getVelocity () * 0.05);
		gameCtrl.attachBuffToEntityController (atkbuff, parentController);
	}
}

