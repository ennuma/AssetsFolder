using UnityEngine;
using System.Collections;

public class RangeEntityController : NinJaController
{
	public float range = 6.0f;
	// Use this for initialization
	
	protected override void onFindHandler(){
		if (tryCastMagic ()) {
			//ctrl.pauseForSeconds (1.0f);
			//state = State.Cast;
			return;
		}
		
		if (!targetEnemy||targetEnemy.isDead) {
			targetEnemy = ctrl.getTargetEnemyForEntity (this);
		}
		if (targetEnemy == null) {
			Debug.Log("victory");
			onIdle();
			//ctrl.pauseAllEntityAndMagic();
			return;
		}
		if (checkFight ()) {

			state = State.Fight;
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

	protected bool checkFight(){
		float dis = Vector3.Distance (transform.position, targetEnemy.transform.position);
		if (dis <= range) {
			return true;		
		}
		return false;
	}

	protected override void attackCallBackFunc(){
		animator.SetBool ("setAttack", false);
		isFighting = false;
		ctrl.attackEnemyControllerRange (this, attackTarget);
		targetEnemy = null;
		attackTarget = null;
		if (state == State.Fight) {
			state = State.Find;
		}
	}
}

