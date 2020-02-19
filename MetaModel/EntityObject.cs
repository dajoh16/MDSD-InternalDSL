using System.Collections.Generic;

namespace LossDataExtractor.MetaModel
{
    public class EntityObject : EntityField
    {
        public List<EntityField> EntityFields { get; set; }
        
    }
}