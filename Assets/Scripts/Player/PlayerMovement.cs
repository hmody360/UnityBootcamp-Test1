using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public float jumpForce = 3;
    public float doubleJumpForce = 1;
    public float rotationSpeed;
    public Transform cameraTransform;

    private Rigidbody theRigidBody;
    private Quaternion targetRotation;

    [SerializeField] private float groundCheckerOffset;
    [SerializeField] private float groundCheckerRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource[] SFXList;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canDoubleJump;
    


    Vector3 movementDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        theRigidBody = GetComponent<Rigidbody>(); //Getting Rigidbody from Player Object.
        targetRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        theRigidBody.freezeRotation = true;
    }

    private void Update()
    {
        // Allow Player to jump if on ground and jump button pressed.
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            theRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = true;
            SFXList[1].PlayOneShot(SFXList[1].clip);
        }
        // Allow Player to double jump if NOT on ground and jump button pressed.
        if (!isGrounded && canDoubleJump && Input.GetButtonDown("Jump"))
        {
            theRigidBody.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
            canDoubleJump = false;
            SFXList[1].PlayOneShot(SFXList[1].clip);
        }


    }

    // Update is called once per frame.
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.up * groundCheckerOffset, groundCheckerRadius, groundLayer); //Checking if player is on ground.
        float Horizontal = Input.GetAxis("Horizontal"); //Defining Char X Axis.
        float Vertical = Input.GetAxis("Vertical"); //Defining Char Z Axis.

        bool isWalking = ((Horizontal != 0 || Vertical != 0) && isGrounded); //Check if player is walking to play walkingSFX

        if(isWalking && !SFXList[0].isPlaying) //if player is walking and the walking audio source is not playing, play it.
        {
            SFXList[0].Play();
        }
        else if(!isWalking && SFXList[0].isPlaying) //if player STOPPED walking and the walking audio source is playing, stop it.
        {
            SFXList[0].Stop();
        }

        // Camera Controls (for Realtive Movement)
        // Taking the Camera Forward and Right
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        //freezing the camera's y axis as we don't want it to be affected for the direction
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        //Realtive Cam Direction
        Vector3 forwardRealtive = Vertical * cameraForward;
        Vector3 rightRealtive = Horizontal * cameraRight;

        movementDir = (forwardRealtive + rightRealtive).normalized * speed; //assigning movement with camera direction in mind, also using normalized to make movement in corner dierctions the same as normal directions (not faster)

        //Movement
        theRigidBody.linearVelocity = new Vector3(movementDir.x, theRigidBody.linearVelocity.y, movementDir.z); // Changing the velocity based on Horizontal and Vertical Movements alongside camera direction.

        //Rotation
        Vector3 targetDirection = new Vector3(movementDir.x, 0f, movementDir.z); //Taking the direction the player is currently facing
        
        if (targetDirection != Vector3.zero) //on player movement
        {
            targetRotation = Quaternion.LookRotation(targetDirection); // makes the target rotation that we want the player to move to
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); //Using lerp to smooth the player rotation using current rotation, target rotaion and rotation speed.
        

    }

    private void OnDrawGizmos() //Gizmo to draw the ground checker sphere.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * groundCheckerOffset, groundCheckerRadius);
    }
}
