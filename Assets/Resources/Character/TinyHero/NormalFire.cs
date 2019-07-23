using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Serialization;

public class NormalFire : MonoBehaviour{
    private bool _coolDown;
    private Camera _camera;
    private Character _character;
    private Character _character1;

    // Start is called before the first frame update
    private void Awake(){
        _camera = Camera.main;
        _character = GetComponent<Character>();
    }

    void Start(){
        _character1 = GetComponent<Character>();
    }


    
    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButton(0)){
            Vector2 target = _camera.ScreenToWorldPoint(Input.mousePosition);
            _character1.FireDanmakuAt(transform.position+Vector3.up);
        }
        if (Input.GetKey(KeyCode.J)){
            _character1.FireDanmakuAt(transform.position+Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            GetComponent<SkillCaster>().Trigger(1);
        }
    }
}
