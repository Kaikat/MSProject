using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class SimpleRoad : MonoBehaviour
{
	[HideInInspector]
	public Vector3[] verts;
	MeshFilter filter;
	public float width;
	public bool isLoop = false;
	public bool smoothEdges;
	private Mesh mesh;

	private Vector3[] vertices = new Vector3[0];
	[HideInInspector]
	public int[] triangles = new int[0];

	public void load () {

//		isLoop = verts [0].Equals (verts [verts.Length - 1]);

		if (smoothEdges && isLoop)
			smoothEdges = false;
		
		filter = gameObject.GetComponent<MeshFilter> ();
		if (filter == null) {
			filter = (MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
		}

		UpdateVertices();

		filter.sharedMesh = CreateMesh();
	}

	public void UpdateVertices()
	{
		if (verts.Length < 2) return; // minimum to make a line

		int count = verts.Length - 1;

		vertices = new Vector3[count*4];
		triangles = new int[(verts.Length-1)*6];

		List <Vector3> dirs = new List<Vector3> ();
		List <Vector3> tans = new List<Vector3> ();
 
		Vector3 tanVect = Vector3.down;


		for (int p = 0; p<verts.Length; p++)
		{
			Vector3 dir;
			Vector3 tangent;

			if (p == 0) // First 
			{
				if (isLoop) {
					dir = (verts[p+1] - verts[p]).normalized; 
					Vector3 dirBefore = (verts [p] - verts [verts.Length-2]).normalized;
					tangent =  Vector3.Cross( tanVect,(dirBefore + dir) * 0.5f ).normalized;
				} 
				else {
					dir = (verts[p+1] - verts[p]).normalized; 
					tangent = Vector3.Cross( tanVect, dir).normalized;
				}
			}

			else if (p != verts.Length-1) // Middles
			{
				dir = (verts[p+1] - verts[p]).normalized; 
				Vector3 dirBefore = (verts [p] - verts [p-1]).normalized;
				tangent =  Vector3.Cross( tanVect,(dirBefore + dir) * 0.5f ).normalized;
			}

			else // Last
			{
				if (isLoop) {
					dir = (verts[1] - verts[p]).normalized; 
					Vector3 dirBefore = (verts [p] - verts [p-1]).normalized;
					tangent =  Vector3.Cross( tanVect,(dirBefore + dir) * 0.5f ).normalized;

				} else {
					dir = (verts [p] - verts [p-1]).normalized;
					tangent = Vector3.Cross( tanVect, dir).normalized;
				}

			}

			dirs.Add (dir);
			tans.Add (tangent);

		}


		for (int i = 0; i<count; i++)
		{
			vertices[(i*4)+0] = verts[i] + (tans[i] * (width));
			vertices[(i*4)+1] = verts[i] - (tans[i] * (width));
			vertices[(i*4)+2] = verts[i+1] + (tans[i+1] * (width));
			vertices[(i*4)+3] = verts[i+1] - (tans[i+1] * (width));

			triangles[(i*6)+0] = (i*4)+0;
			triangles[(i*6)+1] = (i*4)+2;
			triangles[(i*6)+2] = (i*4)+1;
			triangles[(i*6)+3] = (i*4)+2;
			triangles[(i*6)+4] = (i*4)+3;
			triangles[(i*6)+5] = (i*4)+1;

		}

		for (int i = 0; i < vertices.Length; i++) 
		{
			vertices[i] = filter.transform.InverseTransformPoint(vertices[i]);
		}

	}
		
	public Mesh CreateMesh()
	{

		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;


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

}

