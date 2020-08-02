using System;
using UnityEngine;

enum ObjectType { A , B, C, D, E, F, G, H, I, J, K, L, M, N, O, P }

public class SpecificObject : SaveableObject
{

    [SerializeField]
    private float speed;
    /// The cube's strength
    [SerializeField]
    public float ID;
    
    /// <param name="id">The object's id</param>
    public override void Save(int id)
    {
        //Adds the strength and speed to the save string
        save = speed.ToString() + "_" + ID.ToString();

        //Calls the base save to save
        base.Save(id);
       // Debug.Log(ID);
        
    }

    /// Loads the object
    public override void Load(string[] values)
    {
        //Loads the speed
        speed = float.Parse(values[4]);

        //Loads the strength
        ID = float.Parse(values[5]);

        //Calls the base load function
        base.Load(values);
    }

}
