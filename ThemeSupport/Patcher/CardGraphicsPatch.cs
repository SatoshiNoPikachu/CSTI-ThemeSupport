using HarmonyLib;
using ThemeSupport.GameModule;

namespace ThemeSupport.Patcher;

[HarmonyPatch(typeof(CardGraphics))]
internal static class CardGraphicsPatch
{
    [HarmonyPostfix, HarmonyPatch("UpdateInventoryInfo")]
    public static void UpdateInventoryInfo_Postfix(CardGraphics __instance)
    {
        CardUniEx.OnUpdateInventoryInfo(__instance);
    }
}