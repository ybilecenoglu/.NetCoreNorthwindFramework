﻿using Microsoft.EntityFrameworkCore;
using SQLiteNetExtensions.Attributes;

namespace CoreTestFramework.Northwind.Entities.Model;

// [Keyless] //Primary Key olmayan entitiylerde "HasNoKey" fonksiyonuna alternatif olarak kullanildi
public partial class OrderDetail
{
    public int? OrderID { get; set; }
    
    public int? ProductID { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }

    public float Discount { get; set; }
    public virtual Order Order { get; set; } 
    public virtual Product Product { get; set; }
}
