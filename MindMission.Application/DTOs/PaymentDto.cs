﻿using MindMission.Domain.Constants;
using MindMission.Domain.Stripe.StripeModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindMission.Domain.Stripe.CustomValidationAttributes;

namespace MindMission.Application.DTOs
{
    public class PaymentDto
    {
        [Required(ErrorMessage = "Customer Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer Name should be between 3 and 50")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name On Card is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Customer Name should be between 3 and 50")]
        public string NameOnCard { get; set; } = string.Empty;

        [CardNumberValidator]
        public string CardNumber { get; set; } = string.Empty;

        [ExpirationYearValidator]
        public string ExpirationYear { get; set; } = string.Empty;

        [ExpirationMonthValidator("ExpirationYear")]
        public string ExpirationMonth { get; set; } = string.Empty;

        [CvcValidator]
        public string CVC { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "Description maximum length is 100 characters")]
        public string Description { get; set; } = string.Empty;

        [Range(50, long.MaxValue, ErrorMessage = "Amount minimum value is 50")]
        public string Amount { get; set; } = string.Empty;
    }
}