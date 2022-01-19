using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace Tangents
{
    public static class ScoreManager
    {
        private static string fileName = "hiscore.txt";

        public static int Score { get; private set; } = 0;
        public static int HiScore { get; private set; } = 0;

        public static void IncrementScore()
        {
            Score++;
        }

        public static void ResetScore()
        {
            Score = 0;
        }

        public static void UpdateScore(int amount)
        {
            Score += amount;
        }

        public static void CheckAndUpdateHighScore()
        {
            if (Score > HiScore) {
                HiScore = Score;
                SaveHighScore();
            }
        }

        public static void LoadHighScore()
        {
            if (File.Exists(fileName))
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication()) {
                    using (IsolatedStorageFileStream file = new IsolatedStorageFileStream(fileName, FileMode.Open, storage)) {
                        StreamReader reader = new StreamReader(file);
                        HiScore = Convert.ToInt32(reader.ReadLine());

                        reader.Close();
                        file.Close();
                    }
                }
            }
            else
            {
                File.Create(fileName);
                SaveHighScore();
            }
        }

        private static void SaveHighScore()
        {
            using(IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication()) {
                using (IsolatedStorageFileStream file = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, storage)) {
                    StreamWriter writer = new StreamWriter(file);

                    writer.WriteLine(HiScore);

                    writer.Flush();
                    writer.Close();
                    file.Close();
                }
            }
        }
    }
}
