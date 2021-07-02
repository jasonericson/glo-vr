/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   DeveloperSettings.cs
\author Jason Ericson
\par    email: jason/@jasonericson.net
\par    DigiPen login: jason.ericson
\par    Course: GAM450
\brief  
    Defines the DeveloperSettings class.
*/
/******************************************************************************/

using UnityEngine;
using System;   
using System.Collections;
using System.IO;

public class DeveloperSettings : MonoBehaviour
{
    // public GameObject OculusController = null;
    public GameObject MainCamera = null;
    public GameObject HandsController = null;
    public GameObject OculusWand = null;
    public GameObject CameraWand = null;
    public GameObject HydraNotifier = null;

    private bool useOculus = true;
    private bool useHydra = true;

    void Awake()
    {
        // var devSettingsPath = Directory.GetCurrentDirectory() + "\\DeveloperSettings.txt";
        // if (File.Exists(devSettingsPath))
        // {
        //     using (var devSettings = new StreamReader(devSettingsPath))
        //     {
        //         string line;
        //         while ((line = devSettings.ReadLine()) != null)
        //         {
        //             if (line.StartsWith("Oculus"))
        //             {
        //                 if (line.EndsWith("false"))
        //                 {
        //                     useOculus = false;
        //                 }
        //                 else if (!line.EndsWith("true"))
        //                 {
        //                     Debug.LogWarning(string.Format("Improperly formatted developer setting line: {0}", line));
        //                 }
        //             }
        //             else if (line.StartsWith("Hydra"))
        //             {
        //                 if (line.EndsWith("false"))
        //                 {
        //                     useHydra = false;
        //                 }
        //                 else if (!line.EndsWith("true"))
        //                 {
        //                     Debug.LogWarning(string.Format("Improperly formatted developer setting line: {0}", line));
        //                 }
        //             }
        //             else if (!string.IsNullOrEmpty(line))
        //             {
        //                 Debug.LogWarning(string.Format("Could not recognize developer setting line: {0}", line));
        //             }
        //         }
        //     }
        // }
        
        // if (useOculus)
        // {
        //     OculusController.SetActive(true);
            
        //     if (!useHydra)
        //     {
        //         OculusWand.SetActive(true);
        //     }
        // }
        // else
        // {
            // MainCamera.SetActive(true);
            
            // if (!useHydra)
            // {
                // CameraWand.SetActive(true);
            // }
        // }
    }

    // Update is called once per frame
    void Update ()
    {
        // if (useHydra)
        // {
        //     if (!SixenseInput.IsBaseConnected(0))
        //     {
        //         if (!HydraNotifier.activeSelf)
        //         {
        //             HydraNotifier.SetActive(true);
        //         }
        //     }
        //     else
        //     {
        //         HandsController.SetActive(true);
        //         if (HydraNotifier.activeSelf)
        //         {
        //             HydraNotifier.SetActive(false);
        //         }
        //     }
        // }
    }
}
