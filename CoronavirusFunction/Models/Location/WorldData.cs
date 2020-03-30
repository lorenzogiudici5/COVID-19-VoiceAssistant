using System;

namespace CoronavirusFunction.Models
{
    public class WorldData : LocationData
    {
        public WorldData(string name) : base("Mondo") 
        { 
        }
        public override string Description => this.Name;
        public override Uri FlagUri => new Uri($"https://pngimage.net/wp-content/uploads/2018/06/world-graphic-png-6.png");
    }
}
