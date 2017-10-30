using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AnimationFrameAppendPolicy{
	NONE = 0,
	BEFORE = 1,
	AFTER = 2,
}

public class MapAnimationHelper { 
	

	public static Example.MapAnimation ExportMapAnimation(MapAnimation mapAnimation,AnimationFrameAppendPolicy appendPolicy){
		if (mapAnimation == null)
			return null;
		
		Example.MapAnimation anim = new Example.MapAnimation (); 
		List<Example.MapAnimationFrame> frames = new List<Example.MapAnimationFrame> ();
		foreach (var mapFrame in mapAnimation.GetComponentsInChildren<MapAnimationFrame>()) {
			var frame = ExportFrame (mapFrame);
			frames.Add (frame); 
		}

		/*frames.Sort (delegate(Example.MapAnimationFrame x, Example.MapAnimationFrame y) {
			return x.Time - y.Time;
		});*/

		if (frames.Count > 0) {
			var lastFrame = frames [frames.Count - 1];
			if (appendPolicy == AnimationFrameAppendPolicy.AFTER) {
				var frame = CreateFrame (mapAnimation.transform, 0, lastFrame.RotationType== Example.MapAnimationFrame.RelativeType.RELATIVE,lastFrame.Navigation);
				frames.Add (frame);
			}
		}

		anim.Id = mapAnimation.id;
		anim.Frames = frames;
		anim.Duration = mapAnimation.duration;
		return anim;
	}

	private static Example.MapAnimationFrame CreateFrame(Transform target,int time,bool relative,bool navigation){
		Example.MapAnimationFrame frame = new Example.MapAnimationFrame ();
		frame.Duration = time; 
		frame.Navigation = navigation;

		frame.PositionType = relative?Example.MapAnimationFrame.RelativeType.RELATIVE:Example.MapAnimationFrame.RelativeType.ABSOLUTE;
		frame.RotationType = relative?Example.MapAnimationFrame.RelativeType.RELATIVE:Example.MapAnimationFrame.RelativeType.ABSOLUTE;
		frame.ScaleType = relative?Example.MapAnimationFrame.RelativeType.RELATIVE:Example.MapAnimationFrame.RelativeType.ABSOLUTE;

		frame.Position =  MapUtil.ToVector3f (target.position - new Vector3(0,1,0));
		frame.Rotation =  MapUtil.ToVector3f (target.eulerAngles);
		frame.Scale =  MapUtil.ToVector3f (target.localScale);

		return frame;
	}

	private static Example.MapAnimationFrame ExportFrame(MapAnimationFrame mapFrame){
		Example.MapAnimationFrame frame = CreateFrame (mapFrame.transform,mapFrame.duration,mapFrame.relative,mapFrame.navigation);

		List<Example.AnimationFrameAction> actions = new List<Example.AnimationFrameAction> ();

		foreach (var frameAction in mapFrame.GetComponentsInChildren<MapAnimationAction>()) {
			var action =  new Example.AnimationFrameAction ();

			List<Example.AnimationFrameActionValue> args = new List<Example.AnimationFrameActionValue> ();
			foreach (var value in frameAction.Arguments) {
				Example.AnimationFrameActionValue arg = new Example.AnimationFrameActionValue ();
				arg.IntValue = value.intValue;
				arg.StrValue = value.strValue;
				arg.FloatValue = value.floatValue;
				arg.VectorValue = MapUtil.ToVector3f (value.posValue);
				args.Add (arg);
			}

			action.actionType = (Example.AnimationFrameAction.ActionType)frameAction.actionType;
			action.Args = args;
			actions.Add (action);
		}
		 
		frame.Actions = actions; 

		return frame;
	}


}
