
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static int moveSpeed;
	public static bool isSpooked;

	private Rigidbody2D player;
	private float horizontalMovement;
	private bool isJumping = false;
	private float spookTimer;

	void Start () {
		horizontalMovement = 0f;
		moveSpeed = 3;
		player = GetComponent<Rigidbody2D>();

		PlayerState.Instance.Horizontal = Horizontal.Idle;
		PlayerState.Instance.Vertical = Vertical.Grounded;
		PlayerState.Instance.DirectionFacing = DirectionFacing.Right;
		PlayerState.Instance.attack = Attack.passive;
	}

	void FixedUpdate(){
		player.velocity = new Vector2(horizontalMovement * moveSpeed, player.velocity.y);

		if (isJumping){
			if (PlayerState.Instance.Vertical == Vertical.Grounded)
			{
				player.AddForce(new Vector2(player.velocity.x, 6), ForceMode2D.Impulse);
				PlayerState.Instance.Vertical = Vertical.AirBorn;
				GetComponent<AudioSource>().Play();
			}
			isJumping = false;
		}
	}

	void Update(){
		Movement();
		SpookedCheck();
	}

	private void Movement(){
		if (PlayerState.Instance.attack != Attack.passive){
			player.velocity = new Vector2(0, 0f);
			horizontalMovement = 0;
		}
		else {
			horizontalMovement = Input.GetAxisRaw("Horizontal");
			if (horizontalMovement != 0){
				transform.localScale = new Vector3(horizontalMovement, 1, 1);
				PlayerState.Instance.DirectionFacing = (DirectionFacing)horizontalMovement;
			}
			if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Button A"))
				isJumping = true;
			
		}
		if (player.velocity.y == 0 && PlayerState.Instance.attack == Attack.passive)
			PlayerState.Instance.Vertical = Vertical.Grounded;

		Horizontal previousMotion = PlayerState.Instance.Horizontal;
		Horizontal currentMotion = PlayerState.Instance.Horizontal = (Horizontal)horizontalMovement;

		if ((int)previousMotion * (int)currentMotion == -1) {
			PlayerState.Instance.Horizontal = Horizontal.Idle;
		}
		PlayerState.Instance.Horizontal = (Horizontal)horizontalMovement;
	}

	private void JoyStickMovement() {
		Input.GetAxisRaw("LeftJoyStickHorizontal");
	}

	private void SpookedCheck() {
		int lerpTo;
		float lerpSpeed;

		if (isSpooked){
			spookTimer += Time.deltaTime * 60;
			lerpTo = 0;
			lerpSpeed = 0.6f;
		}
		else {
			lerpTo = 1;
			lerpSpeed = 0.8f;
		}

		SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer renderer in renderers) {
			renderer.color = Color.Lerp(renderer.color, new Color(lerpTo, 1, 1, 1), lerpSpeed * Time.deltaTime);
		}
		if (spookTimer > 180) {
			spookTimer = 0;
			moveSpeed = 3;
			isSpooked = false;
		}
	}
}
