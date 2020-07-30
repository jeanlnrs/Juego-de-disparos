using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        public float speed = 6f;            // The speed that the player will move at.
        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.
        RaycastHit floorHit; 

        Camera cam;
        public GameObject mainCamera;

    void Awake()
    {           
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask ("Floor");
        //Debug.Log(floorMask.ToString());
        // Set up references.
        anim = GetComponent <Animator> ();
        
        playerRigidbody = GetComponent <Rigidbody> ();
        //cam = GameObject.Find("mainCamera");
      
        
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");   
       
        Move(h, v);
        Turning();
        Animating(h, v);
        
    }

    void Move(float h, float v){
        movement.Set(h, 0f, v);
        // normailzed corrects the advantage by moving diagonaly x = 1 ; y = 1 hypotenus = 1.4; 
        // normailized corrects hypotenus = 1 back again
        // It also has to move every sencond we  do that by multiplying by Time.deltaTime
        movement = movement.normalized * speed * Time.deltaTime; 
        
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning(){

        //Debug.Log( cam.ScreenPointToRay(Input.mousePosition) );
        //Debug.Log(Camera.main.ScreenPointToRay(Input.mousePosition) );
        //Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Debug.Log("camRay "+camRay.ToString());
        // Debug.Log("floorHit "+ floorHit.collider );

        if ( Physics.Raycast( camRay, out floorHit, camRayLength, floorMask )) {

            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Debug.Log("playerToMouse" +playerToMouse);
            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            playerRigidbody.MoveRotation (newRotation);

        }

    }

    void Animating(float h, float v){

        bool walking = h != 0f || v != 0f;
        anim.SetBool("IsWalking", walking);
    }

}


