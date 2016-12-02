using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterControl : MonoBehaviour {

	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float jumpHeight;

	private Rigidbody rb;
	private Animator anim;

	private void Start () {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}

	private void Update () {
		Vector3 moveChar = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
		transform.position += moveChar * moveSpeed * Time.deltaTime;
	}
}
