using System.Collections;
using HarmonyLib;
using ThemeSupport.GameModule;

namespace ThemeSupport.Patcher;

[HarmonyPatch(typeof(GameManager))]
public static class GameManagerPatch
{
    [HarmonyPrefix, HarmonyPatch("Awake")]
    public static void Awake_Prefix()
    {
        BackCtrl.Create();
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