using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour
{
    [System.Serializable]
    public class TileInfo
    {
       public float xPos;
       public float yPos;
       public float zPos;
       public int ID;
    }

    [System.Serializable]
    public class SaveData
    {  
        public List<TileInfo> TileInfoList = new List<TileInfo>();
        //public List<Chunk> IDList = new List<Chunk>();
    }


    void Start()
    {

    }

    public void Save()
    {
        //Create data and fill
        SaveData data = new SaveData();

        for (int i = 0; i < GameManager.instance.tileList.Count; i++)
        {
            TileInfo info = new TileInfo();
            info.xPos = GameManager.instance.tileList[i].transform.position.x;
            info.yPos = GameManager.instance.tileList[i].transform.position.y;
            info.zPos = GameManager.instance.tileList[i].transform.position.z;

            data.TileInfoList.Add(info);
            
        }

       // DO Save
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream
            (Application.persistentDataPath + "/SaveFile.dat", FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game Saved");
       
    }
    public void Load ()
    {
        if(File.Exists(Application.persistentDataPath + "/SaveFile.dat"))
        {
            //Do Load
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream
                (Application.persistentDataPath + "/SaveFile.dat", FileMode.Open);

            //Pass Back Data 

            SaveData data = (SaveData)formatter.Deserialize(stream);
            
            
            for (int i = 0; i < GameManager.instance.tileList.Count; i++)
            {
                
                Vector3 pos = new Vector3(data.TileInfoList[i].xPos,
                    data.TileInfoList[i].yPos, data.TileInfoList[i].zPos);

                GameManager.instance.tileList[i].transform.position = pos;

                GameManager.instance.tileList[i].SetActive(false);
              
            }

            if (GameManager.instance.currentID == 1 || GameManager.instance.currentID == 2 || GameManager.instance.currentID == 5)
            {
                GameManager.instance.tileList[0].SetActive(true);
               

            }

            if (GameManager.instance.currentID == 1 || GameManager.instance.currentID == 2 || GameManager.instance.currentID == 3 || GameManager.instance.currentID == 6)
            {
                GameManager.instance.tileList[1].SetActive(true);
                Debug.Log("B");
            }


            if (GameManager.instance.currentID == 2 || GameManager.instance.currentID == 3 || GameManager.instance.currentID == 4 || GameManager.instance.currentID == 7)
            {
                GameManager.instance.tileList[2].SetActive(true);
               
            }


            if (GameManager.instance.currentID == 3 || GameManager.instance.currentID == 4 || GameManager.instance.currentID == 8)
            {
                GameManager.instance.tileList[3].SetActive(true);
            }


            if (GameManager.instance.currentID == 1 || GameManager.instance.currentID == 5 || GameManager.instance.currentID == 6 || GameManager.instance.currentID == 9)
            {
                GameManager.instance.tileList[4].SetActive(true);
            }


            if (GameManager.instance.currentID == 2 || GameManager.instance.currentID == 5 || GameManager.instance.currentID == 6 || GameManager.instance.currentID == 7 || GameManager.instance.currentID == 10)
            {
                GameManager.instance.tileList[5].SetActive(true);
            }


            if (GameManager.instance.currentID == 3 || GameManager.instance.currentID == 6 || GameManager.instance.currentID == 7 || GameManager.instance.currentID == 8 || GameManager.instance.currentID == 11)
            {
                GameManager.instance.tileList[6].SetActive(true);
            }


            if (GameManager.instance.currentID == 4 || GameManager.instance.currentID == 7 || GameManager.instance.currentID == 8 || GameManager.instance.currentID == 12)
            {
                GameManager.instance.tileList[7].SetActive(true);
            }


            if (GameManager.instance.currentID == 5 || GameManager.instance.currentID == 9 || GameManager.instance.currentID == 10 || GameManager.instance.currentID == 13)
            {
                GameManager.instance.tileList[8].SetActive(true);
            }


            if (GameManager.instance.currentID == 6 || GameManager.instance.currentID == 9 || GameManager.instance.currentID == 10 || GameManager.instance.currentID == 11 || GameManager.instance.currentID == 14)
            {
                GameManager.instance.tileList[9].SetActive(true);
            }


            if (GameManager.instance.currentID == 7 || GameManager.instance.currentID == 10 || GameManager.instance.currentID == 11 || GameManager.instance.currentID == 12 || GameManager.instance.currentID == 15)
            {
                GameManager.instance.tileList[10].SetActive(true);
            }

            if (GameManager.instance.currentID == 8 || GameManager.instance.currentID == 11 || GameManager.instance.currentID == 12 || GameManager.instance.currentID == 16)
            {
                GameManager.instance.tileList[11].SetActive(true);
            }


            if (GameManager.instance.currentID == 9 || GameManager.instance.currentID == 13 || GameManager.instance.currentID == 14)
            {
                GameManager.instance.tileList[12].SetActive(true);
            }


            if (GameManager.instance.currentID == 10 || GameManager.instance.currentID == 13 || GameManager.instance.currentID == 14 || GameManager.instance.currentID == 15)
            {
                GameManager.instance.tileList[13].SetActive(true);
            }


            if (GameManager.instance.currentID == 11 || GameManager.instance.currentID == 14 || GameManager.instance.currentID == 15 || GameManager.instance.currentID == 16)
            {
                GameManager.instance.tileList[14].SetActive(true);
            }


            if (GameManager.instance.currentID == 12 || GameManager.instance.currentID == 15 || GameManager.instance.currentID == 16)
            {
                GameManager.instance.tileList[15].SetActive(true);
                
            }




            stream.Close();

        }

        else
        {
            Debug.Log("No save File Found");
        }
    }
        
}
