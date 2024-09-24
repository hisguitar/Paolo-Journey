using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    /* YOU HAVE TO CREATE NEW OBJECT THEN ADD "RIGIDBODY 2D, SPRITE RENDERER, ANIMATOR" COMPONENT
     * AND PUT THIS SCRIPT TO IT
     * THEN YOU HAVE TO CREATE CHILDREN OBJECT..
     * - PLAYER COLLIDER THAT HAVE "BOX COLLIDER 2D"
     * - LIGHT 2D FOR BEAUTIFUL CHARACTER! */
    [Header("Movement Variables")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    
    [Header("Other Variables (Animations, Projectile)")]
    //[SerializeField] public Animator animator; // Put player animator to this

    private Rigidbody2D _rb;
    private static readonly int Speed = Animator.StringToHash("Speed"); // Speed parameter in animator

    // Awake, Update and FixedUpdate Methods ---------------------------------------------------------------------------
    // Awake is called before start
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        //animator.GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        Move();
    }

    // Move Method -----------------------------------------------------------------------------------------------------
    // ReSharper disable Unity.PerformanceAnalysis
    private void Move()
    {
        // Store the value of the input in horizontal format in the movement variable
        float movement = Input.GetAxis("Horizontal");
        // Setting float to show walking animation
        //animator.SetFloat(Speed, Mathf.Abs(movement));
        
        // Character walk
        transform.position += new Vector3(movement, 0, 0) * (Time.deltaTime * movementSpeed);
        // Character Rotation
        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
        // Character Jump
        if (Input.GetButton("Jump") && Mathf.Abs(_rb.velocity.y) < 0.001f)
        {
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            //SoundManager.instance.Play(SoundManager.SoundName.JumpEffect);
        }
    }
}