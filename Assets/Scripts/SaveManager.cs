using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager 
{

    public static void Save(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream("C:/Users/Public/Save.dat", FileMode.OpenOrCreate);
        formatter.Serialize(stream, data);
        stream.Close();
    }   
    
    public static SaveData Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        try
        {
            stream = new FileStream("C:/Users/Public/Save.dat", FileMode.Open);
            SaveData data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        catch
        {
            return new SaveData();
        }
    }
}
