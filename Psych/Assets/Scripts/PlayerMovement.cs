using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10;
    public float turnSpeed = 50;
    public float moveBackPenalty = 3;
    public float sideMovePenalty = 2;
    public float sprintSpeedBoost = 5f;
    public float rot = 0;
    public float rotSpeed = 80;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed += sprintSpeedBoost;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed -= sprintSpeedBoost;
        }
        if (Input.GetKey(KeyCode.W)) //Move Forward
        {
            transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S)) //Move Back
        {
            transform.Translate(Vector3.back * ((moveSpeed - moveBackPenalty) * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A)) //Move Left
        {
            transform.Translate(Vector3.left * ((moveSpeed - sideMovePenalty) * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D)) //Move Right
        {
            transform.Translate(Vector3.right * ((moveSpeed - sideMovePenalty) * Time.deltaTime));
        }
    }
}
