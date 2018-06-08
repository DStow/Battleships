using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameUtilityLib;

using BattleshipsGame.Classes;
using BattleshipsGame.Classes.Scenes;
using Microsoft.Xna.Framework.Content;

namespace BattleshipsGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class BattleshipsGame : Game
    {
        public static GraphicsDeviceManager Graphics;
        public static ContentManager GameContent;

        private SpriteBatch _spriteBatch;
        private Camera _camera;


        public static GameWindow GameWindow;
        public static string ServerIP = "";
        public static int ServerPort = 0;
        public static int PlayerNumber = 0;

        private SceneManager _sceneManager;

        public BattleshipsGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 640;

            _camera = new Camera(800, 640, 800);

            GameWindow = Window;
            GameWindow.TextInput += GameWindow_TextInput;

            IsMouseVisible = true;

            // Uhhh some stupid singleton thing I picked up from a Unity tutorial... sorry
            new FontHandler();

            _sceneManager = new SceneManager();
        }

        private void GameWindow_TextInput(object sender, TextInputEventArgs e)
        {
            _sceneManager.TextInputEvent(sender, e);
        }

        protected override void Initialize()
        {
            _sceneManager.Initialize();

            // Do this last
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameContent = Content;

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            FontHandler.Instance.LoadFonts(Content);

            _sceneManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _sceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _sceneManager.Draw(_spriteBatch, _camera);

            _spriteBatch.End();

            base.Draw(gameTime);
        }


    }
}
