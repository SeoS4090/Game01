public static class GameUtils
{

    #region Log
    public static void Log(string log)
    {
        if (Define.b_log)
            UnityEngine.Debug.Log(log);
    }

    public static void LogError(string log)
    {
        if (Define.b_log)
            UnityEngine.Debug.LogError(log);
    }

    public static void LogWarning(string log)
    {
        if (Define.b_log)
            UnityEngine.Debug.LogWarning(log);
    }

    #endregion

}