using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float PlayerID ;
    private int test;
    bool firstLoad = true;

    
    void OnCollisionEnter(Collision other)
    {
      
            if (other.gameObject.tag == "Respawn")
            {
                BS();

                PlayerID = other.gameObject.GetComponent<SpecificObject>().ID;

                PlayerID = other.gameObject.GetComponent<SpecificObject>().ID;
                SaveGameManager.Instance.currentID = PlayerID;
        
            }
        
            return;
        
    }

    private void OnCollisionExit(Collision collision)
    {
        SaveGameManager.Instance.Load();
        firstLoad = false;
    }

    void BS()
    {
        test++;
        if(test <=1)
        {
         SaveGameManager.Instance.Save();
         SaveGameManager.Instance.Load();
        }
       
    }

}
