using System.Collections.Generic;

namespace CollectionModifiedException_App.InventoryWareHouse 
{
    public class InventoryModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
        public string Group { get; set; }
        public string SysId { get; set; }
        public long Identifier { get; set; }
        public Dictionary<string, string> Data { get; set; }
    }
}
