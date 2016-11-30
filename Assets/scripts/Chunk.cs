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

	private Vector2 grassTop = new Vector2(1, 11);
	private Vector2 grassSide = new Vector2(0, 10);
	private Vector2 rock = new Vector2(7, 8);

	private void Start () {
		mesh = GetComponent<MeshFilter>().mesh;
		chunkCollider = GetComponent<MeshCollider>();

		CubeTop(0, 0, 0, (byte)TextureType.rock.GetHashCode());
		CubeNorth(0, 0, 0, (byte)TextureType.rock.GetHashCode());
		CubeEast(0, 0, 0, (byte)TextureType.rock.GetHashCode());
		CubeSouth(0, 0, 0, (byte)TextureType.rock.GetHashCode());
		CubeWest(0, 0, 0, (byte)TextureType.rock.GetHashCode());
		CubeBottom(0, 0, 0, (byte)TextureType.rock.GetHashCode());

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
