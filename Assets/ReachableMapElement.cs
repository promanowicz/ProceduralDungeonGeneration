using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReachableMapElement{
    private List<GameObject> roomGameObjects = new List<GameObject>();
    private List<bool> isCorrespondingGameObjectFree = new List<bool>();

    public void addGameObject(GameObject roompart){
        roomGameObjects.Add(roompart);
        isCorrespondingGameObjectFree.Add(false);
    }

    public void instantiateInRandomRoomsPart(GameObject obj, float yPosFactor){
        Vector3 randObject = getRandomVector3(yPosFactor);
        randObject.y += yPosFactor;
        obj.transform.position = randObject;
    }

    public Vector3 getRandomVector3(float yPosFactor){
        Vector3 vec= roomGameObjects[Random.Range(0, roomGameObjects.Count - 1)].transform.position;
        vec.y = yPosFactor;
        return vec;
    }
}
