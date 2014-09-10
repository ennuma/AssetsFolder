using UnityEngine;
using System.Collections;

public class SpeedBuff : Buff
{
	
	public float affectValue;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

	protected override void Begin(){
		float currentV = parentController.getAtkInterval ();
		float resultV = currentV + affectValue;
		parentController.setAttack (resultV);
	}

	protected override void End(){
		float currentV = parentController.getAtkInterval ();
		float resultV = currentV - affectValue+duration;
		parentController.setAtkInterval (resultV);
		//Debug.Log (resultV);
	}
	
}

