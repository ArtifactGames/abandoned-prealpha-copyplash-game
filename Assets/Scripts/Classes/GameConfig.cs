using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Configuration class which stores all the global game configuration.
/// </summary>
public static class GameConfig
{
    enum LOCALES : int { en_US = 0, es_ES = 1 };
    public static bool fullscreen = false;
    public static int locale = (int)LOCALES.en_US;
}
