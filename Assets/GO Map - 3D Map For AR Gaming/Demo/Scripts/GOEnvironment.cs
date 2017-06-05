using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace GoMap {
	
	public class GOEnvironment : MonoBehaviour {

		public GameObject [] treePrefab;
		public GameObject boatPrefab;
		public GameObject [] baloonPrefab;


		public void SpawnBallons (GOTile tile) {
		
			int spawn = Random.Range (0, 5);
			if (spawn == 0) {
				float y = Random.Range (90, 250);
				Vector3 pos = tile.tileCenter.convertCoordinateToVector ();
				pos.y = y;
				int n = Random.Range (0, baloonPrefab.Length);
				GameObject obj = (GameObject)Instantiate (baloonPrefab[n]);
				obj.transform.position = pos;
				obj.transform.parent = transform;
			}

		}

		public void GrowTrees (Mesh mesh, Layer layer, string kind,Vector3 center) {

			if (kind == "park" || kind == "garden") {
				var randomRotation = Quaternion.Euler( 0 , Random.Range(0, 360) , 0);
				int n = Random.Range (0, treePrefab.Length);
				center.y = treePrefab [n].transform.position.y;
				GameObject obj = (GameObject)Instantiate (treePrefab[n], center,randomRotation);
				obj.transform.parent = transform;
			}
		}
			
		public void AddBoats (Mesh mesh, Layer layer, string kind,Vector3 center) {

			bool spawn = Random.value > 0.5f;
			if (kind != "riverbank" && kind != "water" && spawn) {
				var randomRotation = Quaternion.Euler (0, Random.Range (0, 360), 0);
				center.y = 2;
				GameObject obj = (GameObject)Instantiate (boatPrefab, center, randomRotation);
				obj.transform.parent = transform;	
			}
		}


		public Vector3 RandomPositionInMesh(Mesh mesh){


			Bounds bounds = mesh.bounds;

			float minX = bounds.size.x * 0.5f;
			float minZ = bounds.size.z * 0.5f;

			Vector3 newVec = new Vector3(Random.Range (minX, -minX),
				gameObject.transform.position.y,
				Random.Range (minZ, -minZ));
			return newVec;
		}

	}
}


