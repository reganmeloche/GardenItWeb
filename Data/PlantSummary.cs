using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace gardenit_web.Data
{
    // TODO: Can PlantSummary and PlantForm inherit from the same thing...?
    public class PlantSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int DaysBetweenWatering { get; set; }
        public string Notes { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime NextWateringDate {
            get {
                if (Waterings.Count > 0) {
                    var lastWatering = Waterings.OrderByDescending(x => x.WateringDate).First();
                    return lastWatering.WateringDate.AddDays(DaysBetweenWatering);
                }
                else {
                    return CreateDate.AddDays(DaysBetweenWatering);
                }
            } 
        }
        public List<Watering> Waterings { get; set; }
        public List<MoistureReading> MoistureReadings { get; set; }
        public bool HasDevice { get; set; }
        public int PollPeriodMinutes { get; set; }
        public string ImageName { get; set; }
        public bool HasImage {
            get {
                return !String.IsNullOrEmpty(ImageName) && ImageName != "default.png";
            }
        }
    }
}