using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFire : MonoBehaviour{
    private Mission _mission;

    private GameObject _player;
    // Start is called before the first frame update
    void Start(){
        _mission = gameObject.AddComponent<Mission>();
        _player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update(){
        _mission.MissionStart(() => { GetComponent<Character>().FireDanmakuAt(_player.transform.position);},0.1f);
    }
}
