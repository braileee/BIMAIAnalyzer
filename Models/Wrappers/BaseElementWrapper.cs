using Autodesk.AutoCAD.DatabaseServices;
using BIMAIAnalyzer.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BIMAIAnalyzer.Models.Wrappers
{
    public abstract class BaseElementWrapper<TWrapper>
    {
        public BaseElementWrapper(Entity entity)
        {
            Entity = entity;
        }

        [JsonIgnore]
        public Entity Entity { get; }

        public string Layer
        {
            get
            {
                return Entity?.Layer;
            }
        }

        public string Type
        {
            get
            {
                return Entity?.GetType().Name;
            }
        }

        public string Handle
        {
            get
            {
                return Entity?.Handle.ToString();
            }
        }

        public static List<TWrapper> Create<TAcad>()
        {
            List<TAcad> elements = ElementUtils.GetFromModel<TAcad>();

            List<TWrapper> wrappers = [];

            foreach (TAcad element in elements)
            {
                TWrapper instance = (TWrapper)Activator.CreateInstance(typeof(TWrapper), [element]);

                wrappers.Add(instance);
            }

            return wrappers;
        }
    }
}
