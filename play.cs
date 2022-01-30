using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource vic;
    void Start()
    {
        vic=GetComponent<AudioSource>();
    }
    private void victory()
    {
        vic.Play();
    }
}
