using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    private GameObject cam;
    [SerializeField] private float parallaxeffect;

    void OnEnable()
    {
        startpos = transform.position.y + 10;
        cam = Camera.main.gameObject;
        //cam = GameObject.FindGameObjectWithTag("Player");
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float temp = (cam.transform.position.y * (1 - parallaxeffect));
        float dist = (cam.transform.position.y * parallaxeffect);

        transform.position = new Vector3(transform.position.x, startpos + dist, transform.position.z);

        if (temp > startpos + length)
            startpos += length;
        else if (temp < startpos - length) 
            startpos -= length;
    }
}
