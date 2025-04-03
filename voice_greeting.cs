using System.IO;
using System.Media;
using System;

namespace cybersecurityawarenessbot
{
    public class voice_greeting
    {
        public voice_greeting()
        {
            //getting full location of the project
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            //replace the bin\debug\ folder in the full_location
            string new_path = full_location.Replace("bin\\Debug\\", "");

            //try and catch
            try
            {
                //combine the path
                string full_path = Path.Combine(new_path, "greeting.wav");

                //now we create instance for the SoundPlay class
                using (SoundPlayer play = new SoundPlayer(full_path))
                {
                    //play the file
                    play.PlaySync();
                }//end of using
            }
            catch (Exception error)
            {
                Console.Write(error.Message);
            }
        }
    }
}