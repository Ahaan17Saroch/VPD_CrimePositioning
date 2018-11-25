// This script runs when the mao is being loaded. It will read the data from the file and then place the crime spot on the map.

using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using System;

namespace Wrld.Space
{

    public class Setup : MonoBehaviour
    {
        public GameObject spot, clicked; // Representation of Crime spot.
        List<string> LatitudeList; // Stores the list of latitudes.
        List<string> LongitudeList; // Stores the list of longitudes.
        List<string> crime_typeList; // Stores the crime type
        Hashtable Metadata;
        

        // Read from the file and place the spots when the map is loaded.
        void Start()
        {

            int count = 0;
            using (var reader = new StreamReader(@"Book2.csv"))
            {
                LatitudeList = new List<string>();
                LongitudeList = new List<string>();
                crime_typeList = new List<string>();

                while (!reader.EndOfStream)
                {
                    //Debug.Log(count);

                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (count > 0) // The first row is the name of the columns.
                    {
                        LatitudeList.Add(values[0]);
                        LongitudeList.Add(values[1]);
                        crime_typeList.Add(values[2]);
                    }
                    count++;            
                }
            }

            double latitudePoint, longitudePoint;
            string crime_type;

            LatLong CrimeLatLong = new LatLong();
            Vector3 point = new Vector3();
            for (int i = 0; i < count - 1; i++)
            {

                double.TryParse(LatitudeList[i], out latitudePoint);
                double.TryParse(LongitudeList[i], out longitudePoint);
                crime_type = crime_typeList[i];

                //Debug.Log(crime_type);

                CrimeLatLong.SetLatitude(latitudePoint);
                CrimeLatLong.SetLongitude(longitudePoint);

                point = Api.Instance.SpacesApi.GeographicToWorldPoint(CrimeLatLong); //Convert latitude and longitude to the loacal space in Unity.

                point.y = 10;

                GameObject clone = Instantiate(spot, point, transform.rotation);

            }
        }

        private void OnMouseDown()
        {
            Debug.Log("Ok");
        }
    }
}