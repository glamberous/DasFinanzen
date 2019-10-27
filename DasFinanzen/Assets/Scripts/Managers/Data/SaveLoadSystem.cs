using System.IO;
using UnityEngine;
using MessagePack;
using MessagePack.Formatters;

public interface ISaveLoad {
    FileData LoadFileData();
    void SaveFileData(FileData fileData); 
}

public class SaveLoadSystem : ISaveLoad {
    public void SetFilePath(string filename = "AppData.fin") => filepath = Application.persistentDataPath + $"/{filename}";
    private string filepath;

    public SaveLoadSystem() {
        SetFilePath();

        var resolver = MessagePack.Resolvers.CompositeResolver.Create(
        Array.Empty<IMessagePackFormatter>(),
        new IFormatterResolver[]
        {
            MessagePack.Resolvers.GeneratedResolver.Instance,
            MessagePack.Resolvers.StandardResolver.Instance,
        });
            var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);
    }

    public FileData LoadFileData() {
        FileData myData = new FileData();
        if (File.Exists(filepath)) {
            byte[] serializedData = File.ReadAllBytes(filepath);
            try {
                myData = MessagePackSerializer.Deserialize<FileData>(serializedData);
            } catch {
                Debug.Log("Unable to Load Profile! \nLoading Default Values.");
            }
        } else {
            Debug.Log("File not found. \nLoading Default Values.");
        }
        return myData;
    }

    public void SaveFileData(FileData fileData) {
        byte[] rawData = MessagePackSerializer.Serialize(fileData);
        File.WriteAllBytes(filepath, rawData);
    }
}
