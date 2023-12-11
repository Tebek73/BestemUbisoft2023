using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    private int health;
    private int initialHealth;

    private float thickness;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    // Start is called before the first frame update
    void Start()
    {
         spriteRenderer = GetComponent<SpriteRenderer>();

        float width = spriteRenderer.bounds.size.x;
        float height = spriteRenderer.bounds.size.y;
        thickness = (width>height)?height:width  * 10f;
    

        health = (int)thickness;
        initialHealth = health;
        
    }

    // Update is called once per frame
    void Update()
    {
        double healthProcentage = (health!=0)?(initialHealth / health) * 100:0;
       
        if(health == initialHealth){
            spriteRenderer.sprite = sprite1;
        }
        else if(healthProcentage>=25){
            spriteRenderer.sprite = sprite2;
        }
        else{
            spriteRenderer.sprite = sprite3;
        }
    }

    void Damage(int h){
        health -= h;
    }
   void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the "projectile" tag
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // Get the collision force
            float collisionForce = collision.relativeVelocity.magnitude;
            
            health--;
            if(health<=0){
            // Destroy the projectile
            Invoke("DestroyThis",0.1f);
            }
        }
        
    }

    void DestroyThis(){
        Destroy(this.gameObject);
    }
}
