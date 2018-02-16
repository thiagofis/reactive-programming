using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaDotNetSolution.Actors
{
    public class PlaybackActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.Write($"==== PlaybackActor received a message message: {message} ====");
        }
    }
}
