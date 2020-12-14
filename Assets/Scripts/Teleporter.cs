using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class Teleporter : MonoBehaviour
{
    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleportAction;

    public Material validTransport;
    public Material invalidTransport;

    public float teleportHeight;
    public float teleportDistanceSqr;
    public float validAngle;
    public float heightOffset;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;
    private bool m_isTeleporting = false;
    private bool m_validTransport = false;
    private bool m_validAngle = false;
    private float m_FadeTime = 0.5f;

    

    // Start is called before the first frame update
    void Start()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        // Pointer
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);

        // Get camera rig, and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // Figure out translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y - heightOffset, headPosition.z);
        Vector3 translateVector = m_Pointer.transform.position - groundPosition;

        if (translateVector.y > teleportHeight || translateVector.y < -teleportHeight || translateVector.sqrMagnitude > teleportDistanceSqr || !m_validAngle)
        {
            m_validTransport = false;
        }
        else
        {
            m_validTransport = true;
        }

        if (m_validTransport)
        {
            m_Pointer.GetComponent<MeshRenderer>().material = validTransport;
        }
        else
        {
            m_Pointer.GetComponent<MeshRenderer>().material = invalidTransport;
        }

        // Teleport
        if (m_TeleportAction.GetStateUp(m_Pose.inputSource))
            TryTeleport(cameraRig, translateVector);

        
    }

    void TryTeleport(Transform cameraRig, Vector3 translateVector)
    {
        // Check for valid position, and if already teleporting
        if (!m_HasPosition || m_isTeleporting || !m_validTransport)
            return;

        // Move
        StartCoroutine(MoveRig(cameraRig, translateVector));

    }

    IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        // Flag
        m_isTeleporting = true;

        // Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        // Apply translation
        yield return new WaitForSeconds(m_FadeTime);
        cameraRig.position += translation;

        // Fade to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

        // De-flag
        m_isTeleporting = false;

    }

    bool UpdatePointer()
    {
        // Ray from the controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // It it's a hit
        if(Physics.Raycast(ray, out hit))
        {
            m_Pointer.transform.position = hit.point;
            if(Vector3.Dot(Vector3.up, hit.normal.normalized) < validAngle)
            {
                m_validAngle = false;
                Debug.Log(Vector3.Dot(Vector3.up, hit.normal.normalized));
            }
            else
            {
                m_validAngle = true;
            }
            return true;
        }


        // If not a hit
        return false;
    }

}
