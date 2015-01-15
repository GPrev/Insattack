using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INSAttack
{
    public class GameLoader : GameBuilder
    {
        private String m_saveName;

        public String SaveName
        {
            get
            {
                if (m_saveName == null) return DefaultSaveName;
                return m_saveName; 
            }
            set { m_saveName = value; }
        }

        public Game make(String name)
        {
            m_saveName = name;
            return make();
        }

        public static string DefaultSaveName
        {
            get { return "gameSave.xml"; }
        }
        //Create a game based on a previous save
        public override Game make()
        {

            Game game = null;

            if (File.Exists(SaveName))
            {
                try
                {
                    //Opens save file and deserializes the object from it.
                    Stream stream = File.Open(SaveName, FileMode.Open);
                    BinaryFormatter formatter = new BinaryFormatter();

                    game = (Game) formatter.Deserialize(stream);
                    Unit.Count = (int) formatter.Deserialize(stream);
                    Player.Count = (int) formatter.Deserialize(stream);
                    stream.Close();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                    Console.Error.WriteLine("Problem on Save : " + SaveName);
                }
            }
            return game;
        }
    }
}
