using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace xyz.kinashi
{
    public class kinUGSToggle : UdonSharpBehaviour
    {
        // Declare global synced toggle made by Kinashi ღ#2321
        [Tooltip("The object to toggle on/off.")]
        public GameObject objectToToggle;

        [Tooltip("Toggle state.")] 
        [UdonSynced] public bool isOn;

        // Triggered when a player joins the game.
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            // Only the master client can change the toggle state.
            if (Networking.LocalPlayer.isMaster)
            {
                // Toggle object state based on the current toggle state.
                if (isOn)
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "toggleOn");
                else
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "toggleOff");
            }
        }

        // Triggered when a player interacts with the object.
        public override void Interact()
        {
            // Toggle the current state of the object.
            isOn = !isOn;

            // Toggle object state based on the new toggle state.
            if (isOn)
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "toggleOff");
            else
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "toggleOn");
        }

        // Turn the object on.
        public void toggleOn() => objectToToggle.SetActive(true);

        // Turn the object off.
        public void toggleOff() => objectToToggle.SetActive(false);
    }
}
