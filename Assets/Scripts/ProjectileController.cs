
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	private int direction;
	private int speed;
	private float timer;

	void Start () {
		direction = (int)PlayerState.Instance.DirectionFacing;
		transform.position = GameObject.Find("CheeseFist").transform.position - new Vector3(0.3f, 0, 0) * direction;
		speed = 12;
	}
	
	void Update () {
		transform.position = new Vector2(transform.position.x + Time.deltaTime * speed * direction, transform.position.y);
		transform.Rotate(0, 0, 6 * direction * -1 * Time.deltaTime * 60);
		timer += Time.deltaTime * 60;

		if (timer > 120) {
			GameObject.Destroy(gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Enemy") {
			GameObject.Destroy(gameObject);

			Rigidbody2D enemy = collision.gameObject.GetComponent<Rigidbody2D>();
			enemy.velocity = new Vector2(0, 0);
			enemy.AddForce(new Vector2((float)PlayerState.Instance.DirectionFacing * 11, 14), ForceMode2D.Impulse);
			enemy.GetComponent<Enemies>().DoDamage(1);

			WorldManager.score += 125;
		}
	}
}
