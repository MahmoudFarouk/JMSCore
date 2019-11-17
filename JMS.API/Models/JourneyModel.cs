﻿using JMS.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JMS.API.Models
{
    public class JourneyModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsTruckTransport { get; set; }
        public JourneyStatus JourneyStatus { get; set; }
        public string FromDestination { get; set; }
        public string FromLat { get; set; }
        public string FromLng { get; set; }
        public string ToDistination { get; set; }
        public string ToLat { get; set; }
        public string ToLng { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double? CargoWeight { get; set; }
        public Level CargoPriority { get; set; }
        public Level CargoSeverity { get; set; }
        public string CargoType { get; set; }
        public Guid UserId { get; set; }
        public bool IsThirdParty { get; set; }
    }
}