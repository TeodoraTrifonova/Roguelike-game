using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ScoreSaver 
{
    public static void SaveHighScore (int highScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highScore.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, highScore);
        stream.Close();
    }

    public static int LoadPlayer()
    {
        string path = Application.persistentDataPath + "/highScore.fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int highScore = (int) formatter.Deserialize(stream);
            stream.Close();
            return highScore;

        }
        return 0;

    }
    
}
