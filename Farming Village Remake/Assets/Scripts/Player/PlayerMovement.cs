using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public VisualEffect VFX_FoggyCollider;

    public float moveSpeed_Walk = 5;
    public float moveSpeed_Sprint = 10;

    private Vector2 _moveDirection;
    private float _currentSpeed;

    public InputActionReference move;
    public InputActionReference sprint;

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
        if (sprint != null && sprint.action.IsPressed())
        {
            _currentSpeed = moveSpeed_Sprint;
        }
        else
        {
            _currentSpeed = moveSpeed_Walk;
        }

        if (move != null)
        {
            _moveDirection = move.action.ReadValue<Vector2>();
        }

        VFX_FoggyCollider.SetVector3("Collider Position", transform.position);
    }

    private void FixedUpdate()
    {
        rb.velocity = _moveDirection * _currentSpeed;
    }
}
