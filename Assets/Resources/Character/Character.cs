using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 负责处理角色的视觉效果和交互体验.
/// </summary>
///
public class Character : MonoBehaviour{
    [SerializeField] 
    protected CharacterState state;
    public CharacterState State => this.state;
    public void Start(){
        _mission = Mission.GetMission();
        this.state = CharacterState.CreateCharacterState(state);
    }
    protected bool cd;
    public UnityEvent onDying;
    protected Mission _mission;
    protected bool paused = false;
    public void DefaultGetHit(){
        this.GetComponent<SpriteRenderer>().color  =  new Color(1,0.7f,0.7f);
        _mission.MissionStart(()=>{
            GetComponent<SpriteRenderer>().color = Color.white;
        },_getHitPause);

    }
    protected float _getHitPause = 0.15f;
    public UnityEvent getHit;
    public void GetDamage(float damage){
        state.Life -= damage;
        if (state.Life < 0){
            onDying.Invoke();
        }
        GetComponent<Animator>().speed = 0;
        getHit?.Invoke();
        _mission.MissionStart(() => {
            GetComponent<Animator>().speed = 1;
        },_getHitPause);
    }
    
    public void GetExp(float exp){
        state.Exp += exp;
        if (state.ExpPercentage >= 1){
            state.Exp = 0;
            state.Level++;
        }
    }
    public void DefaultDying(){
//        Debug.Log(this);
        gameObject.SetActive(false);
        //Find all progressing mission to stop;
        Mission mission = Mission.GetMission(gameObject);
        mission.active = false;
    }
    public virtual void FireDanmakuAt(Vector2 pos){
        if(cd) return;
        cd = true;
        _mission.MissionStart(() => { cd = false;},State.Cd);
        Fire(this.transform.position,pos);

    }
    
    public void Fire(Vector2 original,Vector2 pos){
        Vector2 dir = pos - original;
        GameObject danmaku = Instantiate(state.defaultDanmaku,original, Quaternion.LookRotation(Vector3.forward,dir));
        danmaku.GetComponent<BasicDanmaku>().damage = State.BaseDamage;
        danmaku.GetComponent<BasicDanmaku>().Fire(dir.normalized, State.DanmakuSpeed);
        danmaku.GetComponent<BasicDanmaku>().onwer = gameObject;
    }
    public void OnTriggerEnter2D(Collider2D other){
        
        if (other.gameObject.tag.Equals("Danmaku")){
            var onwer = other.GetComponent<BasicDanmaku>().onwer;
            if (onwer == this.gameObject) return;
            if (onwer.tag.Equals("Enemy")&&this.gameObject.tag.Equals("Enemy")){
                //TO DO HERE IF THERE IS SOMETHING
                return;
            }
            GetDamage(other.GetComponent<BasicDanmaku>().damage);
            if (gameObject.tag.Equals("Enemy")){
                Vector2 dis = transform.position - other.transform.position;
                GetComponent<Rigidbody2D>().AddForce(dis.normalized,ForceMode2D.Impulse);
            }
            other.GetComponent<BasicDanmaku>().TriggerDamage();
        }
    }
}
