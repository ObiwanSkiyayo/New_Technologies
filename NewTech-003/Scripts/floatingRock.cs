using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class floatingRock : MonoBehaviour {

	private List<GameObject> rockList = new List<GameObject>();

	private int rockIndex;

	void Start(){

		// Store all tagged spawn points in a list.
		foreach (GameObject i in GameObject.FindGameObjectsWithTag("floatingRocks")) {
			rockList.Add (i);
		}
	}

	void Update(){
			
		for (int i = 0; i < rockList.Count; i++) {
			// Instatiate random crystals at each spawn point with the rotation reset to 0.
			rockList[i].gameObject.transform.Rotate (Vector3.up * 8.0f * Time.deltaTime);
		}
	}

}
