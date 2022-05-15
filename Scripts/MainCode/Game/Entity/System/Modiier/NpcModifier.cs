using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ShuShan
{
    public class NpcModifier:ModifierBase
    {
        public NpcModifier()
        {

        }
        public NpcModifier(NpcModifierDef Def)
        {
            InitDate(Def);
        }
        [JsonIgnore]
        public NpcModifierDef def;

        public string Name;

        public int Duration;

        public int Stack;

        public void InitDate(NpcModifierDef Def)
        {
            def = Def;
            Name = Def.Name;
        }

    }
}
