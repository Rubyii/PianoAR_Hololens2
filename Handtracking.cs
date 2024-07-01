using MixedReality.Toolkit.Subsystems;
using MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft;
using Microsoft.MixedReality.OpenXR.ARSubsystems;
using MixedReality.Toolkit.Input;
using UnityEngine.XR;

public class Handtracking : MonoBehaviour
{
    IHandsAggregatorSubsystem aggregator;
    public GameObject sphereMarker;

    GameObject thumbObjectR;
    GameObject indexObjectR;
    GameObject middleObjectR;
    GameObject ringObjectR;
    GameObject pinkyObjectR;

    GameObject thumbObjectL;
    GameObject indexObjectL;
    GameObject middleObjectL;
    GameObject ringObjectL;
    GameObject pinkyObjectL;


    void Start()
    {
        InitializeHandTracking();
        indexObjectR = Instantiate(sphereMarker, this.transform);
        /*
        thumbObjectR = Instantiate(sphereMarker, this.transform);
        middleObjectR = Instantiate(sphereMarker, this.transform);
        ringObjectR = Instantiate(sphereMarker, this.transform);
        pinkyObjectR = Instantiate(sphereMarker, this.transform);
        */
        indexObjectL = Instantiate(sphereMarker, this.transform);
        /*
        thumbObjectL = Instantiate(sphereMarker, this.transform);
        middleObjectL = Instantiate(sphereMarker, this.transform);
        ringObjectL = Instantiate(sphereMarker, this.transform);
        pinkyObjectL = Instantiate(sphereMarker, this.transform);
        */
    }


    void Update()
    {
        indexObjectR.GetComponent<Renderer>().enabled = false;

        /*
        thumbObjectR.GetComponent<Renderer>().enabled = false;
        middleObjectR.GetComponent<Renderer>().enabled = false;
        ringObjectR.GetComponent<Renderer>().enabled = false;
        pinkyObjectR.GetComponent<Renderer>().enabled = false;
        */

        indexObjectL.GetComponent<Renderer>().enabled = false;
        /*
        thumbObjectL.GetComponent<Renderer>().enabled = false;
        middleObjectL.GetComponent<Renderer>().enabled = false;
        ringObjectL.GetComponent<Renderer>().enabled = false;
        pinkyObjectL.GetComponent<Renderer>().enabled = false;
        */

        //RightHand
        if (aggregator.TryGetJoint(TrackedHandJoint.IndexTip, XRNode.RightHand, out HandJointPose jointPoseI))
        {
            indexObjectR.GetComponent<Renderer>().enabled = true;
            indexObjectR.transform.position = jointPoseI.Position;
        }

        /*
        if (aggregator.TryGetJoint(TrackedHandJoint.ThumbTip, XRNode.RightHand, out HandJointPose jointPoseD))
        {
            thumbObjectR.GetComponent<Renderer>().enabled = true;
            thumbObjectR.transform.position = jointPoseD.Position;
        }


        if (aggregator.TryGetJoint(TrackedHandJoint.MiddleTip, XRNode.RightHand, out HandJointPose jointPoseM))
        {
            middleObjectR.GetComponent<Renderer>().enabled = true;
            middleObjectR.transform.position = jointPoseM.Position;
        }

        if (aggregator.TryGetJoint(TrackedHandJoint.RingTip, XRNode.RightHand, out HandJointPose jointPoseR))
        {
            ringObjectR.GetComponent<Renderer>().enabled = true;
            ringObjectR.transform.position = jointPoseR.Position;
        }

        if (aggregator.TryGetJoint(TrackedHandJoint.LittleTip, XRNode.RightHand, out HandJointPose jointPoseP))
        {
            pinkyObjectR.GetComponent<Renderer>().enabled = true;
            pinkyObjectR.transform.position = jointPoseP.Position;
        }
        */
        //LeftHand
        if (aggregator.TryGetJoint(TrackedHandJoint.IndexTip, XRNode.LeftHand, out HandJointPose jointPoseIL))
        {
            indexObjectL.GetComponent<Renderer>().enabled = true;
            indexObjectL.transform.position = jointPoseIL.Position;
        }
        /*
        if (aggregator.TryGetJoint(TrackedHandJoint.ThumbTip, XRNode.LeftHand, out HandJointPose jointPoseDL))
        {
            thumbObjectL.GetComponent<Renderer>().enabled = true;
            thumbObjectL.transform.position = jointPoseDL.Position;
        }


        if (aggregator.TryGetJoint(TrackedHandJoint.MiddleTip, XRNode.LeftHand, out HandJointPose jointPoseML))
        {
            middleObjectL.GetComponent<Renderer>().enabled = true;
            middleObjectL.transform.position = jointPoseML.Position;
        }

        if (aggregator.TryGetJoint(TrackedHandJoint.RingTip, XRNode.LeftHand, out HandJointPose jointPoseRL))
        {
            ringObjectL.GetComponent<Renderer>().enabled = true;
            ringObjectL.transform.position = jointPoseRL.Position;
        }

        if (aggregator.TryGetJoint(TrackedHandJoint.LittleTip, XRNode.LeftHand, out HandJointPose jointPosePL))
        {
            pinkyObjectL.GetComponent<Renderer>().enabled = true;
            pinkyObjectL.transform.position = jointPosePL.Position;
        }
        */
    }

    void InitializeHandTracking()
    {
        aggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
    }
}
