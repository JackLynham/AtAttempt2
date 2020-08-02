using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This is the superclass for all objects that can be saved
/// </summary>
public abstract class SaveableObject : MonoBehaviour
{

    /// This string contains saveable parameters from subclasses
    protected string save;

    [SerializeField]
    private ObjectType objectType;

    // Use this for initialization
    void Start()
    {
        //Makes sure that all saveable objects are added to the list of saveable objects
        SaveGameManager.Instance.SaveableObjects.Add(this);
    }

  
    /// Saves the object
    public virtual void Save(int id)
    {

        PlayerPrefs.SetString(SceneManager.GetActiveScene().buildIndex + "-" + id.ToString(), objectType + "_" + transform.position.ToString() + "_" + transform.localScale.ToString() + "_" + transform.localRotation + "_" + save);
    }

    
    /// Loads the object
    public virtual void Load(string[] values)
    {
        //Sets the objects position
        transform.localPosition = SaveGameManager.Instance.StringToVector(values[1]);

        //Sets the objects scale
        transform.localScale = SaveGameManager.Instance.StringToVector(values[2]);

        //Sets the object's rotation
        transform.localRotation = SaveGameManager.Instance.StringToQuaternion(values[3]);
    }

    public void DestroySaveable()
    {
        SaveGameManager.Instance.SaveableObjects.Remove(this);
        Destroy(gameObject);
    }
}