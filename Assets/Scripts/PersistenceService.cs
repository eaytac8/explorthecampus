using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ExplorTheCampus
{
    /// <summary>
    /// This Script is used to persist ANY data which is marked as [System.Serializable].
    /// </summary>
    public static class PersistenceService
    {

        private const string FILE_EXTENSION = ".dat";

        /// <summary>
        /// Save the data in the persistent data path of the application.
        /// </summary>
        /// <param name="data">The data to be saved. Should be marked as serializable</param>
        /// <param name="dataName">The name of the file.</param>
        public static void Save(object data, string dataName)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Create(Application.persistentDataPath + "/" + dataName + FILE_EXTENSION);
            try
            {
                bf.Serialize(fs, data);
            }
            catch (SerializationException e)
            {
                Debug.LogError("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// Load the saved data from the persistent data path of the application.
        /// Returns null if the data with the given name is not available.
        /// </summary>
        /// <param name="dataName">The name of the data to be loaded.</param>
        /// <returns>The saved data.</returns>
        public static object Load(string dataName)
        {
            object data = null;
            if (File.Exists(Application.persistentDataPath + "/" + dataName + FILE_EXTENSION))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.Open(Application.persistentDataPath + "/" + dataName + FILE_EXTENSION, FileMode.Open);
                try
                {
                    data = bf.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Debug.LogError("Failed to deserialize. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
            return data;
        }
    }
}