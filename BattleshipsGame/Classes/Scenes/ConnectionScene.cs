using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameUtilityLib;

namespace BattleshipsGame.Classes.Scenes
{
    /// <summary>
    /// This scene is used by the player to input the server details to
    /// connect to
    /// </summary>
    public class ConnectionScene : Scene
    {
        private enum InputControlEnum
        {
            IP,
            Port
        }

        private string _ipInput = "";
        private string _portInput = "";
        private InputControlEnum _selectedInput = InputControlEnum.IP;
        private Texture2D _titleBar;
        private MonoGameUtilityLib.Timers.FixedTimer _colorToggleTimer;
        private Color _textColor = Color.CornflowerBlue;

        public int Port { get; private set; }
        public string IP { get; private set; }
        public bool InputComplete { get; set; }

        public ConnectionScene()
        {
            _colorToggleTimer = new MonoGameUtilityLib.Timers.FixedTimer(200);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _titleBar = content.Load<Texture2D>("connection_title");

        }

        public override void Update(GameTime gameTime)
        {
            _colorToggleTimer.Update(gameTime);

            if (_colorToggleTimer.Tick)
            {
                if (_textColor == Color.CornflowerBlue)
                    _textColor = Color.LightBlue;
                else
                    _textColor = Color.CornflowerBlue;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            base.Draw(spriteBatch, camera);

            spriteBatch.Draw(_titleBar, new Rectangle(0, 20, 800, 104), Color.White);


            Color ipColor = Color.IndianRed;
            if (_selectedInput == InputControlEnum.IP)
                ipColor = _textColor;
            spriteBatch.DrawString(FontHandler.Instance.LargeFont, "Server IP: " + _ipInput, new Vector2(50, 150), ipColor);

            Color portColor = Color.IndianRed;
            if (_selectedInput == InputControlEnum.Port)
                portColor = _textColor;
            spriteBatch.DrawString(FontHandler.Instance.LargeFont, "Server Port: " + _portInput, new Vector2(50, 190), portColor);

        }

        private string _possibleInputs = "1234567890.";
        public override void TextInputEvent(object sender, TextInputEventArgs e)
        {
            // Handle special keys first...
            if (_possibleInputs.Contains(e.Character.ToString()))
            {
                if (_selectedInput == InputControlEnum.IP)
                    _ipInput += e.Character.ToString();
                else
                    _portInput += e.Character.ToString();
            }
            else if (e.Character.ToString() == "\b" && _ipInput.Length > 0)
            {
                _ipInput = _ipInput.Substring(0, _ipInput.Length - 1);
            }
            else if (e.Character.ToString() == "\r" && _ipInput.Length > 0 && _selectedInput == InputControlEnum.IP)
            {
                _selectedInput = InputControlEnum.Port;
            }
            else if (e.Character.ToString() == "\r" && _portInput.Length > 0 && _selectedInput == InputControlEnum.Port)
            {
                // Todo: check values are valid...
                IP = _ipInput;
                Port = Convert.ToInt32(_portInput);
                InputComplete = true;
            }
        }
    }
}
