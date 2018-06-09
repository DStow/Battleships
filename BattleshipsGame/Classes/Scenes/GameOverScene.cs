using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameUtilityLib;

namespace BattleshipsGame.Classes.Scenes
{
    public class GameOverScene : Scene
    {
        public bool PlayerWon { get; set; }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);


            if (PlayerWon)
            {
                spriteBatch.DrawString(FontHandler.Instance.LargeFont, "YOU WON!!!!", new Vector2(25, 25), Color.Red);
            }
            else
            {
                spriteBatch.DrawString(FontHandler.Instance.LargeFont, "YOU LOST!!!!", new Vector2(25, 25), Color.Red);
            }

        }
    }
}
