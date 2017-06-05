using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GoMap {

	[System.Serializable]
	public class GORoadFeature : GOFeature {

		public bool isBridge;
		public bool isTunnel;
		public bool isLink;

		public Vector3 startingPoint;
		public Vector3 endingPoint;

		public GORoadFeature (GameObject parent, string kind, string type, IList coordinates, IDictionary properties, Layer layer) : base (parent, kind, type, coordinates, properties, layer) {

			startingPoint = convertedGeometry [0];
			endingPoint = convertedGeometry [convertedGeometry.Count - 1];
		}

		public IEnumerator BuildRoad (GOTile tile, bool delayedLoad) {
		
			isBridge = properties.Contains ("is_bridge") && properties ["is_bridge"].ToString() == "True";
			isTunnel = properties.Contains ("is_tunnel") && properties ["is_tunnel"].ToString() == "True";
			isLink = properties.Contains ("is_link") && properties ["is_link"].ToString() == "True";

			if ((isBridge && !layer.useBridges) || (isTunnel && !layer.useTunnels) || (isLink && !layer.useBridges)) {
				yield break;
			}

			GameObject road = new GameObject (layer.json);
			RoadPolygon roadPolygon = road.AddComponent<RoadPolygon>();
			road.transform.parent = parent.transform;

			roadPolygon.f = this;

			#if GOLINK
			if (tile.map.goTerrain != null) {
				convertedGeometry = roadPolygon.BreakLine (convertedGeometry,tile.map.goTerrain);
			}
			#endif

			//Layer mask
			if (layer.useLayerMask == true) {
				tile.AddObjectToLayerMask (layer, road);				
			} 

			try {
				roadPolygon.Initialize(convertedGeometry, kind,layer,(int)sort, (string)properties["name"],tile.map);

				Attributes attributes = road.AddComponent<Attributes>();
				attributes.useName = true;
				attributes.loadWithDictionary((Dictionary<string,object>)properties);
			}
			catch (Exception ex) {
				Debug.Log(layer.name + " " + kind + " "+ ex);
			}
			if (delayedLoad)
				yield return null;

		}

		public List<GORoadFeature> FindRoadsMatching(List<GORoadFeature> roads) {
		
			List<GORoadFeature> matching = new List<GORoadFeature>();
			foreach (GORoadFeature r in roads) {

				bool geoMatch = r.startingPoint.Equals (endingPoint) || r.endingPoint.Equals (startingPoint);
				bool reversedGeoMatch = r.startingPoint.Equals (startingPoint) || r.endingPoint.Equals (endingPoint);

				bool nameMatch = r.name == "" || name == "" || r.name == name ;
				bool kindMatch = r.kind == kind;

				if ((geoMatch || reversedGeoMatch) && nameMatch && kindMatch) {
					if (AngleWithRoad (r) > 90) {
						matching.Add (r);
					}
				}
			} 
				
			return matching;
		}

		public float AngleWithRoad (GORoadFeature r) {

			Vector3 dir1 = Vector3.zero; //this
			Vector3 dir2 = Vector3.zero; //other

			if (r.startingPoint.Equals (endingPoint)) {

				dir1 = convertedGeometry [convertedGeometry.Count - 2] - endingPoint;
				dir2 = r.convertedGeometry[1] -r.startingPoint;

			} else if ( r.endingPoint.Equals (startingPoint)){

				dir2 = r.convertedGeometry [r.convertedGeometry.Count - 2] - r.endingPoint;
				dir1 = convertedGeometry[1] - startingPoint;
			}
			else if ( r.startingPoint.Equals (startingPoint)){

				dir1 = convertedGeometry[1] - startingPoint;
				dir2 = r.convertedGeometry[1] - r.startingPoint;

			}
			else if ( r.endingPoint.Equals (endingPoint)){
				
				dir1 = convertedGeometry [convertedGeometry.Count - 2] - endingPoint;
				dir2 = r.convertedGeometry [r.convertedGeometry.Count - 2] - r.endingPoint;
			}

			float angle = Vector3.Angle (dir1, dir2);
			Debug.Log (angle);
			return angle;

		}

		public List<GORoadFeature> Merge(List<GORoadFeature> roads) {

			List<GORoadFeature> merged = new List<GORoadFeature>();

			foreach (GORoadFeature r in roads) {

				if (r.startingPoint.Equals (endingPoint)) {
				
					endingPoint = r.endingPoint;
					r.convertedGeometry.RemoveAt (0);
					convertedGeometry.AddRange (r.convertedGeometry);
					merged.Add (r);

				} else if ( r.endingPoint.Equals (startingPoint)){

					startingPoint = r.startingPoint;
					convertedGeometry.RemoveAt (0);
					r.convertedGeometry.AddRange (convertedGeometry);
					convertedGeometry = r.convertedGeometry;
					merged.Add (r);
				}
				else if ( r.startingPoint.Equals (startingPoint)){

					startingPoint = r.endingPoint;
					r.convertedGeometry.Reverse ();
					convertedGeometry.RemoveAt (0);
					r.convertedGeometry.AddRange (convertedGeometry);
					convertedGeometry = r.convertedGeometry;
					merged.Add (r);
				}
				else if ( r.endingPoint.Equals (endingPoint)){

					endingPoint = r.startingPoint;
					r.convertedGeometry.Reverse ();
					r.convertedGeometry.RemoveAt (0);
					convertedGeometry.AddRange (r.convertedGeometry);
					merged.Add (r);
				}

				if (name == "" && r.name != "") {
					name = r.name;
				}

			}

			return merged;
		}




	}
}
