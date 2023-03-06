using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWayPointIndex = 0;
    public AnalyticsManager analyticsManager;

    [SerializeField] private float speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        if(Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < 1f)
        {
            currentWayPointIndex++;
            if(currentWayPointIndex >= waypoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }

       private void OnCollisionEnter2D(Collision2D other)
    {
        if ( other.gameObject.name == "EndGate2")
        {

            Destroy(gameObject);
            analyticsManager.SendEvent("LEVEL6 PLAYER FINISHED THROUGH YELLOW GATE");
        }
         if ( other.gameObject.name == "EndGate1")
        {

            Destroy(gameObject);
            analyticsManager.SendEvent("LEVEL6 PLAYER FINISHED THROUGH GREEN GATE");
        }
    } 
    
}
