﻿using System;

namespace KP_0_
{
    internal class DataClasses
    {
        internal class Client
        {
            public string Mail { get; set; }
            public DateTime RegistrationDate { get; set; }
            public string Note { get; set; }
        }
        internal class Staff
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string Rights { get; set; }
            public string Description { get; set; }
        }
        internal class Purchase
        {
            public int Id { get; set; }
            public string Mail { get; set; }
            public DateTime Date { get; set; }
        }
        internal class Appeal
        {
            public int Id { get; set; }
            public string Mail { get; set; }
            public DateTime Date { get; set; }
            public string TopicOfAppeal { get; set; }
            public string TextOfAppeal { get; set; }
            public string StatusOfAppeal { get; set; }
        }
        internal class Delivery
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public decimal Price { get; set; }
            public string NameOfProvider { get; set; }
            public string ContactInformation { get; set; }
        }
        internal class DigitalProduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string NameOfPlatformOfKeys { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public decimal Discount { get; set; }
        }
        internal class KeyForSale
        {
            public int Id { get; set; }
            public int IdOfDigitalProduct { get; set; }
            public int IdOfDelivery { get; set; }
            public string ValueOfKey { get; set; }
        }
        internal class Image
        {
            public int Id { get; set; }
            public int IdOfDigitalProduct { get; set; }
            public byte[] BinaryImage { get; set; }
        }
        internal class LinkKeyPurchase
        {
            public int Id { get; set; }
            public int IdOfPurchase { get; set; }
            public int IdOfKey { get; set; }
            public decimal Price { get; set; }
            public decimal Discount { get; set; }
        }
    }
}
