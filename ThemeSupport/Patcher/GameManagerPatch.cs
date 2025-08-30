using System.Collections;
using HarmonyLib;
using ThemeSupport.GameBackModule;

namespace ThemeSupport.Patcher;

[HarmonyPatch(typeof(GameManager))]
internal static class GameManagerPatch
{
    [HarmonyPrefix, HarmonyPatch("Awake")]
    public static void Awake_Prefix()
    {
        BackCtrl.Create();
        BackCtrl.HideLocationSlotImg();
    }

    [HarmonyPostfix, HarmonyPatch("RemoveCard")]
    public static IEnumerator RemoveCard_Postfix(IEnumerator enumerator, InGameCardBase _Card)
    {
        BackCtrl.Instance?.OnRemoveCard(_Card);
        yield return enumerator;
    }

    [HarmonyPostfix, HarmonyPatch("ChangeStatValue")]
    public static IEnumerator ChangeStatValue_Postfix(IEnumerator enumerator, InGameStat _Stat)
    {
        yield return enumerator;
        BackCtrl.Instance?.OnStatChange(_Stat);
    }
}