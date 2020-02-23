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

                PlayerID = other.gameObject.GetComponent<Chunk>().chunkID;

                GameManager.instance.currentID = PlayerID;
        
            }
        
            return;
        
    }

    private void OnCollisionExit(Collision collision)
    {
        GameManager.instance.CallLoad();
      //  firstLoad = false;
    }

    void BS()
    {
        test++;
        if(test <=1)
        {
         GameManager.instance.CallSave();
         GameManager.instance.CallLoad();
        }
       
    }

}
