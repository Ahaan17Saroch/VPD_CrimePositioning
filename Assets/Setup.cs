// This script runs when the mao is being loaded. It will read the data from the file and then place the crime spot on the map.

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;

namespace Wrld.Space
{

    public class Setup : MonoBehaviour
    {
        public GameObject spot, clicked; // Representation of Crime spot.


        Hashtable Metadata;

        // Read from the file and place the spots when the map is loaded.
        void Start()
        {
            double latitudePoint;
            double longitudePoint;
            LatLong CrimeLatLong = new LatLong();
            Vector3 point = new Vector3();

            int count = 0;
            using (var reader = new StreamReader(@"Crime_Data.csv"))
            {
                System.IO.StreamWriter writer;
                writer = new System.IO.StreamWriter("MetaData.txt"); // Dor storing the data related to crime.

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (count > 0) // The first row is the name of the columns.
                    {

                        double.TryParse(values[0], out latitudePoint);
                        double.TryParse(values[1], out longitudePoint);

                        CrimeLatLong.SetLatitude(latitudePoint);
                        CrimeLatLong.SetLongitude(longitudePoint);

                        point = Api.Instance.SpacesApi.GeographicToWorldPoint(CrimeLatLong); //Convert latitude and longitude to the loacal space in Unity.
                        point.y = 10;

                        GameObject clone = Instantiate(spot, point, transform.rotation);                       
                        writer.Write(clone.GetInstanceID() + " " + values[2] + "\n");
                                               
                    }
                    count++;
                }
                writer.Close();
            }

        }
    }
}