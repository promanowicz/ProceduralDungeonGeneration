using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour{

    public Text lastScoreText;
	// Use this for initialization
	void Start (){
	    lastScoreText.text = "Last score: " + Utils.getLastScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame(){
        SceneManager.LoadScene(1);
    }

    public void OnClick(){}

}
