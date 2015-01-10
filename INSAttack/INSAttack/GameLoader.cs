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
                if (m_saveName == null) return "gameSave.xml";
                return m_saveName; 
            }
            set { m_saveName = value; }
        }

        public Game make(String name)
        {
            m_saveName = name;
            return make();
        }


        public override Game make()
        {

            Game game = null;

            if (File.Exists(SaveName))
            {
                try
                {
                    //Opens save file and deserializes the object from it.
                    Stream stream = File.Open(SaveName, FileMode.Open);
                    //SoapFormatter formatter = new SoapFormatter();
                    BinaryFormatter formatter = new BinaryFormatter();

                    game = (Game) formatter.Deserialize(stream);
                    Unit.Count = (int) formatter.Deserialize(stream);
                    Player.Count = (int) formatter.Deserialize(stream);
                    stream.Close();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
            }
            return game;
        }
    }
}
