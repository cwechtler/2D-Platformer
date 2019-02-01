
using UnityEngine;

public class Enemies<T> where T : Enemies {
	public GameObject gameObject;
	public T scriptComponent;

	public Enemies (string name) {
		gameObject = new GameObject(name);
		scriptComponent = gameObject.AddComponent<T>();
	}
}

public abstract class Enemies : MonoBehaviour {

	protected int HP;

	public Rigidbody2D Body { get; set; }
	public SpriteRenderer Sprite { get; set; }
	public CircleCollider2D Collider { get; set; }

	public int Speed { get; set; }
	public int Direction { get; set; }

	protected abstract void MovementPattern();

	private void Awake(){
		//Add common components
		Body = gameObject.AddComponent<Rigidbody2D>();
		Sprite = gameObject.AddComponent<SpriteRenderer>();
		Collider = gameObject.AddComponent<CircleCollider2D>();

		// Set Common sprite
		Sprite.sprite = Resources.Load<Sprite>("EyeBall");
		Body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

		gameObject.tag = "Enemy";
		gameObject.layer = LayerMask.NameToLayer("EyeBall");
	}

	//Insert all unique values to be determined at instantiation here
	public void Initialize(int speed, int direction, Vector3 position) {
		Speed = speed;
		Direction = direction;
		transform.position = position;
	}

	public void Initialize(int speed, Vector3 position){
		Speed = speed;
		transform.position = position;
	}

	protected void BorderHitCheck(float force) {
		force *= Speed;
		Vector2 enemyPosition = Camera.main.WorldToViewportPoint(transform.position);

		if (enemyPosition.x < 0f) {
			Body.velocity = new Vector2(0, Body.velocity.y);
			Body.AddForce(new Vector2(force, 0));
			Direction = 1;
		} else if (enemyPosition.x > 1f) {
			Body.velocity = new Vector2(0, Body.velocity.y);
			Body.AddForce(new Vector2(force * -1, 0));
			Direction = -1;
		}
	}

	protected void DestroyOutOfBounds() {
		if (transform.position.y < -6 || transform.position.y > 20) {
			GameObject.Destroy(gameObject);
		}
	}

	public void DoDamage(int damageAmount) {
		HP -= damageAmount;
		if (HP <= 0) {
			GameObject.Find("Stomper").GetComponent<AudioSource>().Play();
			Destroy(gameObject);
		}
	}

	protected virtual void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.gameObject.tag == "Player"){
			Rigidbody2D cheeseBody = collision.gameObject.GetComponent<Rigidbody2D>();
			cheeseBody.velocity = new Vector2(0, 0);
			cheeseBody.AddForce(new Vector2(0, 300));

			collision.gameObject.GetComponent<PlayerController>().enabled = false;
			collision.gameObject.GetComponent<Collider2D>().enabled = false;

			foreach (Transform child in collision.gameObject.transform)
				Destroy(child.gameObject);

			GameOverManager.isGameOver = true;
		}
	}
}
