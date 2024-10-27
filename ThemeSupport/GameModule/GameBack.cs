using System;
using System.Collections.Generic;
using System.Linq;
using ThemeSupport.Data;
using UnityEngine;

namespace ThemeSupport.GameModule;

/// <summary>
/// 游戏背景
/// </summary>
[Serializable]
public class GameBack : ScriptableObject
{
    /// <summary>
    /// 卡牌映射
    /// </summary>
    public static readonly Dictionary<CardData, GameBack> CardMap = [];

    /// <summary>
    /// 状态映射
    /// </summary>
    public static readonly Dictionary<GameStat, List<GameBack>> StatMap = [];

    /// <summary>
    /// 状态背景列表
    /// </summary>
    public static readonly List<GameBack> StatBacks = [];

    /// <summary>
    /// 绑定卡牌
    /// </summary>
    public CardData Card;

    /// <summary>
    /// 基地背景
    /// </summary>
    public BackSet BaseSet;

    /// <summary>
    /// 场景背景
    /// </summary>
    public BackSet EnvSet;

    /// <summary>
    /// 手牌背景
    /// </summary>
    public BackSet HandSet;

    /// <summary>
    /// 探索背景
    /// </summary>
    public BackSet ExpSet;

    /// <summary>
    /// 过滤器背景左
    /// </summary>
    public BackSet FilterLeftSet;

    /// <summary>
    /// 过滤器背景右
    /// </summary>
    public BackSet FilterRightSet;

    /// <summary>
    /// 书签背景左
    /// </summary>
    public BackSet BookmarkLeftSet;

    /// <summary>
    /// 书签背景右
    /// </summary>
    public BackSet BookmarkRightSet;

    /// <summary>
    /// 条件组
    /// </summary>
    public ConditionSet[] ConditionSets;

    /// <summary>
    /// 条件设置
    /// </summary>
    [Serializable]
    public class ConditionSet
    {
        /// <summary>
        /// 状态条件组
        /// </summary>
        public StatCondition[] StatConditions;

        /// <summary>
        /// 检查条件是否均满足
        /// </summary>
        /// <returns>当条件组不为null或者长度不为0时，且所有条件均检查为true时返回true</returns>
        public bool Check()
        {
            return StatConditions?.Length is not (null or 0) &&
                   StatConditions.All(condition => condition?.Check() is true);
        }

        /// <summary>
        /// 获取所有绑定状态
        /// </summary>
        /// <returns>所有条件包含的状态集合</returns>
        public HashSet<GameStat> GetStats()
        {
            var set = new HashSet<GameStat>();
            if (StatConditions is null) return set;

            foreach (var condition in StatConditions)
            {
                if (!condition?.Stat) continue;
                set.Add(condition.Stat);
            }

            return set;
        }
    }

    /// <summary>
    /// 状态条件
    /// </summary>
    [Serializable]
    public class StatCondition
    {
        /// <summary>
        /// 状态
        /// </summary>
        public GameStat Stat;

        /// <summary>
        /// 最小值
        /// </summary>
        public float MinValue;

        /// <summary>
        /// 最大值
        /// </summary>
        public float MaxValue;

        /// <summary>
        /// 检查条件是否满足
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            if (!Stat) return false;
            if (!GameManager.Instance.StatsDict.TryGetValue(Stat, out var stat)) return false;

            var value = stat.CurrentValue(GameManager.Instance.NotInBase);
            return value >= MinValue && value <= MaxValue;
        }
    }

    static GameBack()
    {
        Loader.LoadCompleteEvent += OnLoadComplete;
    }

    private static void OnLoadComplete()
    {
        CardMap.Clear();

        var data = Database.GetData<GameBack>().Values;
        foreach (var obj in data)
        {
            var card = obj.Card;
            if (card is not null)
            {
                if (card.AlwaysUpdate) continue;
                
                if (CardMap.ContainsKey(card))
                {
                    Plugin.Log.LogWarning($"Card {card.name} has multiple GameBack binding.");
                    continue;
                }

                CardMap[obj.Card] = obj;
                continue;
            }

            var stats = obj.GetStats();
            if (stats.Count < 1) continue;
            StatBacks.Add(obj);

            foreach (var stat in stats)
            {
                if (!StatMap.ContainsKey(stat)) StatMap[stat] = [];
                StatMap[stat].Add(obj);
            }
        }
    }

    /// <summary>
    /// 获取背景
    /// </summary>
    /// <param name="card">卡牌</param>
    /// <returns>卡牌绑定的背景，若不存在则返回null</returns>
    public static GameBack GetBack(CardData card)
    {
        return CardMap.TryGetValue(card, out var back) ? back : null;
    }

    /// <summary>
    /// 获取背景列表
    /// </summary>
    /// <param name="stat">状态</param>
    /// <returns>状态绑定的背景列表，若不存在则返回null</returns>
    public static List<GameBack> GetBack(GameStat stat)
    {
        return StatMap.TryGetValue(stat, out var back) ? back : null;
    }

    /// <summary>
    /// 获取所有绑定状态
    /// </summary>
    /// <returns>所有条件包含的状态集合</returns>
    private HashSet<GameStat> GetStats()
    {
        var set = new HashSet<GameStat>();
        if (ConditionSets is null) return set;

        foreach (var condition in ConditionSets)
        {
            if (condition is null) continue;
            set.UnionWith(condition.GetStats());
        }

        return set;
    }

    /// <summary>
    /// 背景索引器
    /// </summary>
    /// <param name="type">背景类型</param>
    public BackSet this[BackType type] => type switch
    {
        BackType.Base => BaseSet,
        BackType.Env => EnvSet,
        BackType.Hand => HandSet,
        BackType.Exp => ExpSet,
        BackType.FilterLeft => FilterLeftSet,
        BackType.FilterRight => FilterRightSet,
        BackType.BookmarkLeft => BookmarkLeftSet,
        BackType.BookmarkRight => BookmarkRightSet,
        _ => null
    };

    /// <summary>
    /// 检查状态
    /// </summary>
    /// <returns>当条件组不为null且长度不为0时，且任一条件满足则返回true</returns>
    public bool CheckStats()
    {
        return ConditionSets?.Length is not (null or 0) && ConditionSets.Any(condition => condition?.Check() is true);
    }
}