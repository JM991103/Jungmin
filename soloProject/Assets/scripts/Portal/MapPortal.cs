using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPortal : MonoBehaviour
{
    public string loadScene;
    public GameObject spawnPrefab;
    Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {            
            SceneManager.LoadScene(loadScene);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameManager.Inst.Player;
        player.transform.position = spawnPrefab.transform.position;
    }
}
