using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    public float maxSpeed;

    public float sliding;

    private int desiredLane = 1; //0: left, 1: middle, 2: right
    public float laneDistance = 400; //the distance between two lane

    public float jumpForce;
    public float gravity = -300;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;
        
        // increase speed
        if (forwardSpeed < maxSpeed) 
            forwardSpeed += 5f * Time.deltaTime; 
        
        animator.SetBool("isIdle", true);
        
        direction.z = forwardSpeed;
        
        // isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        
        if (controller.isGrounded)
        {
            direction.y = -2;
            if (SwipeManager.swipeUp)
                Jump();
        }
        else 
            direction.y += gravity * Time.deltaTime;
        //Gather the input of the direction on which lane we should be


        if (SwipeManager.swipeDown);
            StartCoroutine(Slide());
       
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //Calculate 

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }else if (desiredLane == 2){
            targetPosition += Vector3.right * laneDistance;
        }

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 650 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude) 
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }

    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        animator.SetBool("isSlide", true);
        controller.center = new Vector3(0, -0.32f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(10f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animator.SetBool("isSlide", false);
    }
}
