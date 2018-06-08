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
    public abstract class Ship : GameObject
    {
        public Vector2 TileIndex { get; set; }

        protected int _shipSize;

        public List<GameBoard.BoardTiles.ShipTile> ShipTiles { get; set; }

        public PlacementDirectionEnum Direction { get; set; }

        public override void Initialize()
        {
            base.Initialize();

            ShipTiles = new List<GameBoard.BoardTiles.ShipTile>();

            for(int i = 0; i < _shipSize; i++)
            {
                // Create tiles
                int x = (int)TileIndex.X, y = (int)TileIndex.Y;

                if (Direction == PlacementDirectionEnum.Up)
                    y -= i;
                else if (Direction == PlacementDirectionEnum.Down)
                    y += i;
                else if (Direction == PlacementDirectionEnum.Right)
                    x += i;
                else if (Direction == PlacementDirectionEnum.Left)
                    x -= i;


                GameBoard.BoardTiles.ShipTile newTile = new GameBoard.BoardTiles.ShipTile(new Vector2(x, y));
                ShipTiles.Add(newTile);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            foreach(var tile in ShipTiles)
            {
                tile.LoadContent(content);
            }
        }


    }
}
