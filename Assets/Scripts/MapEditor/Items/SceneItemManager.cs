using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SceneItemManager : MonoBehaviour {
	private static SceneItemManager _instance = null;

	public static SceneItemManager instance{
		get { 
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<SceneItemManager> ();
			}
			return _instance;
		}
	}

	void Awake(){
		_instance = this;
	}

	void OnDestroy(){
		if (_instance == this)
			_instance = null;
	} 
	 
}
