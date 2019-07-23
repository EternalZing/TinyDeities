using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anu : Hero{
 
    [FireFeatFunction(name = "multi_shot")]
    public void MultiShotApplied(){
       Feat feat =  featTree.GetFeatByName("multi_shot");
       Vector3 gap = 0.1f * Vector3.left;
       if (feat.FeatLevel <= 0) return;
       for (int i=0;i<feat.FeatLevel;i++){
           Vector2 origin = transform.position + ((i % 2 == 0) ? (i + 2.0f) / 2.0f * gap : -(i + 1.0f) / 2.0f * gap);
           Fire(origin,origin+Vector2.up);
       }
    }

    [FireFeatFunction(name = "killing_burst")]
    public void KillingBurstApplied(Character characterKilled){
        
    }
}

public class FeatFunctionAttribute : Attribute{
    public string name;
    
}

public class OnKillingAttribute : FeatFunctionAttribute{
    
    
    
}

public class FireFeatFunctionAttribute : FeatFunctionAttribute{
    
}