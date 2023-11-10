using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karakteryerkontorl : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("yer"))
        {
            this.gameObject.GetComponentInParent<karakter>().dusmanyukarýbakma = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("yer"))
        {
            this.gameObject.GetComponentInParent<karakter>().dusmanyukarýbakma = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("yer"))
        {
            this.gameObject.GetComponentInParent<karakter>().dusmanyukarýbakma = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("yer"))
        {
            this.gameObject.GetComponentInParent<karakter>().dusmanyukarýbakma = true;
        }
    }
}
