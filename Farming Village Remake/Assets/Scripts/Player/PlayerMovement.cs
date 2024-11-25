using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator _animator;
    public VisualEffect VFX_FoggyCollider;
    public VisualEffect VFX_RainCollider;
     public VisualEffect VFX_SnowCollider;

    public float moveSpeed_Walk = 5;
    public float moveSpeed_Sprint = 10;

    private Vector2 _moveDirection;
    private float _currentSpeed;

    public InputActionReference move;
    public InputActionReference sprint;
    [SerializeField] private DialogueUI dialogueUI;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        if (move != null)
            move.action.Enable();

        if (sprint != null)
            sprint.action.Enable();
    }

    private void OnDisable()
    {
        if (move != null)
            move.action.Disable();

        if (sprint != null)
            sprint.action.Disable();
    }


    private void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.F))
        {
            if (!dialogueUI.IsOpen)
            {
                Interactable?.Interact(player: this);
            }
        }*/

        // Determine movement speed based on sprint input
        _currentSpeed = (sprint != null && sprint.action.IsPressed()) ? moveSpeed_Sprint : moveSpeed_Walk;

        // Read movement input
        if (move != null)
        {
            _moveDirection = move.action.ReadValue<Vector2>();

            // Set animation parameters
            if (_moveDirection.magnitude > 0.1f)
            {
                _animator.SetBool("isWalking", true);
                _animator.SetFloat("InputX", _moveDirection.x);
                _animator.SetFloat("InputY", _moveDirection.y);

                // Update last input direction to remember the direction the player was moving
                _animator.SetFloat("LastInputX", _moveDirection.x);
                _animator.SetFloat("LastInputY", _moveDirection.y);
            }
            else
            {
                _animator.SetBool("isWalking", false);
            }
        }

        Vector3 playerPosition = transform.position;
        VFX_FoggyCollider.SetVector3("Collider Position", playerPosition);
        VFX_SnowCollider.SetVector3("Collider Position", playerPosition);
    }


    private void FixedUpdate()
    {
        if (dialogueUI.IsOpen) return;

        rb.velocity = _moveDirection * _currentSpeed;
    }

    public void InteractDialogue()
    {
        if (!dialogueUI.IsOpen)
        {
            _moveDirection = Vector2.zero;
            rb.velocity = Vector2.zero;
            Interactable?.Interact(player: this);

        }
    }

}
