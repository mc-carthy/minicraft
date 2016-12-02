using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField]
	private Transform target;
	[SerializeField]
	private float distance;
	[SerializeField]
	private float targetHeight;

	private float x = 0;
	private float y = 0;
	
	private void LateUpdate () {
		if (!target) {
			return;
		}

		y = target.eulerAngles.y;
		Quaternion rot = Quaternion.Euler(x, y, 0);
		transform.rotation = rot;

		Vector3 pos = target.position - (rot * Vector3.forward * distance + new Vector3(0, -targetHeight, 0));
		transform.position = pos;
	}
}
