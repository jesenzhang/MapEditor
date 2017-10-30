using UnityEngine;
using UnityEditor;
using System.Collections;


public class SceneEditor : MonoBehaviour {

	[MenuItem("SceneEditor/设置为道具",false,1)]
	static void SetItem(){
		var go = Selection.activeGameObject;
		if (go == null) {
			Debug.LogError ("请选择物体");
			return;
		}

		var itemRoot = MapEventHelper.FindItemRoot ();

		if (go.transform.parent == null || go.transform.parent.gameObject != itemRoot) {
			Debug.LogError ("请选择shiqu对象中的道具");
			return;
		} 

		var item = go.GetComponent<SceneItem> ();
		if (item == null) {
			int itemId = 0;
			foreach(var sceneItem in itemRoot.GetComponentsInChildren<SceneItem> ()){				
				itemId = Mathf.Max (itemId, int.Parse(sceneItem.itemId));
			}

			item = go.AddComponent<SceneItem> ();
			item.itemId = (itemId + 1).ToString();
		}
	}

	[MenuItem("SceneEditor/取消道具",false,2)]
	static void CancelItem(){
		var go = Selection.activeGameObject;
		if (go == null) {
			Debug.LogError ("请选择物体");
			return;
		}
		var item = go.GetComponent<SceneItem> ();
		if (item != null) {
			Object.DestroyImmediate (item);
		}
	}

	/*[MenuItem("SceneEditor/设置高亮",false,11)]
	static void SetHighlight(){
		var go = Selection.activeGameObject;
		if (go == null) {
			Debug.LogError ("请选择物体");
			return;
		}
		ScriptableWizard.DisplayWizard<HighlightWizard> ("设置高亮", "确定"); 
	}*/

	/*[MenuItem("SceneEditor/取消高亮",false,12)]
	static void CancelHighlight(){
		var go = Selection.activeGameObject;
		if (go == null) {
			Debug.LogError ("请选择物体");
			return;
		}
		var controller = go.GetComponent<FlashingController> ();
		var ho = go.GetComponent<HighlightableObject> ();

		if (controller != null) {
			Object.DestroyImmediate (controller);
		}
		if (ho != null) {
			Object.DestroyImmediate (ho);
		}


	}*/
}
