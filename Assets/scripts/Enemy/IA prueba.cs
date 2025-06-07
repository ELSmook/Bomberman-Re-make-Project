using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAprueba : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform jugador;

    public NavMeshAgent Coords;
    public float RangoAtaque;
    public float RangoVista;

    public int rutina;
    public float cronometro;
    Animator Ani;
    public Quaternion angulo;
    public float grado;

    public GameObject objetivo_jugador;

    public bool atacando;
    public int velocidad;

    void Awake()
    {
        
        

    }

    void Start(){

        Ani= GetComponent<Animator>();

        // if(Ani != null){
        //     print("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        // }
        Coords= GetComponent<NavMeshAgent>();

        Transform tagJugador = GameObject.FindWithTag("Player").transform;

        objetivo_jugador = GameObject.FindWithTag("Player");

        jugador= tagJugador;

    }
    

    // Update is called once per frame
    void Update()
    {
        //Coords.destination= jugador.position;

        Acciones();
        
    }

    public void Acciones(){

        if(Vector3.Distance(transform.position, objetivo_jugador.transform.position) > RangoVista){

            Coords.enabled= false;

            //Ani.SetBool("Run Forward" , false);

            cronometro += 1 * Time.deltaTime;

            if(cronometro >= 4){
                
                rutina= Random.Range(0, 2);
                cronometro= 0;
            }

            switch(rutina){
                case 0:
                // Caminar
                //Ani.SetBool("WalkForward" , false);
                //print("falso");
                break;

                case 1:
                grado= Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);

                rutina++;
                break;
                case 2:
                transform.rotation= Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

                //Ani.SetBool("WalkForward" , true);
                //print("verdad");
                break;
            }
        }
        else{
            var LookAt = objetivo_jugador.transform.position - transform.position;
            LookAt.y= 0;
            var rotation = Quaternion.LookRotation(LookAt);

            Coords.enabled= true;
            Coords.SetDestination(objetivo_jugador.transform.position);

            if(Vector3.Distance(transform.position, objetivo_jugador.transform.position) > RangoAtaque && !atacando){

                //Ani.SetBool("WalkForward" , false);

                //Ani.SetBool("Run Forward" , true);

            }
            else{

                if(!atacando){
                    transform.rotation= Quaternion.RotateTowards(transform.rotation, rotation, 3);

                    //Ani.SetBool("WalkForward" , false);
                    //Ani.SetBool("Run Forward" , false);
                    
                    //Ani.SetBool("Atacar" , true);
                    //Ani.SetTrigger("Ataque");
                }

            }

            if(atacando){
                Coords.enabled= false;
                
            }
        }

        

    }

    void OnTriggerEnter(Collider colicion){

        if(colicion.CompareTag("Player")){

            print("ahhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh");

        }

    }
    



    public void CdAtaque(){

        //Ani.SetBool("Atacar" , false);
        //atacando= false;
        //Ani.SetBool("Combat Idle" , true);
    }


}
