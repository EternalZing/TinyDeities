using System;
using System.Collections;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class FeatInfo : SerializableDictionaryBase<string,string>{
}

//这是一个天赋.
//这里存储断天赋的基本属性.天赋真正的效用由角色本身判断.
[CreateAssetMenu(menuName = "Feat/Feat")]
public class Feat : ScriptableObject{
    public string featName;
    public string description;
    public Sprite featIcon;
    public string FeatsName{
        get => featName;
        set => featName = value;
    }
    public string Description{
        get => description;
        set => description = value;
    }
    [SerializeField]
    protected int featLevel;

    public int FeatLevel{
        get => featLevel;
        set{
            levelUpApplied.Invoke();
            featLevel = value;
        }
    }

    public int featMaxLevel;
    public List<Feat> requiredFeats;
    public FeatInfo featsInfo;
    public UnityEvent levelUpApplied;
    public bool Available{
        get{
            if (requiredFeats == null || requiredFeats.Count <= 0 ){
                return true;
            }
            foreach (var feat in requiredFeats){
                if (!(feat.featLevel > 0)){
                    return false;
                }
            }
            return true;
        }
    }
}
