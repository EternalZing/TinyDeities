using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "SceneLoader")]
public class SceneMaster : ScriptableObject{
    public static int GameStartMenu = 0;
    private static int SceneLoaderScene = 1;
    private static int SceneTest = 2;
    private AsyncOperation _loadingAsync;

    public float LoadingProgress{
        get{
            if (_loadingAsync != null) return _loadingAsync.progress;
            else return 0;
        }
    }

    private static SceneMaster _singletonSceneMaster;
    public static SceneMaster Singleton{
        get{
            if (_singletonSceneMaster == null)
                _singletonSceneMaster = Resources.Load<SceneMaster>("tools//SceneLoader");
            return _singletonSceneMaster;
        }
    }

    public  int currentLoadingTarget = 0;
    // Start is called before the first frame update
    public void NewBeginning(){
        ToSceneBySceneLoader(SceneTest);
    }
    public void ToSceneBySceneLoader(int sceneId){
        SceneManager.LoadScene(SceneLoaderScene);
        _loadingAsync = SceneManager.LoadSceneAsync(sceneId);
        _loadingAsync.completed += operation => { Debug.Log("Load Complete"); };
    }

    public float CalculateProgress(){
        return LoadingProgress;
    }
    public static void Create(){
        
    }
}
