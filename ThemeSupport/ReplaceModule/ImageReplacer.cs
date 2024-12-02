using System.IO;
using LitJson;
using ThemeSupport.Data;
using UnityEngine;

namespace ThemeSupport.ReplaceModule;

public static class ImageReplacer
{
    public static void LoadReplaceData()
    {
        var modDirs = new DirectoryInfo(BepInEx.Paths.PluginPath).GetDirectories();

        foreach (var modDir in modDirs)
        {
            var path = Path.Combine(modDir.FullName, "ImageReplaceData.json");
            if (!File.Exists(path)) continue;

            var data = JsonMapper.ToObject(File.ReadAllText(path));
            Replace(data);
        }
    }

    private static void Replace(JsonData data)
    {
        if (!data.IsObject) return;

        if (data.ContainsKey("CardData.CardImage"))
            ReplaceCardImage(data["CardData.CardImage"]);

        if (data.ContainsKey("Encounter.EncounterImage"))
            ReplaceEncounterImage(data["Encounter.EncounterImage"]);

        if (data.ContainsKey("CharacterPerk.PerkIcon"))
            ReplacePerkIcon(data["CharacterPerk.PerkIcon"]);

        if (data.ContainsKey("GameStat.DefaultStatusIcon"))
            ReplaceStatDefIcon(data["GameStat.DefaultStatusIcon"]);

        if (data.ContainsKey("CardData.DefaultLiquidImage"))
            ReplaceCardDefLiqImage(data["CardData.DefaultLiquidImage"]);
    }

    private static void ReplaceCardImage(JsonData data)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var card = UniqueIDScriptable.GetFromID<CardData>(uid);
            if (!card) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name);
            if (!img) continue;

            card.CardImage = img;
        }
    }

    private static void ReplaceEncounterImage(JsonData data)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var encounter = UniqueIDScriptable.GetFromID<Encounter>(uid);
            if (!encounter) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name);
            if (!img) continue;

            encounter.EncounterImage = img;
        }
    }

    private static void ReplacePerkIcon(JsonData data)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var perk = UniqueIDScriptable.GetFromID<CharacterPerk>(uid);
            if (!perk) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name);
            if (!img) continue;

            perk.PerkIcon = img;
        }
    }

    private static void ReplaceStatDefIcon(JsonData data)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var stat = UniqueIDScriptable.GetFromID<GameStat>(uid);
            if (!stat) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name);
            if (!img) continue;

            stat.DefaultStatusIcon = img;
        }
    }

    private static void ReplaceCardDefLiqImage(JsonData data)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var card = UniqueIDScriptable.GetFromID<CardData>(uid);
            if (!card) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name);
            if (!img) continue;

            card.DefaultLiquidImage = img;
        }
    }
}