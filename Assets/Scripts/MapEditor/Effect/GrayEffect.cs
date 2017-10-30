using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class GrayEffect : MonoBehaviour {
	private Material _material; 
	void OnRenderImage(RenderTexture source,RenderTexture dest){
		material.SetTexture ("_MainTex", source);
		Graphics.Blit (source, dest,material); 
	}

	public Material material{
		get { 
			if (_material == null) {
				_material = new Material (Shader.Find ("ImageEffect/GrayImageEffectShader"));
			}
			return _material;
		}
	}
}
