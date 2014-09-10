using UnityEngine;
using System.Collections;

public class PassiveSkill
{
	public enum Phase{
		Start,
		Attack,
		Defend,
		CastMagic,
		DefendMagic,
		Dead
	}
	public NinJaController parentController;
	public Phase phase;
	public GameController gameCtrl;
	public virtual void init(){
	
	}
	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public virtual void activeSkill()
	{

	}
}

