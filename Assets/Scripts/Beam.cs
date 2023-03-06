using UnityEngine;
using UnityEngine.SceneManagement;

public class Beam : MonoBehaviour
{
    public AnalyticsManager analyticsManager;
    public Transform TunnelSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            
            // if player is hit by laser, then it respawns to back at beginning of level
            if (SceneManager.GetActiveScene().name == "Level6Tunnel1")
            {
                other.gameObject.transform.position = TunnelSpawnPoint.position;
                analyticsManager.SendEvent("LEVEL6 PLAYER KILLED BY LASER BEAM IN GREEN TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }

            if (SceneManager.GetActiveScene().name == "Level7")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                analyticsManager.SendEvent("LEVEL7 PLAYER GOT KILLED BY LASER BEAM");
                analyticsManager.SendEvent("LEVEL7 GAMESTART AGAIN");
            }

            if (SceneManager.GetActiveScene().name == "Level7-Tunnel")
            {
                other.gameObject.transform.position = TunnelSpawnPoint.position;
                analyticsManager.SendEvent("LEVEL7 PLAYER KILLED BY LASER BEAM IN YELLOW TUNNEL AT POSITION:" + GameObject.Find("Player").GetComponent<Rigidbody2D>().position);
            }


        }
    }
}
