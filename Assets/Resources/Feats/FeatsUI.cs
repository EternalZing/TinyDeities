using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatsUI : MonoBehaviour{
    public FeatTree targetFeatTree;
    public GameObject featUI;
    public Plane targetFeatTreeContainer;
    
    // Update is called once per frame
    public void Awake(){
        foreach (var  node in targetFeatTree.featNodes){
            GameObject featContainer = Instantiate(featUI,this.transform);
            featContainer.GetComponent<FeatContainer>().feat = node.feat;
            featContainer.transform.Find("Image").GetComponent<Image>().sprite = node.feat.featIcon;
            featContainer.transform.Find("FeatText").GetComponent<FeatText>().feat = node.feat;
            featContainer.GetComponent<RectTransform>().anchoredPosition = node.position;
        }
    }
}
