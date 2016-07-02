using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatPaddlesTutorial.Entities
{
    public interface IPaddle
    {
        PaddleControl PaddleControl { get; }
    }
}
