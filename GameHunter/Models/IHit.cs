using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameHunter
{
    public interface IHit : ISkin
    {
        public void Die();
    }
}
