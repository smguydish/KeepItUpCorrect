using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace KeepItUpCorrect
{
    class SoundManager
    {
        private static SoundEffect jump;


        public static void Initialize(ContentManager content)
        {
            try
            {
                jump = content.Load<SoundEffect>(@"Sounds\jump");
            }

            catch
            {
                Debug.Write("SoundManager Initialization Failed");
            }
        }
        public static void playJump()
        {
            try
            {
                jump.Play();
            }

            catch
            {
                Debug.Write("jump failed");
            }
        }

        public static void playGameOver()
        {

        }



    }
}
