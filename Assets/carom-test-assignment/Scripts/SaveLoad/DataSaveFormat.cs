using System;

[Serializable]
public class DataSaveFormat
{
    public string playerName;
    public float gameTime;
    public int strikeCount;
}

[Serializable]
public class PlayerDataCollection
{
    public string collectionName;

    public DataSaveFormat[] playersData;
}
