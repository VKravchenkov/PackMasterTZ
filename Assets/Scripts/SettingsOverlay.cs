using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsOverlay : BaseView
{
    public static SettingsOverlay Instance { get; private set; }

    protected override void Init()
    {
        base.Init();

        Instance = this;
    }
}
