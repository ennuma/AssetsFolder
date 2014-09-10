using UnityEngine;
using System.Collections;

public class WildBuff : Buff
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
		parentController.setWild (true);
	}

	protected override void End(){
		parentController.setWild (false);
	}
	
}

