using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
//FOLLOWING  CODE IS NOT IN USAGE ANYMORE
[CreateAssetMenu(menuName = "Manager/CharacterManager")]
public class CharacterManager : ScriptableObject{
    private static CharacterManager _singleton;
    public static CharacterManager Singleton{
        get{
            if (_singleton == null){
                _singleton = Resources.Load<CharacterManager>("Manager/CharacterManager");
            }

            return _singleton;
            
        }
    }
    protected Character heroCharacter;

    public void CreateHero(GameObject prefabHeroType){
        heroCharacter = GameObject.Instantiate(prefabHeroType).GetComponent<Character>();
    }

    public void LevelUpCharacter(Character character){
        character.State.Level++;
    }

    public void LevelUpHero(){
        heroCharacter.State.Level++;
    }

    public void DamageCharacter(Character character,float damage){
        character.State.Life -= damage;
    }

    public void LevelUpHeroFeat(Character hero, Feat feat){
        if (feat.Available&&hero.State.freeFeatPoint>0){
            feat.FeatLevel++;
            hero.State.freeFeatPoint--;
        }
    }

    public void LevelUpHeroFeat(Character hero, string featName){
        
    }
}
