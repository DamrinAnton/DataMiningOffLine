//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DB_Helper
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recomendations
    {
        public long id { get; set; }
        public Nullable<long> reason_id { get; set; }
        public Nullable<long> number_in { get; set; }
        public string text_st { get; set; }
        public string param_st { get; set; }
        public string place_st { get; set; }
    
        public virtual Reasons Reasons { get; set; }
    }
}