using HarmonyLib;
using ThemeSupport.Data;

namespace ThemeSupport.Patcher;

[HarmonyPatch(typeof(GameLoad))]
public static class GameLoadPatch
{
    [HarmonyPostfix, HarmonyPatch("LoadMainGameData")]
    public static void LoadMainGameData_Postfix()
    {
        Loader.LoadAllData(DataCatalog.Catalog);
    }
}