using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ScoreData
{
    public int score;
}

public class ScoreSaver : MonoBehaviour, ISaveable
{
    [SerializeField] private ScoreManager scoreManager;
    private string savePath;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "score.dat");
    }

    public void Save()
    {
        var scoreField = typeof(ScoreManager).GetField("score",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        int currentScore = (int)scoreField.GetValue(scoreManager);

        ScoreData data = new ScoreData { score = currentScore };

        using (FileStream fs = new FileStream(savePath, FileMode.Create))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
        }

        Debug.Log($"[ScoreSaver] Score {data.score} saved to {savePath}");
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("[ScoreSaver] No score file found.");
            return;
        }

        ScoreData data;
        using (FileStream fs = new FileStream(savePath, FileMode.Open))
        {
            BinaryFormatter bf = new BinaryFormatter();
            data = (ScoreData)bf.Deserialize(fs);
        }

        var scoreField = typeof(ScoreManager).GetField("score",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        scoreField.SetValue(scoreManager, data.score);
        scoreManager.SendMessage("UpdateUI", SendMessageOptions.DontRequireReceiver);

        Debug.Log($"[ScoreSaver] Loaded score {data.score} from {savePath}");
    }
}
