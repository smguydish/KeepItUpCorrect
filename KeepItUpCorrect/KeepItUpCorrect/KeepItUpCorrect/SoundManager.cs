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

namespace KeepItUpCorrect
{
    class SoundManager
    {
        private static SoundEffect jump;


        public static void Initialize(ContentManager content)
        {
            jump = content.Load<SoundEffect>(@"Sounds\jump");
        }
        public static void playJump()
        {
            jump.Play();
        }




    }
}
