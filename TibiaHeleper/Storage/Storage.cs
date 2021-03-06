﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using TibiaHeleper.Modules.WalkerModule;

namespace TibiaHeleper.Storage
{
    class Storage
    {
        const string procedureExtension = "thp";
        const string defaultExtension = "th";
        public const string THVersion = "1.0.0";

        public static Semaphore IOSem;

        static Storage()
        {
            IOSem = new Semaphore(1,1);
        }


        private static bool Save(object toSave, string extension = defaultExtension)
        {
            bool success;
            VersionedObject obj = new VersionedObject(toSave);

            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "*." + extension;
            sfd.DefaultExt = extension;
            sfd.Filter = "tibia helper files (*." + extension + ")|*." + extension;

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    string filename = Path.GetFullPath(sfd.FileName);

                    // Delete old file, if it exists
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }

                    // Persist to file
                    FileStream stream = File.Create(filename);
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                    stream.Close();
                    success = true;
                }
                catch (Exception)
                {
                    success = false;
                }
                return success;
            }
            return false;
        }
        private static object Load(string extension = defaultExtension)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "*." + extension;
            ofd.DefaultExt = extension;
            ofd.Filter = "tibia helper files (*." + extension + ")|*." + extension;
            VersionedObject obj = null;

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    string filename = Path.GetFullPath(ofd.FileName);

                    // Restore from file
                    var formatter = new BinaryFormatter();
                    FileStream stream = File.OpenRead(filename);
                    obj = formatter.Deserialize(stream) as VersionedObject;
                    stream.Close();
                    checkVersion(obj);
                    return obj.obj;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        public static bool SaveAllModules()
        {
            DataCollecter collecter = new DataCollecter();
            return Save(collecter);
        }
        public static void LoadAllModules()
        {
            DataCollecter collecter;
            collecter = (DataCollecter)Load();
            if(collecter!=null)
                collecter.activateLoadedSettings();
        }

        public static bool SaveProcedure(List<WalkerStatement> list)
        {
            return Save(list, procedureExtension);
        }
        public static List<WalkerStatement> LoadProcedure()
        {
            return (List<WalkerStatement>)Load(procedureExtension);
        }

        public static void checkVersion(VersionedObject obj)
        {
            if (obj.version == "0.0.0")
            {
                try
                {
                    DataCollecter data = (DataCollecter)obj.obj;
                }
                catch (Exception)
                {

                }
            }
        }

        public static object Copy(object obj)
        {

            object result;
            string filename = "TemporaryTibiaHelperFile";

            IOSem.WaitOne();

            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }


                using (FileStream stream = File.Create(filename))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
                    stream.Close();
                }
                using (FileStream stream = File.OpenRead(filename))
                {
                    var formatter = new BinaryFormatter();
                    result = formatter.Deserialize(stream);
                    stream.Close();

                    File.Delete(filename);
                }

            }
            catch (IOException)
            {
                result=null;
            }

            IOSem.Release();
            return result;

        }

    }
}
