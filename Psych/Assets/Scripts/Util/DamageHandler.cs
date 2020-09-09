using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    private Material defaultMaterial = null;
    private Material hitMaterial = null;
    private SkinnedMeshRenderer myRenderer = null;

    [SerializeField]
    private float currentWait = 0.0f;

    public void AssignEverything()
    {
        if(GetComponent<SkinnedMeshRenderer>())
        {
            myRenderer = GetComponent<SkinnedMeshRenderer>();     
        }

        if(myRenderer != null)
        {
            defaultMaterial = myRenderer.material;
        }

        hitMaterial = Resources.Load<Material>("HitMaterial");
    }

    public void ShowDamage()
    {
        if (myRenderer != null)
        {
         myRenderer.material = hitMaterial;
        }
    }

    public void ShowNormal()
    {
        if (myRenderer != null)
        {
            myRenderer.material = defaultMaterial;
        }
    }

    public IEnumerator ResetCountdown(float count)
    {
        currentWait = count;
        while (true)
        {
            count -= 1.0f * Time.deltaTime;
            currentWait = count;
            if (count <= 0)
            {
                break;
            }

            yield return null;
        }

        ShowNormal();
    }

}
