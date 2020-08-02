using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class Chunk : MonoBehaviour
{
    public ChunkData data;
    [SerializeField] new MeshCollider collider;
    public Mesh mesh;
    [SerializeField]private int chunkID;
    private ThreadQueuer thread;

    void Start()
    {
        thread = gameObject.GetComponent<ThreadQueuer>();
    }

    public void CreateChunk(int x, int z)
    {
        data = new ChunkData();
        thread = gameObject.GetComponent<ThreadQueuer>();
 
        if (DataManager.FileExist(x,z) )
        {
            gameObject.GetComponent<MeshRenderer>().material = ColourMapData.instance.mat;
            LoadChunk(x,z);
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = ColourMapData.instance.mat;
            data.size = (int)ChunkManager.instance.chunkSize;
            SetChunkPosition(x,z);
            GetcolourMap();
            CreateMesh();

            data.position.x = transform.position.x;
            data.position.z = transform.position.z;
            SetNeighbours();
            CreateJSONFile(x,z);
            ChunkManager.instance.AddChunkToList(gameObject);
        }
    }
    private void CreateMesh()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        collider = GetComponent<MeshCollider>();
        mesh.name = " Map";
        data.vertices = new Vector3[(data.size + 1) * (data.size + 1)];
        data.uv = new Vector2[data.vertices.Length];
        data.tangents = new Vector4[data.vertices.Length];
        data.tangent = new Vector4(1f, 0f, 0f, -1f);

        for (int i = 0, z = 0; z <= data.size; z++)
        {
            for (int x = 0; x <= data.size ; x++, i++)
            {
                data.vertices[i] = new Vector3(x * ChunkManager.instance.verticySpaceing, 0, z*ChunkManager.instance.verticySpaceing);
                data.uv[i] = new Vector2((float)x / data.size, (float)z / data.size);
                data.tangents[i] = data.tangent;
            }
        }

        mesh.vertices = data.vertices;
        mesh.uv = data.uv;
        mesh.tangents = data.tangents;
        data.triangles = new int[data.size * data.size * 6];
        for (int ti = 0, vi = 0, z = 0; z < data.size; z++, vi++)
        {
            for (int x = 0; x < data.size; x++, ti += 6, vi++)
            {
                data.triangles[ti] = vi;
                data.triangles[ti + 3] = data.triangles[ti + 2] = vi + 1;
                data.triangles[ti + 4] = data.triangles[ti + 1] = vi + data.size + 1;
                data.triangles[ti + 5] = vi + data.size + 2;
            }
        }
        mesh.colors = data.meshColor;
        mesh.triangles = data.triangles;
        mesh.RecalculateNormals();
        collider.sharedMesh = mesh;
    }

    private void GetcolourMap()
    {
        data.meshColor = new Color[(data.size + 1) * (data.size + 1)];
        int colourCount = 0;
        Color assignedColour = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));

        for (int z = 0; z <= data.size; ++z)
        {
            for (int x = 0; x <= data.size; ++x)
            {
                data.meshColor[colourCount] = assignedColour;
                ++colourCount;
            }
        }
    }


    void SetChunkPosition(int x, int z)
    {
        data.arrayPos = new Vector2(x, z);

        gameObject.transform.position = new Vector3((x * (data.size * ChunkManager.instance.verticySpaceing)), 0, 
            z * (data.size * ChunkManager.instance.verticySpaceing));
    }

    public void UnloadChunk()
    {
        string path = DataManager.CreateFilepath((int)data.arrayPos.x, (int)data.arrayPos.y);
        thread.StartThreadedFunction(() => { SaveData(this.data, path); });
       
    }

    void DestroyChunk()
    {
        Destroy(this.gameObject);
    }

    public void LoadChunk(int x, int z)
    {
        if (DataManager.FileExist(x, z))
        {
            string path = DataManager.CreateFilepath(x,z);
            thread.StartThreadedFunction(() => { LoadData(path); });
        }
        else
        {
            Debug.Log("Error No chunk Loaded");
        }
    }

    public void LoadData(string path)
    {
        ChunkData newChunk;
        string json = File.ReadAllText(path);
        newChunk = JsonUtility.FromJson<ChunkData>(json);
        thread.QueueMainThreadFunction(() => AssignData(newChunk));
    }
    public void SaveData(ChunkData data, object FilePath)
    {
      
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(FilePath.ToString(), json);
        thread.QueueMainThreadFunction(() => DestroyChunk());
    }

    void CreateJSONFile(int x, int z)
    {
        //DataManager.UnloadChunkData(this.data);
        string path = DataManager.CreateFilepath(x,z);
        thread.StartThreadedFunction(() => { DataManager.SaveChunk(this.data, path ); });
    }

    public void AssignData(ChunkData _cd)
    {
        data = _cd;
        Debug.Log(data.position);
        CreateMeshFromFile();
    }

    public void CreateMeshFromFile()
    {
        gameObject.transform.position = data.position;
        if (data != null)
        {
            collider = GetComponent<MeshCollider>();
            GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.vertices = data.vertices;
            mesh.uv = data.uv;
            mesh.tangents = data.tangents;
            mesh.colors = data.meshColor;
            mesh.triangles = data.triangles;
            mesh.RecalculateNormals();
            collider.sharedMesh = mesh;
        }
        else
        {
            Debug.Log("Chunk Data is NULL");
        }
        ChunkManager.instance.AddChunkToList(gameObject);
    }

    public Bounds GetWorldSpaceBounds()
    {
        return gameObject.GetComponent<Renderer>().bounds;
    }

    void SetNeighbours()
    {
        int size = (int)Mathf.Sqrt(ChunkManager.instance.mapChunkTotal);
        size -= 1;
        
        data.chunkNeighbour = new Vector2[8];

        if(data.arrayPos.y < size)
        {
            data.chunkNeighbour[(int)directions.N] = new Vector2(data.arrayPos.x, data.arrayPos.y + 1);
        }
        else
        {
            data.chunkNeighbour[(int)directions.N] = new Vector2(-1, -1);
        }

        if (data.arrayPos.x < size && data.arrayPos.y < size)
        {
            data.chunkNeighbour[(int)directions.NE] = new Vector2(data.arrayPos.x + 1, data.arrayPos.y + 1);
        }
        else
        {
            data.chunkNeighbour[(int)directions.NE] = new Vector2(-1, -1);
        }

        if (data.arrayPos.x < size)
        {
            data.chunkNeighbour[(int)directions.E] = new Vector2(data.arrayPos.x + 1, data.arrayPos.y);
        }
        else
        {
            data.chunkNeighbour[(int)directions.E] = new Vector2(-1, -1);
        }

        if (data.arrayPos.x < size && data.arrayPos.y > 0)
        {
            data.chunkNeighbour[(int)directions.SE] = new Vector2(data.arrayPos.x + 1, data.arrayPos.y - 1);
        }
        else
        {
            data.chunkNeighbour[(int)directions.SE] = new Vector2(-1, -1);
        }

        if (data.arrayPos.y > 0)
        {
            data.chunkNeighbour[(int)directions.S] = new Vector2(data.arrayPos.x, data.arrayPos.y - 1);
        }
        else
        {
            data.chunkNeighbour[(int)directions.S] = new Vector2(-1, -1);
        }

        if (data.arrayPos.x > 0 && data.arrayPos.y > 0)
        {
            data.chunkNeighbour[(int)directions.SW] = new Vector2(data.arrayPos.x - 1, data.arrayPos.y - 1);
        }
        else
        {
            data.chunkNeighbour[(int)directions.SW] = new Vector2(-1, -1);
        }

        if (data.arrayPos.x > 0)
        {
            data.chunkNeighbour[(int)directions.W] = new Vector2(data.arrayPos.x - 1, data.arrayPos.y);
        }
        else
        {
            data.chunkNeighbour[(int)directions.W] = new Vector2(-1, -1);
        }

        if (data.arrayPos.x > 0 && data.arrayPos.y < size)
        {
            data.chunkNeighbour[(int)directions.NW] = new Vector2(data.arrayPos.x - 1, data.arrayPos.y + 1);
        }
         else
        {
            data.chunkNeighbour[(int)directions.NW] = new Vector2(-1, -1);
        }
    }
}
