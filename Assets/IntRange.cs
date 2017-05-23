using System;

[Serializable]
public class IntRange {
	public int minVal;
	public int maxVal;

	public IntRange(int min, int max){
		minVal = min;
		maxVal = max;
	}

	public int Random()
	{
		return UnityEngine.Random.Range (minVal, maxVal);
	}

}
