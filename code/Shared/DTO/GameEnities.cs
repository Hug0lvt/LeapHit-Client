using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class GameEnities
    {
        Tuple<float, float> Ball { get; set; }
        float Paddle { get; set; }

        public GameEnities(Tuple<float, float> ball, float paddle)
        {
            Ball = ball;
            Paddle = paddle;
        }
    }
}
