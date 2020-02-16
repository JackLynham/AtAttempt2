using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class SaveGameManager : MonoBehaviour
{
    /// <summary>
    /// The SageGameManager's singleton instance
    /// </summary>
    private static SaveGameManager instance;

    /// <summary>
    /// A list of all saveable objects
    /// </summary>
    public List<SaveableObject> SaveableObjects { get; private set; }

    /// <summary>
    /// A property for accessing the SaveGameManager
    /// </summary>
    public static SaveGameManager Instance
    {
        get
        {
            if (instance == null)//Checks if the instance has been found
            {
                //If the instance is null then we need to find it
                instance = GameObject.FindObjectOfType<SaveGameManager>();
            }

            return instance;
        }
    }

    void Awake()
    {
        //Instantiates the list of saveable objects
        SaveableObjects = new List<SaveableObject>();
    }

    /// <summary>
    /// Saves all saveable objects
    /// </summary>
    public void Save()
    {
        //Stores the amount of saveable object for the current level
        PlayerPrefs.SetInt(Application.loadedLevel.ToString(), SaveableObjects.Count);

        //Runs through all the saveable objects
        for (int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i); //Saves the object
        }
    }

    /// <summary>
    /// Loads all the saved objects
    /// </summary>
    public void Load()
    {
        //Runs through all the saveable objects
        foreach (SaveableObject obj in SaveableObjects)
        {
            if (obj != null)
            {
                //removes the object from the game to avoid duplicates
                Destroy(obj.gameObject);
            }

        }

        //Clears the list of objects
        SaveableObjects.Clear();

        //Get the amount of objects to load
        int objCount = PlayerPrefs.GetInt(Application.loadedLevel.ToString());

        //Creates a for loop for loading all the objects
        for (int i = 0; i < objCount; i++)
        {
            //Splits the loaded string into an array
            string[] values = PlayerPrefs.GetString(Application.loadedLevel + "-" + i).Split('_');

            //Crates an object reference for containing the loaded object
            GameObject tmp = null;

            switch (values[0]) //Looks at the object's current type
            {
                case "Cube": //If it's a cube, then we instantiate a cube
                    tmp = Instantiate(Resources.Load("Cube") as GameObject);
                    break;
                case "Capsule": //If it's a capsule then we instantiate a capsule
                    tmp = Instantiate(Resources.Load("Capsule") as GameObject);
                    break;
                case "Cylinder": //If it's a cylinder then we instantiate a capsule
                    tmp = Instantiate(Resources.Load("Cylinder") as GameObject);
                    break;
            }

            if (tmp != null) //If we found something to load
            {
                //Then call the Objects load function
                tmp.GetComponent<SaveableObject>().Load(values);
            }

        }


    }


    /// <summary>
    /// Translates a string to a vector3
    /// </summary>
    /// <param name="value">The string to translate</param>
    /// <returns>Vector3</returns>
    public Vector3 StringToVector(string value)
    {
        //Removes the () from the string
        value = value.Trim(new char[] { '(', ')', });

        //Removes white space from the string
        value = value.Replace(" ", "");

        //Splits the sting into x,y,z values 
        string[] pos = value.Split(',');

        //Returns the vector
        return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
    }

    /// <summary>
    /// Translates a string to a Quaternion
    /// </summary>
    /// <param name="values">The string to translate</param>
    /// <returns>Quaternion</returns>
    public Quaternion StringToQuaternion(string values)
    {
        //Removes the () from the string
        values = values.Trim(new char[] { '(', ')', });

        //Removes white space from the string
        values = values.Replace(" ", "");

        //Splits the sting into x,y,z,w values 
        string[] pos = values.Split(',');

        //Returns the Quaternion
        return new Quaternion(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]), float.Parse(pos[3]));
    }

}
