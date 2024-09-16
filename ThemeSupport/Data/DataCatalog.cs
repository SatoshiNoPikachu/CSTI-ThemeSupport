using ThemeSupport.GameModule;
using ThemeSupport.MainMenuModule;

namespace ThemeSupport.Data;

/// <summary>
/// 数据目录
/// </summary>
public static class DataCatalog
{
    public static readonly DataInfo[] Catalog = [
        new DataInfo(typeof(Cover), "Theme-Cover"),
        new DataInfo(typeof(GameBack), "Theme-GameBack")
    ];
}