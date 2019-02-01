
using UnityEngine;

public class Ghost : Enemies {

	void Start () {
		HP = 1;

		Collider.radius = 0.16f;
		Body.isKinematic = true;

		Sprite.sortingOrder = 1;
		Sprite.color = new Color(1, 1, 1, 0.5f);
		transform.localScale = new Vector3(1.8f, 1.8f, 1);
	}
	
	void Update () {
		MovementPattern();
		DestroyOutOfBounds();
	}

	protected override void MovementPattern(){
		transform.position = Vector3.MoveTowards(
			transform.position,
			GameObject.Find("CheeseHead").transform.position,
			Speed * Time.deltaTime
			);
	}

	protected override void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Player") {
			PlayerController.isSpooked = true;
			PlayerController.moveSpeed -= 1;
			GameObject.Destroy(gameObject);
		}
	}
}
