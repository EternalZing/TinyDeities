using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDanmaku : MonoBehaviour{
    public GameObject onwer;
    public Vector2 direction;

    public float lifeTime;

    public float damage;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Suicide(lifeTime));
    }

    private IEnumerator Suicide(float time){
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }

    public void TriggerDamage(){
        Destroy(gameObject);
    }
    public void Fire(Vector2 dir,float speed){
        GetComponent<Rigidbody2D>().velocity = dir * speed;
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
