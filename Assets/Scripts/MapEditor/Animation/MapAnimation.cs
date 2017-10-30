using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MapAnimation : MonoBehaviour {

	public int id = 0;

	public int duration = 5000; 

	//public bool autoPlay = false;

	// Use this for initialization
	void Start () {
	
	} 
	
	// Update is called once per frame
	void OnDrawGizmos () {
		duration = 0;
		var frames = GetComponentsInChildren<MapAnimationFrame> (); 
		for (int i = 0; i < frames.Length-1; ++i) {
			Gizmos.DrawLine(frames[i].transform.position,frames[i+1].transform.position);
			duration += frames [i].duration;
		}
		if (frames.Length > 0) {
			Gizmos.DrawLine(frames[frames.Length-1].transform.position,transform.position);  
		}

	}
}
