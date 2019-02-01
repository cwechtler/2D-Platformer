using UnityEngine;

public class AttackController : MonoBehaviour {

	[SerializeField] private GameObject Player;
	[SerializeField] private GameObject projectile;

	private float startingAttackPosition;
	private float endingAttackPosition;
	private int maxPause;

	private float attackMotion = Mathf.Infinity;
	private float attackForce;
	private float attackPause;
	private float accumulator;

	private bool reCoiling;

	private void Start () {
		attackMotion = Mathf.Infinity;
		attackForce = 5;
		attackPause = 1;
		accumulator = 0.02f;
	}

	private void Update () {
		GetComponentsInChildren<SpriteRenderer>()[1].enabled = (PlayerState.Instance.attack == Attack.projectile) ? true : false;

		if (Input.GetButtonDown("Button B") || Input.GetButtonDown("Fire1") && PlayerState.Instance.attack == Attack.passive) {
			PlayerState.Instance.attack = Attack.punch;
			startingAttackPosition = Player.transform.position.x;
			endingAttackPosition = startingAttackPosition + (int)PlayerState.Instance.DirectionFacing * 0.7f;

			maxPause = 10;
			GetComponents<AudioSource>()[0].Play();
		} else if (Input.GetButtonDown("Button X") || Input.GetButtonDown("Fire2") && PlayerState.Instance.attack == Attack.passive && GameObject.Find("projectile(Clone)") == null) {
			PlayerState.Instance.attack = Attack.projectile;
			startingAttackPosition = Player.transform.position.x;
			endingAttackPosition = startingAttackPosition + (int)PlayerState.Instance.DirectionFacing * 0.5f;

			maxPause = 20;
			GetComponents<AudioSource>()[1].Play();
		}

		if (PlayerState.Instance.attack == Attack.punch || PlayerState.Instance.attack == Attack.projectile) {
			accumulator += Time.deltaTime;
			if (attackMotion == endingAttackPosition)
			{
				attackPause += Time.deltaTime * 60;
			}
			if (attackPause > maxPause)
			{
				if (PlayerState.Instance.attack == Attack.projectile) {
					GameObject.Instantiate(projectile);
				}
				attackPause = 1;
				accumulator = 0.02f;
				reCoiling = true;
			}
			if (!reCoiling){
				attackMotion = Mathf.Lerp(startingAttackPosition, endingAttackPosition, accumulator * 7);
				transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), accumulator * 5);
			} else{
				attackMotion = Mathf.Lerp(endingAttackPosition, startingAttackPosition, accumulator * 5);
				transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.6f, 0.5f, 1), accumulator * 4);

				if (transform.position.x == startingAttackPosition){
					reCoiling = false;
					PlayerState.Instance.attack = Attack.passive;
					accumulator = 0.02f;
				}
			}

			transform.position = new Vector3(attackMotion, Player.transform.position.y, transform.position.z);
			GetComponent<SpriteRenderer>().enabled = true;
		}
		else {
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Enemy") {

			Rigidbody2D enemy = collision.gameObject.GetComponent<Rigidbody2D>();
			enemy.velocity = new Vector2(0, 0);
			enemy.AddForce(new Vector2((float)PlayerState.Instance.DirectionFacing * attackForce, attackForce), ForceMode2D.Impulse);

			enemy.GetComponent<Enemies>().DoDamage(2);
			WorldManager.score += 200;
		}
	}
}
