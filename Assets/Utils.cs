using System;
using UnityEngine;


public static class Utils
{
	public static Color getRandomColor(int randomSeed){
		UnityEngine.Random.State currState = UnityEngine.Random.state;
		UnityEngine.Random.InitState (randomSeed);
		Color colToReturn = UnityEngine.Random.ColorHSV();
		UnityEngine.Random.state = currState;
		return colToReturn;
	}

    public static void AddPoints(int added){
        int points = PlayerPrefs.GetInt("points",0);
        points += added;
        PlayerPrefs.SetInt("points",points);
        PlayerPrefs.Save();
    }

    public static int GetPoints(){
        return PlayerPrefs.GetInt("points",0);
    }
    public static void setScore(int added){
        int points = PlayerPrefs.GetInt("lastscore",0);
        points = added;
        PlayerPrefs.SetInt("lastscore",points);
        PlayerPrefs.Save();
    }

    public static int getLastScore(){
        return PlayerPrefs.GetInt("lastscore",0);
    }

}


