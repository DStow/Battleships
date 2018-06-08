using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGameUtilityLib;
using Microsoft.Xna.Framework.Content;

namespace BattleshipsGame.Classes.Ships
{
    public class Battleship : Ship
    {
        public override void Initialize()
        {
            _shipSize = 4;

            base.Initialize();
        }
    }
}
