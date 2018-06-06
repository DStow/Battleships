using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsGame.Classes
{
    public class FontHandler
    {
        public static FontHandler Instance { get; set; }

        public SpriteFont SmallFont{ get; set; }
        public SpriteFont MediumFont { get; set; }
        public SpriteFont LargeFont { get; set; }

        public FontHandler()
        {
            Instance = this;
        }

        public void LoadFonts(ContentManager content)
        {
            SmallFont = content.Load<SpriteFont>("Fonts\\font_small");
            MediumFont = content.Load<SpriteFont>("Fonts\\font_medium");
            LargeFont = content.Load<SpriteFont>("Fonts\\font_large");
        }
    }
}
