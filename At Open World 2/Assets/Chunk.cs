using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public static Chunk instance;
    public int chunkID;

    private void Awake()
    {
        instance = this;
    }
}
