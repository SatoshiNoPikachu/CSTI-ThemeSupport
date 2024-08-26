﻿using System.IO;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace ThemeSupport;

[BepInPlugin(PluginGuid, PluginName, PluginVersion)]
internal class Plugin : BaseUnityPlugin
{
    private const string PluginGuid = "Pikachu.CSTIMod.ThemeSupport";
    public const string PluginName = "ThemeSupport";
    public const string PluginVersion = "1.0.0";

    public static Plugin Instance = null!;
    public static ManualLogSource Log = null!;
    private static readonly Harmony Harmony = new(PluginGuid);

    public static string PluginPath => Path.GetDirectoryName(Instance.Info.Location);

    private void Awake()
    {
        Instance = this;
        Log = Logger;
        Harmony.PatchAll();
        Log.LogInfo($"Plugin {PluginName} is loaded!");
    }
}