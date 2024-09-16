using System;
using ThemeSupport.Data;
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
    public Sprite Image => Database.GetData<Sprite>(ImageName);
    
    /// <summary>
    /// 图像名称
    /// </summary>
    public string ImageName;
    
    /// <summary>
    /// 优先度
    /// </summary>
    public int Priority;
}