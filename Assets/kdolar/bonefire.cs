using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonefire : MonoBehaviour
{
    void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<checkpoint>().cpnoktalar�.Contains(this.transform.position))
        {
            foreach (Transform child in transform)
            {
                // �ocuk nesneyi devre d��� b�rak�r
                child.gameObject.SetActive(false);
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

          
            
            
            
            foreach (Transform child in transform)
            {
                // �ocuk nesneyi devre d��� b�rak�r
                child.gameObject.SetActive(true);
            }
        }
    }
}
