using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterControl : MonoBehaviour {

	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float turnSpeed;
	[SerializeField]
	private float jumpHeight;

	private Rigidbody rb;
	private Animator anim;
	private AudioSource source;

	private void Start () {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		source = GetComponent<AudioSource>();
	}

	private void Update () {
		Move();
		RegisterButtonPresses();
	}

	private void Move () {
		Vector3 moveChar = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));

		if (moveChar == Vector3.zero) {
			anim.SetBool("isWalking", false);
		} else {
			anim.SetBool("isWalking", true);
			Quaternion targetRotation = Quaternion.LookRotation(moveChar, Vector3.up);
			transform.rotation = targetRotation;
		}

		transform.localPosition += moveChar * moveSpeed * Time.deltaTime;
	}

	private void RegisterButtonPresses() {
		if (GameManager.Instance.IsJumping) {
			anim.SetTrigger("jump");
			rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
			source.PlayOneShot(AudioManager.Instance.Jump);
			GameManager.Instance.IsJumping = false;
		}

		if (GameManager.Instance.IsPunching) {
			anim.SetTrigger("punch");
			ModifyTerrain.Instance.DestroyBlock(10f, (byte)TextureType.air.GetHashCode());
			source.PlayOneShot(AudioManager.Instance.Hit);
			GameManager.Instance.IsPunching = false;
		}

		if (GameManager.Instance.IsBuilding) {
			anim.SetTrigger("punch");
			ModifyTerrain.Instance.AddBlock(10f, (byte)TextureType.rock.GetHashCode());
			source.PlayOneShot(AudioManager.Instance.Build);
			GameManager.Instance.IsBuilding = false;
		}
	}
}
