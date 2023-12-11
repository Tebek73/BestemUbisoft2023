using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float destroyDelay; // Delay before destroying the object

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Invoke("DestroyAfterDelay", destroyDelay);
    }


    void DestroyAfterDelay()
    {
        // Destroy the object after the delay
        Destroy(this.gameObject);
    }
}
