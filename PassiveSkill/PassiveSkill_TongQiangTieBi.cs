using UnityEngine;
using System.Collections;
/**
铜墙铁壁，自身所受伤害降低3%
 **/
public class PassiveSkill_TongQiangTieBi: PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Start;
	}

	public override void activeSkill(Object data)
	{
		PhysicHurtMultiplierBuff buff1 = new PhysicHurtMultiplierBuff ();
		buff1.duration = 3600;
		buff1.affectValue = 0.03f;
		gameCtrl.attachBuffToEntityController (buff1, parentController);

		MagicHurtMultiplier buff2 = new MagicHurtMultiplier ();
		buff1.duration = 3600;
		buff1.affectValue = 0.03f;
		gameCtrl.attachBuffToEntityController (buff2, parentController);
	}
}

