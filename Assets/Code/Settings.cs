using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Text;

public static class Settings
{
    #region
    public static int defaultCredits = 10000;
    public enum volumeTypes
    {
        sfx,
        music,
        ui,
        voice,
    }
    public class Volumes
    {
        public float sfx = 0.5f;
        public float music = 0.5f;
        public float ui = 0.5f;
        public float voice = 0.5f;
    }
    public class ProfileData
    {
        public string playerName;
        public int credits;

        public uniqueLocation homeStation = new uniqueLocation();
    }
    public static bool settingsOK = false;

    public static Volumes Volume = new Volumes();
    public static ProfileData Profile = new ProfileData();
    public static string activeProfileXML;
    #endregion

    public static Dictionary<string, string> saveList = new Dictionary<string, string>();


    public static void LoadSaveList()
    {
        //Check to see if the Saves directory exists. Else, create it.
        if(!Directory.Exists("Saves")){
            Directory.CreateDirectory("Saves");
        }

        //Make a list of all files found in the Saves directory.
        else{
            string[] saveArray;
            saveArray = Directory.GetFiles("Saves");
            //Make sure the file is a valid save file

            foreach(string file in saveArray)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);

                string verify = xDoc.SelectSingleNode("Profile/Verify/validKey").InnerText;
                string name = xDoc.SelectSingleNode("Profile/Info/playerName").InnerText;
                if(verify == "validKey" && name != null)
                {
                    saveList.Add(file, name);
                    Debug.Log(file + "  " + name);
                }
            }
        }
    }

    public static void ReadSave(string save)
    {
        activeProfileXML = save;
        //Load the file
        ProfileData loadedProfile = new ProfileData();
        uniqueLocation loc = new uniqueLocation();
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(save);
        string name;
        string s_credits;
        string hs_sys, hs_x, hs_y, hs_z, hs_sce;

        //Load the file information into the previous declared members
        name = xDoc.SelectSingleNode("Profile/Info/playerName").InnerText;
        s_credits = xDoc.SelectSingleNode("Profile/Info/credits").InnerText;
        hs_sys = xDoc.SelectSingleNode("Profile/Info/homeStation/system").InnerText;
        hs_x = xDoc.SelectSingleNode("Profile/Info/homeStation/x").InnerText;
        hs_y = xDoc.SelectSingleNode("Profile/Info/homeStation/y").InnerText;
        hs_z = xDoc.SelectSingleNode("Profile/Info/homeStation/z").InnerText;
        hs_sce = xDoc.SelectSingleNode("Profile/Info/homeStation/scene").InnerText;

        //try to convert string information to necessary format
        try
        {
            loadedProfile.playerName = name;
            loc.systemName = hs_sys;
            loc.point.x = Convert.ToSingle(hs_x);
            loc.point.y = Convert.ToSingle(hs_y);
            loc.point.z = Convert.ToSingle(hs_z);
            loc.sceneName = hs_sce;
            loadedProfile.credits = Convert.ToInt32(s_credits);
            loadedProfile.homeStation = loc;
        }
        catch
        {

        }

        //establish the active profile
        finally
        {
            Profile = loadedProfile;
            GameManager.homeStation = Profile.homeStation;
            GameManager.credits = Profile.credits;
            GameManager.playerName = name;
        }
        ReadSettings();
    }

    public static void ReadSettings()
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(activeProfileXML);

        Volumes loadVolumes = new Volumes();
        string s, m, u, v;
        s = xDoc.SelectSingleNode("Profile/Settings/Volume/sfx").InnerText;
        m = xDoc.SelectSingleNode("Profile/Settings/Volume/music").InnerText;
        u = xDoc.SelectSingleNode("Profile/Settings/Volume/ui").InnerText;
        v = xDoc.SelectSingleNode("Profile/Settings/Volume/voice").InnerText;

        try
        {
            loadVolumes.sfx = Convert.ToSingle(s);
            loadVolumes.music = Convert.ToSingle(m);
            loadVolumes.ui = Convert.ToSingle(u);
            loadVolumes.voice = Convert.ToSingle(v);
        }
        catch
        {

        }
        finally
        {
            Volume = loadVolumes;
        }
    }

    public static void WriteHomeStation(string sys, Vector3 pos, string sce)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(activeProfileXML);

        xDoc.SelectSingleNode("Profile/Info/homeStation/system").InnerText = sys;
        xDoc.SelectSingleNode("Profile/Info/homeStation/x").InnerText = pos.x.ToString();
        xDoc.SelectSingleNode("Profile/Info/homeStation/y").InnerText = pos.y.ToString();
        xDoc.SelectSingleNode("Profile/Info/homeStation/z").InnerText = pos.z.ToString();
        xDoc.SelectSingleNode("Profile/Info/homeStation/scene").InnerText = sce;

        xDoc.Save(activeProfileXML);
    }

    public static void WriteCredits(int c)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(activeProfileXML);

        xDoc.SelectSingleNode("Profile/Info/credits").InnerText = c.ToString();

        xDoc.Save(activeProfileXML);
    }

    public static void WriteSettings (float s, float m, float u, float v)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(activeProfileXML);

        xDoc.SelectSingleNode("Profile/Settings/Volume/sfx").InnerText = s.ToString();
        xDoc.SelectSingleNode("Profile/Settings/Volume/music").InnerText = m.ToString();
        xDoc.SelectSingleNode("Profile/Settings/Volume/ui").InnerText = u.ToString();
        xDoc.SelectSingleNode("Profile/Settings/Volume/voice").InnerText = v.ToString();

        xDoc.Save(activeProfileXML);
        ReadSettings();
    }

    public static void WriteNewProfile (string name)
    {
        XmlTextWriter xWriter = new XmlTextWriter("Saves/" + name + ".xml", Encoding.UTF8);
        xWriter.Formatting = Formatting.Indented;
        xWriter.WriteStartElement("Profile");
        xWriter.WriteStartElement("Verify");
        xWriter.WriteStartElement("validKey"); xWriter.WriteString("validKey"); xWriter.WriteEndElement();
        xWriter.WriteEndElement();//Verify

        xWriter.WriteStartElement("Info");
        xWriter.WriteStartElement("playerName"); xWriter.WriteString(name); xWriter.WriteEndElement();
        xWriter.WriteStartElement("credits"); xWriter.WriteString("10000"); xWriter.WriteEndElement();

        xWriter.WriteStartElement("homeStation");
        xWriter.WriteStartElement("system"); xWriter.WriteString("A1"); xWriter.WriteEndElement();
        xWriter.WriteStartElement("x"); xWriter.WriteString("-28.5"); xWriter.WriteEndElement();
        xWriter.WriteStartElement("y"); xWriter.WriteString("0.0"); xWriter.WriteEndElement();
        xWriter.WriteStartElement("z"); xWriter.WriteString("-12.0"); xWriter.WriteEndElement();
        xWriter.WriteStartElement("scene"); xWriter.WriteString("player_station_1"); xWriter.WriteEndElement();
        xWriter.WriteEndElement();//Homestation
        xWriter.WriteEndElement();//Info

        xWriter.WriteStartElement("Settings");

        xWriter.WriteStartElement("Volume");
        xWriter.WriteStartElement("sfx"); xWriter.WriteString("0.5"); xWriter.WriteEndElement();
        xWriter.WriteStartElement("music"); xWriter.WriteString("0.5"); xWriter.WriteEndElement();
        xWriter.WriteStartElement("ui"); xWriter.WriteString("0.5"); xWriter.WriteEndElement();
        xWriter.WriteStartElement("voice"); xWriter.WriteString("0.5"); xWriter.WriteEndElement();
        xWriter.WriteEndElement();//Volume
        xWriter.WriteEndElement();//Settings
        xWriter.WriteEndElement();//Profile

        xWriter.Close();
    }
}