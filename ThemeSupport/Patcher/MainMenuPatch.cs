using HarmonyLib;
using ThemeSupport.MainMenuModule;

namespace ThemeSupport.Patcher;

[HarmonyPatch(typeof(MainMenu))]
public static class MainMenuPatch
{
    [HarmonyPostfix, HarmonyPatch("Awake")]
    public static void Awake_Postfix(MainMenu __instance)
    {
        MainMenuCtrl.Create(__instance);
    }
}