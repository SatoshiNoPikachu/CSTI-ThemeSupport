using HarmonyLib;
using ThemeSupport.GameModule;

namespace ThemeSupport.Patcher;

[HarmonyPatch(typeof(InGameCardBase))]
public static class InGameCardBasePatch
{
    [HarmonyPostfix, HarmonyPatch("Init")]
    public static void Init_Postfix(InGameCardBase __instance)
    {
        BackCtrl.Instance?.OnCardInit(__instance);
    }
}