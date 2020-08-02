using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;
    public List<GameObject> chunkList;
    public int chunkNumber;
    public int mapChunkTotal = 16;
    public int startChunk;
    [SerializeField] private int playerActiveChunk;
    private bool newActiveChunk = true;
    public Chunk activeChunk;

    public float verticySpaceing;
    public float chunkSize;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        chunkList = new List<GameObject>();
        startChunk = mapChunkTotal / 4;
        playerActiveChunk = startChunk;
        newActiveChunk = true;
    }

    void Start()
    {
        int size = (int)Mathf.Sqrt(mapChunkTotal);
    }

    public void StartGame()
    {
        GenerateChunk(3, 2);
        activeChunk = chunkList[1].GetComponent<Chunk>();
        newActiveChunk = true;
    }

    private void Update()
    {
        if (chunkList.Count != 0 && PlayerManager.instance.Player != null)
        {
            if (activeChunk == null)
            {
                activeChunk = chunkList[0].GetComponent<Chunk>();
            }
            else
            {
                foreach (GameObject c in chunkList.ToArray())
                {
                    if (Vector3.Distance(c.gameObject.transform.position, PlayerManager.instance.GetPlayer().transform.position) >
                        (c.GetComponent<Chunk>().GetWorldSpaceBounds().size.x * 3))
                    {
                        chunkList.Remove(c);
                        c.GetComponent<Chunk>().UnloadChunk();
                    }
                }

                //Checks if player has left activechunk.
                if (!PlayerInside())
                {

                    FindActiveChunk();
                }

                if (newActiveChunk)
                {

                    LoadNeighbours();
                    newActiveChunk = false;
                }
            }
        }
    }

    public void AddChunkToList(GameObject chunk)
    {
        chunkList.Add(chunk);
    }

    public GameObject GenerateChunk(int x, int z)
    {
        //Check if chunk doesnt exist.
        GameObject newChunk = new GameObject("Chunk " + x.ToString() + z.ToString());
        newChunk.AddComponent<Chunk>();
        newChunk.AddComponent<ThreadQueuer>();
        newChunk.GetComponent<Chunk>().CreateChunk(x, z);
        return newChunk;
    }

    bool ChunkExists(int x, int z)
    {
        bool exists = false;
        foreach (GameObject go in chunkList.ToArray())
        {
            Vector2 tempArrayPos = new Vector2(x, z);
            ChunkData test = go.GetComponent<Chunk>().data;
            Vector2 arrayPos2 = new Vector2(go.GetComponent<Chunk>().data.arrayPos.x, go.GetComponent<Chunk>().data.arrayPos.y);
            if (arrayPos2 == tempArrayPos)
            {
                exists = true;
            }
        }
        return exists;
    }

    void AssignActiveChunk()
    {
        foreach (GameObject c in chunkList.ToArray())
        {
            if (c.GetComponent<Chunk>().GetWorldSpaceBounds().Contains(PlayerManager.instance.GetPlayer().transform.position))
            {
                activeChunk = c.GetComponent<Chunk>();
                newActiveChunk = true;
            }
        }
    }

    void FindActiveChunk()
    {
        foreach (GameObject c in chunkList.ToArray())
        {
            Vector3 playerPos = PlayerManager.instance.GetPlayer().gameObject.transform.position;
            Vector3 chunkPos = c.GetComponent<Chunk>().gameObject.transform.position;
            Bounds chunkSize = c.GetComponent<Chunk>().GetWorldSpaceBounds();
            if (playerPos.x >= chunkPos.x && playerPos.x <= chunkPos.x + chunkSize.size.x &&
                playerPos.z >= chunkPos.z && playerPos.z <= chunkPos.z + chunkSize.size.z)
            {
                activeChunk = c.GetComponent<Chunk>();
                newActiveChunk = true;

            }
        }
    }

    bool PlayerInside()
    {
        Vector3 playerPos = PlayerManager.instance.GetPlayer().gameObject.transform.position;
        Vector3 chunkPos = activeChunk.GetComponent<Chunk>().gameObject.transform.position;
        Bounds chunkSize = activeChunk.GetComponent<Chunk>().GetWorldSpaceBounds();
        if (playerPos.x >= chunkPos.x && playerPos.x <= chunkPos.x + chunkSize.size.x &&
            playerPos.z >= chunkPos.z && playerPos.z <= chunkPos.z + chunkSize.size.z)
        {
            return true;
        }
        else
        {

            return false;
        }
    }

    void LoadNeighbours()
    {

        for (int i = 0; i < activeChunk.data.chunkNeighbour.Length; ++i)
        {
            if (activeChunk.data.chunkNeighbour[i].x != -1 || activeChunk.data.chunkNeighbour[i].y != -1)
            {
                if (!ChunkExists((int)activeChunk.data.chunkNeighbour[i].x, (int)activeChunk.data.chunkNeighbour[i].y))
                {
                    GenerateChunk((int)activeChunk.data.chunkNeighbour[i].x, (int)activeChunk.data.chunkNeighbour[i].y);
                }
            }
        }
    }
}