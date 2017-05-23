

using UnityEngine;

public class TrapController: MonoBehaviour{
    public ParticleSystem particles;
    private bool isFired = false;
    private Renderer mRenderer;
    private float burnOutFactor = 0.01f;
    private void Start(){
        mRenderer=GetComponent<Renderer>();
    }

    private float particleStartTime = 0;
    private void OnCollisionEnter(Collision other){
        if (!isFired && other.gameObject.tag == "Player"){
            particles.Play();
            isFired = true;
            particleStartTime = Time.time;
        }
    }

    private void Update(){
        if (isFired && Time.time > particleStartTime + particles.main.duration){
            particles.Stop();
        }
        if (isFired && !mRenderer.material.color.Equals(Color.black)){
            Color currColor = mRenderer.material.color;
            currColor.a += burnOutFactor;
            currColor.r -= burnOutFactor;
            currColor.g -= burnOutFactor;
            currColor.b -= burnOutFactor;
            mRenderer.material.color = currColor;
        }
    }
}
