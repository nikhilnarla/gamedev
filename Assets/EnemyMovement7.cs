using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement7 : MonoBehaviour
{

    [SerializeField] private GameObject[] waypoints;
    private int currentWayPointIndex = 0;
    public AnalyticsManager analyticsManager;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWayPointIndex].transform.position, transform.position) < 1f)
        {
            currentWayPointIndex++;
            if (currentWayPointIndex >= waypoints.Length)
            {
                currentWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWayPointIndex].transform.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            analyticsManager.SendEvent("LEVEL7 ENEMY DESTROYED WITH BULLET");

            gameObject.SetActive(false);
        }
        else if (other.gameObject.tag.Equals("Player"))
        {
            // if player is hit by enemy, then it respawns to back at beginning of level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
