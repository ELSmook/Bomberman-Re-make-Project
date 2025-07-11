
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Main script for third-person movement of the character in the game.
/// Make sure that the object that will receive this script (the player) 
/// has the Player tag and the Character Controller component.
/// </summary>
///
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class ThirdPersonController : MonoBehaviour
{

    // Configuracion general
    [Header("Player Configuration")]
    [SerializeField] float velocity = 5f;
    [SerializeField] float sprintAdittion = 3.5f;
    [SerializeField] float jumpForce = 18f;
    [SerializeField] float jumpTime = 0.85f;
    [SerializeField] float gravity = 9.8f;
    float jumpElapsedTime = 0;

    // Configuracion de agachado
    [Header("Crounch config")]
    [SerializeField] float CrouchColliderHeight;
    [SerializeField] Vector3 CrouchCollierCenter;
    float StandingColliderHeight;
    Vector3 StandingCollierCenter;

    // Estados del jugador
    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    // Manejo de inputs
    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputCrouch;
    bool inputSprint;


    Animator animator;
    CharacterController cc;
    Transform playerPosition;
    GameObject playerObject;

    [Header("Bomb Configuration")]
    public GameObject Bomb;
    public Vector3 posicion;
    public float KnockbackForce= 10f;
    public float ActionBombRange= 5000f;
    //bool canKickBomb= false;
    private Camera ActionCamera;
    public Vector3 AbovePlayer= new Vector3(0, 2,0);
    //public Vector3 FixAbovePlayer= new Vector3(0, 2,);
    bool isHolding = false;
    private GameObject BombObject;
    private Bomb BombFunctions;
    //int layerMask = 1 << 8;

    //private Camera Camara;

    void Start()
    {
        // Obtengo los componenetes de mi jugador
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        // Player coordinates
        playerPosition = GameObject.FindWithTag("Point").transform;
        // Gameobject Model of the player
        playerObject = GameObject.FindWithTag("Point");
   
        // Configuro los parametros del collider 
        StandingColliderHeight = cc.height;
        StandingCollierCenter =  cc.center;
        GameObject FindCam = GameObject.FindWithTag("action");

        ActionCamera = FindCam.GetComponentInChildren<Camera>();
    }

    // Update is only being used here to identify keys and trigger animations
    void Update()
    {

        // Obtengo los inputs del jugador para el movimiento
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
       


        // #### Control de animacion ####

        // Control de agacharce
        inputCrouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1);
        if (inputCrouch)
        {
            // Cambio el estado del jugador
            isCrouching = !isCrouching;

            // Cambio la altura y el centro del controller
            cc.height = isCrouching? CrouchColliderHeight : StandingColliderHeight;
            cc.center = isCrouching? CrouchCollierCenter  : StandingCollierCenter;
            
            // Ejecuto la animacion de agacharce
            if (animator != null)
            {
                // Ejecuto agachar si es necesario
                //animator.SetBool("crouch", isCrouching);
            }
        }


        // Control de correr y caminar
        inputSprint = Input.GetAxis("Fire3") == 1f;
        if ( cc.isGrounded && animator != null )
        {
            // Caminar
            float minimumSpeed = 0.9f;
            //animator.SetBool("run", cc.velocity.magnitude > minimumSpeed );

            // Correr
            isSprinting = cc.velocity.magnitude > minimumSpeed && inputSprint;
            //animator.SetBool("sprint", isSprinting );

        }

        // Si acciono la tecla de salto cambio el estado de jugador a saltando
        inputJump = Input.GetAxis("Jump") == 1f;
        if (inputJump && cc.isGrounded)
        {
            isJumping = true;
 
        }

        // Si esto en el aire activo la animacion de estar en el aire
        if ( animator != null)
        {
            //animator.SetBool("air", cc.isGrounded == false);
        }
       
       // Spawn bomb with Z
        if (Input.GetKeyDown(KeyCode.Z))
        {
            posicion= playerPosition.position;
            GameObject Clon= Instantiate(Bomb, posicion,Quaternion.identity);

            StartCoroutine(Order(Clon));
            Debug.Log("boom");
        }

        // Kick bomb with X
        if (Input.GetKeyDown(KeyCode.X))
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = ActionCamera.ScreenPointToRay(screenCenter);
            Debug.DrawRay(ray.origin, ray.direction * 1000.0f, Color.red, 2.0f);
            RaycastHit hit;
            print("i shot!");
            if(Physics.Raycast(ray, out hit, ActionBombRange)){
                Debug.Log(hit.collider.name); // Mostrará el nombre del objeto impactado
                Bomb Bomb = hit.collider.gameObject.GetComponent<Bomb>();
                print("im hitting it!");

                if (Bomb != null){
                    Bomb.KnockbackEntity(transform,KnockbackForce);
                }   
                else{
                    print("im not hitting it!");
                }
            }

        }

        // Pick up bomb with C
        if (Input.GetKeyDown(KeyCode.C))
        {

            if (isHolding==false)
            {

                Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
                Ray ray = ActionCamera.ScreenPointToRay(screenCenter);
                Debug.DrawRay(ray.origin, ray.direction * 1000.0f, Color.red, 2.0f);
                RaycastHit hit;
                //print("i shot!");
                if(Physics.Raycast(ray, out hit, ActionBombRange)){
                    Debug.Log(hit.collider.name);
                    // tenia bomb 
                    //GetComponent<BoxCollider>().bounds.center
                    BombFunctions = hit.collider.gameObject.GetComponent<Bomb>();
                    BoxCollider BombBoxCollider = hit.collider.gameObject.GetComponent<BoxCollider>();
                    BombObject = hit.collider.gameObject;
                    //Rigidbody BombRJ= hit.collider.gameObject.GetComponent<Rigidbody>();
                    //BoxCollider box = Bomb.GetComponent<BoxCollider>();
                    //bool isHolding = false;

                    if (BombFunctions != null){
                        
                        //
                        isHolding = true;
                        Vector3 ObjectAbove= playerPosition.transform.position + AbovePlayer;

                        print(ObjectAbove);

                        BombObject.transform.position= ObjectAbove;
                        BombBoxCollider.transform.position=ObjectAbove;

                        // usinggravity in the rigid body 
                        // becomes false so the bomb can stay still above the player s head
                        BombFunctions.gravityState(isHolding);
                        BombObject.transform.SetParent(playerObject.transform);
                        //BombRJ.useGravity = false;
                        
                    }   
                    else{
                        print("im not hitting it!");
                    }
                }
            }
            else{
                isHolding = false;
                BombObject.transform.SetParent(null, true);
                BombFunctions.gravityState(isHolding);
                BombFunctions.KnockbackEntityFixed(transform,KnockbackForce);

            }
        }

        HeadHittingDetect();

    }




   // Movieminto en fixed update
    private void FixedUpdate()
    {
        // #### Configuracion para salto #### 
        float directionY = 0;
        if ( isJumping )
        {

            //// Agrego una fuerza de salto que se reduce segun el tiempo trascurrido

            // Agrego una fuerza de salto que depende de " (jumpElapsedTime / jumpTime) "
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime) * Time.deltaTime;

            // Hago que "(jumpElapsedTime / jumpTime)" resulte en un numero un poco mayor para aplicar la fuerza en el siguiente 
            // frame hasta que se cumpla el jumpTime Necesario
            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        // Modifico la direccion en y segun la gravedad
        directionY  -= gravity * Time.deltaTime;


        // #### Rotacion del jugador ####

        // Determino mi velocidad de movimiento, 
        float velocityAdittion = isSprinting && !isCrouching ? sprintAdittion : -(velocity * 0.50f);

        // Direccion de movimiento en cada eje
        float directionX = inputHorizontal * (velocity + velocityAdittion) * Time.deltaTime;
        float directionZ = inputVertical * (velocity + velocityAdittion) * Time.deltaTime;

        // Obtengo la direccion objetivo segun la posicion de la camara
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Elimino la componenete y de cada vector
        forward.y = 0;
        right.y = 0;

        // Normalizo para tener una amplitud de 1
        forward.Normalize();
        right.Normalize();

        // Cambio su amplitud basandome en los inputs del jugador
        forward *= directionZ;
        right *= directionX;

        // Si aprieta alguna tecla del los ejes x o z:
        if (directionX != 0 || directionZ != 0)
        {
            // Matetrucos para rotacion fluida
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        
        // Detemrino direcciones de movimeinto y muevo el CharacterController
        Vector3 verticalDirection = Vector3.up * directionY;
        Vector3 horizontalDirection = forward + right;
        Vector3 movemet = verticalDirection + horizontalDirection;
        cc.Move(movemet);

    }


    // Funcion que hace que el jugador temrine su salto si se choca con el techo
    void HeadHittingDetect()
    {
        // Obtiene parametros para crear un reaycast un poco mas alto que el personaje
        float headHitDistance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(cc.center);
        float hitCalc = cc.height / 2f * headHitDistance;

        // Uncomment this line to see the Ray drawed in your characters head
        // Debug.DrawRay(ccCenter, Vector3.up * headHeight, Color.red);

        // Si el rayo choca con un objeto con collider
        if (Physics.Raycast(ccCenter, Vector3.up, hitCalc))
        {
            // Restaura el salto
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }

    IEnumerator Order(GameObject GameObject)
    {
        yield return new WaitForSecondsRealtime(10);
        Destroy(GameObject);
    }
    //OnCollisionEnter OnTriggerEnter

    

    

}
