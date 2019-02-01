
using UnityEngine;

public class OvercastCycle : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private float minOpacity;
	private float maxOpacity;
	private float cycleSpeed;

	void Start(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		minOpacity = 0.6f;
		maxOpacity = 1;
		cycleSpeed = 0.05f;
	}

	void Update(){
		spriteRenderer.color = Color.Lerp(
			new Color(1,1,1, maxOpacity),
			new Color(1,1,1, minOpacity),
			Mathf.PingPong(Time.time * cycleSpeed, maxOpacity)
			);
	}
}
