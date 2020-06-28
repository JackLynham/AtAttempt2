using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public List<GameObject> tileList = new List<GameObject>();

   public float currentID;
   public PlayerScript player;
   public  SaveLoad Save;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
       
    }

    public void CallSave()
    {
        Save.Save();
    }

    public void CallLoad()
    {
        Save.Load();
       // Test();
    }

    //void Test()
    //{
       
    //    if (currentID == 1 || currentID == 2 || currentID == 5)
    //    {
    //        tileList[1].SetActive(true);
    //       
    //    }

    //    if (currentID == 1 || currentID == 2 || currentID == 3 || currentID == 6)
    //    {
    //        tileList[2].SetActive(true);
    //    }
     

    //    if (currentID == 2 || currentID == 3 || currentID == 4 || currentID == 7)
    //    {
    //        tileList[3].SetActive(true);
    //    }
      

    //    if (currentID == 3 || currentID == 4 || currentID == 8)
    //    {
    //        tileList[4].SetActive(true);
    //    }
       

    //    if (currentID == 1 || currentID == 5 || currentID == 6 || currentID == 9)
    //    {
    //        tileList[5].SetActive(true);
    //    }
       

    //    if (currentID == 2 || currentID == 5 || currentID == 6 || currentID == 7 || currentID == 10)
    //    {
    //        tileList[6].SetActive(true);
    //    }
      

    //    if (currentID == 3 || currentID == 6 || currentID == 7 || currentID == 8 || currentID == 11)
    //    {
    //        tileList[7].SetActive(true);
    //    }
       

    //    if (currentID == 4 || currentID == 7 || currentID == 8 || currentID == 12)
    //    {
    //        tileList[8].SetActive(true);
    //    }
    

    //    if (currentID == 5 || currentID == 9 || currentID == 10 || currentID == 13)
    //    {
    //        tileList[9].SetActive(true);
    //    }
       

    //    if (currentID == 6 || currentID == 9 || currentID == 10 || currentID == 11 || currentID == 14)
    //    {
    //        tileList[10].SetActive(true);
    //    }
      

    //    if (currentID == 7 || currentID == 10 || currentID == 11 || currentID == 12 || currentID == 15)
    //    {
    //        tileList[11].SetActive(true);
    //    }
       
    //    if (currentID == 8 || currentID == 11 || currentID == 12 || currentID == 16)
    //    {
    //        tileList[12].SetActive(true);
    //    }
        

    //    if (currentID == 9 || currentID == 13 || currentID == 14)
    //    {
    //        tileList[13].SetActive(true);
    //    }
      

    //    if (currentID == 10 || currentID == 13 || currentID == 14 || currentID == 15)
    //    {
    //        tileList[14].SetActive(true);
    //    }
        

    //    if (currentID == 11 || currentID == 14 || currentID == 15 || currentID == 16)
    //    {
    //        tileList[15].SetActive(true);
    //    }


    //    if (currentID == 12 || currentID == 15 || currentID == 16)
    //    {
    //        tileList[16].SetActive(true);
    //    }
     

    //}
   
}
