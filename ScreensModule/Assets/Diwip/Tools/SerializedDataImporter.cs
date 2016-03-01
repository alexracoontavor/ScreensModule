using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SerializedDataImporter
{
    static string dirName = "Data";

    public static T LoadData<T>(string filename)
    {
        T loaded;

        if (Directory.Exists(dirName) && File.Exists(dirName + "/" + filename))
        {
            loaded = LoadFromHD<T>(filename);
        }
        else
        {
            loaded = Load<T>(filename);
        }

        return loaded;
    }

    private static T LoadFromHD<T>(string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(dirName + "/" + filename, FileMode.Open);
        T loadedData = (T)bf.Deserialize(file);
        file.Close();
        return loadedData;
    }

    private static T Load<T>(string fileName)
    {
        if (!File.Exists(Application.persistentDataPath + "/" + fileName)) UnpackMobileFile(fileName);
        //check again in case it didn't work
        if (File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            //Load data from file to loader
            BinaryFormatter bf = new BinaryFormatter();//engine that will interface with file
            FileStream file = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
            T loadedData = (T)bf.Deserialize(file);
            file.Close();
            return loadedData;
        }

        return default(T);
    }

    static void UnpackMobileFile(string fileName)
    {  //copies and unpacks file from apk to persistentDataPath where it can be accessed
        string destinationPath = System.IO.Path.Combine(Application.persistentDataPath, fileName);
        string sourcePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

        //if DB does not exist in persistent data folder (folder "Documents" on iOS) or source DB is newer then copy it
        if (!System.IO.File.Exists(destinationPath) || (System.IO.File.GetLastWriteTimeUtc(sourcePath) > System.IO.File.GetLastWriteTimeUtc(destinationPath)))
        {
            if (sourcePath.Contains("://"))
            {// Android  
                WWW www = new WWW(sourcePath);
                while (!www.isDone) {; }                // Wait for download to complete - not pretty at all but easy hack for now 
                if (String.IsNullOrEmpty(www.error))
                {
                    System.IO.File.WriteAllBytes(destinationPath, www.bytes);
                }
                else
                {
                    Debug.Log("ERROR: the file DB named " + fileName + " doesn't exist in the StreamingAssets Folder, please copy it there.");
                }
            }
            else
            {                // Mac, Windows, Iphone                
                             //validate the existence of the DB in the original folder (folder "StreamingAssets")
                if (System.IO.File.Exists(sourcePath))
                {
                    //copy file - alle systems except Android
                    System.IO.File.Copy(sourcePath, destinationPath, true);
                }
                else
                {
                    Debug.Log("ERROR: the file DB named " + fileName + " doesn't exist in the StreamingAssets Folder, please copy it there.");
                }
            }
        }
    }
}
