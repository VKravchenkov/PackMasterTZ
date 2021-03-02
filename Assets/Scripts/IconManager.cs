using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour
{
    private static IconManager iconManager;

    [SerializeField] private List<Sprite> sprites1x1;
    [SerializeField] private List<Sprite> sprites1x2;
    [SerializeField] private List<Sprite> sprites2x1;

    [SerializeField] private List<Sprite> spriteLand;
    [SerializeField] private List<Sprite> spritePeople;

    public List<Sprite> Sprites1x1 => sprites1x1;
    public List<Sprite> Sprites1x2 => sprites1x2;
    public List<Sprite> Sprites2x1 => sprites2x1;
    public List<Sprite> SpriteLand => spriteLand;
    public List<Sprite> SpritePeople => spritePeople;

    private void Awake()
    {
        iconManager = this;
    }

    public static Sprite GetRandomSprite1x1()
    {
        int rnd = Random.Range(0, iconManager.sprites1x1.Count);

        return iconManager.sprites1x1[rnd];
    }

    public static Sprite GetRandomSprite1x2()
    {
        int rnd = Random.Range(0, iconManager.sprites1x2.Count);

        return iconManager.sprites1x2[rnd];
    }

    public static Sprite GetRandomSprite2x1()
    {
        int rnd = Random.Range(0, iconManager.sprites2x1.Count);

        return iconManager.sprites2x1[rnd];
    }
}
