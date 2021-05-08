using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DaiHeadPose : MonoBehaviour
{
    //Lets make our calls from the Plugin

    [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool InitHeadPose(string nnPath,string nnPath2);

    [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool HeadPosePreview(IntPtr data, int width, int height);

    [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
    private static extern float HeadPoseYaw();

    [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
    private static extern float HeadPoseRoll();

    [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
    private static extern float HeadPosePitch();

    [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
    private static extern bool FinishHeadPose();


    public Image cameraImage;
    private Texture2D cameraTexture;
    
    private Texture2D tex;
    private Color32[] pixel32;
    private GCHandle pixelHandle;
    private IntPtr pixelPtr;

    private bool deviceRunning;
    
    public float headPoseYaw;
    public float headPoseRoll;
    public float headPosePitch;

    private float oldHeadPoseYaw;
    private float oldHeadPoseRoll;
    private float oldHeadPosePitch;

    public Text valueCenterYaw;
    public Text valueCenterRoll;
    public Text valueCenterPitch;

    public GameObject cube;

    void Start()
    {
        cameraTexture = new Texture2D(300, 300);    
        InitTexture();
        cameraImage.material.mainTexture = tex;
        deviceRunning = false;

        oldHeadPoseYaw = 0.0f;
        oldHeadPoseRoll = 0.0f;
        oldHeadPosePitch = 0.0f;

    }

    public void ConnectDevice()
    {
        InitHeadPose(Application.dataPath+"/Models/face-detection-retail-0004_openvino_2021.2_4shave.blob", Application.dataPath+"/Models/head-pose-estimation-adas-0001_openvino_2021.2_4shave.blob");
        deviceRunning = true;
    }

    public void FinishDevice()
    {
        if (deviceRunning) 
        {
            deviceRunning = false;
            FinishHeadPose();
        }
    }

    void InitTexture()
    {
        tex = new Texture2D(300, 300, TextureFormat.ARGB32, false);
        pixel32 = tex.GetPixels32();
        //Pin pixel32 array
        pixelHandle = GCHandle.Alloc(pixel32, GCHandleType.Pinned);
        //Get the pinned address
        pixelPtr = pixelHandle.AddrOfPinnedObject();
    }

    void OnApplicationQuit()
    {
        FinishDevice();
        //Free handle
        pixelHandle.Free();
    }

    void Update()
    {
        if (deviceRunning)
        {
            HeadPosePreview(pixelPtr, tex.width, tex.height);
            headPoseYaw = HeadPoseYaw();
            headPoseRoll = HeadPoseRoll();
            headPosePitch = HeadPosePitch();

            valueCenterYaw.text = headPoseYaw.ToString();
            valueCenterRoll.text = headPoseRoll.ToString();
            valueCenterPitch.text = headPosePitch.ToString();

            if (Mathf.Abs(headPoseRoll - oldHeadPoseRoll) > 2.0f && Mathf.Abs(headPosePitch - oldHeadPosePitch) > 2.0f && Mathf.Abs(headPoseYaw - oldHeadPoseYaw)>2.0f)
            {

                // roll
                //cube.transform.Rotate(0f, 0f, headPoseRoll - oldHeadPoseRoll, Space.Self);
                
                cube.transform.Rotate(0f, (headPoseRoll - oldHeadPoseRoll), 0f, Space.Self);
    
                // // pitch
                //cube.transform.Rotate(-(headPosePitch - oldHeadPosePitch), 0f, 0f, Space.Self);
    
                cube.transform.Rotate(0f, 0f, -(headPosePitch - oldHeadPosePitch), Space.Self);
                // // yaw
                //cube.transform.Rotate(0f, (headPoseYaw - oldHeadPoseYaw), 0f, Space.Self);
                cube.transform.Rotate((headPoseYaw - oldHeadPoseYaw), 0f, 0f, Space.Self);

                oldHeadPoseRoll = headPoseRoll;
                oldHeadPoseYaw = headPoseYaw;
                oldHeadPosePitch = headPosePitch;
            }
            tex.SetPixels32(pixel32);
            tex.Apply();
        }
    }
}
