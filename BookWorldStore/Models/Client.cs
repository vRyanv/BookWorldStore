﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookWorldStore.Models
{
	public class Client
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int user_id { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email")]
		public string email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string password { get; set; }

		public string? role { get; set; }

		[Required(ErrorMessage = "Gender is required")]
		public string gender { get; set; }

		[Required(ErrorMessage = "Phone is required")]
		public string phone { get; set; }

		[Required(ErrorMessage = "Address is required")]
		public string address { get; set; }
		public int? status { get; set; }
		public string? token_reset_pass { get; set; }
		public virtual ICollection<Order>? orders { get; set; }
	}
}
