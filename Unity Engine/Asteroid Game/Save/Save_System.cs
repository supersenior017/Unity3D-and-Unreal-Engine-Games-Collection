using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
   

public static class Save_System 
{

    public static void debug_start()
    {

        BinaryFormatter formatter = new BinaryFormatter();

       
        string path = Application.persistentDataPath + "/player.statistic";


        //wird nur ausgeführt, wenn path nicht existiert
        if (path.Length == 0)
        {
            // create file on system
            FileStream stream = new FileStream(path, FileMode.Create);


            Game_Save data = new Game_Save();


            data.debug();

            formatter.Serialize(stream, data);
            stream.Close();

        }
    }



    public static void saveAsteroid_stat(Statistics_this_level stat)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        

        string path = Application.persistentDataPath + "/player.statistic";

        // create file on system
        FileStream stream = new FileStream(path, FileMode.Create);


        Game_Save data = new Game_Save();


        data.save_asteroid_destroy(stat);


        formatter.Serialize(stream, data);
        stream.Close();     
    }





    public static Game_Save loadStats()
    {

    string path = Application.persistentDataPath + "/player.statistic";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            Game_Save data = formatter.Deserialize(stream) as Game_Save;


            stream.Close();

            return data;
                
        }

        else
        {

            Debug.Log("Save_file not found" + path);
            return null;
        }
        
    }

 
}
