using UnityEngine;
using UnityEngine.SceneManagement;

public class Beam : MonoBehaviour
{
    public AnalyticsManager analyticsManager;
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
            analyticsManager.SendEvent("LEVEL7 PLAYER GOT KILLED BY LASER BEAM");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
