using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatPaddlesTutorial.Entities
{
    public class Participant
    {
        public Participant(IPaddle paddle)
        {
            Paddle = paddle;
            Points = 0;
        }

        public IPaddle Paddle { get; private set; }

        public uint Points { get; private set; }

        public void Score(uint awardedPoints)
        {
            Points += awardedPoints;
        }
    }
}
