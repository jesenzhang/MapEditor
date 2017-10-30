using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using System.IO;

public class MapAreaHelper {

	static Bounds mapBounds;


	[MenuItem("MapEditor/工具/清理场景区域配置")]
	static void ClearSceneConfguration(){
		var meshObjects = GameObject.FindObjectsOfType<MeshRenderer> ();
		foreach (var meshRenderer in meshObjects) { 
			var sceneExt = meshRenderer.GetComponent<SceneExt> ();
			if (sceneExt != null) {
				Object.DestroyImmediate (sceneExt);
			}
		}

	}

	[MenuItem("MapEditor/工具/生成场景区域配置")]
	static void GenerateSceneConfguration(){
		Vector3 min = new Vector3(float.MaxValue,float.MaxValue,float.MaxValue);
		Vector3 max = new Vector3(float.MinValue,float.MinValue,float.MinValue);

		Debug.LogFormat ("GenerateSceneBound");

		var settings = GameObject.FindObjectOfType<MapAreaSettings> ();

		GenerateSceneConfguration (settings,"Dynamic/Low/Surface",ref min,ref max);
		GenerateSceneConfguration (settings,"Solid/Surface",ref min,ref max);
		GenerateSceneConfguration (settings,"Solid/Other/EasyWater",ref min,ref max);

		Debug.LogFormat ("Bounds {0},{1},Center {2}",min,max,(min+max)/2); 

		mapBounds =  new Bounds ((min+max)/2, max - min); 

		var map = MapEventHelper.FindAreaRoot ();
		var box = map.GetComponent<BoxCollider> ();
		if (box == null) {
			box = map.AddComponent<BoxCollider> ();
		}
		box.center = mapBounds.center;
		box.size = mapBounds.size;
	}

	private static void GenerateSceneConfguration(MapAreaSettings settings,string path,ref Vector3 min,ref Vector3 max){
		var meshObjects = GameObject.Find(path).GetComponentsInChildren<MeshRenderer> (); 
		foreach (var meshRenderer in meshObjects) {
			if (meshRenderer.gameObject.layer != LayerMask.NameToLayer ("Surface")) {
				continue;
			}	
			var sceneExt = meshRenderer.GetComponent<SceneExt> ();
			if (sceneExt == null) {
				sceneExt = meshRenderer.gameObject.AddComponent<SceneExt> (); 
			}
			sceneExt.areaType = GetAreaType (settings,meshRenderer);
			if (meshRenderer.bounds.min.x < min.x) {
				min.x = meshRenderer.bounds.min.x;
			}
			if (meshRenderer.bounds.min.y < min.y) {
				min.y = meshRenderer.bounds.min.y;
			}
			if (meshRenderer.bounds.min.z < min.z) {
				min.z = meshRenderer.bounds.min.z;
			}

			if (meshRenderer.bounds.max.x > max.x) {
				max.x = meshRenderer.bounds.max.x;
			}
			if (meshRenderer.bounds.max.y > max.y) {
				max.y = meshRenderer.bounds.max.y;
			}
			if (meshRenderer.bounds.max.z > max.z) {
				max.z = meshRenderer.bounds.max.z;
			}
		}
	}

	[MenuItem("MapEditor/工具/生成场景区域")]
	static void GenerateSceneAreas(){

		if (mapBounds.size.sqrMagnitude < 0.1f) {
			GenerateSceneConfguration ();
		}

		var settings = GameObject.FindObjectOfType<MapAreaSettings> ();

		float resolution = settings.resolution;
		int width = Mathf.CeilToInt (mapBounds.size.x * resolution);
		int height = Mathf.CeilToInt (mapBounds.size.z * resolution);
		var texture = new Texture2D (width,height,TextureFormat.ARGB4444,false);

		Example.MapTile[,] tiles = new Example.MapTile[height,width]; 

		EditorUtility.DisplayProgressBar ("GenerateSceneAreas", "", 0);

		float total = (width * height);
		var upper = mapBounds.max + Vector3.up;
		var lower = mapBounds.min;
		RaycastHit hit;
		Color color;
		for (int y = 0; y < height; ++y) {
			for (int x = 0; x < width; ++x) {
				upper.x = x / resolution + mapBounds.center.x - mapBounds.size.x/2;
				upper.z = y / resolution + mapBounds.center.z - mapBounds.size.z/2;
				if (Physics.Raycast (upper, Vector3.down, out hit,1000,LayerMask.GetMask("Surface"))) {
					var sceneExt = hit.collider.GetComponent<SceneExt> (); 
					if (sceneExt != null) {
						var areaType = sceneExt.areaType;
						var t4m = hit.collider.gameObject.GetComponent ("T4MObjSC");
						if (t4m != null) {
							areaType = GetT4MAreaColor (settings, sceneExt, hit.textureCoord); 
						}
						color = GetAreaColor (areaType); 
						texture.SetPixel (x, y, color);

						var tile = new Example.MapTile();
						tile.Type = (Example.MapArea.Type)areaType;
						tiles [y,x] = tile;
					} else {
						texture.SetPixel (x, y, Color.clear); 
					}

				} else {
					texture.SetPixel (x, y, Color.clear); 
				}
			}
			EditorUtility.DisplayProgressBar ("GenerateSceneAreas", "line "+(y+1)+"/"+height, (float)y/height);
		}

		EditorUtility.ClearProgressBar ();

		int sceneID = MapManager.current.sceneId;

		texture.Apply ();
		var data = texture.EncodeToPNG ();
		File.WriteAllBytes (MapUtil.DataPath() + "MapArea_"+sceneID+".png", data);

		ExportMapArea (MapUtil.DataPath() + "MapArea_"+sceneID+".bytes",tiles,settings,width,height);
	
	} 

	static MapAreaType GetT4MAreaColor(MapAreaSettings setting, SceneExt sceneExt,Vector2 uv){
		MapAreaType areaType = sceneExt.areaType;
		var material = sceneExt.GetComponent<MeshRenderer> ().sharedMaterial;
		var controlTex = material.GetTexture ("_Control") as Texture2D; 

		var px = controlTex.width * uv.x;
		var py = controlTex.height * uv.y;
		var color = controlTex.GetPixel(Mathf.RoundToInt(px),Mathf.RoundToInt(py));
		 
		var maxChannel = Mathf.Max (color.r, Mathf.Max (color.g, Mathf.Max (color.b, color.a)));

		/*if (color.r >= maxChannel) {
			areaType = MapAreaType.ROCK;
		} else if (color.g >= maxChannel) {
			areaType = MapAreaType.EARTH;
		} else if (color.b >= maxChannel) {
			areaType = MapAreaType.GRASS;
		} else {
			areaType = MapAreaType.GRASS;
		}*/

		if (color.r >= maxChannel) {
			areaType = MapAreaType.ROCK;
		} else if (color.b >= maxChannel) {
			areaType = MapAreaType.GRASS;
		} else if (color.g >= maxChannel) {
			areaType = MapAreaType.EARTH;
		}  else {
			areaType = MapAreaType.GRASS;
		}

		return areaType;
	}

	static MapAreaType GetAreaType(MapAreaSettings setting, MeshRenderer meshRender){
		MapAreaType areaType = MapAreaType.EARTH;
		var t4m = meshRender.gameObject.GetComponent ("T4MObjSC");
		if (t4m != null) {
			var controlTex = meshRender.sharedMaterial.GetTexture ("_Control");
			areaType = MapAreaType.GRASS;
		} else if(meshRender.sharedMaterial.name.Contains("Water")){
			areaType = MapAreaType.WATER;
		}else {
			Dictionary<MapAreaType,int> areaTypes = new Dictionary<MapAreaType,int> ();
			foreach (var mat in meshRender.sharedMaterials) {
				var mainTex = mat.GetTexture ("_MainTex");
				if (mainTex != null) {
					areaType = GetAreaType (setting, mainTex);
					if (areaTypes.ContainsKey (areaType)) {
						areaTypes [areaType] = areaTypes [areaType] + 1;
					} else {
						areaTypes.Add(areaType,1);
					}
				}
			}
			int maxCount = 0;
			foreach (var t in areaTypes) {
				if (t.Value > maxCount) {
					areaType = t.Key;
				}
			}
		}
		return areaType;
	}

	static MapAreaType GetAreaType(MapAreaSettings setting,Texture mainTex){	
	
		//var texPath = AssetDatabase.GetAssetOrScenePath (mainTex);
		var texPath = mainTex.name;

		foreach (var name in setting.rockTexturePrefix) {
			if (texPath.Contains (name))
				return MapAreaType.ROCK;
		}

		foreach (var name in setting.grassTexturePrefix) {
			if (texPath.Contains (name))
				return MapAreaType.GRASS;
		}

		foreach (var name in setting.woodTexturePrefix) {
			if (texPath.Contains (name))
				return MapAreaType.WOOD;
		}

		foreach (var name in setting.waterTexturePrefix) {
			if (texPath.Contains (name))
				return MapAreaType.WATER;
		}

		foreach (var name in setting.earthTexturePrefix) {
			if (texPath.Contains (name))
				return MapAreaType.EARTH;
		}

		return MapAreaType.EARTH;
	}

	public static Color GetAreaColor(MapAreaType areaType){
		Color color = Color.black;
		switch (areaType) {
		case MapAreaType.EARTH:
			color = Color.black;
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

	static void ExportMapArea(string path,Example.MapTile[,] tiles,MapAreaSettings settings,int width,int height){

		var tileList = new List<int> ();
		Example.MapTile tile = null;

		for (int y = 0; y < height; ++y) {
			for (int x = 0; x < width; ++x) {
				tile = tiles [y, x];
				if (tile != null) {
					tileList.Add((int)tile.Type);
				} else {
					tileList.Add(-1);
				}

			}
		}

		/*for (int y = 0; y < height; ++y) {
			for (int x = 0; x < width; ++x) {
				Debug.LogFormat ("x={0},y={1}",x,y);
				tileList [y * height + x] = tiles [y, x];
			}
		}*/

		Example.MapTiles tileAll = new Example.MapTiles ();
		tileAll.CellSize = settings.resolution;
		tileAll.Row = height;
		tileAll.Column = width;
		tileAll.Pos = MapUtil.ToVector3f (mapBounds.min);
		tileAll.Tiles = tileList;

		File.WriteAllBytes(path,Example.MapTiles.SerializeToBytes(tileAll));
	}

}
