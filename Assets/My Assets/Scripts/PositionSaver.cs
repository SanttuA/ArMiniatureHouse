using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionSaver : MonoBehaviour {

    public Transform[] saveableObjects;    //use editor to pick which objects are to be saved
    //public TextAsset file;  //save data to this file
    public bool loadPositions = false;  //do we load saved data at the start of the scene

	// Use this for initialization
	void Start ()
    {
        if (loadPositions && File.Exists(getPath()))
        {
            LoadPositions();
        }
	}

    public void LoadPositions()
    {
        string fileData = File.ReadAllText(getPath());

        //This is to get all the lines
        string[] lines = fileData.Split("\n"[0]);

        if (lines.Length == 0)
        {
            Debug.Log("The save file is empty! Cannot load.");
        }
        else
        {
            for (int i = 0; i < lines.Length-1; i++)
            {
                //Debug.Log("Inserting position data to object at index: "+i);
                //This is to get every thing that is comma separated
                string[] coordinates = lines[i].Split(","[0]);
                saveableObjects[i].position =
                    new Vector3(float.Parse(coordinates[0]), float.Parse(coordinates[1]), float.Parse(coordinates[2]));
                saveableObjects[i].rotation =
                    new Quaternion(saveableObjects[i].rotation.x, float.Parse(coordinates[3]),
                    saveableObjects[i].rotation.z, saveableObjects[i].rotation.w);
            }
        }     
    }

    public void SavePositions()
    {
        string filePath = getPath();

        if (!File.Exists(filePath))
        {
            File.CreateText("Saved_Positions.csv");
            //Debug.Log("Created csv save file.");
        }

        //This is the writer, it writes to the filepath
        StreamWriter writer = new StreamWriter(filePath);

        //This is writing the header line
        //writer.WriteLine("X,Y,Z, rotY");
        //This loops through every gameobject in the saveableObjects and writes them to the file.
        for (int i = 0; i < saveableObjects.Length; i++)
        {
            writer.WriteLine(saveableObjects[i].position.x +
                "," + saveableObjects[i].position.y +
                "," + saveableObjects[i].position.z +
                "," + saveableObjects[i].rotation.y);
            /*
            Debug.Log("Wrote: " +saveableObjects[i].position.x +
                "," + saveableObjects[i].position.y +
                "," + saveableObjects[i].position.z +
                "," + saveableObjects[i].rotation.y);
                */
        }
        writer.Flush();
        //This closes the file
        writer.Close();

        //Debug.Log("Done writing to save file!");
    }

    public void ResetPositions()
    {
        //loadPositions = false;
        File.WriteAllText(getPath(), string.Empty);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private string getPath()
    {
        #if UNITY_EDITOR
                return Application.dataPath + "/" + "Saved_Positions.csv";
#elif UNITY_ANDROID
                return Application.persistentDataPath+"/"+"Saved_Positions.csv";
#elif UNITY_IPHONE
                return Application.persistentDataPath+"/"+"Saved_Positions.csv";
#else
                return Application.dataPath +"/"+"Saved_Positions.csv";
#endif
    }

}
