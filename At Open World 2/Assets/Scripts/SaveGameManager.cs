using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    private static SaveGameManager instance;

    public List<SaveableObject> SaveableObjects { get; private set; }

    public static SaveGameManager Instance


    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveGameManager>();
            }
            return instance;
        }
    }

    private void Awake()  //Needs to be Instantiated other wise saveable wont exsit
    {
        SaveableObjects = new List<SaveableObject>();
    }
    public void Save()
    {
        PlayerPrefs.SetInt("ObjectCount", SaveableObjects.Count);
        
        //Gives Objects and ID

        for (int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i);
        }
    }
    public void Load()
    {
        int objectCount = PlayerPrefs.GetInt("ObjectCount");
        //Instanistes objects
        for (int i = 0; i < objectCount; i++)
        {
            string value = PlayerPrefs.GetString(i.ToString());
        }
    }

    public Vector3 StringToVector(string value)
    {
        return Vector3.zero;
    }


    public Quaternion StringToQuaternion(string value)
    {
        return Quaternion.identity;
    }
}
