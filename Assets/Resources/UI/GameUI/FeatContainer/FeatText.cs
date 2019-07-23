using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeatText : MonoBehaviour{
    [HideInInspector] public Feat feat = null;
    private Text _text;
    // Start is called before the first frame update
    private void OnEnable(){
        _text = GetComponent<Text>();
    }

    public void Update(){
        if (feat != null){
            _text.text = $"{feat.featName} lv:{feat.FeatLevel}:\n{feat.description}";
        }
    }
}
