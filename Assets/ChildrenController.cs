
    using UnityEngine;public class ChildrenController : MonoBehaviour{
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Player"){
            Utils.AddPoints(1000);
            Destroy(gameObject);
        }
    }
}
