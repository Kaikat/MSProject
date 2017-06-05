using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace GoMap
{

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class RoadPolygon : MonoBehaviour
    {
        public List<Vector3> _verts;
		public GORoadFeature f;

		public void Initialize( List<Vector3> verts, string kind, Layer layer, int sort, string name, GOMap map)
        {

			if (verts.Count == 2 && verts[0].Equals(verts[1])) {
				return;
			}

			_verts = verts;

			RenderingOptions renderingOptions = layer.defaultRendering;
			foreach (RenderingOptions r in layer.renderingOptions) {
				if (r.kind == kind) {
					renderingOptions = r;
					break;
				}
			}

			float width = layer.defaultRendering.lineWidth;
			float defaultY = layer.defaultRendering.distanceFromFloor;
			Material material = layer.defaultRendering.material;
			Material outlineMaterial = layer.defaultRendering.outlineMaterial;
			float outlineWidth = width + layer.defaultRendering.outlineWidth;
			string tag = layer.defaultRendering.tag;

			if (renderingOptions != null) {
				width = renderingOptions.lineWidth;
				material = renderingOptions.material;
				defaultY = renderingOptions.distanceFromFloor;
				outlineMaterial = renderingOptions.outlineMaterial;
				outlineWidth = width + renderingOptions.outlineWidth;
				tag = renderingOptions.tag;
			}
				
			if (tag.Length > 0) {
				gameObject.tag = tag;
			}

			if (defaultY == 0f)
				defaultY = sort/1000.0f;

			if (material)
				material.renderQueue = -sort;
			if (outlineMaterial)
				outlineMaterial.renderQueue = -sort;
			
			SimpleRoad road = gameObject.AddComponent<SimpleRoad> ();

			Vector3[] vertices = _verts.ToArray();

			road.verts = vertices;
			road.width = width;
			road.smoothEdges = true;
			road.load ();

			gameObject.GetComponent<Renderer>().material = material;

			Vector3 position = transform.position;
			position.y = defaultY;

			#if GOLINK
			//[GOLINK] Trigger GOMap Tile creation (This requires both GoMap and GoTerrain)
			if (renderingOptions.polygonHeight > 0) {
				int offset = 5;
				gameObject.GetComponent<MeshFilter> ().sharedMesh = SimpleExtruder.Extrude (gameObject.GetComponent<MeshFilter> ().sharedMesh, gameObject, renderingOptions.polygonHeight + offset);
				position.y -= offset;
			}
			#else


			if (layer.json == "roads" && name != null && name.Length > 0 && renderingOptions.useStreetNames) {
				GOStreetName streetName = new GameObject ().AddComponent<GOStreetName> ();
				streetName.gameObject.name = name + "_streetname";
				streetName.transform.SetParent (road.transform);
				StartCoroutine(streetName.Build (name,map.textShader));
			}

			#endif

			transform.position = position;

			if (outlineMaterial != null) {
				GameObject outline = CreateRoadOutline (verts, outlineMaterial, outlineWidth, sort, defaultY);
				if (layer.useColliders)
					outline.AddComponent<MeshCollider> ().sharedMesh = outline.GetComponent<MeshFilter> ().sharedMesh;

				outline.layer = gameObject.layer;
				outline.tag = gameObject.tag;
				
			} else if (layer.useColliders) {
//				Mesh m = gameObject.GetComponent<MeshFilter> ().sharedMesh;
				gameObject.AddComponent<MeshCollider> ();
			}
				

        }

		GameObject CreateRoadOutline (List<Vector3> verts, Material material, float width, int sort, float y) {

			GameObject outline = new GameObject ("outline");
			outline.transform.parent = transform;

			material.renderQueue = -(sort-1);

			SimpleRoad road = outline.AddComponent<SimpleRoad> ();

			Vector3[] vertices = _verts.ToArray();
			road.verts = vertices;
			road.width = width;

			road.load ();

			Vector3 position = outline.transform.position;
			position.y = -0.029f;
			outline.transform.localPosition = position;

			outline.GetComponent<Renderer>().material = material;

			return outline;
		}

		//[GOLink]
		#if GOLINK
		public List<Vector3> BreakLine (List<Vector3> verts, GoTerrain.GOTerrain terrain) {

			int treshold = 25;
			List<Vector3> brokenVerts = new List <Vector3> ();
			for (int i = 0; i<verts.Count-1; i++) {
				
				float d = Vector3.Distance (verts [i], verts [i + 1]);
				if (d > treshold) {
					for (int j = 0; j < d; j += treshold) {
						Vector3 P = LerpByDistance (verts [i], verts [i + 1], j);
						P.y = terrain.FindAltitudeForVector (P);
						brokenVerts.Add (P);
					}
				} else {
					Vector3 P = verts [i];
					P.y = terrain.FindAltitudeForVector (P);
					brokenVerts.Add(P);
				}

			}
			Vector3 Pn = verts [verts.Count - 1];
			Pn.y = terrain.FindAltitudeForVector (Pn);
			brokenVerts.Add (Pn);
			return brokenVerts;
		}

		public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
		{
			Vector3 P = x * Vector3.Normalize(B - A) + A;
			return P;
		}
		#endif




    }




}
