using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraControler : MonoBehaviour{
    private Camera mCamera;
    public Text isCameraOnText;
    public String textDscr = "Is Camera Moving: ";
    public float cameraMoveMargin = 0.1f;
    public float maxCameraSpeed = 0.5f;
    private float screenWidth;
    private float screenHeight;
    public bool isMovementEnabled = true;

    void Start(){
        mCamera = GetComponent<Camera>();
        screenWidth = Screen.width * cameraMoveMargin;
        screenHeight = Screen.height * cameraMoveMargin;
        updateCameraText();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            isMovementEnabled = !isMovementEnabled;
            isCameraOnText.text = textDscr + isMovementEnabled;
            updateCameraText();
        }
        if (isMovementEnabled){
            Vector3 mPos = Input.mousePosition;
            Vector3 cameraPos = transform.position;
            float flowSpeed = 1f;
            if (mPos.x < screenWidth){
                flowSpeed = (screenWidth - mPos.x) / 100;
                cameraPos.x -= flowSpeed * maxCameraSpeed;
            }
            if (mPos.x > Screen.width - screenWidth){
                float w = Screen.width;
                w = w - (w - screenWidth);
                float progress = mPos.x - (Screen.width - screenWidth);
                flowSpeed = progress / w;
                cameraPos.x += flowSpeed * maxCameraSpeed;
            }
            if (mPos.y < screenHeight){
                flowSpeed = (screenHeight - mPos.y) / 100;
                cameraPos.z -= flowSpeed * maxCameraSpeed;
            }
            if (mPos.y > Screen.height - screenHeight){
                cameraPos.z += flowSpeed * maxCameraSpeed;
            }
            transform.position = cameraPos;
        }
    }

    private void updateCameraText(){
        isCameraOnText.text = ( textDscr + isMovementEnabled);
    }

    void p(object msg){
        Debug.Log(msg);
    }
}
