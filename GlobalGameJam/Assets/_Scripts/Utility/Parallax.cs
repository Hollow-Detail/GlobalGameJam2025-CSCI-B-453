using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    private GameObject cam;
    /// <summary>
    /// Values greater than 0 will make the object move slower relative to the camera (further away)
    /// Values less than 0 will make the object move faster realtive to the camera (foreground)
    /// </summary>
    [SerializeField] private float parallaxeffect;
    [SerializeField] private bool isLooping; // Enable if you want the parallax object to loop
    [SerializeField] private bool destroyAfterUse; // Enable if you want the parallax object to be destroyed

    private int numUses;
    void Start()
    {
        startpos = transform.position.y;
        cam = Camera.main.gameObject;
        //cam = GameObject.FindGameObjectWithTag("Player");
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float temp = (cam.transform.position.y * (1 - parallaxeffect));
        float dist = (cam.transform.position.y * parallaxeffect);

        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);


        if (numUses == 0)
        {
            if (temp > startpos + length)
                startpos += length * 2;
            else if (temp < startpos - length) 
                startpos -= length * 2;
        }
        else
        {
            if (isLooping)
            {
                if (temp > startpos + length)
                    startpos += length * 2;
                else if (temp < startpos - length) 
                    startpos -= length * 2;
            }

            if (destroyAfterUse)
            {
                if (temp > startpos + length)
                    Destroy(gameObject);
                else if (temp < startpos - length) 
                    Destroy(gameObject);
            }
        }
       
        
        

    }
}
