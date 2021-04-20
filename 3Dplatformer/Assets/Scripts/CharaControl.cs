using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaControl : MonoBehaviour
{
    private CharacterController CC;
    private float xMove;
    private float yMove;
    private bool jump;
    private Vector3 MoveDir = Vector3.zero;
    private float rotateSpd;

    [SerializeField]
    float speed = 6f;
    [SerializeField]
    float jumpSpd = 1f;
    [SerializeField]
    float gravity = 9.8f;

    void Start()
    {
        CC = GetComponent<CharacterController>();

    }

    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        if(CC.isGrounded)
        {
            MoveDir = new Vector3(xMove,0f,yMove).normalized;
            MoveDir *= speed;
            if (MoveDir.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(MoveDir.x, MoveDir.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            }
            if(jump)
            {
                MoveDir.y = jumpSpd;
            }

        }
        MoveDir.y -= gravity * Time.fixedDeltaTime;
        CC.Move(MoveDir * Time.fixedDeltaTime);

    }
}
