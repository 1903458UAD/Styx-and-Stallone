using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BridgeHandler : EditorWindow
{
    #region Variables
    public GameObject bridgePrefab;

    private float bridgeLength;
    private float segmentLength;

    private int required;

    private Vector3 startPoint;
    private Vector3 endPoint;

    private bool firstSelected = false;
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
            firstSelected = false;
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        Event eventBuild = Event.current;
        if (eventBuild.type == EventType.MouseDown && eventBuild.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(eventBuild.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!firstSelected)
                {
                    startPoint = hit.point;
                    firstSelected = true;
                }
                else
                {
                    endPoint = hit.point;
                    CreateBridge(startPoint, endPoint);
                    firstSelected = false;
                }
                eventBuild.Use();
            }
        }
    }

    //Handles GUI visibility
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    public void CreateBridge(Vector3 startPoint, Vector3 endPoint)
    {
        bridgePrefab = Resources.Load<GameObject>("BridgeSegment");
        segmentLength = bridgePrefab.transform.localScale.z;
        bridgeLength = Vector3.Distance(startPoint, endPoint);
        required = Mathf.CeilToInt(bridgeLength / segmentLength);
        //Create a parent container
        GameObject bridgeParent = new GameObject ("Bridge");

        for (int i = 0; i < required; i++)
        {
            Vector3 position = Vector3.Lerp(startPoint, endPoint, (float)i / required);
            GameObject segment = (GameObject)PrefabUtility.InstantiatePrefab(bridgePrefab);
            segment.transform.position = position;
            segment.transform.rotation = Quaternion.LookRotation(endPoint - startPoint);
            segment.transform.SetParent(bridgeParent.transform);
            Undo.RegisterCreatedObjectUndo(segment, "Created Bridge Segment");
        }
    }
}
