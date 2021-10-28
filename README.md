# depthai-unity-plugin

![Demo](img/depthai-unity-plugin-face-detector.gif)
Face detector example

![Demo](img/head-pose-rt.gif)

Head pose example

# OAK for Unity - Coming soon to Unity AssetStore !
DepthAI Unity Plugin (**OAK for Unity**) is native plugin designed to bring Edge AI to Unity thanks to DepthAI cameras (OAK-1, OAK-D)

OAK cameras are Edge AI devices powered by Intel Movidius Myriad-X Inference vision processing units, so cameras are able to do inference deep learning models without need a host.
It combines neural inference, depth vision, and feature tracking into an easy-to-use solution.

More info: https://www.luxonis.com
(I'm not related to luxonis)

DepthAI Unity Plugin is based on C++ DepthAI-Core library

**if you find this repo interesting please star and watch !**

# Main Goals

- Provide high-level API with predefined pipelines (MobileNet, Face Detector, Pose, Face Mesh, Eye gaze, Hand Tracking, ....) very easy to use for Unity users (usually non-developers) p.eg: Face Detector prefab just drag and drop to your scene and be able to build more advanced applications like gesture control, motion capture, ...

- Provide full "low" API to create gen2 pipelines inside Unity

- Provide virtual OAK cameras to use inside Unity for other type of applications (p.eg: syntetic dataset creaation)

# Usage

# Face Detector Example

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

- int FaceDetectorResultsMX(): Get center x-coordinate of face detection result with highest score

- int FaceDetectorResultsMY(): Get center y-coordinate of face detection result with highest score

Note: Coordinate system is (0,0) bottom left and (max,max) top right

# Head Pose Example

2-stage head pose pipeline: face detector + head pose

![Demo](img/head-pose-pipeline.gif)

In new scene, just drag and drop the HeadPoseCanvas prefab

You can check example scene: HeadPoseScene

![Demo](img/unity-plugin-head-pose.gif)

Head update is filtered to avoid some small flickness. You can tweak this on the Update() method inside the DaiHeadPose.cs script

Here example with no limitation:

![Demo](img/head-pose-rt.gif)

Note: Blocky guy is from Block People free demo asset so it's included in the repo but you can find it also on the Unity AssetStore

## API

- void InitHeadPose(string nnPath,string nnPath2): Initialize head pose pipeline with first available device using the 2 blob models located in nnPath/nnPath2 (by default /Assets/Models)

- void FinishHeadPose(): Close the device. This is very important to avoid Unity hangout or crash next time you start the pipeline again. In the example is called with "Stop Head Pose" button is pressed or OnApplicationQuit.

- void HeadPosePreview(IntPtr data, int width, int height): Get results from running pipeline and pass image preview in data parameter. Important because updates results

- float HeadPoseYaw(): Get yaw from head pose estimation

- float HeadPoseRoll(): Get roll from head pose estimation

- float HeadPosePitch(): Get pitch from head pose estimation

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

**Please star / watch this repo and stay tunned !**

# Requirements

Requires OpenCV 3.0 installed

Tested on
Unity 2020.1.7f (macOS Catalina 10.15.4)
Unity 2020.1.7f (Windows 10)

More coming soon !

All render pipelines supported :)

# Known issues

## [Any Platform - Unity] Failed to load window layout
Sometimes when you open the project Unity gives this error:
"Failed to load window layout"

To fix this issue just copy "CurrentLayout-default.dwlt" file you can find in the root folder of this repo to "Library" folder and replace the old file.

## [MacOS] Issues opening depthai-core lib because it's not verified
As it's early version modified depthai-core lib is not signed/verified. So as usual with Mac when you download something directly from internet is blocking the file by default. You need to go with finder to folder Assets/Plugins/macOS, do right-click on libdepthai-core, click "Open" and confirm again with "Open". From here file is ok so Unity editor can access to the library.

## [MacOS] DllNotFoundException: libdepthai-core
If after previous step, Unity editor is still complaining about issues with libdepthai-core is very likely because can't load the library file due dependencies failing. See above it requires OpenCV 3.0 installed on the machine. To confirm the issue is because missing OpenCV 3.0 installation, inside Unity editor go to project window, Assets/Plugins/macOS and click on libdepthai-core. In the inspector window, click on "Load on startup" and press apply button. If you don't have OpenCV 3.0 installed you will get a message in the console window similar to "library not loaded: /usr/local/opt/opencv@3/lib/libxxxxx"


# Preview Version

Work in this repo is at very early stage

Until July 2021 we'll be very busy working to complete and submit our proposal for OpenCV Spatial AI Contest so probably we'll not have enough time to update this repo fast enough. That said the plugin had a very warm welcome and rised lot of interest so we commit to work on this plugin and deliver the roadmap asap.
