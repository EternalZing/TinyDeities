using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour{
    private Dictionary<MissionToDo, bool> _missionExec = new Dictionary<MissionToDo, bool>();
    public delegate void MissionToDo();

    public bool active = true;
    public static Mission Instance;
    IEnumerator MissionEnd(MissionToDo missionToDo,float time){
        if (_missionExec.ContainsKey(missionToDo)){
            bool res = _missionExec[missionToDo];
            if (res) yield break;
            else
                _missionExec[missionToDo] = true;
        }
        else{
            _missionExec.Add(missionToDo,true);
        }
    
        yield return new WaitForSeconds(time);
        if(active==false)
            yield break;
        missionToDo();
        _missionExec[missionToDo] = false;
    }
    
    IEnumerator MissionCoolDown(MissionToDo missionToDo,float time){
        if (_missionExec.ContainsKey(missionToDo)){
            bool res = _missionExec[missionToDo];
            if (res) yield break;
            else
                _missionExec[missionToDo] = true;
        }
        else{
            _missionExec.Add(missionToDo,true);
        }
        missionToDo();
        yield return new WaitForSeconds(time);
        if(active==false)
            yield break;
        _missionExec[missionToDo] = false;
    }
    public void MissionStart(MissionToDo missionToDo,float delayedTime){
        StartCoroutine(MissionEnd(missionToDo, delayedTime));
    }

    public void CoolDownForMission(MissionToDo missionToDo, float cdTime){
        StartCoroutine(MissionEnd(missionToDo, 0));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static Mission GetMission(GameObject gameObject){
        if (gameObject.GetComponent<Mission>()!=null){
            return gameObject.GetComponent<Mission>();
        }
        return gameObject.AddComponent<Mission>();
    }

    public static Mission GetMission(){
        if (Camera.main != null)
            return Instance == null ? Instance = Camera.main.gameObject.AddComponent<Mission>() : Instance;
        Debug.LogError("Cant find game object that contains global mission");
        return null;
    }

}
