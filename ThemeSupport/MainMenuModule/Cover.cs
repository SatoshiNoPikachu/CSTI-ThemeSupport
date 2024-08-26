﻿using System;
using System.Linq;
using ThemeSupport.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ThemeSupport.MainMenuModule;

/// <summary>
/// 封面
/// </summary>
[Serializable]
public class Cover : ScriptableObject
{
    /// <summary>
    /// 获取随机封面
    /// </summary>
    /// <returns>封面</returns>
    public static Cover GetRandomCover()
    {
        var covers = Database.GetData<Cover>();
        return covers.Count == 0 ? null : covers.Values.ToArray()[Random.Range(0, covers.Count)];
    }

    /// <summary>
    /// 背景图像
    /// </summary>
    public Sprite BackImage => Database.GetData<Sprite>(BackImageName);
    
    /// <summary>
    /// 封面图像
    /// </summary>
    public Sprite CoverImage => Database.GetData<Sprite>(CoverImageName);
    
    /// <summary>
    /// 背景图像名称
    /// </summary>
    public string BackImageName;
    
    /// <summary>
    /// 封面图像名称
    /// </summary>
    public string CoverImageName;

    /// <summary>
    /// 标题
    /// </summary>
    public LocalizedString Title;

    /// <summary>
    /// 背景音乐
    /// </summary>
    public AudioClip BGM;
}