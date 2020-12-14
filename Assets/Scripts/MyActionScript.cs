using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MyActionScript : MonoBehaviour
{
    public SteamVR_Action_Boolean Trigger;
    public SteamVR_Action_Boolean Grip;
    public SteamVR_Action_Boolean EquipMenuBool;

    public SteamVR_Input_Sources RightHand;
    public SteamVR_Input_Sources LeftHand;

    public GameObject Player;
    public GameObject EquipMenu;


    void Start()
    {
        Trigger.AddOnStateDownListener(TriggerDown, RightHand);
        Trigger.AddOnStateUpListener(TriggerUp, RightHand);

        Grip.AddOnStateDownListener(GripDown, RightHand);
        Grip.AddOnStateUpListener(GripUp, RightHand);

        EquipMenuBool.AddOnStateDownListener(ToggleMenu, LeftHand);

    }

    public void TriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //Debug.Log("Trigger is Up");
        
    }

    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //Debug.Log("Trigger is Down");
        Player.GetComponent<ShootTransform>().ShootMatrix();
        
    }

    public void GripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //Debug.Log("Grip is Up");
    }

    public void GripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        //Debug.Log("Grip is Down");
        Player.GetComponent<ShootTransform>().ShootInverse();
    }

    public void ToggleMenu(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if(EquipMenu.activeSelf == true)
        {
            EquipMenu.SetActive(false);
        }
        else
        {
            EquipMenu.SetActive(true);
        }
        
    }


}
