﻿namespace WeaponsAPI.Weapons.DTO
{
    public class UpdateWeaponRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public double? Price { get; set; }

    }
}