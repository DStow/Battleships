using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsServer.Extensions
{
    public static class Extensions
    {
        public static string GetPiecesOrNull(this string[] pieces, int index)
        {
            if (index >= pieces.Count())
            {
                return null;
            }
            else
            {
                return pieces[index];
            }
        }
    }
}
