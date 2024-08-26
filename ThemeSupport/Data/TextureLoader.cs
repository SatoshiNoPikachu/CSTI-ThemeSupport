using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ThemeSupport.Data;

public static class TextureLoader
{
    private const string TexturePath = "Resource/ThemePicture";
    
    public static void LoadAllTexture()
    {
        var modDirs = new DirectoryInfo(BepInEx.Paths.PluginPath).GetDirectories();
        var sprites = new Dictionary<string, object>();
        
        foreach (var modDir in modDirs)
        {
            var path = Path.Combine(modDir.FullName, TexturePath);
            if (!Directory.Exists(path)) continue;

            var files = new DirectoryInfo(path).GetFiles();
            foreach (var file in files)
            {
                var extension = file.Extension.ToLower();
                if (extension is not (".png" or ".jpg" or ".jpeg")) continue;
                
                var name = Path.GetFileNameWithoutExtension(file.Name);
                if (sprites.ContainsKey(name))
                {
                    Plugin.Log.LogWarning($"{modDir.Name} not load texture same key {name}.");
                    continue;
                }

                var bytes = File.ReadAllBytes(file.FullName);
                var tex = new Texture2D(0, 0)
                {
                    name = name
                };
                tex.LoadImage(bytes);

                var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
                sprite.name = name;
                
                sprites.Add(name, sprite);
            }
        }
        
        Database.AddData(typeof(Sprite), sprites);
    }
}