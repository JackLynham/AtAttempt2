using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ObjectType {Cube, Plane, Sphere }

public abstract class SaveableObject : MonoBehaviour
{
    protected string save;

    private ObjectType objectType;

   private void Start() // Adding Object to list
    {
        SaveGameManager.Instance.SaveableObjects.Add(this);
        PlayerPrefs.SetInt("Age", 30);
      

    }
    public virtual void Save(int id)
    {
        PlayerPrefs.SetString(id.ToString(), transform.position.ToString());
    }
    
    public virtual void Load(string [] values)
    {
       
    }

    public void DestroySaveable()
    {

    }
}
