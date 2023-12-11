using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    public GameObject projectilePrefab; // Referință către prefab-ul proiectilului
    public Transform shotSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Call your function here
            activate();
        }
    }

    

    public void activate(){
        float aux = -15f;
        for (int i = 0; i < 3; i++)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, shotSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

            // Modifică forța și direcția pentru fiecare proiectil
            
            Vector2 direction = Quaternion.Euler(0, 0, aux) * Vector2.down; // Modifică unghiul direcției
            aux = aux + 15f;
            rb.AddForce(direction * 5000f); // Modifică forța de mișcare a proiectilului
            
        }
        Destroy(this.gameObject);
    }

    public float destroyDelay = 2.0f; // Delay before destroying the object

    private void OnCollisionEnter(Collision collision)
    {
        
            // Invoke the DestroyAfterDelay function after the specified delay
            Invoke("DestroyAfterDelay", destroyDelay);
        
    }

    void DestroyAfterDelay()
    {
        // Destroy the object after the delay
        Destroy(this);
    }
}
