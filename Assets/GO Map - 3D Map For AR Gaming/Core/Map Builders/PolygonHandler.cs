using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using GoShared;


namespace GoMap {
	
	public class PolygonHandler
    {

		public Mesh mesh2D;
		public Vector3 center;
 		private IList subject;
		private List<object> clips;
		private List<Vector3> properties;

		public List<Vector3> convertedSubject;

		public PolygonHandler(IList _subject, List<object> _clips)
        {
			subject = _subject;
			clips = _clips;

			convertedSubject = CoordsToVerts (subject);
			center = convertedSubject.Aggregate((acc, cur) => acc + cur) / convertedSubject.Count;
        }

		public GameObject CreateModel(Layer layer, float height)
        {

			GameObject polygon = new GameObject();
			try {
				Poly2Mesh.Polygon poly = new Poly2Mesh.Polygon();
				poly.outside = convertedSubject;
				if (clips != null ) {
					foreach (IList clipVerts in clips) {
						poly.holes.Add(CoordsToVerts(clipVerts));
					}
				}

				MeshFilter filter = polygon.AddComponent<MeshFilter>();
				polygon.AddComponent(typeof(MeshRenderer));
				Mesh mesh = Poly2Mesh.CreateMesh (poly);


				if (mesh) {
					Vector2[] uvs = new Vector2[mesh.vertices.Length];
					for (int i=0; i < uvs.Length; i++) {
						uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
					}
					mesh.uv = uvs;

					mesh2D = Mesh.Instantiate(mesh);

					if (height > 0) {
						mesh = SimpleExtruder.Extrude (mesh, polygon, height);
					}


				}
				filter.sharedMesh = mesh;

				if (layer.useColliders)
					polygon.AddComponent<MeshCollider>().sharedMesh = mesh;
					
			} catch (Exception ex) {
				Debug.Log ("[PolygonHandler] Catched exeption: " + ex);
			}




			return polygon;

        }

		public GameObject CreateRoof (){

			GameObject roof = new GameObject();
			MeshFilter filter = roof.AddComponent<MeshFilter>();
			roof.AddComponent(typeof(MeshRenderer));
			filter.mesh = mesh2D;
			return roof;
		}

		public List<Vector3> CoordsToVerts (IList polygon) {
		
			var l = new List<Vector3>();
			for (int i = 0; i < polygon.Count - 1; i++)
			{
				IList c = (IList)polygon[i];
				Coordinates coords = new Coordinates ((double)c[1], (double)c[0],0);
				Vector3 p = coords.convertCoordinateToVector (0);
//				if (l.Contains (p)) {
//					//That's totally empirical =(
//					p.x = p.x + 0.01f;
//				}
				l.Add (p);
			}
			return l;
		}



		public bool IsClockwise(IList<Vector3> vertices)
		{
			double sum = 0.0;
			for (int i = 0; i < vertices.Count; i++) {
				Vector3 v1 = vertices[i];
				Vector3 v2 = vertices[(i + 1) % vertices.Count];
				sum += (v2.x - v1.x) * (v2.z + v1.z);
			}
			return sum > 0.0;
		}
    }
}
