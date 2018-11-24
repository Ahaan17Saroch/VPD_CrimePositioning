// This script runs when the mao is being loaded. It will read the data from the file and then place the crime spot on the map.

using System.Collections.Generic;
using UnityEngine;
using System.IO;


namespace Wrld.Space
{

    public class Setup : MonoBehaviour
    {
        public Rigidbody spot; // Representation of Crime spot.
        List<string> LatitudeList; // Stores the list of latitudes.
        List<string> LongitudeList; // Stores the list of longitudes.


        // Read from the file and place the spots when the map is loaded.
        void Start()
        {

            int count = 0;
            using (var reader = new StreamReader(@"Book2.csv"))
            {
                Debug.Log(count);
                LatitudeList = new List<string>();
                LongitudeList = new List<string>();

                while (!reader.EndOfStream)
                {
                    Debug.Log(count);

                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (count > 0) // The first row is the name of the columns.
                    {
                        LatitudeList.Add(values[0]);
                        LongitudeList.Add(values[1]);
                    }
                    count++;            
                }
            }

            double latitudePoint, longitudePoint;
            LatLong CrimeLatLong = new LatLong();
            Vector3 point = new Vector3();
            for (int i = 0; i < count - 1; i++)
            {

                double.TryParse(LatitudeList[i], out latitudePoint);
                double.TryParse(LongitudeList[i], out longitudePoint);

                CrimeLatLong.SetLatitude(latitudePoint);
                CrimeLatLong.SetLongitude(longitudePoint);

                point = Api.Instance.SpacesApi.GeographicToWorldPoint(CrimeLatLong); //Convert latitude and longitude to the loacal space in Unity.

                point.y = 10;

                Rigidbody clone = Instantiate(spot, point, transform.rotation); // Make a copy of crime representer and place on the spot in Unity.

            }
        }
    }

}