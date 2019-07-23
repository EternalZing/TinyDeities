using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FeatContainer : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler{

    private GameObject _text;
    public Feat feat;
    public void Start(){
       _text =  transform.Find("FeatText").gameObject;
    }
    
    // Start is called before the first frame update

    public void OnPointerEnter(PointerEventData eventData){
        if (!_text.activeSelf) _text.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData){
        if (_text.activeSelf) _text.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData){
        //To do here to change to reduce string usage
        if (feat == null) return;
        Debug.Log($"Level up feat {feat.featName}");
        Character cs = GameObject.FindWithTag("Player").GetComponent<Hero>();
        CharacterManager.Singleton.LevelUpHeroFeat(cs,feat);
    }
}
