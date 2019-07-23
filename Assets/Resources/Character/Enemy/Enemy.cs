
using UnityEngine;

public class Enemy : Character{
    public string enemyName;

    public void Dying(){
        base.DefaultDying();
        var player = GameObject.FindWithTag("Player");
        Debug.Log(State.Level*2);
        player.GetComponent<Character>().GetExp(State.Level*2);
    }
    public new void Start(){
        base.Start();
        if (GetComponent<AIMove>()){
            GetComponent<AIMove>().movingSpeed = State.MovingSpeed;
        }
    }
    // Start is called before the first frame update

    
    // Update is called once per frame

}
