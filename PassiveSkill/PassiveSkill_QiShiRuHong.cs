using UnityEngine;
using System.Collections;
/**
气势如虹，己方单位生命力提升5%
 **/
public class PassiveSkill_QiShiRuHong : PassiveSkill
{
	public override void init(){
		phase = PassiveSkill.Phase.Start;
	}

	public override void activeSkill(Object data)
	{
		if (parentController.side == GameController.Side.leftSide) {
			foreach(NinJaController entity in gameCtrl.leftEntities){
				entity.health+= (float)entity.health*0.05f;
			}		
		}else if(parentController.side == GameController.Side.rightSide){
			foreach(NinJaController entity in gameCtrl.rightEntities){
				entity.health+= (float)entity.health*0.05f;
			}		
		}

	}
}

