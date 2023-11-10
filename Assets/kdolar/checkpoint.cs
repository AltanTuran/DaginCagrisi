using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private static checkpoint instance;
    public Transform lastcheckpoint;
    public List<Vector2> cpnoktalarý;
    public bool cpsilme;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else { Destroy(gameObject); }
        
    
    }


    void Start()
    {
         


    }

    // Update is called once per frame
    void Update()
    {

        cpsilme = GameObject.FindGameObjectWithTag("Player").GetComponent<karakter>().cpsilme;

        cpnoktalarý = GameObject.FindGameObjectWithTag("Player").GetComponent<karakter>().cpnoktalarý; 

    }
}
