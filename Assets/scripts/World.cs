using UnityEngine;
using Noise;

public class World : MonoBehaviour {

	[SerializeField]
	private GameObject chunk;
	[SerializeField]
	private int worldX = 16;
	[SerializeField]
	private int worldY = 16;
	[SerializeField]
	private int worldZ = 16;
	[SerializeField]
	private int rockInitialY;
	[SerializeField]
	private float rockPerlinScale;
	[SerializeField]
	private int rockPerlinHeight;
	[SerializeField]
	private float rockPerlinPower;
	[SerializeField]
	private int rockUnkownY;
	[SerializeField]
	private float rockSecondPerlinScale;
	[SerializeField]
	private int rockSecondPerlinHeight;
	[SerializeField]
	private float rockSecondPerlinPower;
	[SerializeField]
	private int rockSecondPerlinConstant;
	[SerializeField]
	private int grassInitialY;
	[SerializeField]
	private float grassPerlinScale;
	[SerializeField]
	private int grassPerlinHeight;
	[SerializeField]
	private float grassPerlinPower;
	[SerializeField]
	private int grassPerlinConstant;
	[SerializeField]
	private int grassUnkownY;
	[SerializeField]
	private float grassSecondPerlinScale;
	[SerializeField]
	private int grassSecondPerlinHeight;
	[SerializeField]
	private float grassSecondPerlinPower;
	[SerializeField]
	private int grassSecondPerlinConstant;

	[SerializeField]
	private int chunkSize = 16;
	public int ChunkSize {
		get {
			return chunkSize;
		}
	}

	private Chunk[,,] chunks;
	public Chunk[,,] Chunks {
		get {
			return chunks;
		}
	}

	private byte[,,] worldData;
	public byte[,,] WorldData {
		get {
			return worldData;
		}
	}

	private void Start () {
		worldData = new byte[worldX, worldY, worldZ];

		for (int x = 0; x < worldX; x++) {
			for (int z = 0; z < worldZ; z++) {
				int rock = PerlinNoise(x, 0, z, rockPerlinScale, rockPerlinHeight, rockPerlinPower);
				rock += PerlinNoise(x, rockUnkownY, z, rockSecondPerlinScale, rockSecondPerlinHeight, rockSecondPerlinPower) + rockSecondPerlinConstant;
				int grass = PerlinNoise(x, 0, z, grassPerlinScale, grassPerlinHeight, grassPerlinPower);
				// grass += PerlinNoise(x, grassUnkownY, z, grassSecondPerlinScale, grassSecondPerlinHeight, grassSecondPerlinPower);
				for (int y = 0; y < worldY; y++) {
					if (y <= rock) {
						worldData[x, y, z] = (byte)TextureType.grass.GetHashCode();
					} else if (y <= grass) {
						worldData[x, y, z] = (byte)TextureType.rock.GetHashCode();
					}
				}
			}
		}

		chunks = new Chunk[
			Mathf.FloorToInt(worldX / chunkSize), 
			Mathf.FloorToInt(worldY / chunkSize), 
			Mathf.FloorToInt(worldZ / chunkSize)
		];

		for (int x = 0; x < chunks.GetLength(0); x++) {
			for (int y = 0; y < chunks.GetLength(1); y++) {
				for (int z = 0; z < chunks.GetLength(2); z++) {
					GameObject newChunk = Instantiate(chunk, new Vector3(x * chunkSize - 0.5f, y * chunkSize + 0.5f, z * chunkSize - 0.5f), Quaternion.identity) as GameObject;
					chunks[x, y, z] = newChunk.GetComponent<Chunk>();
					chunks[x, y, z].WorldGo = gameObject;
					chunks[x, y, z].ChunkX = x * chunkSize;
					chunks[x, y, z].ChunkY = y * chunkSize;
					chunks[x, y, z].ChunkZ = z * chunkSize;
					chunks[x, y, z].ChunkSize = chunkSize;
				}
			}
		}

	}

	public int PerlinNoise (int x, int y, int z, float scale, float height, float power) {
		float perlinValue = Noise.Noise.GetNoise((double)x / scale, (double)y / scale, (double)z / scale);
		perlinValue *= height;

		if (power != 0) {
			perlinValue = Mathf.Pow(perlinValue, power);
		}

		return (int)perlinValue;
	}

	public byte Block (int x, int y, int z) {
		if (
			x >= worldX || x < 0 ||
			y >= worldY || y < 0 ||
			z >= worldZ || z < 0
		) {
			return (byte)TextureType.rock.GetHashCode();
		}
		return worldData[x, y, z];
	}
}
