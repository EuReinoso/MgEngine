using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MgEngine.Effect
{
    public struct ParticleMoveEffect
    {
        public Vector2 Gravity { get; set; }
        public Vector2 Wind { get; set; }
        public Vector2 VelocityMinStart { get; set; }
        public Vector2 VelocityMaxStart { get; set; }
        public Vector2 VelocityDecay { get; set; }
        public float MinRotationVelocity { get; set; }
        public float MaxRotationVelocity { get; set; }
        public float SizeDecay { get; set; }
        public int SizeMinStart { get; set; }
        public int SizeMaxStart { get; set; }
        public int MaxSize { get; set; }
        public int MinSize { get; set; }
        public bool IsSizeClampOn { get; set; }

        public ParticleMoveEffect Default 
        { 
            get
            {
                return default(ParticleMoveEffect);
            } 
        }
    }

}
