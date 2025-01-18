using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using _ProjectFiles.SaveSystem.Surrogates;
using UnityEngine;

namespace _ProjectFiles.SaveSystem
{
    public class Storage : IStorage
    {
        private const string FileName = "GameData.save";
        private string _filePath;
        private string _fileDirectory;
        private BinaryFormatter _binaryFormatter;
        
        public Storage()
        {
            InitDirectory();
            InitBinaryFormatter();
        }
        
        public object Load(object defaultData = null)
        {
            if (!File.Exists(_filePath))
            {
                if(defaultData != null)
                    Save(defaultData);
                return defaultData;
            }
                
            FileStream fileStream = File.Open(_filePath, FileMode.Open);
            object savedData = _binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return savedData;
        }

        public void Save(object saveData)
        {
            FileStream fileStream = File.Create(_filePath);
            _binaryFormatter.Serialize(fileStream, saveData);
            fileStream.Close();
        }
        
        private void InitDirectory()
        {
            _fileDirectory = Application.persistentDataPath + "/Saves";
            if(!Directory.Exists(_fileDirectory))
                Directory.CreateDirectory(_fileDirectory);
            
            _filePath = _fileDirectory + "/" + FileName;
        }
        
        private void InitBinaryFormatter()
        {
            _binaryFormatter = new BinaryFormatter();
            SurrogateSelector selector = new SurrogateSelector();
            Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
            
            selector.AddSurrogate(typeof(Vector3), 
                new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            _binaryFormatter.SurrogateSelector = selector;
        }
    }
}
