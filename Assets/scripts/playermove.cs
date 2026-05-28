using UnityEngine;
using UnityEngine.InputSystem;


public class playermove : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private InputActionAsset input;
    [SerializeField] private string Action = "Player1";
    private InputActionMap map;

    private InputAction m;
    private InputAction j;
    private InputAction s;


    public CharacterController controller;
    public float wspeed = 5f;
    public float rspeed = 12f;
    public float gravity = -9.81f;
    public float speed;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpFore = 2.5f;
    float speed2;
    public float rot = 200f;
 
    Vector2 verlosity2;

   [SerializeField] Vector3 velocity;
    [SerializeField] bool isGrounded;
    private void Awake()
    {
        map = input.FindActionMap(Action);
        m = map.FindAction("Move");
        j =  map.FindAction("Jump");
        s = map.FindAction("Sprint");
        speed = wspeed;
        animator = GetComponentInChildren<Animator>();
    }
    private void OnEnable()
    {
        map.Enable();
    }
    private void OnDisable()
    {
        map.Disable();
    }
    // Update is called once per frame
    void Update()
    {
       
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
       
        verlosity2 = m.ReadValue<Vector2>();
      
            if (s.IsPressed())
            {
                speed = rspeed;
            }
            else
            {
                speed = wspeed;
            }

        
        speed2 = ((Mathf.Abs(verlosity2.y) + 0.5f) * (Mathf.Abs(verlosity2.x) + 0.5f)) * speed;
        animator.SetFloat("Speed", speed2);
        velocity.x = Mathf.Lerp(velocity.x, verlosity2.x * speed, Time.deltaTime * 20f);
        velocity.z = Mathf.Lerp(velocity.z, verlosity2.y * speed, Time.deltaTime * 20f);
        float angle = verlosity2.x * rot * Time.deltaTime;
        Vector3 dir = new Vector3(0 , verlosity2.y, 0);
        
        
        if (j.WasPressedThisFrame())
        {
            if (isGrounded)
            {
              velocity.y = Mathf.Sqrt(2f * jumpFore * -gravity);
              Debug.Log("jump");

                animator.SetTrigger("JumpTrigger");

            }
           

        }
        
        animator.SetBool("isGrounded", isGrounded);
        controller.Move(velocity * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        
       
        
    }
}
