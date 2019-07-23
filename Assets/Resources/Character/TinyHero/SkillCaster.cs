using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillCaster : MonoBehaviour{
    public SkillChain skillChain;

    private void Flowering(Skill skill,Skill.TimelineUnit unit){
        float speedModifier = unit.GetAdditionalInfo<float>(Skill.TimelineUnit.AdditionalInfo.SpeedModifier);
        float damageModifier = unit.GetAdditionalInfo<float>(Skill.TimelineUnit.AdditionalInfo.DamageModifier);
        int number = unit.GetAdditionalInfo<int>(Skill.TimelineUnit.AdditionalInfo.FloweringNumbers);
        if (number < 1){
            Debug.LogError($"Constant FloweringNumber must be greater or equal to 1");
            return ;
        }
        float offset = (2 * Mathf.PI) / number;
        
        //implementation
        for (int i = 0; i < number; i++){
            Vector2 dir =  new Vector2(Mathf.Cos(offset*i),Mathf.Sin(offset*i));
            GameObject tempDanmaku =  Instantiate(unit.danmakuReference,transform.position,Quaternion.LookRotation(Vector3.forward,dir));
            tempDanmaku.GetComponent<Rigidbody2D>().velocity = speedModifier * skill.basicSpeed * dir;
            tempDanmaku.GetComponent<BasicDanmaku>().damage = damageModifier * skill.baseDamage;
            tempDanmaku.GetComponent<BasicDanmaku>().onwer = gameObject;
        }
    }
    // Start is called before the first frame update
    public IEnumerator Cast(Skill skill){
        skill.LoadForPlayer(GetComponent<Character>());
        skill.Reset();
        var enumerator = skill.NextTimeLineUnit();
        while(enumerator.MoveNext()){
            yield return new WaitForEndOfFrame();
            if (enumerator.Current is IEnumerator timelineEnumerator && timelineEnumerator.Current is Skill.TimelineUnit currentTimeline){
                GameObject danmaku = currentTimeline.danmakuReference;
                switch (currentTimeline.unitType){
                    case Skill.TimelineUnit.UnitType.Flowering:
                        Flowering(skill,currentTimeline);
                        break;
                    case Skill.TimelineUnit.UnitType.Single:
                        break;
                    case Skill.TimelineUnit.UnitType.Sector:
                        break;
                    case Skill.TimelineUnit.UnitType.Beam:
                        break;
                    case Skill.TimelineUnit.UnitType.BeamingDanmaku:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        yield return null;
    }
    public void Trigger(int id){
       Skill triggerSkill =  skillChain.GetType().GetField("slot" + id).GetValue(skillChain) as Skill;
       
       Debug.Log("skill triggered");

       StartCoroutine(Cast(triggerSkill));

       if (null == triggerSkill){
           Debug.Log($"No such skill in {this.gameObject} ,which skill id in skill chain is {id}" );
       }
    }
}
