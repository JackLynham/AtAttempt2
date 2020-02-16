using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificObject : SaveableObject
{
   private float speed;
   private float strength;

    public override void Save(int ID)
    {
        base.Save(ID);
    }
    private void Update()
    {
        SaveGameManager.Instance.Save();
    }
    public override void Load(string[] values)
    {
        base.Load(values);
    }
}
