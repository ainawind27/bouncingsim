using UnityEngine;
using System.Collections;
using CsvHelper;
using System.IO;
using System.Collections.Generic;
using System;

public class simulate : MonoBehaviour {

    //private Dictionary<float, PendulumRes> records = new Dictionary<float, PendulumRes>();
    private List<BouncingRes> records = new List<BouncingRes>();
    private float? timeStep = null;
    private float maxTime;
    private int maxTimeInt;
    private const float speed = 100f;
    //private Vector3 initialEuler;
    private Vector3 initialPos;

    // Use this for initialization
    void Start () {
        //initialEuler = new Vector3(270, -90, 90);
        initialPos = transform.position;
        using (var textReader = File.OpenText("BouncingBall_res.csv"))
        {
            using (var csv = new CsvReader(textReader))
            {
                float? firstTime = null;
                while (csv.Read())
                {
                    //Debug.LogFormat("Reading: {0}", (object)csv.CurrentRecord);
                    var record = new BouncingRes {
                        time = float.Parse(csv.CurrentRecord[0]),
                        Vx = float.Parse(csv.CurrentRecord[1]),
                        Vy = float.Parse(csv.CurrentRecord[2]),
                        x = float.Parse(csv.CurrentRecord[3]),
                        y = float.Parse(csv.CurrentRecord[4]),
                        der_Vx = float.Parse(csv.CurrentRecord[5]),
                        der_Vy = float.Parse(csv.CurrentRecord[6]),
                        der_x = float.Parse(csv.CurrentRecord[7]),
                        der_y = float.Parse(csv.CurrentRecord[8]),
                        ax = float.Parse(csv.CurrentRecord[9]),
                        g = float.Parse(csv.CurrentRecord[10]),
                        stair1x = float.Parse(csv.CurrentRecord[11]),
                        stair1y = float.Parse(csv.CurrentRecord[12]),
                    };
                    //if (!records.ContainsKey(record.time))
                    //{
                    //    records.Add(record.time, record);
                    //}
                    records.Add(record);
                    if (firstTime == null)
                    {
                        firstTime = record.time;
                    } else if (timeStep == null)
                    {
                        timeStep = record.time - firstTime;
                    }
                    maxTime = record.time;
                }
            }
        }
        maxTimeInt = records.Count - 1;
        Debug.LogFormat("Records {0}: {1}", records.Count, records);
	}
	
	// Update is called once per frame
	void Update () {
        //float simTime = Mathf.Round(Time.time * speed / timeStep.Value) * timeStep.Value;
        //if (simTime > maxTime)
        //{
        //    simTime = maxTime;
        //}
        int simTimeInt = Mathf.RoundToInt(Time.time * speed);
        if (simTimeInt > maxTimeInt)
        {
            simTimeInt = maxTimeInt;
        }
        try {
            var res = records[simTimeInt];
            //Debug.LogFormat("{0} => {1}", simTimeInt, res);
            //transform.eulerAngles = initialEuler + new Vector3(res.x1 / Mathf.PI * 180, 0, 0);
            //transform.eulerAngles = new Vector3(270, 0, 0);
            transform.position = initialPos + new Vector3(0, res.y, res.x);
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("{0} => {1}", simTimeInt, "ERROR");
        }
    }
}
