using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapAreaType{
	EARTH,
	WOOD,
	ROCK,
	GRASS,
	WATER,
	METAL,
}

[ExecuteInEditMode]
public class MapArea : MonoBehaviour {
	public int areaId;
	public MapAreaType areaType;	
	public string audioPath;
	private TextMesh textMesh;
	private MeshRenderer render;

	void Start(){ 
		render = GetComponent<MeshRenderer> ();  
		render.material.renderQueue = 3000;
	}
	// Update is called once per frame
	void Update () {
		name = displayName;  
		var color = displayColor;
		color.a = 0.1f;
		render.material.SetColor ("_Color", color);
	}

	public virtual string displayName{
		get { 
			string typeString = areaType.ToString ();
			switch (areaType) {
			case MapAreaType.EARTH:
				typeString = "泥土地面";
				break;
			case MapAreaType.WOOD:
				typeString = "木头地面";
				break;
			case MapAreaType.ROCK:
				typeString = "石头地面";
				break;
			case MapAreaType.GRASS:
				typeString = "草地";
				break;
			case MapAreaType.WATER:
				typeString = "水面";
				break;
			}
			return "区域-" + typeString;
		}
	}

	public virtual Color displayColor{
		get { 
			Color color = Color.black;
			switch (areaType) {
			case MapAreaType.EARTH:
				color = Color.white;
				break;
			case MapAreaType.WOOD:
				color = Color.yellow;
				break;
			case MapAreaType.ROCK:
				color = Color.gray;
				break;
			case MapAreaType.GRASS:
				color = Color.green;
				break;
			case MapAreaType.WATER:
				color = Color.blue;
				break;
			}

			return color;
		}
	}


}
