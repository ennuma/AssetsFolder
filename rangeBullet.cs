using UnityEngine;
using System.Collections;

public class rangeBullet : MonoBehaviour {

	// Use this for initialization
	public NinJaController target;
	public NinJaController owner;
	private float velocity = 4.0f;
	void Start () {
		transform.tag = "bullet";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!target) {
			return;		
		}
		float distance = Vector2.Distance (target.transform.position, transform.position);
		Vector2 nextPos = Vector2.Lerp (transform.position, target.transform.position, velocity*Time.deltaTime / distance);
		rigidbody2D.MovePosition (nextPos);
		if(renderer.bounds.Intersects(target.renderer.bounds)){
			Debug.Log("hit");
			target.takeDamageFromEnemy(owner);
			Destroy(gameObject);
		}
	}


}
