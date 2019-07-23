using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMove : MonoBehaviour{
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    private Character _character;

    // Start is called before the first frame update
    private void Awake(){
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _character = GetComponent<Character>();
    }
    void Update(){
        #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Vector2 dir = new Vector2 (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        if (Math.Abs(dir.magnitude) > 0){
            _rigidbody2D.velocity = dir.normalized* _character.State.MovingSpeed;
        }
        else{
            _rigidbody2D.velocity = Vector2.zero;
        }

        if (Input.GetMouseButton(1)){
            dir = _camera.ScreenToWorldPoint(Input.mousePosition)-transform.position;
            _rigidbody2D.velocity = dir.normalized*_character.State.MovingSpeed;
         
        }
       // transform.rotation = Quaternion.LookRotation(Vector3.forward,dir);
        #endif
    }
}
