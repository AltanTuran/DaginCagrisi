using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private static checkpoint instance;
    public Transform lastcheckpoint;
    public List<Vector2> cpnoktalar�;
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

        cpnoktalar� = GameObject.FindGameObjectWithTag("Player").GetComponent<karakter>().cpnoktalar�; 

    }
}
