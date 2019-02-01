
using UnityEngine;

public class DarkenCycle : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private float minBrightness;
	private float maxBrightness;
	private float cycleSpeed;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		minBrightness = 0.8f;
		maxBrightness = 1;
		cycleSpeed = 0.05f;
	}
	
	void Update () {
		spriteRenderer.color = Color.Lerp(
			new Color(maxBrightness, maxBrightness, maxBrightness, 1),
			new Color(minBrightness, minBrightness, minBrightness, 1),
			Mathf.PingPong(Time.time * cycleSpeed, maxBrightness)
			);
	}
}
