using UnityEngine;
using UnityEngine.InputSystem;


public class playermove : MonoBehaviour
{
    [SerializeField] private InputActionAsset input;
    [SerializeField] private string Action = "Player1";
    private InputActionMap map;

    private InputAction m;
    private InputAction j;
    private InputAction s;


    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpFore = 2.5f;

 
    Vector2 verlosity2;

   [SerializeField] Vector3 velocity;
    [SerializeField] bool isGrounded;
    private void Awake()
    {
        map = input.FindActionMap(Action);
        m = map.FindAction("Move");
        j =  map.FindAction("Jump");
        s = map.FindAction("Sprint");
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
        velocity.x = Mathf.Lerp(velocity.x, verlosity2.x * speed, Time.deltaTime * 20f);
        velocity.z = Mathf.Lerp(velocity.z, verlosity2.y * speed, Time.deltaTime * 20f);

        if (j.WasReleasedThisFrame())
        {
            if (isGrounded)
            {
              velocity.y = Mathf.Sqrt(2f * jumpFore * -gravity);
              Debug.Log("jump");

            }
        }
        controller.Move(velocity * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        
       
        
    }
}
