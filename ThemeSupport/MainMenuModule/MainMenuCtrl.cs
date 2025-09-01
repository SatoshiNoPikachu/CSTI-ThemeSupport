using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ThemeSupport.MainMenuModule;

public class MainMenuCtrl : MonoBehaviour
{
    public static void Create(MainMenu menu)
    {
        if (!menu) return;

        menu.gameObject.AddComponent<MainMenuCtrl>();
    }

    private AudioSource? _audio;
    
    private Image? _backImage;
    
    private Image? _coverImage;

    private LocalizedStaticText? _demoTitle;

    private void Awake()
    {
        _audio = this.GetComponent<AudioSource>("Sound");
        _backImage = this.GetComponent<Image>("BG");
        
        var mask = transform.Find("Main/Splash/Postcard/Mask");
        if (!mask) return;

        _coverImage = mask.GetComponent<Image>("Island_New");
        _demoTitle = mask.GetComponent<LocalizedStaticText>("Titles/Demo");
    }

    private void Start()
    {
        Setup(Cover.GetRandomCover());
    }

    private void Setup(Cover? cover)
    {
        if (!cover) return;

        var coverImage = cover!.CoverImage;
        var backImage = cover.BackImage ?? coverImage;
        
        if (backImage) _backImage?.sprite = backImage;
        if (coverImage) _coverImage?.sprite = coverImage;

        var audio = cover.BGM;
        if (audio && _audio is not null)
        {
            _audio.clip = audio;
            _audio.Play();
        }

        var tmp = _demoTitle?.GetComponent<TextMeshProUGUI>();
        if (!tmp) return;
        
        var title = cover.Title;
        var text = title.ToString();
        if (text == "") return;

        tmp!.text = text;
        _demoTitle!.LocalizedStringKey = title.LocalizationKey;
        _demoTitle.LocalizedText = title;
        _demoTitle.gameObject.SetActive(true);
    }
}