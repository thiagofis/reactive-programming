using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaDotNetSolution.Actors;

namespace AkkaDotNetSolution
{
    class Program
    {

        private static ActorSystem movieStreamingActorSystem;

        static void Main(string[] args)
        {
            using (movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem"))
            {
                // Actor factory
                Props playbackActorProps = Props.Create<PlaybackActor>();
                IActorRef playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

                playbackActorRef.Tell("Uhhuuuu!");

                Console.ReadLine();
            }
        }
    }
}
