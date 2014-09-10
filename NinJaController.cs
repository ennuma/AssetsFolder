using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NinJaController : MonoBehaviour {

	public GameController ctrl;
	public GameController.Side side;
	public float attackCoolDown;
	public bool isDead = false;
	public float manaSpeed = 10;
	
	protected float currentMana = 0;
	protected float manaRequiredToCast = 1000;
	protected bool isFacingRight;
	protected float horiVelocity = 10.0f;
	protected float vertVelocity = 6.0f;
	protected float velocity = 1.0f;
	protected bool isFighting = false;
	protected Animator animator;
	protected float _attackCoolDown;
	protected bool canCast = false;
	protected bool isCasting = false;
	protected bool hasCast = false;

	public int maxhealth = 30;
	public float health;
	public float attack = 10;
	protected float defence = 10;
	protected float lucky = 10;
	protected float buffTimeElapseMultiplier = 1.0f;
	protected float physicalHurtMultiplier = 1.0f;//when physic def
	protected float magicHurtMultiplier = 1.0f;//when magic def
	protected float magicPowerMultiplier = 1.0f;//when magic atk
	protected float physicPowerMultiplier = 1.0f;//when physic atk
	protected bool isPause = false;

	protected List<Buff> buffList = new List<Buff>();
	protected List<MagicController> magicList = new List<MagicController>();
	protected List<PassiveSkill> passiveSkillList = new List<PassiveSkill>();
	protected enum State{
		Find,
		Fight,
		Def,
		Escape,
		Cast,
		Dead,
		Wild
	}

	protected State state; 
	public NinJaController targetEnemy;
	protected string healthBarResource = "prefab/HealthBar" ;
	protected string manaBarResource = "prefab/ManaBar" ;
	protected Vector3 randomPosition;
	public NinJaController attackTarget;
	// Use this for initialization
	void Start () {
		gameObject.tag = "entity";

		animator = GetComponent<Animator>();
		state = State.Find;
		if (transform.localScale.x > 0) {
			isFacingRight = false;		
		} else {
			isFacingRight = true;		
		}
		_attackCoolDown = attackCoolDown;
		isDead = false;
		health = maxhealth;
		GameObject healthbar = Instantiate (Resources.Load (healthBarResource)) as GameObject;
		healthbar.transform.parent = transform;

		GameObject manabar = Instantiate (Resources.Load (manaBarResource)) as GameObject;
		manabar.transform.parent = transform;


		//test------------------------------
		//magic
		GameObject magic1 = ctrl.initMagic ("prefab/xuanyuanjianzhen");
		MagicController con = magic1.GetComponent<MagicController> () as MagicController;
		//magic1.transform.parent = transform;
		con.setParentController (this,ctrl);
		magicList.Add (con);

		//test for buff
		SpeedBuff spdbuff = new SpeedBuff ();
		spdbuff.affectValue = 3.0f;
		spdbuff.duration = 3.0f;
		ctrl.attachBuffToEntityController (spdbuff, this);

		PassiveSkill_ManTianGuoHai skl1 = new PassiveSkill_ManTianGuoHai ();
		learnPassiveSkill (skl1);
		//------------------------------------

		//trigger passive skill in start phase
		foreach (PassiveSkill skl in passiveSkillList) {
			if(skl.phase == PassiveSkill.Phase.Start)
			{
				skl.activeSkill(null);
			}
		}
	}
	void Update(){
		if (isPause) {
			return;		
		}
		if (isDead) {
			return;		
		}
		_attackCoolDown -= Time.deltaTime;
		if (_attackCoolDown < 0) {
			_attackCoolDown = 0;		
		}
		//renderer.sortingOrder = (int)transform.position.y;
	}
	// Update is called once per frame
	void FixedUpdate () {
		if (isPause) {
			return;		
		}
		if (isDead) {
			return;		
		}

		///user control
		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2 (move * horiVelocity, rigidbody2D.velocity.y);
		if (move < 0 && isFacingRight) {
			flipFacing();		
		}
		if (move > 0 && !isFacingRight) {
			flipFacing();		
		}

		float move2 = Input.GetAxis("Vertical");
		rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, move2 * vertVelocity);
		//Debug.Log(rigidbody2D.velocity);
		if (rigidbody2D.velocity != Vector2.zero) {
			onMove();		
		}


		if (Input.anyKey) {
			//Debug.Log("input key");
			return;	

		}
		//my code begins here
		currentMana = manaSpeed + currentMana;
		updateManaBar ();
		if (currentMana > manaRequiredToCast) {
			canCast = true;
			currentMana = manaRequiredToCast;		
		}

		//update buff
		foreach (Buff buff in buffList) {
			buff.elapse(Time.deltaTime*buffTimeElapseMultiplier);	
		}
		ctrl.updateBufferList ();

		switch (state) {
		case State.Find:{
			onFindHandler();
		}	
			break;
		case State.Fight:{
			onFightHandler();
		}
			break;
		case State.Def:{

		}
			break;
		case State.Escape:{

		}
			break;
		case State.Cast:{
			onCastHandler();
		}
			break;
		case State.Dead:{
			onDeadHandler();
		}
			break;
		case State.Wild:{
			onWildHandler();
		}
			break;
		}

	}
	protected void onWildHandler(){
		//not checking cast, so entity cannot cast in this state

		//perform random move
		if (randomPosition.sqrMagnitude ==0) {
			randomPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), Random.Range (0, Screen.height), Camera.main.farClipPlane / 2));
		}
		if (Random.Range (0, 100) < 1) {
			randomPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, Screen.width), Random.Range (0, Screen.height), Camera.main.farClipPlane / 2));
		}
		float distance = Vector2.Distance (randomPosition, transform.position);
		Vector2 nextPos = Vector2.Lerp (transform.position, randomPosition, velocity*Time.deltaTime / distance);
		rigidbody2D.MovePosition (nextPos);
		//Debug.Log (nextPos);
		if (nextPos.x > transform.position.x&&!isFacingRight) {
			flipFacing();
		} else if(nextPos.x < transform.position.x&&isFacingRight){
			flipFacing();
		}
		onMove ();
	
	}
	protected void onCastHandler(){
		//do cast anim
		//Debug.Log("cast handler");
		if (!isCasting) {
			isCasting = true;
			MagicController magic = magicList[0] as MagicController;
			animator.SetBool("setCast",true);
			ctrl.castMagic(magic,transform.localScale.x);
		}
		if (hasCast) {
			state = State.Find;
			finishCast ();
		}
		//hasCast = true;
	}
	protected void finishCast(){
		hasCast = false;
		isCasting = false;
		canCast = false;
		currentMana = 0.0f;
		ManaBarConfig config = transform.GetComponentInChildren(typeof(ManaBarConfig)) as ManaBarConfig;
		config.reset ();
	}

	protected void onFightHandler(){
		if (tryCastMagic ()) {
			//ctrl.pauseForSeconds (1.0f);
			state = State.Cast;
			return;
		}

		if (_attackCoolDown == 0) {
			onFight ();		
			_attackCoolDown = attackCoolDown;
		} else {
			if(!isFighting){
				onIdle();
			}		
		}

	}
	protected bool tryCastMagic(){
		if (canCast) {
			//if (Input.GetMouseButtonDown (0)) {
				//return true;	
			//}
			return true;		
		}

		return false;
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag != "entity") {
			return;		
		}
		coll.gameObject.rigidbody2D.velocity = new Vector2 (0, 0);
	}
	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag != "entity") {
			//Debug.Log("magic col");
			return;		
		}
		if (state == State.Wild) {//wild state cannot attack
			return;		
		}
		NinJaController colliderContoller = coll.gameObject.GetComponent(typeof(NinJaController)) as NinJaController;
		if (isEnemy (colliderContoller)) {
			targetEnemy = colliderContoller;
			state = State.Fight;
		}
	}
	void OnCollisionExit2D(Collision2D coll){
		if (coll.gameObject.tag != "entity") {
			//Debug.Log("magic col");
			return;		
		}
		if (state == State.Wild) {//wild state cannot change to any other state
			return;		
		}
		state = State.Find;
	}
	protected bool isEnemy(NinJaController other)
	{
		//Debug.Log (other.gameObject.tag);
		if (side == other.side) {
			return false;
		}
		return true;
	}
	protected void onFindHandler(){
		if (tryCastMagic ()) {
			//ctrl.pauseForSeconds (1.0f);
			state = State.Cast;
			return;
		}

		if (!targetEnemy||targetEnemy.isDead) {
			targetEnemy = ctrl.getTargetEnemyForEntity (this);
		}
		if (targetEnemy == null) {
			Debug.Log("victory");
			//ctrl.pauseAllEntityAndMagic();
			return;
		}
		float distance = Vector2.Distance (targetEnemy.transform.position, transform.position);
		Vector2 nextPos = Vector2.Lerp (transform.position, targetEnemy.transform.position, velocity*Time.deltaTime / distance);
		rigidbody2D.MovePosition (nextPos);
		//Debug.Log (nextPos);
		if (nextPos.x > transform.position.x&&!isFacingRight) {
			flipFacing();
		} else if(nextPos.x < transform.position.x&&isFacingRight){
			flipFacing();
		}
		onMove ();
	}
	protected void onDeadHandler(){

		if (!isDead) {
			onDead ();
		}
		isDead = true;
		//Destroy(rigidbody2D);
		Collider2D col = GetComponent (typeof(PolygonCollider2D)) as Collider2D;
		col.enabled = false;
		ctrl.entityDead (this);

		int childs = transform.childCount;
		for (int i =  0; i <childs; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
		rigidbody2D.velocity = new Vector2 (0, 0);
	}
	public void takeDamageFromEnemy(NinJaController other){
		float dechealth = other.attack;
		dechealth *= physicalHurtMultiplier;
		health = (int)(health - dechealth);
		//=--------------------------------
		//trigger passive skill
		foreach (PassiveSkill skl in passiveSkillList) {
			if(skl.phase == PassiveSkill.Phase.Defend)
			{
				skl.activeSkill(other);
			}
		}
		//--------------------------------
		updateHealthBar ();
		if (health <= 0) {
			state = State.Dead;	
			//==========================
			//trigger passive skill
			foreach (PassiveSkill skl in passiveSkillList) {
				if(skl.phase == PassiveSkill.Phase.Dead)
				{
					skl.activeSkill(null);
				}
			}
			//===========================
		}
	}
	public void takeDamageFromMagic(MagicController other){
		float dechealth = (float)other.attack;
		dechealth *= magicHurtMultiplier;
		//Debug.Log (other.attack);
		health -= dechealth;
		updateHealthBar ();
		//wild test
		//WildBuff wb = new WildBuff ();
		//wb.duration = 3.0f;
		//ctrl.attachBuffToEntityController (wb, this);
		//
		if (health <= 0) {
			state = State.Dead;	
			//==========================
			//trigger passive skill
			foreach (PassiveSkill skl in passiveSkillList) {
				if(skl.phase == PassiveSkill.Phase.Dead)
				{
					skl.activeSkill(null);
				}
			}
			//===========================
		}
	}
	protected void flipFacing(){
		isFacingRight = !isFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	protected void attackCallBackFunc(){
		animator.SetBool ("setAttack", false);
		isFighting = false;
		ctrl.attackEnemyController (this, attackTarget);
		targetEnemy = null;
		attackTarget = null;
		if (state == State.Fight) {
			state = State.Find;
		}
	}

	protected void onFight(){
		animator.SetBool ("setAttack", true);
		attackTarget = targetEnemy;
		isFighting = true;

		//trigger passive here
		foreach (PassiveSkill skl in passiveSkillList) {
			if(skl.phase == PassiveSkill.Phase.Attack)
			{
				skl.activeSkill(attackTarget);
			}
		}
	}
	protected void onMove(){
		//float velo = Vector2.SqrMagnitude (rigidbody2D.velocity);
		//Debug.Log (velo);
		animator.SetFloat ("velocity", 1.0f);
	}
	protected void onIdle(){
		animator.SetFloat ("velocity", 0.0f);
	}
	protected void onDead(){
		animator.SetBool ("setDead", true);
	}
	/**protected void deadCallBackFunc(){
		animator.SetBool ("setDead", false);
	}**/
	protected void updateHealthBar(){
		HealthBarConfig config = transform.GetComponentInChildren(typeof(HealthBarConfig)) as HealthBarConfig;
		float percentage = (float)health / (float)maxhealth;
		if (!config) {
			return;		
		}
		config.setHealthBarPercentage (percentage);
	}

	protected void updateManaBar(){
		ManaBarConfig config = transform.GetComponentInChildren(typeof(ManaBarConfig)) as ManaBarConfig;
		float percentage = currentMana / manaRequiredToCast;
		if (!config) {
			return;		
		}
		config.setmanaBarPercentage (percentage);
	}

	public void castCallBack(){
		//Debug.Log("callback called");
		hasCast = true;
		animator.SetBool ("setCast", false);
	}
	public void setPause(bool pause){
		isPause = pause;
		if (pause) 
		{
			animator.speed = 0.0f;
		}else{
			animator.speed = 1;
		}
	}
	public void addBuff(Buff buff)
	{
		buffList.Add (buff);
	}

	public void deleteBuff(Buff buff)
	{
		Debug.Log("remove buff");
		buffList.Remove (buff);
	}

	public void setVelocity(float v)
	{
		velocity = v;
	}

	public float getVelocity()
	{
		return velocity;
	}
	public float getAttack()
	{
		return attack;
	}
	public void setAttack(float atk)
	{
		attack = atk;
	}
	public float getDefence()
	{
		return defence;
	}
	public void setDefence(float def)
	{
		defence = def;
	}
	public float getLucky()
	{
		return lucky;
	}
	public void setLucky(float luc)
	{
		lucky = luc;
	}
	public float getPH()
	{
		return physicalHurtMultiplier;
	}
	public void setPH(float ph)
	{
		physicalHurtMultiplier = ph;
	}
	public float getMH()
	{
		return magicHurtMultiplier;
	}
	public void setMH(float mh)
	{
		magicHurtMultiplier = mh;
	}
	public float getPP()
	{
		return physicPowerMultiplier;
	}
	public void setPP(float pp)
	{
		physicPowerMultiplier = pp;
	}
	public float getMP()
	{
		return magicPowerMultiplier;
	}
	public void setMP(float mp)
	{
		magicPowerMultiplier = mp;
	}
	public float getBuffElapseTimeMultiplier()
	{
		return buffTimeElapseMultiplier;
	}
	public void setBuffElapseTimeMultiplier(float tm)
	{
		buffTimeElapseMultiplier = tm;
	}
	public float getAtkInterval()
	{
		return _attackCoolDown;
	}
	public void setAtkInterval(float mp)
	{
		_attackCoolDown = mp;
	}
	public void setWild(bool b)
	{
		if (b) {
						state = State.Wild;		
				} else {
						state = State.Find;
				}
	}
	public void setBetray(bool b)
	{
		if (b) {
			if(side==GameController.Side.leftSide){
				side = GameController.Side.rightSide;
				//int idx = ctrl.leftEntities.IndexOf(this);
				ctrl.leftEntities.Remove(this);
				ctrl.rightEntities.Add(this);
			}else{
				side = GameController.Side.leftSide;
				ctrl.rightEntities.Remove(this);
				ctrl.leftEntities.Add(this);
			}
			targetEnemy = null;
		}else{
			if(side==GameController.Side.leftSide){
				side = GameController.Side.rightSide;
				//int idx = ctrl.leftEntities.IndexOf(this);
				ctrl.leftEntities.Remove(this);
				ctrl.rightEntities.Add(this);
			}else{
				side = GameController.Side.leftSide;
				ctrl.rightEntities.Remove(this);
				ctrl.leftEntities.Add(this);
			}
			targetEnemy = null;
		}
	}

	protected void learnPassiveSkill(PassiveSkill skill){
		passiveSkillList.Add (skill);
		skill.parentController = this;
		skill.gameCtrl = ctrl;
		skill.init ();
	}
	public void revive()
	{
		state = State.Find;
	}
}
