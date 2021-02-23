﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPnetCoreSyntraExample.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        static List<House> houses = new List<House>();
        public HouseController()
        {
            if (houses.Count == 0)
            {
                var house1 = new House();
                house1.Name = "H1";
                house1.Area = "100m2";
                house1.Price = 100000;
                houses.Add(house1);
            }
        }
        [HttpGet("many")]
        public ActionResult<List<House>> GetAllHouses()
        {
            return Ok(houses);
        }
        [HttpGet("one")]
        public ActionResult<House> GetHouse(string houseName)
        {
            var house = houses.FirstOrDefault(x => x.Name == houseName);
            if (house == null)
            {
                return NotFound();

            }
            return Ok(house);
        }
        [HttpPost]
        public ActionResult CreateNewHouse(House newHouse)
        {
            houses.Add(newHouse);
            return Ok();
        }
        [HttpDelete]
        public ActionResult DeleteHouse(string houseName)
        {
            var houseToDelete = houses.First(x => x.Name == houseName);
            houses.Remove(houseToDelete);
            return Ok();
        }

        [HttpPut]
        public ActionResult<House> UpdateHouseByName(string nameOfHouseToEdit, House houseEditValues)
        {
            var houseToEdit = houses.First(x => x.Name == nameOfHouseToEdit);
            houseToEdit.Price = houseEditValues.Price;
            houseToEdit.Area = houseEditValues.Area;
            return Ok(houseToEdit);


        }

    }
    public class House
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public int Price { get; set; }
    }
}
