using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    //public float moveSpeed = 10;
    //public float turnSpeed = 50;
    //public float moveBackPenalty = 3;
    //public float sideMovePenalty = 2;
    //public float sprintSpeedBoost = 5f;
    //public float rot = 0;
    //public float rotSpeed = 80;

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        moveSpeed += sprintSpeedBoost;
    //    }
    //    if (Input.GetKeyUp(KeyCode.LeftShift))
    //    {
    //        moveSpeed -= sprintSpeedBoost;
    //    }
    //    if (Input.GetKey(KeyCode.W)) //Move Forward
    //    {
    //        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
    //    }
    //    if (Input.GetKey(KeyCode.S)) //Move Back
    //    {
    //        transform.Translate(Vector3.back * ((moveSpeed - moveBackPenalty) * Time.deltaTime));
    //    }
    //    if (Input.GetKey(KeyCode.A)) //Move Left
    //    {
    //        transform.Translate(Vector3.left * ((moveSpeed - sideMovePenalty) * Time.deltaTime));
    //    }
    //    if (Input.GetKey(KeyCode.D)) //Move Right
    //    {
    //        transform.Translate(Vector3.right * ((moveSpeed - sideMovePenalty) * Time.deltaTime));
    //    }
    //}

    public float moveSpeed;
    public CharacterController playerController;
    public float jumpForce;
    public float gravityScale;

    Vector3 moveDirection;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            playerController.Move(transform.forward * (moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerController.Move(-transform.forward * (moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerController.Move(transform.right * (moveSpeed * Time.deltaTime));

        }
        if (Input.GetKey(KeyCode.A))
        {
            playerController.Move(-transform.right * (moveSpeed * Time.deltaTime));

        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime);
    }
}
