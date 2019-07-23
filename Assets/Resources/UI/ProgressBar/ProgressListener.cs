using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProgressListener : MonoBehaviour{
    public enum ProgressType{
        Approaching,
        Instant
    }

    public enum ShowType{
        Triggered,
        Always
    }

    public Image image;
    private bool _isTargetNotNull;

    [HideInInspector] public float progressListened;

    [HideInInspector] public string progressName = "identified";

    public ProgressType progressType;
    public ShowType showType;
    public ScriptableObject target;

    private void Awake(){
        _isTargetNotNull = target != null;
        if(image==null)
            image = GetComponent<Image>();
    }

    public float approachingTime;
    public float triggeredTimeLeft;
    private float GetValue(){
        if (_isTargetNotNull){
            var targetType = target.GetType();
            var propertyInfo = targetType.GetProperty(progressName);
            if (propertyInfo != null) return (float) propertyInfo.GetValue(target);
        }

        return 0;
    }

    public IEnumerator Approaching(float v,float time){
        if (_approaching) yield break;
        _approaching = true;
        float begin = progressListened;
        float currentTime = 0;
        while (currentTime < time){
            progressListened = Mathf.Lerp(begin, v, currentTime/time);
            currentTime += Time.deltaTime;
            yield return  new WaitForEndOfFrame();
        }

        progressListened = v;
        _approaching = false;
        yield return null;
    }

    public IEnumerator ApproachingAlpha(float v,float time){
        if (_approaching) yield break;
        _approaching = true;
        var color = image.color;
        float begin = color.a;
        float currentTime = 0;
        while (currentTime < time){
            color.a = Mathf.Lerp(begin, v, currentTime/time);
            image.color = color;
            currentTime += Time.deltaTime;
            yield return  new WaitForEndOfFrame();
        }
        color.a = v;
        image.color = color;
        _approaching = false;
        yield return null;
    }
    private bool _approaching = false;
    private void Update(){
        var valueTemp = GetValue();
        switch (progressType){
            case ProgressType.Approaching:{
                if (Mathf.Abs(progressListened - valueTemp )> 0.01f){
                    StartCoroutine(Approaching(valueTemp, approachingTime));
                }
                break;
            }

            case ProgressType.Instant:{
                progressListened = valueTemp;
                break;
            }
        }
        switch (showType){
            case ShowType.Always:{
                break;
            }
            case ShowType.Triggered:{
                if (progressListened - valueTemp > 0.01f){
                    var color = image.color;
                    color.a = 255;
                    image.color = color;
                }
                else{
                    StartCoroutine(ApproachingAlpha(0,1f));
                }
                break;
            }
        }
        image.fillAmount = progressListened;
    }
}