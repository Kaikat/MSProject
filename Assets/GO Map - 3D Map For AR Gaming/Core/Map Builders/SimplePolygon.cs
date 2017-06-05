using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using GoShared;

#if UNITY_EDITOR
using UnityEditor;


[CustomEditor(typeof(SimplePolygon))]
public class ObjectBuilderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SimplePolygon myScript = (SimplePolygon)target;
		if(GUILayout.Button("Rebuild mesh"))
		{
			
			myScript.Rebuild();

		}
	}
}


#endif



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimplePolygon : MonoBehaviour
{
	public bool isClockWise;
	public bool hasDuplicates;
	public List<Vector3> verts;
	MeshFilter filter;

	public Mesh load (List<Vector3> _verts) {

		verts = _verts;

		filter = gameObject.GetComponent<MeshFilter> ();
		if (filter == null) {
			filter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
		}
			
		Mesh mesh = CreateMesh(verts.ToList());
		filter.sharedMesh = mesh;
		return mesh;
	}

	public Mesh loadExtruded (List<Vector3> _verts, float height) {

		verts = _verts;

		filter = gameObject.GetComponent<MeshFilter> ();
		if (filter == null) {
			filter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
		}

		List <Vector3> vertices = verts.ToList ();

		Mesh mesh = CreateMesh(vertices);
		filter.sharedMesh = SimpleExtruder.Extrude (mesh, gameObject,height);
		return mesh;
	}




	public bool IsClockwise(IList<Vector3> vertices)
	{
		double sum = 0.0;
		for (int i = 0; i < vertices.Count; i++) {
			Vector3 v1 = vertices[i];
			Vector3 v2 = vertices[(i + 1) % vertices.Count]; // % is the modulo operator
			sum += (v2.x - v1.x) * (v2.z + v1.z);
		}
		return sum > 0.0;
	}

	public Mesh CreateMesh(List<Vector3> verts)
	{

		Triangulator triangulator = new GoShared.Triangulator(verts.Select(x => x.ToVector2xz()).ToArray());
		Mesh mesh = new Mesh();

		List<Vector3> vertices = verts;
		List<int> indices = triangulator.Triangulate().ToList();

		var n = vertices.Count;
		for (int index = 0; index < n; index++)
		{
			var v = vertices[index];
			vertices.Add(new Vector3(v.x, v.y, v.z));
		}

		for (int i = 0; i < n - 1; i++)
		{
			indices.Add(i);
			indices.Add(i + n);
			indices.Add(i + n + 1);
			indices.Add(i);
			indices.Add(i + n + 1);
			indices.Add(i + 1);
		}

		indices.Add(n - 1);
		indices.Add(n);
		indices.Add(0);

		indices.Add(n - 1);
		indices.Add(n + n - 1);
		indices.Add(n);

		mesh.vertices = vertices.ToArray();
		mesh.triangles = indices.ToArray();

		Vector2[] uvs = new Vector2[vertices.ToArray().Length];

		for (int i=0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
		}
		mesh.uv = uvs;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		;

		return mesh;
	}

	public void Rebuild () {
		Debug.Log ("Rebuild mesh");
		filter = GetComponent<MeshFilter> ();
		load (verts);
		isClockWise = IsClockwise (verts);

		//		TestClipper ();
		//		filter.mesh = PlaneMesh (10, 10);
	}

	public static Mesh PlaneMesh(float width, float height)
	{
		Mesh m = new Mesh();
		m.name = "ScriptedMesh";
		m.vertices = new Vector3[] {
			new Vector3(-width, 0,-height),
			new Vector3(width, 0,  -height),
			new Vector3(width, 0, height),
			new Vector3(-width,0 , height)
		};
		m.uv = new Vector2[] {
			new Vector2 (0, 0),
			new Vector2 (0, 1),
			new Vector2(1, 1),
			new Vector2 (1, 0)
		};
		m.triangles = new int[] { 0, 1, 2, 0, 2, 3};
		m.RecalculateNormals();

		return m;
	}
}

