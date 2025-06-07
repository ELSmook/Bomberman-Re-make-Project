
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
    [SerializeField] float ScrollSensivity = 35f;
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
    public float CameraSpeed= -2.5f;
    [Header("The fraction in which the camera rotates around (Default is X=2 and Z=2")]
    public Vector3 Plus;
    public Vector3 Take;
    [Header("The rotation limit")]
    float RotLimitR= 120f;
    float RotLimitL= -120f;


    Vector3 initialCameraPosition;
    Quaternion initialCameraRotation;

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
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomSens + ScrollSensivity;
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
           
        // add Down to make it 
        if(Input.GetMouseButton(0)){
            if(RotLimitR<=0f){ // Invert the right rotation
                Take= new Vector3(20f,0f,20f);
                //Vector3 RightRotation = new Vector3(Take.x - RotLimit,0f,2f);
                RotLimitR+= 20;
                print("invertidaaaaaaaaaaaaaaaaaaaaa");
                if(RotLimitR<=-240f){
                    // -24 to -36 
                    CameraDistance.x-= Mathf.Cos(Take.x) * CameraSpeed * Time.deltaTime;
                    CameraDistance.z+= Mathf.Sin(Take.z)* CameraSpeed * Time.deltaTime;
                    print("invertida");
                    print(RotLimitR);
                }
                else if(RotLimitR<=-120f){
                    // -12 to -24
                    CameraDistance.x+= Mathf.Cos(Take.x)* CameraSpeed * Time.deltaTime;
                    CameraDistance.z+= Mathf.Sin(Take.z)* CameraSpeed * Time.deltaTime;
                    print("invertida");

                    
                }
                else{
                    // From 0 to -12
                    CameraDistance.x+= Mathf.Cos(Take.x)* CameraSpeed * Time.deltaTime;
                    CameraDistance.z-= Mathf.Sin(Take.z)* CameraSpeed * Time.deltaTime;
                    print(RotLimitR);
                    print("invertidaaaaaaaaaaaaaaaaaaaaa");
                }

                float CameraRotation= 15f;

                RotateCamera(CameraRotation);
            }
            else if(RotLimitR<=120f ){
                Take= new Vector3(20f,0f,20);
                //Vector3 RightRotation = new Vector3(Take.x - RotLimit,0f,2f);
                RotLimitR+= 20;
                CameraDistance.x-= Mathf.Cos(Take.x)* CameraSpeed * Time.deltaTime;
                CameraDistance.z-= Mathf.Sin(Take.z)* CameraSpeed * Time.deltaTime;

                float CameraRotation= 15f;

                RotateCamera(CameraRotation);

            }
            // Normal left rotation
            else{
                Plus= new Vector3(20f,0f,20f);
                RotLimitL+= 20;

                if(RotLimitL>=0f){

                    if(RotLimitL>=240f){
                        // 24 to 36 
                        CameraDistance.x-= Mathf.Cos(Plus.x) * CameraSpeed * Time.deltaTime;
                        CameraDistance.z-= Mathf.Sin(Plus.z)* CameraSpeed * Time.deltaTime;
                        print("Indice");
                        print(RotLimitL);
                    }
                    else if(RotLimitL>=120f){
                        // 12 to 24
                        CameraDistance.x+=  Mathf.Cos(Plus.x)* CameraSpeed * Time.deltaTime;
                        CameraDistance.z-= Mathf.Sin(Plus.z)* CameraSpeed * Time.deltaTime;
                        print(RotLimitL);
                        print("No indice");
                    }
                    else{
                        // From 0 to 12
                        CameraDistance.x+= Mathf.Cos(Plus.x)* CameraSpeed * Time.deltaTime;
                        CameraDistance.z+=Mathf.Sin(Plus.z)* CameraSpeed * Time.deltaTime;
                        print("Caso extraño");
                        print(RotLimitL);
                    }
                }
                else{
                    // Position from -12 to 0
                    CameraDistance.x-= Mathf.Cos(Plus.x)* CameraSpeed * Time.deltaTime;
                    CameraDistance.z+= Mathf.Sin(Plus.z)* CameraSpeed * Time.deltaTime;



                    print(RotLimitL);
                }
                //print(RightRotation.x);  

                if(RotLimitL==360f){
                    RotLimitL=-120f;
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

        }
        if(Input.GetMouseButton(1)){
            
            if(RotLimitL>=0f){
                    Plus= new Vector3(20f,0f,20f);
                    RotLimitL-= 20;
                if(RotLimitL>=240f){
                    // 24 to 36 
                    CameraDistance.x+= Mathf.Cos(Plus.x)* CameraSpeed * Time.deltaTime;
                    CameraDistance.z+= Mathf.Sin(Plus.z)* CameraSpeed * Time.deltaTime;
                    print("Caso 3");
                    print(RotLimitL);
                }
                else if(RotLimitL>=120f){
                    // 12 to 24
                    CameraDistance.x-= Mathf.Cos(Plus.x)* CameraSpeed * Time.deltaTime;
                    CameraDistance.z+= Mathf.Sin(Plus.z) * CameraSpeed * Time.deltaTime;
                    print(RotLimitL);
                    print("Caso 2");
                }
                else{
                    // From 0 to 12
                    CameraDistance.x-=  Mathf.Cos(Plus.x)* CameraSpeed * Time.deltaTime;
                    CameraDistance.z-= Mathf.Sin(Plus.z)* CameraSpeed * Time.deltaTime;
                    print(RotLimitL);
                    print("Caso 1");
                }              

                float CameraRotation= -15f;

                RotateCamera(CameraRotation);
            }
            else if(RotLimitL>=-120f){
                Plus= new Vector3(20f,0f,20f);
                RotLimitL-= 20;
                // Position from -12 to 0
                CameraDistance.x+= Mathf.Cos(Plus.x)* CameraSpeed * Time.deltaTime;
                CameraDistance.z-= Mathf.Sin(Plus.z)* CameraSpeed * Time.deltaTime;

                float CameraRotation= -15f;

                RotateCamera(CameraRotation);
                print(RotLimitL);
                print("Caso 0");
            }
            else{
                //Normal rotation
                Take= new Vector3(20f,0f,20);
                //Vector3 RightRotation = new Vector3(Take.x - RotLimit,0f,2f);
                RotLimitR-= 20; 

                if(RotLimitR<=0f){

                    if(RotLimitR<=-240f){
                        // -24 to -36 
                        CameraDistance.x+= Mathf.Cos(Take.x) * CameraSpeed * Time.deltaTime;
                        CameraDistance.z-= Mathf.Sin(Take.z)* CameraSpeed * Time.deltaTime;
                        print("Indice");
                        print(RotLimitR);
                    }
                    else if(RotLimitR<=-120f){
                        // -12 to -24
                        CameraDistance.x-= Mathf.Cos(Take.x) * CameraSpeed * Time.deltaTime;
                        CameraDistance.z-= Mathf.Sin(Take.z) * CameraSpeed * Time.deltaTime;
                        print(RotLimitR);
                        print("No indice");

                        
                    }
                    else{
                        // From 0 to -12
                        CameraDistance.x-= Mathf.Cos(Take.x) * CameraSpeed * Time.deltaTime;
                        CameraDistance.z+= Mathf.Sin(Take.z) * CameraSpeed * Time.deltaTime;
                        print(RotLimitR);
                    }

                }
                else{
                    // Position from 12 to 0
                    CameraDistance.x+= Mathf.Cos(Take.x) * CameraSpeed * Time.deltaTime;
                    CameraDistance.z+= Mathf.Sin(Take.z)* CameraSpeed * Time.deltaTime;



                    print(RotLimitR);
                }
                //print(RightRotation.x);
                

                if(RotLimitR==-360f){
                    RotLimitR=120f;
                    CameraDistance= new Vector3(0,10,-12);

                    Quaternion CameraRotationReset= Camara.transform.rotation;
                    Quaternion newRotation = Quaternion.Euler(CameraRotationReset.eulerAngles.x,0f, CameraRotationReset.eulerAngles.z);

                    // Aplicar la nueva rotación
                    Camara.transform.rotation = newRotation;

                }

                float CameraRotation= -15f;

                RotateCamera(CameraRotation);
            }
                

            
        }

        if(Input.GetMouseButton(1) && Input.GetMouseButton(0)){
                RotLimitR=120f;
                RotLimitL=-120f;
                CameraDistance= new Vector3(0,10,-12);

                Quaternion CameraRotationReset= Camara.transform.rotation;
                Quaternion newRotation = Quaternion.Euler(CameraRotationReset.eulerAngles.x,0f, CameraRotationReset.eulerAngles.z);

                    // Aplicar la nueva rotación
                Camara.transform.rotation = newRotation;
        }

        transform.LookAt(player);

        void RotateCamera(float YRotation){
            Quaternion CameraRotation = Camara.transform.rotation;

            Quaternion newRotation = Quaternion.Euler(CameraRotation.eulerAngles.x, CameraRotation.eulerAngles.y + YRotation, CameraRotation.eulerAngles.z);

            // Aplicar la nueva rotación
            Camara.transform.rotation = newRotation;


        }

        void MoveCamera(Vector3 CameraDistance){

            transform.position = player.position +  CameraDistance;

        }

        void InvertRight(float RotLimitR)
        {
            
        }

        void InvertLeft(float RotLimitL)
        {


        }

    }
}