using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPositionTracker : MonoBehaviour{
    public Vector3 offset;

    public enum CanvasType{
        SpaceScreen
    };

    public Transform target;
    public CanvasType canvasType;
    private Camera _camera;
    private RectTransform _rectTransform;


    private void Start(){
        _rectTransform = this.GetComponent<RectTransform>();
        _camera = Camera.main;
    }

    
    void Update(){
        _rectTransform.position = target.transform.position + offset;
    }
}
