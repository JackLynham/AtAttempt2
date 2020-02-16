using System;
using UnityEngine;

enum ObjectType { Cube, Capsule, Cylinder }

public class SpecificObject : SaveableObject
{
    /// <summary>
    /// The cube's speed
    /// </summary>
    [SerializeField]
    private float speed;

    /// <summary>
    /// The cube's strength
    /// </summary>
    [SerializeField]
    private float strength;

    /// <summary>
    /// Saves the object
    /// </summary>
    /// <param name="id">The object's id</param>
    public override void Save(int id)
    {
        //Adds the strength and speed to the save string
        save = speed.ToString() + "_" + strength.ToString();

        //Calls the base save to save
        base.Save(id);
    }

    /// <summary>
    /// Loads the object
    /// </summary>
    /// <param name="values">The loadable values</param>
    public override void Load(string[] values)
    {
        //Loads the speed
        speed = float.Parse(values[4]);

        //Loads the strength
        strength = float.Parse(values[5]);

        //Calls the base load function
        base.Load(values);
    }

}
