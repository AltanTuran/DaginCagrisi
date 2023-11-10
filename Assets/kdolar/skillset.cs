using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillset : MonoBehaviour
{
    bool üstte = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gir()
    {
        if(üstte == false)
        {
            this.GetComponent<Animator>().SetTrigger("panelgir");
            üstte = true; 
        }
    }
    public void Çýk()
    {
        if (üstte)
        {
            this.GetComponent<Animator>().SetTrigger("panelcýk");
            üstte = false;
        }
    }
}
