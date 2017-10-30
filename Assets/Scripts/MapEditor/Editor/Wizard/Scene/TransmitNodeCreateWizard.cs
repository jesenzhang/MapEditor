using UnityEngine;
using UnityEditor;
using System.Collections;

public class TransmitNodeCreateWizard : ScriptableWizard {  
	public bool isBorn = false;
	public int jumpToMap = -1;
	public int jumpToNode = -1;

	private Vector3 nodeSize = new Vector3(3,3,3);

	void OnWizardUpdate(){
		isValid = true;
		errorString = "";

		if (jumpToMap == 0) {
			errorString = "请输入跳转到的地图ID";
			isValid = false;
		}else if (jumpToNode == 0) {
			errorString = "请输入跳转到的跳转点ID";
			isValid = false;
		} 
	}

	void OnWizardCreate(){
		if (isBorn) {
			MapManager.CreateTransmitNode (MapManager.current,0,jumpToMap,jumpToNode,nodeSize);
		} else {
			MapManager.CreateTransmitNode (MapManager.current,jumpToMap,jumpToNode,nodeSize);
		}

	}
}
