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
		Move();
	}

	private void Move () {
		Vector3 moveChar = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
		transform.position += moveChar * moveSpeed * Time.deltaTime;

		if (moveChar == Vector3.zero) {
			anim.SetBool("isWalking", false);
		} else {
			anim.SetBool("isWalking", true);
		}
	}
}
