using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillset : MonoBehaviour
{
    bool �stte = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Gir()
    {
        if(�stte == false)
        {
            this.GetComponent<Animator>().SetTrigger("panelgir");
            �stte = true; 
        }
    }
    public void ��k()
    {
        if (�stte)
        {
            this.GetComponent<Animator>().SetTrigger("panelc�k");
            �stte = false;
        }
    }
}
