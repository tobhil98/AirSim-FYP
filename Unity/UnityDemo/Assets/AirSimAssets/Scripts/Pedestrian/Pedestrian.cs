﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace AirSimUnity
{
    public class Pedestrian : MonoBehaviour
    {
        private PedestrianCompanion pedestrianInterface;
        private bool isServerStarted = false;
        private bool destroySelf_ = false;

        public string pedestrian_name;

        
        public void Start()
        {
            pedestrian_name = transform.GetComponentInParent<PedestrianOverhead>().name;

            InitialisePedestrian();

            pedestrianInterface = PedestrianCompanion.GetPedestrianCompanion(this, pedestrian_name);
            isServerStarted = pedestrianInterface.StartPedestrianServer(AirSimSettings.GetSettings().GetPort(AirSimSettings.AgentType.Pedestrian));

            if (isServerStarted == false)
            {
#if UNITY_EDITOR
                EditorUtility.DisplayDialog("Problem in starting AirSim server!!!", "Please check logs for more information.", "Exit");
                EditorApplication.Exit(1);
#else
                Application.Quit();
#endif
            }

            //AirSimGlobal.Instance.Weather.AttachToVehicle(this);

            //count = UnityEngine.Random.Range(0, 10);
        }

        private void FixedUpdate()
        {
            if (destroySelf_) Destroy(transform.parent.gameObject);

        }


        private void OnApplicationQuit()
        {
            pedestrianInterface.StopPedestrianServer();
        }

        private void InitialisePedestrian()
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = AssetHandler.getInstance().pedestrianAnimation;
        }

        public void DestroySelf()
        {
            destroySelf_ = true;
        }
    };

};