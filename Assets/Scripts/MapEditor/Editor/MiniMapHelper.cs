using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;

public class MiniMapHelper  {

	private static string EXPORT_DIR = null;

	public static void GenerateMiniMap(){
		var go = GameObject.Find ("MiniMap");
		if (go == null) {
			ScriptableWizard.DisplayWizard<MiniMapCreateWizard> ("创建小地图", "创建"); 
		} else {
			MiniMap miniMap = go.GetComponent<MiniMap> ();
			GenerateMiniMap (miniMap.mapId);
		}
	}

	public static void ExportBundle(){
		var go =  Selection.activeObject as GameObject;
		MiniMap miniMap = go.GetComponent<MiniMap> (); 

		string filename = "minimap_"+miniMap.mapId;
		string prefabPath = "Assets/Res/MiniMap/" + filename + ".prefab";

		BuildBundle (prefabPath);
	}

	public static void GenerateMiniMap(string mapID){ 
		
		var miniMap = GetOrCreateMiniMap (mapID);

		string filename = "minimap_"+miniMap.mapId;
		string prefabPath = "Assets/Res/MiniMap/" + filename + ".prefab";
		string spritePath = "Assets/Res/MiniMap/" + filename + ".png"; 

		CaptureSceneView (spritePath,miniMap);

		CreatePrefab(prefabPath,miniMap,spritePath);

		BuildBundle (prefabPath); 
	}

	private static MiniMap GetOrCreateMiniMap(string mapID){
		var go = GameObject.Find ("MiniMap");
		if (go == null) {
			go = new GameObject (); 
			go.name = "MiniMap";
		}

		MiniMap miniMap = go.GetComponent<MiniMap> ();
		if (miniMap == null) { 
			miniMap = go.AddComponent<MiniMap> (); 
			miniMap.mapId = mapID;
		} 

		if (EXPORT_DIR == null) {
			string exportDir = Application.dataPath+"/Res/MiniMap/";
			if (!Directory.Exists (exportDir)) {
				Directory.CreateDirectory (exportDir);
			}
			EXPORT_DIR = exportDir;
		}

		return miniMap;
	}

	private static void CaptureSceneView(string spritePath,MiniMap miniMap){

		var go = GameObject.Find ("MiniMap"); 

		Camera camera = go.GetComponent<Camera> ();;
		if (camera == null) { 
			camera = go.AddComponent<Camera> (); 
		}


		/* var bounds = MapManager.terrianBounds;

		miniMap.center = bounds.center;
		miniMap.size = bounds.size; 

		camera.transform.position = new Vector3 (bounds.center.x,bounds.max.y + 10,bounds.center.z);
		camera.transform.rotation = Quaternion.LookRotation (Vector3.down,Vector3.forward);
		camera.orthographic = true; 
		camera.orthographicSize = Mathf.Max(bounds.size.z / 2,bounds.size.z/2);  
		camera.cullingMask = ~ (1 << LayerMask.NameToLayer ("Sky"));
		camera.clearFlags = CameraClearFlags.SolidColor;
		camera.rect = new Rect (0,0, 1, 1);

		RenderTexture rt = new RenderTexture ((int)( 2 * bounds.size.x),(int)(2 * bounds.size.z ),16);
		*/

		var sceneView = SceneView.lastActiveSceneView;  

		miniMap.position = sceneView.camera.transform.position;
		miniMap.direction = sceneView.camera.transform.rotation.eulerAngles;

		camera.CopyFrom (sceneView.camera); 
		RenderTexture rt = new RenderTexture ((int)sceneView.camera.pixelWidth,(int)sceneView.camera.pixelHeight,32);

		camera.targetTexture = rt;

		camera.Render ();
		camera.targetTexture = null; 

		var texture = SaveRendererTexture (rt,spritePath);

		AssetDatabase.Refresh ();

		var importer = AssetImporter.GetAtPath (spritePath) as TextureImporter;
		importer.textureType = TextureImporterType.Sprite;
		importer.mipmapEnabled = false; 
		importer.textureFormat = TextureImporterFormat.ARGB16;
		importer.SaveAndReimport ();	

		Object.DestroyImmediate (rt);  
	}



	private static Texture2D SaveRendererTexture(RenderTexture rendererTexture,string path){
		var rt = RenderTexture.active;
		RenderTexture.active = rendererTexture;
		var texture = new Texture2D (rendererTexture.width, rendererTexture.height, TextureFormat.ARGB32,false); 
		texture.ReadPixels (new Rect (0, 0, rendererTexture.width, rendererTexture.height), 0, 0);
		texture.Apply ();
		var data = texture.EncodeToPNG ();
		File.WriteAllBytes (path, data);
		RenderTexture.active = rt;

		return texture;
	}

	private static void CreatePrefab(string path,MiniMap sourceMap,string spritePath){ 
		GameObject go = new GameObject ();
		var minimap = go.AddComponent<MiniMap> ();
		minimap.position = sourceMap.position;
		minimap.direction = sourceMap.direction;
		minimap.mapId = sourceMap.mapId; 

		var camera = go.AddComponent<Camera> (); 
		camera.CopyFrom (sourceMap.GetComponent<Camera>());
		camera.cullingMask = 0;
		camera.clearFlags = CameraClearFlags.Nothing;

		var renderer = go.AddComponent<SpriteRenderer> ();
		var sprite = AssetDatabase.LoadAssetAtPath<Sprite> (spritePath);
		renderer.sprite = sprite; 
		 
		PrefabUtility.CreatePrefab (path, go);

		GameObject.DestroyImmediate (go);
	}

	private static void BuildBundle(string path){
		string exportDir = Application.dataPath+"/../Export/Res/MiniMap/";

		if (!Directory.Exists (exportDir)) {
			Directory.CreateDirectory (exportDir);
		}

		var asset = AssetDatabase.LoadAssetAtPath<MiniMap> (path);
		BuildPipeline.BuildAssetBundle (asset, null, exportDir + Path.ChangeExtension(Path.GetFileName(path),""), BuildAssetBundleOptions.CollectDependencies, EditorUserBuildSettings.activeBuildTarget);
	}
}
