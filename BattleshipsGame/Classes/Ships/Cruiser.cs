using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsGame.Classes.Ships
{
    class Cruiser : Ship
    {
        public override void Initialize()
        {
            _shipSize = 3;

            base.Initialize();
        }
    }
}
