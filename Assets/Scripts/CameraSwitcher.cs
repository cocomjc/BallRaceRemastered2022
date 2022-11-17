using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    private static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    private static CinemachineVirtualCamera currentCamera = null;


    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        return currentCamera == camera;
    }
    
    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }

    public static void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = 10;
        currentCamera = camera;

        foreach (CinemachineVirtualCamera cam in cameras)
        {
            if (cam != currentCamera)
            {
                cam.Priority = 0;
            }
        }
    }
}