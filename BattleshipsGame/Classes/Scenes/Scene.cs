using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using MonoGameUtilityLib;

namespace BattleshipsGame.Classes.Scenes
{
    public abstract class Scene : GameObject
    {
        private Texture2D _backgroundTexture;

        public override void LoadContent(ContentManager content)
        {
            _backgroundTexture = content.Load<Texture2D>("scene_background");
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            // Get area of the whole screen
            RectangleF drawArea = camera.ComputeWorldAreaToPixelRectangle(new Vector2(0, 0), new Vector2(camera.VisibleWorldWidth, camera.VisibleWorldHeight));
            spriteBatch.Draw(_backgroundTexture, drawArea, Color.White);

            // Draw test text
            spriteBatch.DrawString(FontHandler.Instance.LargeFont, "Hello, World!", new Vector2(25, 25), Color.Black);
        }
    }
}
