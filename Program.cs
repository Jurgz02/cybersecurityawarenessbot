using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cybersecurityawarenessbot
{
    public class Program
    {
        static void Main(string[] args)
        {
            //create instance for voice_greeting
            new voice_greeting() { };
            //create instance for welcome_message
            new welcome_message() { };
            //create instance for user_name
            new user_name() { };
            //create instance for logo_design
            new logo_design() { };
            //create instance for user_interaction
            new user_interaction() { };
        }
    }
}
