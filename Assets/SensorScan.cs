using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    //List<int> castAngles = new List<int> { -15 , -13, -11, -9, -7, -5, -3, -1, 1, 3, 5, 7, 9, 11, 13, 15 };
    float resolution = 0.4f;
    string path = Application.dataPath + "/pcd/";
    string fileName = "PointCloudData";
    int frameIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        frameIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        string filePath = path + "/" + fileName + frameIndex.ToString() + ".csv";
        var dataFile = File.Create(filePath);
        StreamWriter sw = new StreamWriter(dataFile);
        for (float angle = 0f; angle < 360f; angle += resolution)
        {   
            for (int laserCastAngle = -30; laserCastAngle <= 30; laserCastAngle++)
            {
                //var laserPolarAngle = castAngles[laserChannel] * Mathf.Deg2Rad;
                
                Vector3 rayDirection = calcDirection(angle * Mathf.Deg2Rad, laserCastAngle * Mathf.Deg2Rad);
                if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit))
                {
                    Debug.Log(hit.point);
                    var hitPoint = hit.point;
                    sw.WriteLine(hitPoint[0] + "," + hitPoint[2] + "," + hitPoint[1]);

                }
          
            }
        }
        sw.Close();
        frameIndex++;
        
    }

    Vector3 calcDirection(float azimuth, float polar)
    {
        float x_val = Mathf.Cos(polar) * Mathf.Cos(azimuth);
        float z_val = Mathf.Cos(polar) * Mathf.Sin(azimuth);
        float y_val = Mathf.Sin(polar);
        var vec = new Vector3(x_val, y_val, z_val).normalized;
        return vec;
    }

}
