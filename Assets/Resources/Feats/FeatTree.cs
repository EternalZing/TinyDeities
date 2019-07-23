using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

//天赋结底用来定位天赋
[Serializable]
public class FeatNode{
    public Feat feat;
    public Vector2 position;
}
//这是一个天赋树,里面存放着所有的顶级天赋节点.
[CreateAssetMenu(menuName = "Feat/FeatTree")]
public class FeatTree : ScriptableObject{
    protected Dictionary<string, FeatNode> indexedFeatsNode = new Dictionary<string, FeatNode>();
    public List<FeatNode> featNodes;
    public void Load(){
        indexedFeatsNode.Clear();
        foreach (var node in featNodes){
            indexedFeatsNode.Add(node.feat.featName,node);
        }
    }

    public Feat GetFeatByName(string name){
        if (indexedFeatsNode.ContainsKey(name)){
            return indexedFeatsNode[name].feat;
        }
        return null;
    }

    public int FeatLevel(string name){
        var featLevel = GetFeatByName(name)?.FeatLevel;
        return featLevel ?? 0;
    }

    public int FeatLevel(Feat feat){
        return feat.FeatLevel;
    }
    // Start is called before the first frame update
}
