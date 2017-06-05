using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;
using System;

namespace GoMap {

	[System.Serializable]
	public class GOFeature {

		public string name;
		public string kind;
		public string type;
		public Int64 sort;
		[HideInInspector] public IList geometry;
		public List <Vector3> convertedGeometry;
		[HideInInspector] public GameObject parent;
		public IDictionary properties;
		public Layer layer;

		public GOFeature () {
		
		}

		public GOFeature (GameObject parent_, string kind_, string type_, IList coordinates_, IDictionary properties_, Layer layer_) {
		
			kind = kind_;
			type = type_;
			geometry = coordinates_;
			properties = properties_;
			layer = layer_;
			parent = parent_;

			if (properties.Contains("name")) {
				name = (string)properties ["name"];
			}

			if (properties.Contains("sort_key")) {
				sort = (Int64)properties["sort_key"];
			} else sort = (Int64)properties["sort_rank"];

			convertedGeometry = new List<Vector3>();
			for (int i = 0; i < geometry.Count; i++)
			{
				IList c = (IList)geometry[i];
				Coordinates coords = new Coordinates ((double)c[1], (double)c[0],0);
				float defaultY = layer.defaultRendering.distanceFromFloor;
				convertedGeometry.Add(coords.convertCoordinateToVector(defaultY));
			}

		}
	}
}