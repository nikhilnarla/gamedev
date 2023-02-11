using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance); // objects do not destroy themselves between scenes
        } else {
            Destroy(gameObject); // don't want multiple gameMasters in same scene
        }
    }
}
