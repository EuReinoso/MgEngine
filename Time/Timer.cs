using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#pragma warning disable CS8618
namespace MgEngine.Time
{
    public class Timer
    {
        private bool _isActivate;
        private float _elapsedTime;

        public int Duration { get; set; }
        public bool AutoReset { get; set; }

        public event Action Elapsed;
        public event Action OnStart;

        public Timer()
        {
        }

        public Timer(int duration) 
        {
            Duration = duration;
        }

        public bool IsActivate { get { return _isActivate; } }
        public float ElapsedTime { get { return _elapsedTime; } }

        public void Start()
        {
            _elapsedTime = 0;
            _isActivate = true;
            OnStart?.Invoke();
        }

        public void Resume()
        {
            _isActivate = true;
        }

        public void Pause()
        {
            _isActivate = false;
        }

        public void Stop()
        {
            _isActivate = false;
            _elapsedTime = 0;
        }

        public void Update(float dt)
        {
            if (!_isActivate)
                return;

            _elapsedTime += 1 * dt;

            if (_elapsedTime >= Duration)
            {
                Elapsed?.Invoke();

                if (AutoReset)
                    Start();
                else
                {
                    _isActivate = false;
                    _elapsedTime = 0;
                }
            }
        }

    }
}
