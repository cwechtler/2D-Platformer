
using UnityEngine;

public class Tweaker : Enemies {

	[SerializeField] private bool TweakOut;

	private float timer;

	void Start () {
		HP = 1;
		Collider.radius = 0.16f;
		Sprite.material.color = new Color(1, 0.8f, 0, 1);
	}
	
	void Update () {
		timer += Time.deltaTime * 60;

		if (timer > 30) {
			TweakOut = true;
			timer = 0;
		}
	}

	private void FixedUpdate(){
		MovementPattern();
		BorderHitCheck(50);
		DestroyOutOfBounds();
	}

	protected override void MovementPattern(){
		if (TweakOut) {
			float randomX = UnityEngine.Random.Range(-29f, 29f);
			float randomY = UnityEngine.Random.Range(-120f, 120f);
			Body.AddForce(new Vector2(randomX * Speed, randomY * Speed));
			TweakOut = false;
		}
	}
}
