using HarmonyLib;
using ThemeSupport.GameBackModule;

namespace ThemeSupport.Patcher;

[HarmonyPatch(typeof(InGameCardBase))]
internal static class InGameCardBasePatch
{
    [HarmonyPostfix, HarmonyPatch("Init")]
    public static void Init_Postfix(InGameCardBase __instance)
    {
        BackCtrl.Instance?.OnCardInit(__instance);
    }
}