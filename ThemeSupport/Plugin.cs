using BepInEx;
using HarmonyLib;
using ModCore;
using ModCore.Data;
using ThemeSupport.ReplaceModule;

namespace ThemeSupport;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
internal class Plugin : BaseUnityPlugin<Plugin>
{
    private const string PluginGuid = "Pikachu.CSFF.ThemeSupport";
    public const string PluginName = "ThemeSupport";
    public const string PluginVersion = "0.1.1";

    private static readonly Harmony Harmony = new(PluginGuid);

    protected override void Awake()
    {
        base.Awake();
        Harmony.PatchAll();

        Loader.LoadCompleteEvent += ImageReplacer.LoadReplaceData;

        Log.LogMessage($"Plugin {PluginName} is loaded!");
    }
}