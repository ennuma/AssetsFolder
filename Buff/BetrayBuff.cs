using UnityEngine;
using System.Collections;

public class BetrayBuff : Buff
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

	protected override void Begin(){
		parentController.setBetray (true);
	}

	protected override void End(){
		parentController.setBetray (false);
	}
	
}

