
using UnityEngine;

public class Lush : Enemies{

	[SerializeField] private float Timer;
	[SerializeField] private int horizontalMotion;

	private int maxWobbleWidth;

	void Start () {
		HP = 1;
		Collider.radius = 0.2f;
		Sprite.material.color = new Color(0.8f, 1, 1);
		transform.localScale = new Vector3(1.4f, 1.4f, 1);

		maxWobbleWidth = 2000 / Speed;

		if (Camera.main.WorldToViewportPoint(transform.position).x > 0.5f) {
			Direction = -1;
		}
		else {
			Direction = 1;
		}
	}
	
	void Update () {
		if (Timer < maxWobbleWidth / 2){
			horizontalMotion = 1 * Direction;
		}
		else if (Timer < maxWobbleWidth){
			horizontalMotion = -1 * Direction;
		}
		else if (Timer > maxWobbleWidth){
			Timer = 0;
		}

		Timer += Time.deltaTime * 60;
	}

	private void FixedUpdate(){
		MovementPattern();
		BorderHitCheck(20);
		DestroyOutOfBounds();
	}

	protected override void MovementPattern(){
		Body.AddForce(new Vector2(horizontalMotion * Speed, Body.velocity.y), ForceMode2D.Force);

		if (Body.velocity.x < Speed / 2 * -1) {
			Body.velocity = new Vector2(Speed / 2 * -1, Body.velocity.y);
		}
		if (Body.velocity.x > Speed / 2) {
			Body.velocity = new Vector2(Speed / 2, Body.velocity.y);
		}
	}
}
