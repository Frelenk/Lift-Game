using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerScore.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Stats saved");
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + "/playerScore.bin";
        Debug.Log(path);

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            PlayerProgress.lastLevelIndex = data.lastLevelIndex;
            PlayerProgress.tutorialPassed = data.tutorialPassed;
            PlayerProgress.tutorial2Passed = data.tutorial2Passed;
        }
        else
        {
            PlayerProgress.lastLevelIndex = 1;
            PlayerProgress.tutorialPassed = false;
            PlayerProgress.tutorial2Passed = false;

            Save();
            return;
        }

    }
}

[System.Serializable]
public class PlayerData
{
    public int lastLevelIndex;
    public bool tutorialPassed;
    public bool tutorial2Passed;

    public PlayerData()
    {
        lastLevelIndex = PlayerProgress.lastLevelIndex;
        tutorialPassed = PlayerProgress.tutorialPassed;
        tutorial2Passed = PlayerProgress.tutorial2Passed;
    }
}
