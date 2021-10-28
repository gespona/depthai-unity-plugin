using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OverCloudDaiFaceDetector : MonoBehaviour
{
    //Lets make our calls from the Plugin

    #if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        [DllImport("depthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool InitFaceDetector(string nnPath);
        [DllImport("depthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool FaceDetectorPreview(IntPtr data, int width, int height);
        [DllImport("depthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern int FaceDetectorResultsMX();
        [DllImport("depthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern int FaceDetectorResultsMY();
        [DllImport("depthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool FinishFaceDetector();
    #else
        [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool InitFaceDetector(string nnPath);
        [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool FaceDetectorPreview(IntPtr data, int width, int height);
        [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern int FaceDetectorResultsMX();
        [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern int FaceDetectorResultsMY();
        [DllImport("libdepthai-core", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool FinishFaceDetector();
    #endif


    public Image cameraImage;
    private Texture2D cameraTexture;
    
    private Texture2D tex;
    private Color32[] pixel32;
    private GCHandle pixelHandle;
    private IntPtr pixelPtr;

    private bool deviceRunning;
    
    public int faceDetectorResultsMX;
    public int faceDetectorResultsMY;
    
    public Transform mouseAim;
    
    void Start()
    {
        cameraTexture = new Texture2D(300, 300);    
        InitTexture();
        cameraImage.material.mainTexture = tex;
        deviceRunning = false;
        ConnectDevice();
    }

    public void ConnectDevice()
    {
        InitFaceDetector(Application.dataPath+"/Models/face-detection-retail-0004_openvino_2021.2_4shave.blob");
        deviceRunning = true;
    }

    public void FinishDevice()
    {
        if (deviceRunning) 
        {
            deviceRunning = false;
            FinishFaceDetector();
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
            FaceDetectorPreview(pixelPtr, tex.width, tex.height);
            faceDetectorResultsMX = FaceDetectorResultsMX();
            faceDetectorResultsMY = FaceDetectorResultsMY();

            mouseAim.Rotate(0.0f,(faceDetectorResultsMX - 150)/75.0f,0.0f,Space.Self);

            tex.SetPixels32(pixel32);
            tex.Apply();
        }
    }
}
