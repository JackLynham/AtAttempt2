using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]private GameObject playerPrefab;
    public GameObject Player;
    public int x;
    public int y;

    public Camera camera;
    public Canvas canvas;
    void Start()
    { 
        
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChunkManager.instance.StartGame();
    
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerManager.instance.SpawnPlayer();
            Destroy(camera);
            Destroy(canvas);
        }

  
    }
}
