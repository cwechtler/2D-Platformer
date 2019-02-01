using System;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField] private GameObject player;
	[SerializeField] private CameraState cameraState;

	void Start () {
		cameraState = CameraState.Stationary;
	}
	
	void LateUpdate () {
		float offSet = Camera.main.orthographicSize * Camera.main.aspect / 2;
		Vector3 playerScreenPosition = Camera.main.WorldToViewportPoint(player.transform.position);

		if (playerScreenPosition.x < 0.25f || playerScreenPosition.x > 0.75f) {
			cameraState = CameraState.Following;
		}
		if (cameraState == CameraState.Following && PlayerState.Instance.Horizontal == Horizontal.Idle){
			cameraState = CameraState.Recentering;
		} else if (cameraState == CameraState.Following) {
			transform.position = new Vector3(player.transform.position.x - offSet * (int)PlayerState.Instance.DirectionFacing, transform.position.y, transform.position.z);
		}
		if (cameraState == CameraState.Recentering) {
			float x = Mathf.Lerp(transform.position.x, player.transform.position.x, 0.02f * Time.deltaTime * 60);
			transform.position = new Vector3(x, transform.position.y, transform.position.z);
			if (Math.Round(playerScreenPosition.x, 1) == 0.5f) {
				cameraState = CameraState.Stationary;
			}    
		}
	}

	public enum CameraState {
		Stationary,
		Following,
		Recentering
	}
}
