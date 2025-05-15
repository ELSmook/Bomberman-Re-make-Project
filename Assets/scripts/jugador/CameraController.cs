
using UnityEngine;

/*
    This file has a commented version with details about how each line works. 
    The commented version contains code that is easier and simpler to read. This file is minified.
*/

/// <summary>
/// Camera movement script for third person games.
/// This Script should not be applied to the camera! It is attached to an empty object and inside
/// it (as a child object) should be your game's MainCamera.
/// </summary>
public class CameraController : MonoBehaviour
{
    // Configuracion general
    [Header("Base Camera Config")]
    [SerializeField] bool clickToMoveCamera = false;
    //[SerializeField] float sensitivity = 5f;
    [SerializeField] Vector2 cameraLimit = new Vector2(-45, 40);
    [Space]
    [SerializeField] bool canZoom = true;
    [SerializeField] float ZoomSens = 100;
    [SerializeField] float MinFieldOfView = 35;
    [SerializeField] float MaxFieldOfView = 75;
    [Header("Camera position and rotation configuration")]
    public Camera Camara;
    Transform CameraLocation;
    //Vector3 rotateCamera = Camera.main.transform.forward;
    //Quaternion MoveCamera = Camera.main.transform.forward;
    //Configuracion Extra
    float mouseX;
    float mouseY;
    float offsetDistanceY;

    Transform player;
    [Header("The distance between the player and the camera (Default is X=0, Y=10 and Z=-12)")]
    public Vector3 CameraDistance;
    [Header("The fraction in which the camera rotates around (Default is X=2 and Z=2")]
    public Vector3 Plus;
    public Vector3 Take;
    [Header("The rotation limit")]
    float RotLimitR= 12f;
    float RotLimitL= -12f;

    private void Awake() {
        Camara = Camera.main;

        //Quaternion CameraLocation= Camara.transform.rotation;
    }


    void Start()
    {
        //print(CameraLocation.position);
        // Defino varaibles iniciales
        player = GameObject.FindWithTag("Point").transform;
        offsetDistanceY = transform.position.y;
        //CameraPosition= player.position;

        // Bloqueo y esocndo el mouse
        if ( ! clickToMoveCamera )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        CameraDistance= new Vector3(0,10,-12);
        


    }


    void Update()
    {
        //transform.position = player.position + new Vector3(0, 5, -12);

        // Sigo al jugador
        MoveCamera(CameraDistance);

        // Set camera zoom when mouse wheel is scrolled
        float ScrollValue = Input.GetAxis("Mouse ScrollWheel");
        if ( canZoom && ScrollValue != 0)
        {
            // Cambio el campo de vision de la camara
            print(Input.GetAxis("Mouse ScrollWheel"));
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomSens;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, MinFieldOfView, MaxFieldOfView);
        }
           
        // Si la opcion de apretar para mover esta activada y NO se aprieta finaliza la funcion
        if ( clickToMoveCamera)
        {
            // Si aprieto el click
            if (Input.GetAxisRaw("Fire2") == 1)
            {
                // Calculo la rotacion de la camara
                //mouseX += Input.GetAxis("Mouse X") * sensitivity;
                //mouseY += Input.GetAxis("Mouse Y") * sensitivity;
                //mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);

                // Roto la camara
                //transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);
            //print("3");
            }
        }
        else
        {
            // Calculo la rotacion de la camara
            //mouseX += Input.GetAxis("Mouse X") * sensitivity;
            //mouseY += Input.GetAxis("Mouse Y") * sensitivity;
            //mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);

            // Roto la camara
            //transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);

            print("2");
        }
           
        // Down para
        if(Input.GetMouseButton(0)){
            
            //float PlusY= 45f;
            //float PlusXrotation= 5f;
            //float PlusZrotation= 5f;

            Plus= new Vector3(2f,0f,2f);
            RotLimitL+= 2;

            //RotateCamera(PlusY);
            //CameraDistance.x-= Plus.x;
            //CameraDistance.z+= Plus.z; 
            //MoveCamera(PlusXrotation,PlusZrotation);

            if(RotLimitL>=0f){

                if(RotLimitL>=24f){
                    // 24 to 36 
                    CameraDistance.x-= Plus.x;
                    CameraDistance.z-= Plus.z;
                    print("Indice");
                    print(RotLimitL);
                }
                else if(RotLimitL>=12f){
                    // 12 to 24
                    CameraDistance.x+= Plus.x;
                    CameraDistance.z-= Plus.z;
                    print(RotLimitL);
                    print("No indice");

                    
                }
                else{
                    // From 0 to 12
                    CameraDistance.x+= Plus.x;
                    CameraDistance.z+= Plus.z;
                    print(RotLimitL);
                }

            }
            else{
                // Position from -12 to 0
                CameraDistance.x-= Plus.x;
                CameraDistance.z+= Plus.z;
                print(RotLimitL);
            }
            //print(RightRotation.x);  

            if(RotLimitL==36f){
                RotLimitL=-12f;
                CameraDistance= new Vector3(0,10,-12);

                Quaternion CameraRotationReset= Camara.transform.rotation;
                Quaternion newRotation = Quaternion.Euler(CameraRotationReset.eulerAngles.x,0f, CameraRotationReset.eulerAngles.z);

                // Aplicar la nueva rotación
                Camara.transform.rotation = newRotation;

            }

            float CameraRotation= 15f;

            RotateCamera(CameraRotation);

            print("rotar izq");
        }
        if(Input.GetMouseButton(1)){
            

            //float TakeXrotation= 5f;
            //float TakeZrotation= 5f;
            Take= new Vector3(2f,0f,2f);
            //Vector3 RightRotation = new Vector3(Take.x - RotLimit,0f,2f);
            RotLimitR-= 2; 

            //Vector3 BreakPointRight1= new Vector3(2f,0f,2f) + player.transform.position;
            //Vector3 BreakPointRight2= new Vector3(6f,0f,2f) + CameraDistance;
            //Vector3 BreakPointRight3= new Vector3(6f,0f,2f) + CameraDistance;

            if(RotLimitR<=0f){

                if(RotLimitR<=-24f){
                    // -24 to -36 
                    CameraDistance.x+= Take.x;
                    CameraDistance.z-= Take.z;
                    print("Indice");
                    print(RotLimitR);
                }
                else if(RotLimitR<=-12f){
                    // -12 to -24
                    CameraDistance.x-= Take.x;
                    CameraDistance.z-= Take.z;
                    print(RotLimitR);
                    print("No indice");

                    
                }
                else{
                    // From 0 to -12
                    CameraDistance.x-= Take.x;
                    CameraDistance.z+= Take.z;
                    print(RotLimitR);
                }

            }
            else{
                // Position from 12 to 0
                CameraDistance.x+= Take.x;
                CameraDistance.z+= Take.z;



                print(RotLimitR);
            }
            //print(RightRotation.x);  

            if(RotLimitR==-36f){
                RotLimitR=12f;
                CameraDistance= new Vector3(0,10,-12);

                Quaternion CameraRotationReset= Camara.transform.rotation;
                Quaternion newRotation = Quaternion.Euler(CameraRotationReset.eulerAngles.x,0f, CameraRotationReset.eulerAngles.z);

                // Aplicar la nueva rotación
                Camara.transform.rotation = newRotation;

            }

            float CameraRotation= -15f;

            RotateCamera(CameraRotation);

            //print(BreakPointRight1);
            //print("c:");
            //print(Camara.transform.position);
            
            

            //RotateCamera(TakeY);
            //CameraDistance-= Take; 
            //MoveCamera(TakeXrotation,TakeZrotation);
            //Camara.transform.position = Vector3.Slerp(CameraDistance, BreakPointRight, 2f);

            //print("rotar der");
        }

        void RotateCamera(float YRotation){
            Quaternion CameraRotation = Camara.transform.rotation;

            Quaternion newRotation = Quaternion.Euler(CameraRotation.eulerAngles.x, CameraRotation.eulerAngles.y + YRotation, CameraRotation.eulerAngles.z);

            // Aplicar la nueva rotación
            Camara.transform.rotation = newRotation;


        }

        void MoveCamera(Vector3 CameraDistance){

            transform.position = player.position +  CameraDistance;

        }

        void MoveX()
        {

        }

        void MoveZ()
        {


        }

    }
}