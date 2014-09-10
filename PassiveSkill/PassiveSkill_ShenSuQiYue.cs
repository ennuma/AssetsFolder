using UnityEngine;
using System.Collections;
/**
神速契约，己方所有单位攻击速度提升5%
 **/
public class PassiveSkill_ShenSuQiYue : PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Start;
	}

	public override void activeSkill(Object data)
	{
		if (parentController.side == GameController.Side.leftSide) {
			foreach(NinJaController entity in gameCtrl.leftEntities){
				SpeedBuff atkbuff = new SpeedBuff ();
				atkbuff.duration = 3600;
				atkbuff.affectValue = (float)(entity.getVelocity () * 0.05);
				gameCtrl.attachBuffToEntityController (atkbuff, entity);
			}		
		}else if(parentController.side == GameController.Side.rightSide){
			foreach(NinJaController entity in gameCtrl.rightEntities){
				SpeedBuff atkbuff = new SpeedBuff ();
				atkbuff.duration = 3600;
				atkbuff.affectValue = (float)(entity.getVelocity () * 0.05);
				gameCtrl.attachBuffToEntityController (atkbuff, entity);
			}		
		}

	}
}

