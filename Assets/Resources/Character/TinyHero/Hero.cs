using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero :Character{   
    private delegate void FireFeatDelegate();
    private FireFeatDelegate _fireFeatDelegate;
    public FeatTree featTree;
    private void ReadFeat(){
        Type t = GetType();
        var methodList = t.GetMethods();
        foreach (var method in methodList){
            bool res = method.IsDefined(typeof(FireFeatFunctionAttribute), false);
            if (res){
                var fireFeatFunctionAttributes = method.GetCustomAttributes(typeof(FireFeatFunctionAttribute), true);
                foreach (var fireFeatFunctionAttribute in fireFeatFunctionAttributes){
                    FireFeatFunctionAttribute featFunctionAttribute = (fireFeatFunctionAttribute) as FireFeatFunctionAttribute;
                    string attrName = featFunctionAttribute.name;
                    _fireFeatDelegate += method.CreateDelegate(typeof(FireFeatDelegate),this) as FireFeatDelegate;
                }
            }
        }
    }
   
    // Start is called before the first frame update
    new void  Start(){
        base.Start();
        var objects = GameObject.FindGameObjectsWithTag("HeroState");
        foreach (var ui in objects){
            ui.GetComponent<ProgressListener>().target = State;
        }
        featTree.Load();
        ReadFeat();
    }
    public override  void FireDanmakuAt(Vector2 pos){
        if (cd) return;
        base.FireDanmakuAt(pos);
        _fireFeatDelegate?.Invoke();
        Debug.Log(_fireFeatDelegate);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
