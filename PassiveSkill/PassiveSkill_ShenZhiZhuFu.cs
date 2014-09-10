using UnityEngine;
using System.Collections;
/**
神之祝福，己方所有单位幸运增加5%
 **/
public class PassiveSkill_ShenZhiZhuFu : PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Start;
	}

	public override void activeSkill(Object data)
	{
		if (parentController.side == GameController.Side.leftSide) {
			foreach(NinJaController entity in gameCtrl.leftEntities){
				LuckyBuff atkbuff = new LuckyBuff ();
				atkbuff.duration = 3600;
				atkbuff.affectValue = (float)(entity.getLucky () * 0.05);
				gameCtrl.attachBuffToEntityController (atkbuff, entity);
			}		
		}else if(parentController.side == GameController.Side.rightSide){
			foreach(NinJaController entity in gameCtrl.rightEntities){
				LuckyBuff atkbuff = new LuckyBuff ();
				atkbuff.duration = 3600;
				atkbuff.affectValue = (float)(entity.getLucky () * 0.05);
				gameCtrl.attachBuffToEntityController (atkbuff, entity);
			}		
		}

	}
}

