using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slingshot : MonoBehaviour
{
    public GameObject explosion;
    float health = 20f;
    public GameObject projectilePrefab;
    public Transform launchPoint;

    Vector2 dragStartPos;
    Vector2 dragEndPos;

    bool isDragging = false;

    public LineRenderer lineRenderer;
    public LineRenderer guidelineRenderer;

    public bool isActive;

    private MatchManager matchManager;

    
    void Start(){
        
        GameObject matchManagerGameObject = GameObject.Find("MatchManager");
        matchManager = matchManagerGameObject.GetComponent<MatchManager>();
        // Create a LineRenderer component and set its properties
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f; // Set the width of the line
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2; // Two points for start and end

        // Set the color of the line
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;

        GameObject guidelineObject = new GameObject("Guideline");
        guidelineRenderer = guidelineObject.AddComponent<LineRenderer>();
        guidelineRenderer.startWidth = 0.3f;
        guidelineRenderer.endWidth = 0.3f;
        guidelineRenderer.positionCount = 2;
        guidelineRenderer.startColor = Color.red;
        guidelineRenderer.endColor = Color.red;

        lineRenderer.sortingLayerName = "Default"; // Replace with your sorting layer name
        lineRenderer.sortingOrder = 10; // Replace with your desired order
        guidelineRenderer.sortingLayerName = "Default"; // Replace with your sorting layer name
        guidelineRenderer.sortingOrder = 10; // Replace with your desired order
    }
    void Update()
    {
        if(matchManager.timeScript.elapsedTime <0.1){
            forceGuidelineFalse();
        }
        //tragere
        if(isActive == true && matchManager.timeScript.elapsedTime>0){
        if (Input.GetMouseButtonDown(0) )
        {
            lineRenderer.enabled = true;
            guidelineRenderer.enabled = true;
            isDragging = true;
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Visualize the drag distance (optional)
            Debug.DrawLine(dragStartPos, dragEndPos, Color.red);

            UpdateTrajectoryLine();

            lineRenderer.SetPosition(0, dragStartPos);
            lineRenderer.SetPosition(1, dragEndPos);
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            lineRenderer.enabled = false;
            guidelineRenderer.enabled = false;
            
            isDragging = false;
            LaunchProjectile();
            matchManager.timeScript.StopTimer();
            isActive = false;
        }
        }

        if(health<=0){
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            
        }
    }

     void UpdateTrajectoryLine()
    {
    // Calculate and update the trajectory line based on drag distance and force
    Vector2 force = dragStartPos - dragEndPos;
    int segmentCount = 20; // Number of line segments for the trajectory line

    guidelineRenderer.positionCount = segmentCount + 1;
    guidelineRenderer.SetPosition(0,launchPoint.position);
    for (int i = 1; i <= segmentCount; i++)
    {
        float t = i / (float)segmentCount;
        Vector2 point = CalculatePointOnTrajectory(launchPoint.position, force * 5f, t);
        guidelineRenderer.SetPosition(i, point);
    }
    }

    Vector2 CalculatePointOnTrajectory(Vector2 start, Vector2 force, float t)
    {
        // Calculate the position of a point on the trajectory using quadratic motion equations
        float x = start.x + force.x * t;
        float y = start.y + force.y * t + 0.5f * Physics2D.gravity.y * t * t;
        return new Vector2(x, y);
    }

    public void forceGuidelineFalse(){
        guidelineRenderer.enabled = false;
        lineRenderer.enabled = false;
        
    }

    GameObject newProjectile;
    void LaunchProjectile()
    {
        Vector2 force = dragStartPos - dragEndPos;
        newProjectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

        CircleCollider2D circleCollider = newProjectile.GetComponent<CircleCollider2D>();
        BoxCollider2D boxCollider = this.GetComponent<BoxCollider2D>();

        if (circleCollider != null && boxCollider != null)
        {
            Physics2D.IgnoreCollision(circleCollider, boxCollider);
        }
        newProjectile.GetComponent<Rigidbody2D>().AddForce(force * 5f, ForceMode2D.Impulse);
        matchManager.NextRound();

        // Physics.IgnoreCollision(newProjectile.GetComponent<BoxCollider>(), collider2);
    }

      void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionForce = collision.relativeVelocity.magnitude;
        health -= collisionForce;
        Debug.Log(health);
        if(collision.gameObject.CompareTag("Projectile")) Destroy(this.gameObject);

    }

}
