using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsGame.Classes.Ships
{
    public class Submarine : Ship
    {
        public override void Initialize()
        {
            _shipSize = 1;

            base.Initialize();
        }
    }
}
