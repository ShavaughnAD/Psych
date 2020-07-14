using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    #region Variables

    public bool useMaterial = false;

    Renderer objRend;

    Material defaultMaterial;
    Material highlightMaterial;

    Color startColor;

    #endregion

    void Start()
    {
        objRend = GetComponent<Renderer>();
        if(objRend == null) // Checking if its still null after the parent assignment, if yes. Use the child renderererer
        {
            objRend = GetComponentInChildren<Renderer>();
        }
        startColor = objRend.material.color;
        defaultMaterial = objRend.material;
    }

    void OnMouseEnter()
    {
        if (useMaterial == true)
        {
            if(highlightMaterial != null)
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
}
