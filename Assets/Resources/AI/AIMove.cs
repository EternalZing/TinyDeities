using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour{
    public float movingSpeed;

    private float _deltaTime;
    // Start is called before the first frame update
    void Start(){
        _rigidbody2D = this.GetComponent<Rigidbody2D>();
        _deltaTime = 0;
    }
    public enum MovingType{
        Toward,
        Random,
        Downward,
        Upward,
        Horizontal,
        HorizontalApproaching,
        FromConfig

    }

    private static float timeScale = 3f;
    public MovingType movingType;

    private Rigidbody2D _rigidbody2D;

    // Update is called once per frame
    void Update(){
        _deltaTime += Time.deltaTime;
        switch (movingType){
            case MovingType.Downward:{
                if(_rigidbody2D.velocity.magnitude<0.1)
                    _rigidbody2D.velocity = movingSpeed* Vector2.down;
                break;
            }
            case MovingType.Horizontal:{
                _rigidbody2D.velocity = movingSpeed*Vector3.Lerp(Vector3.left, Vector3.right, Mathf.PingPong(_deltaTime/timeScale,1));
                break;
            }
            case MovingType.HorizontalApproaching:{
                Vector2  vel = Vector3.Lerp(Vector3.left, Vector3.right, Mathf.PingPong(_deltaTime/timeScale*2,1))*2f;
                vel += Vector2.down;
                _rigidbody2D.velocity = vel.normalized * movingSpeed;
                break;
            }

        }
    }
}
