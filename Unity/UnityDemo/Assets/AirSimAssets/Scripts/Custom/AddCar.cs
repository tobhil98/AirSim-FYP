﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AirSimUnity
{
    public class AddCar : MonoBehaviour
    {
        // Start is called before the first frame update

        private static AddCar instance;
        public static AddCar GetInstance()
        {
            return instance;
        }
        private void Awake()
        {
            instance = this;
        }

        private struct InitVehicle
        {
            public string vehicle_name;
            public string vehicle_type;
            public Vector3 pos;
            public Quaternion rotation;
        }
        private static int counter = 0;

        private static Queue<InitVehicle> VehicleQueue = new Queue<InitVehicle>();
        private void Update()
        {
           if(VehicleQueue.Count > 0)
            {
                InitVehicle s = VehicleQueue.Dequeue();
                var type = Array.Find(AssetHandler.getInstance().vehicles, element => element.name == s.vehicle_type);
                var obj = Instantiate(type.vehicle, s.pos, s.rotation);
                obj.GetComponent<Vehicle>().vehicle_name = s.vehicle_name;
                AirSimServer.vehicleList.Add(obj);
            }
        }
         
        async public void Pressed()
        {
            var s = new InitVehicle();
            s.vehicle_name = "car"+ ++counter;
            s.vehicle_type = AssetHandler.getInstance().getVehicle();
            s.pos = new Vector3(-250, 2, 50);
            s.rotation = Quaternion.identity;
            VehicleQueue.Enqueue(s);
        }

        async public void SpawnVehicle(string name, string type, Vector3 pos, Quaternion rotation)
        {
            var s = new InitVehicle();
            s.vehicle_name = name;
            s.vehicle_type = type;
            s.pos = pos;
            s.rotation = rotation;
            VehicleQueue.Enqueue(s);
        }

    }
}
