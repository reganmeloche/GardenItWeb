using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace gardenit_web.Data
{
    public class PlantFormModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        [Range(0,30)]
        public int DaysBetweenWatering { get; set; }

        [MaxLength(400)]
        public string Notes { get; set; }

        public bool HasDevice { get; set; }

        [Required]
        [Range(1,120)]
        public int PollPeriodMinutes { get; set; }

        public IBrowserFile ImageFile { get; set; }

        public string ImageName { get; set; }
        public bool HasImage {
            get {
                return !String.IsNullOrEmpty(ImageName) && ImageName != "default.png";
            }
        }
    }
}