using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance; 
    
    public List<GameObject> chunks;
    public int maxChunks = 32;
    int startChunk;
    int activeChunk;
    bool newChunk;

    //Need to add Chunk info here


    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }

        chunks = new List<GameObject>();
        startChunk = maxChunks;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
