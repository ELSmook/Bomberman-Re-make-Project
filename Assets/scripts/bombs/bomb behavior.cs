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
    // Start is called before the first frame update
    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        
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




}
