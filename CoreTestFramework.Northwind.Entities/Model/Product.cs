﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoreTestFramework.Core.Entities;

namespace CoreTestFramework.Northwind.Entities.Model;
public class Product : IEntity
{
   
    public int product_id { get; set; }
    
    public string product_name { get; set;}
    
    public int supplier_id { get; set; }
    
    public int category_id { get; set; }
    
    public string quantity_per_unit { get; set; }
    
    public double unit_price { get; set; }
    
    public int units_in_stock {get; set;}
    
    public int units_on_order {get; set;}
    
    public int reorder_level {get; set;}
    
    public int discontinued {get; set;}
    public virtual Supplier Supplier {get; set;}
    public virtual Category Category {get; set;}
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}
