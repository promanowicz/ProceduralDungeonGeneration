using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour{
    private float hitPoints = 0;

    private Rigidbody mRigidbody;
    public
    // Use this for initialization
    void Start(){
        hitPoints = Random.Range(30, 100);
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
    }

    private int i = 0;

    private void OnParticleCollision(GameObject other){
        Debug.Log("curr hp " + hitPoints);
        hitPoints--;
        if (hitPoints <= 0){
            Utils.AddPoints(100);
            Destroy(gameObject);
        }
    }

    private const float coolDownTime = 4;
    private float collisionTime = 0;

    private void OnCollisionEnter(Collision other){
        GameObject obj = other.gameObject;
        if (obj.tag == "Player"){
            mRigidbody.velocity = Vector3.zero;
            transform.LookAt(obj.transform);
            mRigidbody.AddForce((transform.position - obj.transform.position).normalized * 50);
            collisionTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider other){
        GameObject obj = other.gameObject;
        if (obj.tag == "Player"){
            transform.LookAt(obj.transform);
            if (Time.time > collisionTime + coolDownTime){
                mRigidbody.velocity = (transform.position - obj.transform.position).normalized * -10;
            }
        }
    }
}
