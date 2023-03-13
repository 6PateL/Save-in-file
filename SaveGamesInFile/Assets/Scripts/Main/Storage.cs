using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class Storage
{
   public string _filePath;
   public BinaryFormatter _formatter;

   public Storage()
   {
      var directory = Application.persistentDataPath + "/saves";
      if (!Directory.Exists(directory))
         Directory.CreateDirectory(directory);
      _filePath = Application.persistentDataPath + "/GameSave.save";
      InitBinaryFormatter();
   }

   private void InitBinaryFormatter()
   {
      _formatter = new BinaryFormatter();
      var selector = new SurrogateSelector();

      var v3Surrogate = new Vector3SerializationSurrogate();
      var qSurrogate = new QuaternionSerializationSurrogate();

      selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), v3Surrogate);
      selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), qSurrogate);

      _formatter.SurrogateSelector = selector;
   }
   
   public void Save(object saveData) {
      var file = File.Create(_filePath);
      _formatter.Serialize(file,saveData);
      file.Close();
   } 
}
[Serializable] public static class SerializeLoad
{
   private static Storage _storage; 
   
   public static object Load(object saveDataByDefault) {
     if (!File.Exists(_storage._filePath)) {
        if (saveDataByDefault != null) {
           _storage.Save(saveDataByDefault);
           return saveDataByDefault;
        }
     }
     
     var file = File.Open(_storage._filePath, FileMode.Open);
     var savedData = _storage._formatter.Deserialize(file);
     file.Close();
     return savedData;
  }
}