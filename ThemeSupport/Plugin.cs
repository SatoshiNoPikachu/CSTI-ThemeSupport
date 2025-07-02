using BepInEx;
using HarmonyLib;
using ModCore;
using ModCore.Data;
using ThemeSupport.ReplaceModule;

namespace ThemeSupport;

[BepInDependency("Pikachu.CSTI.ModCore")]
[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
[ModNamespace(PluginName)]
internal class Plugin : BaseUnityPlugin<Plugin>
{
    private const string PluginGuid = "Pikachu.CSTIMod.ThemeSupport";
    public const string PluginName = "ThemeSupport";
    public const string PluginVersion = "2.0.1";

    private static readonly Harmony Harmony = new(PluginGuid);

    protected override void OnAwake()
    {
        Harmony.PatchAll();

        Loader.LoadCompleteEvent += ImageReplacer.LoadReplaceData;
    }
}