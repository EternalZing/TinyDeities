using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
//负责处理角色的属性和属性和属性逻辑.
[CreateAssetMenu(menuName = "Character")]
public class CharacterState :ScriptableObject{
    [SerializeField]
    protected new string name = "None";
    [SerializeField]
    protected float movingSpeed;
    [SerializeField]
    protected int level;
    [SerializeField]
    protected float life;
    [SerializeField]
    protected float lifeMax;
    [SerializeField]
    protected float cd;
    [SerializeField]
    protected float danmakuSpeed;
    [SerializeField]
    protected SkillChain skillChain;

    public static int FeatPointPerLevel = 5;
    public int freeFeatPoint;
    public SkillChain SkillChain{
        get => skillChain;
        set => skillChain = value;
    }

    [SerializeField] protected float exp;

    public float Exp{
        get => exp;
        set => exp = value;
    }

    [SerializeField] protected float baseDamage;
    public string Name{
        get => name;
        set => name = value;
    }

    public float BaseDamage{
        get => baseDamage;
        set => baseDamage = value;
    }

    public float MovingSpeed{
        get => movingSpeed;
        set => movingSpeed = value;
    }

    public int Level{
        get => level;
        set{
            int levelGap  = value - level;
            freeFeatPoint += levelGap * FeatPointPerLevel;
            level = value;
        }
    }

    public float Life{
        get => life;
        set => life = value;
    }

    public float LifePercentage => Mathf.Clamp(life / lifeMax, 0, 1);
    public float ExpPercentage => Mathf.Clamp(exp / (level * 10), 0, 1);
    
    public float LifeMax{
        get => lifeMax;
        set => lifeMax = value;
    }

    public float Cd{
        get => cd;
        set => cd = value;
    }

    public float DanmakuSpeed{
        get => danmakuSpeed;
        set => danmakuSpeed = value;
    }
    public static CharacterState CreateCharacterState(CharacterState template){
        CharacterState state = ScriptableObject.Instantiate(template) as CharacterState;
        return state;

    }

    public GameObject defaultDanmaku;
}
