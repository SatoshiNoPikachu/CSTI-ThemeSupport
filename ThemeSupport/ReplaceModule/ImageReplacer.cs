using System.IO;
using LitJson;
using ModCore.Data;
using ModCore.Services;
using UnityEngine;

namespace ThemeSupport.ReplaceModule;

public static class ImageReplacer
{
    public static void LoadReplaceData()
    {
        foreach (var mod in ModService.GetMods())
        {
            var path = Path.Combine(mod.RootPath, "ImageReplaceData.json");
            if (!File.Exists(path)) continue;

            var data = JsonMapper.ToObject(File.ReadAllText(path));
            Replace(data, mod);
        }
    }

    private static void Replace(JsonData data, ModData mod)
    {
        if (!data.IsObject) return;

        if (data.ContainsKey("CardData.CardImage"))
            ReplaceCardImage(data["CardData.CardImage"], mod);

        if (data.ContainsKey("Encounter.EncounterImage"))
            ReplaceEncounterImage(data["Encounter.EncounterImage"], mod);

        if (data.ContainsKey("CharacterPerk.PerkIcon"))
            ReplacePerkIcon(data["CharacterPerk.PerkIcon"], mod);

        if (data.ContainsKey("GameStat.DefaultStatusIcon"))
            ReplaceStatDefIcon(data["GameStat.DefaultStatusIcon"], mod);

        if (data.ContainsKey("GameStat.Statuses"))
            ReplaceStatStatusesIcon(data["GameStat.Statuses"], mod);

        if (data.ContainsKey("CardData.DefaultLiquidImage"))
            ReplaceCardDefLiqImage(data["CardData.DefaultLiquidImage"], mod);
    }

    private static void ReplaceCardImage(JsonData data, ModData mod)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var card = UniqueIDScriptable.GetFromID<CardData>(uid);
            if (!card) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name, mod);
            if (!img) continue;

            card.CardImage = img;
        }
    }

    private static void ReplaceEncounterImage(JsonData data, ModData mod)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var encounter = UniqueIDScriptable.GetFromID<Encounter>(uid);
            if (!encounter) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name, mod);
            if (!img) continue;

            encounter.EncounterImage = img;
        }
    }

    private static void ReplacePerkIcon(JsonData data, ModData mod)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var perk = UniqueIDScriptable.GetFromID<CharacterPerk>(uid);
            if (!perk) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name, mod);
            if (!img) continue;

            perk.PerkIcon = img;
        }
    }

    private static void ReplaceStatDefIcon(JsonData data, ModData mod)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var stat = UniqueIDScriptable.GetFromID<GameStat>(uid);
            if (!stat) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name, mod);
            if (!img) continue;

            stat.DefaultStatusIcon = img;
        }
    }

    private static void ReplaceStatStatusesIcon(JsonData data, ModData mod)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var names = data[uid];
            if (!names.IsArray) continue;

            var status = UniqueIDScriptable.GetFromID<GameStat>(uid)?.Statuses;
            if (status is null) continue;

            for (var i = 0; i < names.Count; i++)
            {
                if (i >= status.Length) break;

                var name = names[i];
                if (name?.IsString is null or false) continue;

                var img = Database.GetData<Sprite>((string)name, mod);
                if (!img) continue;

                status[i].Icon = img;
            }
        }
    }

    private static void ReplaceCardDefLiqImage(JsonData data, ModData mod)
    {
        if (!data.IsObject) return;

        foreach (var uid in data.Keys)
        {
            var card = UniqueIDScriptable.GetFromID<CardData>(uid);
            if (!card) continue;

            var name = data[uid];
            if (!name.IsString) continue;

            var img = Database.GetData<Sprite>((string)name, mod);
            if (!img) continue;

            card.DefaultLiquidImage = img;
        }
    }
}