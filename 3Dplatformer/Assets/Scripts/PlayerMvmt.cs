using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMvmt : MonoBehaviour
{
    private Rigidbody thisRb;
    private float rotate;
    private float forward;
    private float jump;
    private bool isGrounded;
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] float gravityScale;
    [SerializeField] float jumpSpd;
    public TextMeshProUGUI instruction;

    void Start()
    {
        thisRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rotate = Input.GetAxis("Horizontal");
        forward = Input.GetAxis("Vertical");
        jump = Input.GetAxis("Jump");
        thisRb.MovePosition(thisRb.position + (transform.forward * speed * forward * Time.deltaTime));
        thisRb.MoveRotation(Quaternion.AngleAxis(rotate * turnSpeed, Vector3.up) * thisRb.rotation);

        thisRb.AddForce(new Vector3(0, gravityScale, 0), ForceMode.Acceleration);

        if (isGrounded == true)
        {
            thisRb.AddForce(Vector3.up * jump * jumpSpd);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "Candy")
        {
            instruction.text = "";
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Candy")
        {
            instruction.text = "Press E to eat candy";
            if(Input.GetKeyDown(KeyCode.E))
            {
                collision.gameObject.SetActive(false);
            }
        }
    }

}
