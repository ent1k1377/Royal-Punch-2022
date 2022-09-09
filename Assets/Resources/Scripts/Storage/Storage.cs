using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Resources.Scripts.Storage
{
    public class Storage
    {
        private readonly string _filePath;
        private readonly BinaryFormatter _formatter;
        
        public Storage()
        {
            _formatter = new BinaryFormatter();
            var directory = Application.persistentDataPath + "/saves";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            _filePath = Application.persistentDataPath + "/saves/";
        }

        public T Load<T>() where T : Data, new()
        {
            var data = new T();
            if (!File.Exists(_filePath + data.NameFile))
            {
                Save(data);
                return data;
            }

            var file = File.Open(_filePath + data.NameFile, FileMode.Open);
            var savedData = (T)_formatter.Deserialize(file);
            file.Close();
            return savedData;
        }

        public void Save(Data saveData)
        {
            var file = File.Create(_filePath + saveData.NameFile);
            _formatter.Serialize(file, saveData);
            file.Close();
        }
    }
}
