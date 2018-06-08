using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGameUtilityLib;
using Microsoft.Xna.Framework.Content;

namespace BattleshipsGame.Classes.Scenes
{
    /// <summary>
    /// This scene is used by the player to place their pieces on the world
    /// </summary>
    public class PlacementScene : Scene
    {
        private GameBoard.PlacementBoard _board;



        public override void Initialize()
        {
            _board = new GameBoard.PlacementBoard();
            _board.Position = new Vector2(25, 60);
            _board.Size = new Vector2(520, 520);
            _board.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _board.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            _board.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            spriteBatch.DrawString(FontHandler.Instance.LargeFont, "Placement Scene", new Vector2(25, 25), Color.White);

            _board.Draw(spriteBatch, camera);
        }
    }
}
