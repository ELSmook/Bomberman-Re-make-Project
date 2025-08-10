using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
    //CapsuleCollider
[RequireComponent(typeof(BoxCollider))]


public class Bomb : MonoBehaviour
{

    //[RequireComponent(typeof(Objetivo))]
    Rigidbody rb;
    //[SerializeField] GameObject Explosion;
    //[SerializeField] GameObject Player;
    Vector3 AbovePlayer=new Vector3(0, 2, 0);
    private int PlayerDamage;
    Animator animator;
    Transform playerPosition;
    
    // Start is called before the first frame update
    private void Start()
    {
        playerPosition = GameObject.FindWithTag("Point").transform;
        rb= GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        //Explode(Explosion);
    }

    public void KnockbackEntity(Transform executionSource,float KnockbackForce){
        //throw new NotImplementedException();

        Vector3 dir = (transform.position - executionSource.position).normalized;
        // it use to be executionSource.transform.position

        // for the knockback to be just vertical
        dir.y = 0;
        dir.Normalize();

        // the pulse
        rb.AddForce(dir * KnockbackForce, ForceMode.Impulse);


        //GetComponent<Objetivo>().recibir(ingflijr);

        
    }

    public void KnockbackEntityFixed(Transform executionSource,float KnockbackForce){
        //throw new NotImplementedException();

        Vector3 dir = (transform.position - executionSource.position).normalized;
        // it use to be executionSource.transform.position

        // for the knockback to be just vertical
        dir.y = 0;
        dir.Normalize();

        // the pulse
        rb.AddForce(dir * KnockbackForce, ForceMode.Impulse);


        //GetComponent<Objetivo>().recibir(ingflijr);

        
    }

    // To make the bomb object stay still above the player s head when picked up
    public void gravityState(bool isHolding){
        
        if(isHolding== true){
            rb.useGravity = false;
        }
        else{
            rb.useGravity = true;
        }

    }


    // To create the explosion object and set the animation of the bomb exploding
    public void Explode(){
        //transform position= transform.position;
        //print("i spawned");
        //animator.SetBool("isExploding", true);
        //Vector3 BombPosition= Player.GetComponent<ThirdPersonController>().BombClonePosition;
        //Transform BP= BombPosition.transform.position;
        StartCoroutine(Order());
    }

    IEnumerator Order()
    {
        //animator.SetBool("isExploding", true);
        //Explode(Explosion);
        yield return new WaitForSecondsRealtime(8f);
        animator.SetBool("isExploding", true);
        
    }

    



}
