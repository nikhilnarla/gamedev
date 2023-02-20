using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaRespawn : MonoBehaviour
{
    public Transform LavaSpawnPoint;
    public AnalyticsManager analyticsManager;
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D Collision)
    {
      if (Collision.gameObject.tag == ("LavaParticle"))
      {
          //Debug.Log("LAVA");
          player.transform.position = LavaSpawnPoint.position;
          analyticsManager.SendEvent("LEVEL6 PLAYER FELL INTO LAVA - RESPAWNED");
      }
    }
}
