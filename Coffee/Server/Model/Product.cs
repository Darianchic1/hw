namespace Coffee;
using System;
using System.ComponentModel.DataAnnotations;

public class Purchase
{
    [Required]
    [Range(0.01, 10000)]
    public int Id { get; set; }

    [Range(0.01, 10000)]
    public double Price { get; set; }

    [Range(0, 10000)]
    public string Date { get; set; }

    public Purchase(int id, double price, string date)
    {
        Id = id;
        Price = price;
        Date = date;
    }
}
