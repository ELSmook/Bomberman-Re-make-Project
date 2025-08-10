using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombexplosion : MonoBehaviour
{
    //private Bomb BombFunctions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Im working");
        if (other.CompareTag("Enemy"))  
        {
            print("Im exploding with the enemy");
            Attributes Functions= other.GetComponent<Collider>().gameObject.GetComponent<Attributes>();
            GameObject Player = GameObject.FindWithTag("Point");
            ThirdPersonController PlayerScript= Player.GetComponent<ThirdPersonController>();;
            int PlayerDamage=  PlayerScript.BombDamage;

            Functions.Life-= PlayerDamage;

            if(Functions.Life <= 0){
                Destroy(other.gameObject);
                
            }


        }


    }

}
