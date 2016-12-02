using UnityEngine;

public class ModifyTerrain : Singleton<ModifyTerrain> {

	private World world;
	private GameObject player;

	private void Start () {
		world = GetComponent<World>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void AddBlock (float range, byte block) {
		Ray ray = new Ray(player.transform.position, player.transform.forward);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit)) {
			if (hit.distance < range) {
				AddBlockAt(hit, block);
			}
		}
	}

	public void DestroyBlock (float range, byte block) {
		Ray ray = new Ray(player.transform.position, player.transform.forward);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit)) {
			if (hit.distance < range) {
				DestroyBlockAt(hit, block);
			}
		}
	}

	public void AddBlockAt (RaycastHit hit, byte block) {
		Vector3 pos = hit.point;
		pos += (hit.normal * 0.5f);
		SetBlockAt(pos, block);
	}

	public void DestroyBlockAt (RaycastHit hit, byte block) {
		Vector3 pos = hit.point;
		pos += (hit.normal * -0.5f);
		SetBlockAt(pos, block);
	}

	public void SetBlockAt (Vector3 pos, byte block) {
		int x = Mathf.RoundToInt(pos.x);
		int y = Mathf.RoundToInt(pos.y);
		int z = Mathf.RoundToInt(pos.z);

		world.WorldData[x, y, z] = block;
		UpdateChunkAt(x, y, z);
	}

	public void UpdateChunkAt (int x, int y, int z) {
		int updateX = Mathf.FloorToInt(x / world.ChunkSize);
		int updateY = Mathf.FloorToInt(y / world.ChunkSize);
		int updateZ = Mathf.FloorToInt(z / world.ChunkSize);

		world.Chunks[updateX, updateY, updateZ].IsUpdate = true;
	}
}
