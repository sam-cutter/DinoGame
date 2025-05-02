using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DinoGame
{
    class Leaderboard
    {
        private readonly string filename;
        private readonly Dictionary<string, int> leaderboard;

        public Leaderboard(string filename)
        {
            this.filename = filename;
            leaderboard = new Dictionary<string, int>();

            using (BinaryReader reader = new BinaryReader(new FileStream(filename, FileMode.OpenOrCreate)))
            {
                while (true)
                {
                    try
                    {
                        string name = reader.ReadString();
                        int score = reader.ReadInt32();

                        leaderboard[name] = score;
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }

        public void Entry(string name, int score)
        {
            if (leaderboard.ContainsKey(name) && leaderboard[name] >= score) return;

            leaderboard[name] = score;

            using (BinaryWriter writer = new BinaryWriter(new FileStream(filename, FileMode.OpenOrCreate)))
            {
                foreach (KeyValuePair<string, int> entry in leaderboard)
                {
                    writer.Write(entry.Key);
                    writer.Write(entry.Value);
                }
            }
        }

        public void Display()
        {
            Console.WriteLine();

            if (leaderboard.Count == 0)
            {
                Console.WriteLine("The leaderboard is currently empty.");
                return;
            }

            Console.WriteLine("Here is the current leaderboard:");
            Console.WriteLine("NAME".PadLeft(10) + " | " + "SCORE");

            foreach (KeyValuePair<string, int> entry in leaderboard.OrderByDescending(kvp => kvp.Value))
            {
                Console.WriteLine(entry.Key.PadLeft(10) + " | " + entry.Value);
            }
        }
    }
}
