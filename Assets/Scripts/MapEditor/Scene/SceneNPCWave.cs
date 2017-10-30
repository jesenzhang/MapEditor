using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class SceneNPCWave : MonoBehaviour {
	 
	 
	public int waveSequence = 0;
	public int delayTime = 0;  

	public string displayName{
		get { 
			return "波次-" + waveSequence;
		}
	}
}
