using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ThemeSupport.GameModule;

public class BackCtrl : MBSingleton<BackCtrl>
{
    public static void Create()
    {
        var scene = SceneManager.GetSceneByPath("Assets/Scenes/GameScene.unity");
        if (!scene.IsValid()) return;

        var roots = scene.GetRootGameObjects();
        if (roots is null) return;

        var main = roots.FirstOrDefault(s => s.name is "MainCanvas");
        main?.transform.Find("ShakeParent/StaticBackground")?.gameObject.AddComponent<BackCtrl>();
    }

    private class BackImage(Image image, BackType type)
    {
        public BackType Type { get; } = type;

        public BackSet Set;

        private readonly Sprite _def = image.sprite;

        public void Setup(BackSet set)
        {
            Set = set;
            image.sprite = set.Image;
        }

        public void Reset()
        {
            Set = null;
            image.sprite = _def;
        }
    }

    private BackImage _baseBack;

    private BackImage _envBack;

    private BackImage _handBack;

    private BackImage _expBack;

    // private BackImage _filterLeftBack;
    //
    // private BackImage _filterRightBack;

    private BackImage _bookmarkLeftBack;

    private BackImage _bookmarkRightBack;

    private readonly Dictionary<GameBack, int> _cardMap = [];

    private void Awake()
    {
        var env = this.GetComponent<Image>("EnvBGImage");
        env.transform.Find("Frame")?.gameObject.SetActive(false);
        
        _baseBack = new BackImage(this.GetComponent<Image>("BaseBG"), BackType.Base);
        _envBack = new BackImage(env, BackType.Env);
        _handBack = new BackImage(this.GetComponent<Image>("HandBG"), BackType.Hand);
        _expBack = new BackImage(this.GetComponent<Image>("ExplorableBG"), BackType.Exp);
        // _filterLeftBack = new BackImage(this.GetComponent<Image>("LeftFiltersBG"), BackType.FilterLeft);
        // _filterRightBack = new BackImage(this.GetComponent<Image>("RightFiltersBG"), BackType.FilterRight);
        _bookmarkLeftBack = new BackImage(this.GetComponent<Image>("BookmarkMenuLeft"), BackType.BookmarkLeft);
        _bookmarkRightBack = new BackImage(this.GetComponent<Image>("BookmarkMenuRight"), BackType.BookmarkRight);
    }

    private void Start()
    {
        StartCoroutine(UpdateBackOnGameInitFinish());
    }

    private IEnumerator UpdateBackOnGameInitFinish()
    {
        while (GameManager.Instance.IsInitializing)
        {
            yield return null;
        }

        foreach (var image in GetBackImages())
        {
            UpdateBack(image);
        }
    }

    private IEnumerable<BackImage> GetBackImages()
    {
        yield return _baseBack;
        yield return _envBack;
        yield return _handBack;
        yield return _expBack;
        // yield return _filterLeftBack;
        // yield return _filterRightBack;
        yield return _bookmarkLeftBack;
        yield return _bookmarkRightBack;
    }

    public void OnRemoveCard(InGameCardBase cardBase)
    {
        var card = cardBase?.CardModel;
        if (card is null) return;

        var back = GameBack.GetBack(card);
        if (!back || !_cardMap.ContainsKey(back)) return;

        _cardMap[back]--;
        if (_cardMap[back] > 0) return;

        foreach (var image in GetBackImages())
        {
            if (image.Set != back[image.Type]) continue;
            UpdateBack(image);
        }
    }

    public void OnCardInit(InGameCardBase cardBase)
    {
        if (!cardBase) return;

        var card = cardBase.CardModel;
        if (!card || card.AlwaysUpdate) return;
        // if (cardBase.Environment != GameManager.Instance.CurrentEnvironment) return;

        var back = GameBack.GetBack(card);
        if (!back) return;

        if (_cardMap.ContainsKey(back)) _cardMap[back]++;
        else _cardMap[back] = 1;

        if (GameManager.Instance.IsInitializing) return;
        if (_cardMap[back] != 1) return;

        foreach (var image in GetBackImages())
        {
            var set = back[image.Type];
            if (set?.Image is null) continue;

            if (image.Set is null)
            {
                image.Setup(set);
                continue;
            }

            if (set.Priority > image.Set.Priority) image.Setup(set);
        }
    }

    public void OnStatChange(InGameStat inGameStat)
    {
        if (!inGameStat) return;

        var stat = inGameStat.StatModel;
        if (!stat) return;

        var backs = GameBack.GetBack(stat);
        if (backs is null) return;

        foreach (var back in backs)
        {
            var check = back.CheckStats();

            foreach (var image in GetBackImages())
            {
                var set = back[image.Type];
                if (set?.Image is null) continue;

                if (!check)
                {
                    if (image.Set != set) continue;
                    UpdateBack(image);
                    continue;
                }

                if (image.Set is null)
                {
                    image.Setup(set);
                    continue;
                }

                if (set.Priority > image.Set.Priority) image.Setup(set);
            }
        }
    }

    private void UpdateBack(BackImage image)
    {
        var set = CheckBacks(image.Type);
        if (set is null) image.Reset();
        else image.Setup(set);
    }

    private BackSet CheckBacks(BackType type)
    {
        BackSet targetSet = null;

        foreach (var (back, num) in _cardMap)
        {
            if (num < 1) continue;

            var set = back[type];
            if (set?.Image is null) continue;

            if (targetSet is null)
            {
                targetSet = set;
                continue;
            }

            if (targetSet.Priority < set.Priority) targetSet = set;
        }

        foreach (var set in from back in GameBack.StatBacks
                 let set = back[type]
                 where set?.Image is not null
                 where back.CheckStats()
                 select set)
        {
            if (targetSet is null)
            {
                targetSet = set;
                continue;
            }

            if (targetSet.Priority < set.Priority) targetSet = set;
        }

        return targetSet;
    }

    public static void HideLocationSlotImg()
    {
        GraphicsManager.Instance?.LocationSlotSettings?.Visuals?.transform.Find("Image")?.gameObject.SetActive(false);
    }
}