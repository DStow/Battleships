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
    /// <summary>
    /// Manages the scenes and displaying them to the user
    /// </summary>
    public class SceneManager : GameObject
    {
        private Scene _currentScene = null;

        private ConnectionScene _connectionScene;
        private PlacementScene _placementScene;
        private GameScene _gameScene;

        private enum SceneEnum
        {
            Connection,
            Placement,
            Game
        }
        private SceneEnum _currentSceneType = SceneEnum.Connection;

        public SceneManager()
        {
            _connectionScene = new ConnectionScene();
            _placementScene = new PlacementScene();
            _gameScene = new GameScene();

            _currentScene = _connectionScene;
        }

        public override void Initialize()
        {
            _connectionScene.Initialize();
            _placementScene.Initialize();
            _gameScene.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            _connectionScene.LoadContent(content);
            _placementScene.LoadContent(content);
            _gameScene.LoadContent(content);
        }


        public override void Update(GameTime gameTime)
        {
            // Set the correct current scene
            switch (_currentSceneType)
            {
                case SceneEnum.Connection:
                    _currentScene = _connectionScene;
                    break;
                case SceneEnum.Placement:
                    _currentScene = _placementScene;
                    break;
                case SceneEnum.Game:
                    _currentScene = _gameScene;
                    break;
            }

            _currentScene.Update(gameTime);

            if (_currentSceneType == SceneEnum.Connection)
            {
                ConnectionScene scene = (ConnectionScene)_currentScene;
                if (scene.InputComplete)
                {
                    BattleshipsGame.ServerIP = scene.IP;
                    BattleshipsGame.ServerPort = scene.Port;
                    _currentSceneType = SceneEnum.Placement;
                }
            }
            else if(_currentSceneType == SceneEnum.Placement)
            {
                if (_placementScene.PlacementDone)
                {
                    _currentSceneType = SceneEnum.Game;
                    // Reinit this scene as a load of information about hte current player would of been updated since
                    // it was first loaded
                    _gameScene.Initialize();
                    _gameScene.LoadContent(BattleshipsGame.GameContent);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            _currentScene.Draw(spriteBatch, camera);
        }

        public void TextInputEvent(object sender, TextInputEventArgs e)
        {
            _currentScene.TextInputEvent(sender, e);
        }
    }
}
