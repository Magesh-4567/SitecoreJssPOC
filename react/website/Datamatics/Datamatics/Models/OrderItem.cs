using System.Collections.Generic;

namespace Datamatics.Models
{
    public class OrderItem
    {
        public string orderId { get; set; }
        public string dateCreated { get; set; }
        public Customer cusomterDetails { get; set; }
        public List<Treatment> treatmentDetails { get; set; }
    }

    public class Customer
    {
        public string memberShipNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dob { get; set; }
        public string postCode { get; set; }
        public string policyNumber { get; set; }
    }

    public class Treatment
    {
        public string treatmentId { get; set; }
        public string treatmentName { get; set; }
        public string treatmentDescription { get; set; }
    }
}