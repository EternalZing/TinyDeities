using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerManager : MonoBehaviour{
    public Dictionary<string, List<GameObject>> spawnPool;
    public Dictionary<string, GameObject> spawnPoolTemplate = new Dictionary<string, GameObject>();
    [SerializeField] protected List<GameObject> spawnTemplate;
    [SerializeField] protected List<GameObject> spawnedEntity;
    public void SpawnAt(string name, GameObject spawningArea){
        if (spawnPoolTemplate.ContainsKey(name)){
            GameObject entity = spawnPoolTemplate[name];
            Instantiate(entity, spawningArea.transform);
        }
        else{
            Debug.LogError($"no such type of mob {name} in spawner manager");
        }
    }
    public void InitSpawn(){
        if (spawnTemplate != null){
            spawnPoolTemplate.Clear();
            foreach (var prefab in spawnTemplate){
                if (prefab != null){
                    spawnPoolTemplate.Add(prefab.name,prefab);
                    Debug.Log(prefab.name);
                }
                
            }
          
        }
    }
    private void Awake(){
        Debug.Log("Mobs info loaded");
      
    }
    void Start()
    {
        InitSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
