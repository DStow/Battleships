using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameUtilityLib;

using BattleshipsGame.Classes;
using BattleshipsGame.Classes.Scenes;

namespace BattleshipsGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BattleshipsGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera _camera;

        private Scene _currentScene;

        public BattleshipsGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 640;

            _camera = new Camera(800, 640, 800);
        }

        protected override void Initialize()
        {
            _currentScene = new ConnectionScene();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentScene.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

    

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _currentScene.Draw(_spriteBatch, _camera);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
