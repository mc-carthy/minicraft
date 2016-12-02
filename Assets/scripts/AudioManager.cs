using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

	[SerializeField]
	private AudioClip build;
	public AudioClip Build {
		get {
			return build;
		}
	}

	[SerializeField]
	private AudioClip hit;
	public AudioClip Hit {
		get {
			return hit;
		}
	}

	[SerializeField]
	private AudioClip jump;
	public AudioClip Jump {
		get {
			return jump;
		}
	}

}
