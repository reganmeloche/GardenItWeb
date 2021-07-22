using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace gardenit_web.Data
{
    public class Watering
    {
        //public Guid Id { get; set; }
        //public Guid PlantId { get; set; }
        public DateTime WateringDate { get; set; }
    }
}