using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    CharacterController cc;
    float moveSpeed = 5.0f;
    float gravity = 9.8f;

    Vector3 moveDir = Vector3.zero;
    float moveX;
    float moveY;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVector = input.Get<Vector2>();

        moveX = inputVector.x;
        moveY = inputVector.y;
    }

    public void OnNormalAttack()
    {

    }

    public void OnDash()
    {

    }

    private void FixedUpdate()
    {
        if(cc.isGrounded)
        {
            moveDir = new Vector3(moveX, 0, moveY);

            moveDir *= moveSpeed;

            if(moveDir.magnitude > float.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(moveDir);
            }
        }

        moveDir.y -= gravity * Time.fixedDeltaTime;
        cc.Move(moveDir * Time.fixedDeltaTime);
    }
}
