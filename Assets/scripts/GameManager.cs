using UnityEngine;

public class GameManager : Singleton<GameManager> {

	private bool isJumping;
	public bool IsJumping {
		get {
			return isJumping;
		}
		set {
			isJumping = value;
		}
	}

	private bool isPunching;
	public bool IsPunching {
		get {
			return isPunching;
		}
		set {
			isPunching = value;
		}
	}
	
	private bool isBuilding;
	public bool IsBuilding {
		get {
			return isBuilding;
		}
		set {
			isBuilding = value;
		}
	}

	public void JumpButton () {
		IsJumping = true;
	}

	public void PunchButton () {
		IsPunching = true;
	}

	public void BuildButton () {
		IsBuilding = true;
	}
	
}
