using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmsWebApplication.Models
{
    public class CheckoutViewModel
    {
        // Shipping Information
        [Required(ErrorMessage = "Please enter your shipping address.")]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Please enter your city.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your state.")]
        public string BillingAddress { get; set; }

        [Required(ErrorMessage = "Please enter your state.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your postal code.")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        // Billing Information
        [Required(ErrorMessage = "Please enter your credit card number.")]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required(ErrorMessage = "Please enter the cardholder's name.")]
        [Display(Name = "Cardholder Name")]
        public string CardholderName { get; set; }

        [Required(ErrorMessage = "Please enter the card's expiration date.")]
        [Display(Name = "Expiration Date")]
        public string ExpirationDate { get; set; }

        [Required(ErrorMessage = "Please enter the CVV code.")]
        public string CVV { get; set; }

        // Cart Items
        public List<CartItemViewModel> CartItems { get; set; }

        // Total Price
        public decimal TotalPrice { get; set; }

    }

    

}