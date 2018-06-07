using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using MonoGameUtilityLib;

namespace BattleshipsGame.Classes
{
    public abstract class GameObject
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public RectangleF AreaF
        {
            get
            {
                return new RectangleF(Position.X, Position.Y, Size.X, Size.Y);
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, Camera camera)
        {

        }
    }
}
