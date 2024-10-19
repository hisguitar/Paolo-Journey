using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
	[SerializeField] public Animator animator; // Put player animator to this

	private Rigidbody2D _rb;
	private Vector2 moveInput;
	private PlayerInput playerInput;
	private static readonly int Speed = Animator.StringToHash("Speed"); // Speed parameter in animator

	// Awake, Update and FixedUpdate Methods ---------------------------------------------------------------------------
	// Awake is called before start
	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
		playerInput = new PlayerInput();
		//animator.GetComponent<Animator>();
	}
	
	private void OnEnable()
	{
		playerInput.Gameplay.Enable();
		playerInput.Gameplay.Move.performed += OnMove;
		playerInput.Gameplay.Move.canceled += OnMove;
	}
	
	private void OnDisable()
	{
		playerInput.Gameplay.Move.performed -= OnMove;
		playerInput.Gameplay.Move.canceled -= OnMove;
		playerInput.Gameplay.Disable();
	}
	
	private void OnMove(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
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
		float movement = moveInput.x;
		
		// Setting float to show walking animation
		animator.SetFloat(Speed, Mathf.Abs(movement));
		
		// Character walk
		transform.position += new Vector3(movement, 0, 0) * (Time.deltaTime * movementSpeed);
		/*// Character Rotation
		if (!Mathf.Approximately(0, movement))
		{
			transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
		}*/
		// Character Rotation (localScale method to flip character)
		if (movement > 0)   
		{
			transform.localScale = new Vector3(4.5f, 4.5f, 1); // Face right
		}
		else if (movement < 0)
		{
			transform.localScale = new Vector3(-4.5f, 4.5f, 1); // Face left
		}
		// Character Jump
		if (Input.GetButton("Jump") && Mathf.Abs(_rb.velocity.y) < 0.001f)
		{
			_rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			//SoundManager.instance.Play(SoundManager.SoundName.JumpEffect);
		}
	}
}