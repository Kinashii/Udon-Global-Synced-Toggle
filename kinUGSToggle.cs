using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class kinUGSToggle : UdonSharpBehaviour
{
    // Global synced toggle made by: Kinashi ღ#2321

    public GameObject objectToToggle;
    [UdonSynced] public bool isOn;

    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        if (Networking.LocalPlayer.isMaster)
        {
            if (isOn)
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "toggleOn");
            else
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "toggleOff");
        }
    }

    public override void Interact()
    {
        if (isOn)
        {
            isOn = false;
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "toggleOff");
        }
        else
        {
            isOn = true;
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "toggleOn");
        }
    }

    public void toggleOn()
    {
        objectToToggle.SetActive(true);
    }

    public void toggleOff()
    {
        objectToToggle.SetActive(false);
    }
}
