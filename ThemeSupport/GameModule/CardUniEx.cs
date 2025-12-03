using System;
using ModCore.Data;
using UnityEngine;

namespace ThemeSupport.GameModule;

[Serializable]
[DataInfo("Theme-CardUniEx")]
public class CardUniEx : ScriptableObject
{
    private static readonly Dictionary<CardData, CardUniEx> CardMap = [];

    public CardData? Card;

    public bool IsHideInventoryInfo;

    static CardUniEx()
    {
        Loader.LoadCompleteEvent += OnLoadComplete;
    }

    private static void OnLoadComplete()
    {
        CardMap.Clear();

        var data = Database.GetData<CardUniEx>()?.Values;
        if (data is null) return;

        foreach (var obj in data)
        {
            var card = obj.Card!;
            if (!card) continue;

            if (CardMap.TryAdd(card, obj)) continue;
            Plugin.Log.LogWarning($"Card {card.name} has multiple CardUniEx binding.");
        }
    }

    public static void OnUpdateInventoryInfo(CardGraphics? cg)
    {
        var card = cg?.CardLogic?.CardModel;
        if (card is null) return;
        if (!CardMap.TryGetValue(card, out var ex)) return;
        if (!ex.IsHideInventoryInfo) return;
        
        cg!.InventoryBarIndicatorParent?.SetActive(false);
        cg.InventoryIndicatorsParent?.gameObject.SetActive(false);
    }
}