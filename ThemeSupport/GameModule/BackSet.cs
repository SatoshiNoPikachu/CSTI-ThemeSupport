using System;
using UnityEngine;

namespace ThemeSupport.GameModule;

/// <summary>
/// 背景设置
/// </summary>
[Serializable]
public class BackSet
{
    /// <summary>
    /// 图像
    /// </summary>
    public Sprite Image;

    /// <summary>
    /// 优先度
    /// </summary>
    public int Priority;
}