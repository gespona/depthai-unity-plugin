# depthai-unity-plugin

![Demo](img/depthai-unity-plugin-face-detector.gif)
DepthAI Unity Plugin is native plugin designed to bring DepthAI cameras (OAK-1, OAK-D) to Unity

OAK cameras are Edge AI devices powered by Intel Movidius Myriad-X Inference vision processing units, so cameras are able to do inference deep learning models without need a host.

More info: https://www.luxonis.com
(I'm not related to luxonis)

DepthAI Unity Plugin is based on C++ DepthAI-Core library

# Main Goals

- Provide predefined pipelines (MobileNet, Face Detector, Pose, Face Mesh, Eye gaze, Hand Tracking, ....) very easy to use for Unity users (non-developers) p.eg: Face Detector prefab just drag and drop to your scene and be able to build more advanced applications like gesture control, motion capture, ...

- Provide full "low" API to create gen2 pipelines inside Unity

- Provide virtual OAK cameras to use inside Unity (p.eg: syntetic dataset creaation)

# Usage

## Face Detector Example

In new scene, just drag and drop the FaceDetectorCanvas prefab

You can check example scene: FaceDetectorScene

![Demo](img/depthai-unity-face-detector.gif)

## OverCloud demo (airplane control)

OverCloud (aka airplane demo) is paid asset from assetstore, so it's not included in this repo.
(I'm not related to the creator of OverCloud asset)

If you own OverCloud it's very easy to control the airplane of demo scene:

- Add OverCloudFaceDetectorImage prefab inside the canvas of demo scene.

That's all :) This will start automatically the face detector pipeline and has linked the mouseAim of the airplane to the x-axis center.

## API

- void InitFaceDetector(string nnPath): Initialize face detector pipeline with first available device using blob model located in nnPath (by default /Assets/Models)

- void FinishFaceDetector(): Close the device. This is very important to avoid Unity hangout or crash next time you start the pipeline again. In the example is called with "Stop Face Detector" button is pressed or OnApplicationQuit.

- void FaceDetectorPreview(IntPtr data, int width, int height): Get results from running pipeline and pass image preview in data parameter. Important because updates results

- void FaceDetectorResultsMX(): Get center x-coordinate of face detection result with highest score

- void FaceDetectorResultsMY(): Get center y-coordinate of face detection result with highest score

Note: Coordinate system is (0,0) bottom left and (max,max) top right

# Roadmap

- Add more predefined gen2 pipelines: 3D depth information, Emotion Detector, MobileNet, Face Mesh, Human Body Pose Estimation, Eye gaze, ....
- Multiple device support
- Virtual OAK cameras
- Plugin version for Windows and Linux
- Low level API to create pipelines from Unity
- Support to create pipelines using node-based tools like Playmaker, Bolt, ..
- Advanced examples like the airplane:
  - Motion Capture using human pose estimation
  - Face animation and lip sync using face mesh
  - Parallax camera movement using face pose

Please star / watch this repo and stay tunned !

# Requirements

Tested on

Unity 2020.1.7f (macOS Catalina 10.15.4)
More coming soon !

All render pipelines supported :)

# Preview Version

Work in this repo is at very early stage

Until July 2021 we'll be very busy working to complete and submit our proposal for OpenCV Spatial AI Contest so probably we'll not have enough time to update this repo fast enough. That said the plugin had a very warm welcome and rised lot of interest so we commit to work on this plugin and deliver the roadmap asap.
