
using UnityEngine;

public class Gigantor : Enemies{

	void Start (){
		HP = 3;
		Collider.radius = 0.21f;
		transform.localScale = new Vector3(5, 5, 1);

		Body.gravityScale = 2;
		Body.mass = 2;

		GameObject cheeseHead = GameObject.Find("CheeseHead");
		Direction = (cheeseHead.transform.position.x - transform.position.x < 0) ? -1 : 1;
	}
	
	void FixedUpdate () {
		MovementPattern();
		DestroyOutOfBounds();
	}

	protected override void MovementPattern(){
		Body.velocity = new Vector2(Speed * Direction, Body.velocity.y);
	}
}
