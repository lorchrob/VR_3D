using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationVisualization : MonoBehaviour
{
    // ShootTransform monobehaviour for transformable raycasts
    public ShootTransform shootTransform;

    // The GameObjects making up the visualization
    public GameObject xAxisCylinder;
    public GameObject zAxisCylinder;
    public GameObject yAxisCylinder;
    public GameObject xAxisSphere1;
    public GameObject xAxisSphere2;
    public GameObject zAxisSphere1;
    public GameObject zAxisSphere2;
    public GameObject yAxisSphere1;
    public GameObject yAxisSphere2;

    private GameObject[] visualizationObjects;

    // Parameters for the visualization objects
    public float axisLengthHeadRoom = .5f;
    public float axisWidth = .01f;
    public float sphereRadius = .01f;

    // Monobehaviour states
    public enum OVState {
        idling,
        visualizing
    }

    // We start in the idling state
    private OVState state = OVState.idling;

    // Initialize the array of visualization objects
    private void Start()
    {
        visualizationObjects = new GameObject[9];
        visualizationObjects[0] = xAxisCylinder;
        visualizationObjects[1] = zAxisCylinder;
        visualizationObjects[2] = yAxisCylinder;
        visualizationObjects[3] = xAxisSphere1;
        visualizationObjects[4] = xAxisSphere2;
        visualizationObjects[5] = zAxisSphere1;
        visualizationObjects[6] = zAxisSphere2;
        visualizationObjects[7] = yAxisSphere1;
        visualizationObjects[8] = yAxisSphere2;

        // Deactivate all visualization GameObjects
        SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool hitTransformable = shootTransform.RaycastTransformable(out hit);

        if (state == OVState.idling)
        {
            if (hitTransformable)
            {
                state = OVState.visualizing;
                SetActive(true);
                UpdateVisualization(hit.collider.gameObject);
            }
        }
        else if (state == OVState.visualizing)
        {
            if (hitTransformable)
                UpdateVisualization(hit.collider.gameObject);
            else
            {
                SetActive(false);
                state = OVState.idling;
            }
        }
    }

    // Set the visualization GameObjects active / unactive
    private void SetActive(bool value)
    {
        foreach (GameObject gameObject in visualizationObjects)
        {
            gameObject.SetActive(value);
        }
    }

    // Given a hit transformable, update the visualization
    private void UpdateVisualization(GameObject transformable)
    {
        Vector3 forward = transformable.transform.forward;
        Vector3 right = transformable.transform.right;
        Vector3 up = transformable.transform.up;

        Utils.OrientCylinder(xAxisCylinder, transformable.transform.position, transformable.transform.position + right * (transformable.GetComponent<BlenderMesh>().maxDist.x + axisLengthHeadRoom), axisWidth); ;
        Utils.OrientSphere(xAxisSphere1, transformable.transform.position + right * (transformable.GetComponent<BlenderMesh>().maxDist.x + axisLengthHeadRoom), sphereRadius);
        Utils.OrientSphere(xAxisSphere2, transformable.transform.position - right * (transformable.GetComponent<BlenderMesh>().maxDist.x + axisLengthHeadRoom), sphereRadius);


        Utils.OrientCylinder(zAxisCylinder, transformable.transform.position, transformable.transform.position + forward * (transformable.GetComponent<BlenderMesh>().maxDist.z + axisLengthHeadRoom), axisWidth);
        Utils.OrientSphere(zAxisSphere1, transformable.transform.position + forward * (transformable.GetComponent<BlenderMesh>().maxDist.z + axisLengthHeadRoom), sphereRadius);
        Utils.OrientSphere(zAxisSphere2, transformable.transform.position - forward * (transformable.GetComponent<BlenderMesh>().maxDist.z + axisLengthHeadRoom), sphereRadius);

        Utils.OrientCylinder(yAxisCylinder, transformable.transform.position, transformable.transform.position + up * (transformable.GetComponent<BlenderMesh>().maxDist.y + axisLengthHeadRoom), axisWidth);
        Utils.OrientSphere(yAxisSphere1, transformable.transform.position + up * (transformable.GetComponent<BlenderMesh>().maxDist.y + axisLengthHeadRoom), sphereRadius);
        Utils.OrientSphere(yAxisSphere2, transformable.transform.position - up * (transformable.GetComponent<BlenderMesh>().maxDist.y + axisLengthHeadRoom), sphereRadius);


    }

}
