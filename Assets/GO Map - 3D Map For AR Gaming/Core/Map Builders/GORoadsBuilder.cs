using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GoMap {

	public class GORoadsBuilder {

		public static IEnumerator BuildRoads (GameObject parent,IList features, Layer layer, bool delayedLoad, GOTile tile) {
		
			List<GORoadFeature> roads = new List<GORoadFeature> ();

			foreach (IDictionary geo in features) {

				IDictionary geometry = (IDictionary)geo ["geometry"];
				IDictionary properties = (IDictionary)geo ["properties"];
				string type = (string)geometry ["type"];
				string kind = (string)properties ["kind"];

				if (properties.Contains("kind_detail") && layer.json != "roads") {
					kind = (string)properties["kind_detail"];
				}

				var id = properties ["id"]; 

				if (layer.useOnly.Length > 0 && !layer.useOnly.Contains (kind)) {
					continue;
				}
				if (layer.avoid.Length > 0 && layer.avoid.Contains (kind)) {
					continue;
				}

				if (type == "MultiLineString" || (type == "Polygon" && !layer.isPolygon)) {
					IList lines = new List<object>();
					lines = (IList)geometry ["coordinates"];

//					Debug.Log ("Multi line: " + name + "count: "+lines.Count);

					foreach (IList coordinates in lines) {
						roads.Add (new GORoadFeature (parent, kind, type, coordinates, properties, layer));
					}
				} 

				else if (type == "LineString") {
					IList coordinates = (IList)geometry ["coordinates"];
					roads.Add (new GORoadFeature (parent, kind, type, coordinates, properties, layer));
				} 
			}

			roads = MergeRoads (roads);

			int n = 25;
			for (int i = 0; i < roads.Count; i+=n) {

				for (int k = 0; k<n; k++) {
					if (i + k >= roads.Count) {
						yield return null;
						break;
					}

					GORoadFeature r = roads [i + k];
					tile.StartCoroutine (r.BuildRoad(tile,delayedLoad));
				}

				yield return null;
			}



//			foreach (GORoadFeature road in roads) {
//				tile.StartCoroutine (road.BuildRoad (tile,delayedLoad));
//			}

			yield return null;
		}
			

		static List <GORoadFeature> MergeRoads (IList roads) {

			List <GORoadFeature> merged = new List <GORoadFeature> ();

			foreach (GORoadFeature r in roads) {

				List <GORoadFeature> m = r.FindRoadsMatching (merged);
				if (m.Count == 0) {
					merged.Add (r);
					continue;
				}

				List<GORoadFeature> toRemove = r.Merge (m);
				merged = merged.Except (toRemove).ToList();
				merged.Add (r);
				
			}
//
//			if (roads.Count != merged.Count) {
//				return MergeRoads (merged);
//			} else 
				return merged;

		}


	}
}