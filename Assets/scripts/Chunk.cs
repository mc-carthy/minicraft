using System.Collections.Generic;
using UnityEngine;

public enum TextureType {
	air,
	grass,
	rock
}

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour {

	private List<Vector3> newVertices = new List<Vector3>();
	private List<int> newTriangles = new List<int>();
	private List<Vector2> newUV = new List<Vector2>();

	private Mesh mesh;
	private MeshCollider chunkCollider;
	private float textureWidth = 0.083f;
	private int faceCount;
	private World world;

	[SerializeField]
	private GameObject worldGO;
	[SerializeField]
	private int chunkSize = 16;

	private Vector2 grassTop = new Vector2(1, 11);
	private Vector2 grassSide = new Vector2(0, 10);
	private Vector2 rock = new Vector2(7, 8);

	private void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		chunkCollider = GetComponent<MeshCollider>();
		world = worldGO.GetComponent<World>() as World;

		GenerateMesh();
	}

	private void GenerateMesh () {
		for (int x = 0; x < chunkSize; x++) {
			for (int y = 0; y < chunkSize; y++) {
				for (int z = 0; z < chunkSize; z++) {
					if (world.Block(x, y, z) != (byte)TextureType.air.GetHashCode()) {
						// Block above is air
						if (world.Block(x, y + 1, z) == (byte)TextureType.air.GetHashCode()) {
							CubeTop(x, y, z, world.Block(x, y, z));
						}
						// Block below is air
						if (world.Block(x, y - 1, z) == (byte)TextureType.air.GetHashCode()) {
							CubeBottom(x, y, z, world.Block(x, y, z));
						}
						// Block to east is air
						if (world.Block(x + 1, y, z) == (byte)TextureType.air.GetHashCode()) {
							CubeEast(x, y, z, world.Block(x, y, z));
						}
						// Block to west is air
						if (world.Block(x - 1, y, z) == (byte)TextureType.air.GetHashCode()) {
							CubeWest(x, y, z, world.Block(x, y, z));
						}
						// Block to north is air
						if (world.Block(x, y, z + 1) == (byte)TextureType.air.GetHashCode()) {
							CubeNorth(x, y, z, world.Block(x, y, z));
						}
						// Block to south is air
						if (world.Block(x, y, z - 1) == (byte)TextureType.air.GetHashCode()) {
							CubeSouth(x, y, z, world.Block(x, y, z));
						}
					}
				}
			}
		}
		UpdateMesh();
	}

	private void UpdateMesh () {
		mesh.Clear();
		mesh.vertices = newVertices.ToArray();
		mesh.uv = newUV.ToArray();
		mesh.triangles = newTriangles.ToArray();
		// mesh.Optimize(); ?
		mesh.RecalculateNormals();

		chunkCollider.sharedMesh = null;
		chunkCollider.sharedMesh = mesh;
		
		newVertices.Clear();
		newUV.Clear();
		newTriangles.Clear();
		faceCount = 0;
	}

	private void Cube (Vector2 texturePos) {

		newTriangles.Add(faceCount * 4);
		newTriangles.Add(faceCount * 4 + 1);
		newTriangles.Add(faceCount * 4 + 2);

		newTriangles.Add(faceCount * 4);
		newTriangles.Add(faceCount * 4 + 2);
		newTriangles.Add(faceCount * 4 + 3);

		newUV.Add(new Vector2(textureWidth * texturePos.x + textureWidth, textureWidth * texturePos.y));
		newUV.Add(new Vector2(textureWidth * texturePos.x + textureWidth, textureWidth * texturePos.y + textureWidth));
		newUV.Add(new Vector2(textureWidth * texturePos.x, textureWidth * texturePos.y + textureWidth));
		newUV.Add(new Vector2(textureWidth * texturePos.x, textureWidth * texturePos.y));

		faceCount++;
	}

	private void CubeTop (int x, int y, int z, byte block) {
		newVertices.Add(new Vector3(x, y, z + 1));
		newVertices.Add(new Vector3(x + 1, y, z + 1));
		newVertices.Add(new Vector3(x + 1, y, z));
		newVertices.Add(new Vector3(x, y, z));

		Vector2 texturePos;
		texturePos = rock; // TODO - Currently hardcoded

		Cube(texturePos);
	}

	private void CubeNorth (int x, int y, int z, byte block) {
		newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
		newVertices.Add(new Vector3(x + 1, y, z + 1));
		newVertices.Add(new Vector3(x, y, z + 1));
		newVertices.Add(new Vector3(x, y - 1, z + 1));

		Vector2 texturePos;
		texturePos = rock; // TODO - Currently hardcoded

		Cube(texturePos);
	}

	private void CubeEast (int x, int y, int z, byte block) {
		newVertices.Add(new Vector3(x + 1, y - 1, z));
		newVertices.Add(new Vector3(x + 1, y, z));
		newVertices.Add(new Vector3(x + 1, y, z + 1));
		newVertices.Add(new Vector3(x + 1, y - 1, z + 1));

		Vector2 texturePos;
		texturePos = rock; // TODO - Currently hardcoded

		Cube(texturePos);
	}

	private void CubeSouth (int x, int y, int z, byte block) {
		newVertices.Add(new Vector3(x, y - 1, z));
		newVertices.Add(new Vector3(x, y, z));
		newVertices.Add(new Vector3(x + 1, y, z));
		newVertices.Add(new Vector3(x + 1, y - 1, z));

		Vector2 texturePos;
		texturePos = rock; // TODO - Currently hardcoded

		Cube(texturePos);
	}

	private void CubeWest (int x, int y, int z, byte block) {
		newVertices.Add(new Vector3(x, y - 1, z + 1));
		newVertices.Add(new Vector3(x, y, z + 1));
		newVertices.Add(new Vector3(x, y, z));
		newVertices.Add(new Vector3(x, y - 1, z));

		Vector2 texturePos;
		texturePos = rock; // TODO - Currently hardcoded

		Cube(texturePos);
	}

	private void CubeBottom (int x, int y, int z, byte block) {
		newVertices.Add(new Vector3(x, y - 1, z));
		newVertices.Add(new Vector3(x + 1, y - 1, z));
		newVertices.Add(new Vector3(x + 1, y - 1, z + 1));
		newVertices.Add(new Vector3(x, y - 1, z + 1));

		Vector2 texturePos;
		texturePos = rock; // TODO - Currently hardcoded

		Cube(texturePos);
	}
}
