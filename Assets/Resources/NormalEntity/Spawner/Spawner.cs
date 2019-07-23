using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;

public class Spawner : MonoBehaviour{
    public List<string> spawnerTypes;
    public float duration;
    public int limit;
    public SpawnerManager spawnerManager;
    private Mission _spawnMission;
    private int _currentSpawnedNumber = 0;
    // Start is called before the first frame update
    void Start(){
        _spawnMission = gameObject.AddComponent<Mission>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentSpawnedNumber < limit || limit==-1){
            _spawnMission.MissionStart((() => {
                int nums = UnityEngine.Random.Range(0,spawnerTypes.Count);
                _currentSpawnedNumber++;
                spawnerManager.SpawnAt(spawnerTypes[nums],this.gameObject);
            }),duration);
        }
    }
}
