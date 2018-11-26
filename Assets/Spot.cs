using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spot : MonoBehaviour {

	public GameObject spot;

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {          
            GetData(spot.GetInstanceID().ToString());
        }
    }

    private void GetData(string ID)
    {
        string filename = "MetaData.txt";

        int lineCount = File.ReadAllLines(@filename).Length;             

        System.IO.StreamReader reader;
        reader = new System.IO.StreamReader(filename);

        for (int i = 0; i < lineCount; i++){
            string line = reader.ReadLine();

           // Debug.Log(line.Substring(0, line.IndexOf(" ")));
            if(line.Substring(0,line.IndexOf(" ")).Equals(ID)){

                Debug.Log(line.Substring(line.IndexOf(" ")));
                break;

            }
        }

        reader.Close();
    }

}
