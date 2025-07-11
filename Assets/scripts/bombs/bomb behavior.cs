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
    [SerializeField] GameObject Explosion;
    Vector3 AbovePlayer=new Vector3(0, 2, 0);
    
    // Start is called before the first frame update
    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        //rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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

        Vector3 dir = (transform.position - executionSource.position);
        // it use to be executionSource.transform.position

        // for the knockback to be just vertical
        dir.y = 0;
        dir.Normalize();

        // the pulse
        rb.AddForce(dir * KnockbackForce, ForceMode.Impulse);


        //GetComponent<Objetivo>().recibir(ingflijr);

        
    }

    public void gravityState(bool isHolding){
        
        if(isHolding== true){
            rb.useGravity = false;
        }
        else{
            rb.useGravity = true;
        }

    }

    public void Explode(GameObject GameObject){
        //transform position= transform.position;
        GameObject Clon= Instantiate(GameObject, transform.position,Quaternion.identity);

        StartCoroutine(Order(Clon));
    }

    IEnumerator Order(GameObject GameObject)
    {
        yield return new WaitForSecondsRealtime(10);
        Destroy(GameObject);
    }


}
