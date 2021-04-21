# depthai-unity-plugin

![Demo](img/depthai-unity-plugin-face-detector.gif)
DepthAI Unity Plugin is native plugin designed to bring DepthAI cameras (OAK-1, OAK-D cameras) to Unity

OAK cameras are Edge AI cameras powered by Intel Myriad-X Inference processor, so cameras are able to inference deep learning models.

More info: https://www.luxonis.com

DepthAI Unity Plugin is based on C++ DepthAI-Core library

# Main Goals

- Provide predefined pipelines (MobileNet, Face Detector, Pose, Face Mesh, Eye gaze, Hand Tracking, ....) very easy to use for Unity users (non-developers) p.eg: Face Detector prefab just drag and drop to your scene

- Provide full "low" API to create gen2 pipelines inside Unity

- Provide virtual OAK cameras to use inside Unity (p.eg: syntetic dataset creaation)

# Usage

## Face Detector Example

In new scene, just drag and drop the FaceDetector prefab

You can check example scene: FaceDetector

Example using face detector results to control airplane in demo scene from OverCloud asset/package (not included in this repo)

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
