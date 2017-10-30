using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MapAudioSource :MonoBehaviour {

	public int audioID ;

	public string audioPath;

	public float volume = 1; 

	public float minDistance = 1;

	public float maxDistance = 15;

	public int loop = -1;

	public bool autoplay = true;

	void Update(){
		name = ToString ();
	}

	public override string ToString ()
	{
		return string.Format ("音频源-"+audioPath);
	}
}
