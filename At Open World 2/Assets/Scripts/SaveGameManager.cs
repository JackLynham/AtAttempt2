using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveGameManager : MonoBehaviour
{
    public float currentID =0  ;
    public PlayerScript PlayerScript;
    /// The SageGameManager's singleton instance
    private static SaveGameManager instance;

 
    /// A list of all saveable objects
    public List<SaveableObject> SaveableObjects { get; private set; }


    /// A property for accessing the SaveGameManager
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

   

    /// Saves all saveable objects
    public void Save()
    {
        //Stores the amount of saveable object for the current level
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), SaveableObjects.Count);

        //Runs through all the saveable objects
        for (int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i); //Saves the object
        }
    }


    /// Loads all the saved objects
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
        int objCount = PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString());

        //Creates a for loop for loading all the objects
        for (int i = 0; i < objCount; i++)
        {
            //Splits the loaded string into an array
             string[] values = PlayerPrefs.GetString(SceneManager.GetActiveScene().buildIndex + "-" + i).Split('_');

            //Crates an object reference for containing the loaded object
            GameObject tmp = null;

            switch (values[0]) //Looks at the object's current type
            {
                case "A":

                    if (currentID == 1 || currentID == 2 || currentID == 5)
                    {
                        tmp = Instantiate(Resources.Load("A") as GameObject);
                    }
                
                    break;

                case "B":
                    if (currentID == 1 || currentID == 2 || currentID == 3 || currentID ==6)
                    {
                        tmp = Instantiate(Resources.Load("B") as GameObject);
                    }
                        
                    break;

                case "C":
                    if (currentID == 2 || currentID == 3 || currentID == 4 || currentID == 7)
                    {
                        tmp = Instantiate(Resources.Load("C") as GameObject);
                    }
                        
                    break;

                case "D":
                    if (currentID == 3 || currentID == 4 || currentID == 8 )
                    {
                        tmp = Instantiate(Resources.Load("D") as GameObject);
                    }
                    break;

                case "E":
                    if (currentID == 1 || currentID == 5 || currentID == 6|| currentID == 9)
                    {
                        tmp = Instantiate(Resources.Load("E") as GameObject);
                    }
                        
                    break;

                case "F":
                    if (currentID == 2 || currentID == 5 || currentID == 6 || currentID ==7|| currentID == 10)
                    {
                        tmp = Instantiate(Resources.Load("F") as GameObject);
                    }
                        
                    break;

                case "G":
                    if (currentID == 3 || currentID == 6 || currentID == 7 ||currentID == 8|| currentID == 11)
                        tmp = Instantiate(Resources.Load("G") as GameObject);
                    break;

                case "H":
                    if (currentID == 4 ||currentID ==7|| currentID == 8 || currentID == 12)
                    {
                        tmp = Instantiate(Resources.Load("H") as GameObject);
                    }
                        
                    break;
                case "I":
                    if (currentID == 5 || currentID == 9 ||currentID == 10|| currentID == 13)
                    {
                        tmp = Instantiate(Resources.Load("I") as GameObject);
                    }
                        
                    break;

                case "J":
                    if (currentID == 6 ||currentID == 9|| currentID ==10 || currentID == 11 || currentID == 14)
                    {
                        tmp = Instantiate(Resources.Load("J") as GameObject);
                    }
                        
                    break;

                case "K":
                    if (currentID == 7 || currentID == 10 || currentID == 11 || currentID == 12 || currentID == 15)
                    {
                        tmp = Instantiate(Resources.Load("K") as GameObject);
                    }
                       
                    break;

                case "L":
                    if (currentID == 8 || currentID == 11 || currentID == 12|| currentID == 16)
                    {
                          tmp = Instantiate(Resources.Load("L") as GameObject);
                    }
                       
                    break;

                case "M":
                    if (currentID == 9 || currentID == 13 || currentID == 14 )
                    {
                         tmp = Instantiate(Resources.Load("M") as GameObject);
                    }
                       
                    break;

                case "N":
                    if (currentID == 10 || currentID == 13 || currentID == 14 || currentID == 15)
                    {
                        tmp = Instantiate(Resources.Load("N") as GameObject);
                    }
                        
                    break;

                case "O":
                    if (currentID == 11 || currentID == 14 || currentID == 15 || currentID == 16)
                    {
                        tmp = Instantiate(Resources.Load("O") as GameObject);
                    }
                       
                    break;

                case "P":
                    if (currentID == 12 || currentID == 15 || currentID == 16)
                    {
                        tmp = Instantiate(Resources.Load("P") as GameObject);
                    }
                        
                    break;

                    //case "Cylinder": //If it's a cylinder then we instantiate a capsule
                    //    tmp = Instantiate(Resources.Load("Cylinder") as GameObject);
                    //    break;
            }

            if (tmp != null) //If we found something to load
            {
                //Then call the Objects load function
                tmp.GetComponent<SaveableObject>().Load(values);
            }

        }


    }


   
    /// Translates a string to a vector3
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

    
    /// Translates a string to a Quaternion
    /// Translates a string to a Quaternion
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
