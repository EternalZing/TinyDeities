using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[Serializable]
public class SkillInfo : SerializableDictionaryBase<string, string>{
};
[CreateAssetMenu(menuName = "Skill")]
public class Skill : ScriptableObject{
    public GameObject danmaku;
    public float baseDamage;
    public float basicSpeed;
    public float CD;
    protected SortedSet<TimelineUnit> timelineUnits = new SortedSet<TimelineUnit>();
    public List<TimelineUnit> timelineList;
    [Serializable]
    public class TimelineUnit:IComparable{
        protected UnitAdditionalInfo UnitAdditionalInfo;
        public float showingTime;
        public GameObject danmakuReference;
        public UnitType    unitType;
        public SkillInfo arguments;
        public string GetAdditionalInfo(AdditionalInfo additionalInfo){
            string key=  additionalInfo.ToString();
            if (arguments.ContainsKey(key)){
                return arguments[key];
            }
            else{
                Debug.LogError($"no such key in {this} called {key}");
            }
            return key;
        }

        public T GetAdditionalInfo<T>(AdditionalInfo additionalInfo){
            string value = GetAdditionalInfo(additionalInfo);
            object result = 0;
            if (typeof(T) == typeof(int)){
                result =  int.Parse(value);
            }

            if (typeof(T) == typeof(float)){
                result = float.Parse(value);
            }

            if (typeof(T) == typeof(string)){
                result = value;
            }

            return (T) result;
        }
        public enum UnitType{
            Flowering,
            Single,
            Sector,
            Beam,
            BeamingDanmaku
        }
        public int CompareTo(object other){
            TimelineUnit timeLineOther =other as TimelineUnit;
            if (timeLineOther != null) return (int) ((showingTime - timeLineOther.showingTime) * 100);
            return 0;
        }
        public enum AdditionalInfo{
            SpeedModifier,
            DamageModifier,
            FloweringNumbers,
            FloweringTimes,
            FloweringInterval
        }
    }

    private IEnumerator _currentTimelineUnit;
    public void Reset(){
        _currentTimelineUnit = timelineUnits.GetEnumerator();
    }
    public IEnumerator NextTimeLineUnit(){
        while (_currentTimelineUnit.MoveNext()){
            yield return _currentTimelineUnit;
        }
    }
    public void LoadForPlayer(Character character){
        timelineUnits.UnionWith(timelineList);
        baseDamage = character.State.BaseDamage;
        basicSpeed = character.State.DanmakuSpeed;
        danmaku = character.State.defaultDanmaku;
    }
    [Serializable]
    public class UnitAdditionalInfo{
        public float speedModifier;
    }
}

