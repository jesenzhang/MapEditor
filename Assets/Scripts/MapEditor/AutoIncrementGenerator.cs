using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public interface IRecycle{
	int recycleId {
		get;
	}
}

public class AutoIncrementGenerator {	 

	private AutoIncrementGenerator( ){ 
	}
	 

	public static int Get<T>(List<T> usedValues) where T : IRecycle{ 
		usedValues.Sort (delegate(T x, T y) {
			return x.recycleId - y.recycleId;
		});
		int lastUsedId = 0;
		foreach (var usedId in usedValues) {
			if (usedId.recycleId - lastUsedId > 1) {
				return lastUsedId + 1;
			}
			lastUsedId = usedId.recycleId;
		}
		return lastUsedId + 1;
	}

	public static int Get<T>(T[] array) where T : IRecycle{ 
		List<T> usedValues = new List<T> ();
		usedValues.AddRange (array);
		usedValues.Sort (delegate(T x, T y) {
			return x.recycleId - y.recycleId;
		});
		int lastUsedId = 0;
		foreach (var usedId in usedValues) {
			if (usedId.recycleId - lastUsedId > 1) {
				return lastUsedId + 1;
			}
			lastUsedId = usedId.recycleId;
		}
		return lastUsedId + 1;
	}
	 
}
