using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public List<GameObject> TextObjects;
    public List<Transform> TextSpawn;

    private float timer = 4.0f;
    public void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //check if all TextObjects are off screen
            //if yes spawn new objects delete old ones
            foreach (GameObject GO in TextObjects)
            {
                int Pos = TextObjects.IndexOf(GO);

                if (GO.GetComponent<Renderer>().isVisible)
                {
                    //Debug.LogError("Still Visible");
                }
                else
                {
                    //Debug.Log("Exists?");
                    GO.SetActive(false);
                    GO.GetComponent<Transform>().transform.position = TextSpawn[Pos].transform.position;
                    GO.SetActive(true);
                    //TextObject[GO] = Temp;
                    
                    //Instantiate(Temp, TextSpawn[Pos], false);
                    
                }

            }
            timer = 4.0f;
        }
    }
}
