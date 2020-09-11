using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    #region Variables

    public bool useMaterial = false;

    Renderer objRend;

    Material defaultMaterial;
    Material highlightMaterial;

    Color startColor;
    Rigidbody myRigidBody;
    #endregion

    void Start()
    {
        objRend = GetComponent<Renderer>();
        if (objRend == null) // Checking if its still null after the parent assignment, if yes. Use the child renderererer
        {
            objRend = GetComponentInChildren<Renderer>();
        }
        if (myRigidBody == null)
        {
            myRigidBody = GetComponent<Rigidbody>();
        }
        startColor = objRend.material.color;
        defaultMaterial = objRend.material;
    }

    void OnMouseEnter()
    {
        if (useMaterial == true)
        {
            if (highlightMaterial != null)
            {
                objRend.material = highlightMaterial;
            }
            else
            {
                objRend.material.color = Color.yellow;
            }
        }
        else
        {
            objRend.material.color = Color.yellow;
        }
    }

    void OnMouseExit()
    {
        if (useMaterial == true)
        {
            objRend.material = defaultMaterial;
        }
        else
        {
            objRend.material.color = startColor;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.collider.gameObject.layer < 8)
            {

                if (myRigidBody != null && !collision.collider.gameObject.GetComponent<Generator>() && collision.collider.gameObject.tag != "Player" && !collision.collider.gameObject.GetComponent<Bullet>() && collision.collider.gameObject.tag != "Projectile")
                {
                    Vector3 storedVelocity = myRigidBody.velocity;
                    myRigidBody.velocity = Vector3.zero;
                    if (myRigidBody.isKinematic == false && storedVelocity.sqrMagnitude > 0)
                    {

                        myRigidBody.AddForce(storedVelocity.x * -0.5f * Time.fixedDeltaTime, storedVelocity.y * -0.5f * Time.fixedDeltaTime, storedVelocity.z * -0.5f * Time.fixedDeltaTime, ForceMode.VelocityChange);
                    }
                }
            }


        }
    }
}
