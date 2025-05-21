using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BridgeHandler : EditorWindow
{
    #region Variables
    public GameObject bridgePrefab;
    private GameObject anchorPlatform;
    private GameObject rotatePlatform;
    private GameObject rotatePar;
    private GameObject lastObject;

    private float bridgeLength;
    private float segmentLength;

    private int required;

    #endregion

    [MenuItem("Tools/Bridge Connector")]
    public static void ShowWindow()
    {
        GetWindow<BridgeHandler>("Bridge Connector");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select two platform edges to connect a bridge.", EditorStyles.boldLabel);

        if (GUILayout.Button("Reset Selection"))
        {
            anchorPlatform = null;
            rotatePlatform = null;
        }

        anchorPlatform = (GameObject)EditorGUILayout.ObjectField("Anchor", anchorPlatform, typeof(GameObject), true);
        rotatePlatform = (GameObject)EditorGUILayout.ObjectField("Rotate Target", rotatePlatform, typeof(GameObject), true);

        if (GUILayout.Button("Rotate") && anchorPlatform != null && rotatePlatform != null)
        {
            AlignPlatforms();
        }

        if (GUILayout.Button("Create Bridge") && anchorPlatform != null && rotatePlatform != null)
        {
            lastObject = null;
            CreateBridge(anchorPlatform.transform.position, rotatePlatform.transform.position);
        }
    }

    private void AlignPlatforms()
    {
        Vector3 localDirection = anchorPlatform.transform.position - rotatePlatform.transform.position;
        rotatePar = rotatePlatform.transform.parent.gameObject;
        Vector3 globalDirection = rotatePar.transform.TransformDirection(localDirection);
        Quaternion rotation = Quaternion.LookRotation(globalDirection, Vector3.up);
        
        rotatePar.transform.rotation = rotation;
    }

    public void CreateBridge(Vector3 startPoint, Vector3 endPoint)
    {
        bridgePrefab = Resources.Load<GameObject>("BridgeSegment");
        segmentLength = bridgePrefab.transform.localScale.z;
        bridgeLength = Vector3.Distance(startPoint, endPoint);
        required = Mathf.CeilToInt(bridgeLength / segmentLength);
        //Create a parent container
        GameObject bridgeParent = new GameObject ("Bridge");

        for (int i = 0; i < (required +1); i++)
        {
            Vector3 position = Vector3.Lerp(startPoint, endPoint, (float)i / required);
            GameObject segment = (GameObject)PrefabUtility.InstantiatePrefab(bridgePrefab);
            segment.transform.position = position;
            segment.transform.rotation = Quaternion.LookRotation(endPoint - startPoint);
            segment.transform.SetParent(bridgeParent.transform);
            if(lastObject != null)
            {
                segment.GetComponent<FixedJoint>().connectedBody = lastObject.GetComponent<Rigidbody>();
            }
            
            lastObject = segment;
        }
    }
}
