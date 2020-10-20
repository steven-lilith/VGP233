using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum JumpState
    {
        Grounded,
        Jumping,
    }
    public float speed = 10.0f;
    public float jumpForce = 1000.0f;
    public int score;
    private Rigidbody rb;
    private UIManager uiManager;
    private JumpState jumpState = JumpState.Grounded;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        ServiceLocator.Register<UIManager>(uiManager);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
    }


    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        if(score == 100)
        {
            Debug.Log("You win!");
        }
        Move();
    }
    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
    

        Vector3 movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);
        rb.AddForce(movement);
    }

    private void Jump()
    {
        float jumpValue = Input.GetKeyDown(KeyCode.Space) ? jumpForce : 0.0f;
        switch(jumpState)
        {
            case JumpState.Grounded:
                if(jumpValue>0.0f)
                {
                    jumpState = JumpState.Jumping;
                }
                break;
            case JumpState.Jumping:
                if(jumpValue>0.0f)
                {
                    jumpValue = 0.0f;
                }
                break;
        }
        Vector3 jumping = new Vector3(0.0f, jumpValue, 0.0f);
        rb.AddForce(jumping);
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if(jumpState==JumpState.Jumping && collision.gameObject.CompareTag("Ground"))
        {
            jumpState = JumpState.Grounded;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PickUp pickUp = other.gameObject.GetComponent<PickUp>();
            if(pickUp!=null)
            {
                score += pickUp.Collect();
                ServiceLocator.Get<UIManager>().UpdateScoreDisplay(score);
            }
        }
        if(other.gameObject.CompareTag("Hazard"))
        {
            Debug.Log("you lose");
        }
    }
}
