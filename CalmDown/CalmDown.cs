using MelonLoader;
using System.Collections;
using VRC.UI.Core;

namespace VRCMods
{
    public class CalmDown : MelonMod
    {
        public static MelonLogger.Instance MyLogger => new("MyMod", System.ConsoleColor.Cyan);

        private static MelonPreferences_Category CalmDownCat = MelonPreferences.CreateCategory("CalmDown");
        private static MelonPreferences_Entry<bool> Enabled = CalmDownCat.CreateEntry("Enable PanicButton suppression (Requires Restart)", true);
        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(WaitForUiManagerInit());
        }

        private IEnumerator WaitForUiManagerInit()
        {
            while (VRCUiManager.field_Private_Static_VRCUiManager_0 == null) yield return null;
            while (UIManager.field_Private_Static_UIManager_0 == null) yield return null;

            if (Enabled.Value)
            {
                var InputHolder = VRCInputManager.field_Public_Static_ObjectPublicIInputActionCollection2IDisposableIInputActionCollectionIEnumerableIEnumerable1InputActionInObInNuOb1ReInObNuUnique_0;
                var safeMode = InputHolder.field_Private_InputAction_36;
                safeMode.Disable();

                LoggerInstance.Msg("Panic Button Disabled");
            }
        }
    }
}
