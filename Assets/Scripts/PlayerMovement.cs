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

    float dashSpeed = 6.0f;
    float maxDashtime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
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
        StartCoroutine(Dashing());
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

    IEnumerator Dashing()
    {
        //took inspiration from the answer to this question: https://forum.unity.com/threads/adding-a-dash-in-character-controller.878986/
        //gets the time at the frame when dashing starts
        float startDashTime = Time.time;

        //only allows the dash to happen for the time specified by maxDashTime
        while(Time.time < startDashTime + maxDashtime)
        {
            cc.Move(moveDir * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
